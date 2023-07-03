using FluentAssertions;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;
using NUnit.Framework;

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
        public void Testing()
        {
            var nested = new MyNestedType();

            nested.Simple.M1 = true;

            nested.Tmr.PRE = new DINT(5000);

            nested.Flags[2] = true;
        }

        [Test]
        public void Members_WhenCalled_ShouldReturnInstantiatedMembers()
        {
            var type = new MyNestedType();

            var members = type.Members;

            foreach (var member in members)
            {
                member.Should().NotBeNull();
                member.DataType.Should().NotBeNull();
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