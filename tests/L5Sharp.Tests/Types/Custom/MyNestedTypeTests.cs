using FluentAssertions;
using L5Sharp.Types.Atomics;
using NUnit.Framework;

namespace L5Sharp.Tests.Types.Custom
{
    [TestFixture]
    public class MyNestedTypeTests
    {
        [Test]
        public void Testing()
        {
            var nested = new MyNestedType();
            
            nested.Simple.M1= true;
            ;
            nested.Tmr.PRE = new DINT(5000);
            
            var flag = nested.Flags[2];

            var message = nested.Messages[4];
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