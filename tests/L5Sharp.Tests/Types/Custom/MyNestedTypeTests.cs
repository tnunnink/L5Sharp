using FluentAssertions;

namespace L5Sharp.Tests.Types.Custom
{
    [TestFixture]
    public class MyNestedTypeTests
    {
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var type = new MyNestedType();

            type.Should().NotBeNull();
        }

        [Test]
        public Task SetSimpleM4_Valid_ShouldBeValid()
        {
            var type = new MyNestedType();

            type.Simple.M4 = 5000;

            var xml = type.Serialize().ToString();
            
            return Verify(xml);
        }

        [Test]
        public void Members_WhenCalled_ShouldReturnInstantiatedMembers()
        {
            var type = new MyNestedType();

            var members = type.Members;

            foreach (var member in members)
            {
                member.Should().NotBeNull();
                member.Value.Should().NotBeNull();
            }
        }

        [Test]
        public void Flags_AccessIndex_ShouldReturnExpectedValue()
        {
            var type = new MyNestedType();

            var flag = type.Flags[4];

            flag.Should().NotBeNull();
            flag.Should().BeOfType<BOOL>();
            flag.Should().Be(false);
        }
    }
}