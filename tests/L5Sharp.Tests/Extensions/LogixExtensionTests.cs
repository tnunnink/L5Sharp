using FluentAssertions;
using L5Sharp.Extensions;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Tests.Extensions
{
    [TestFixture]
    public class LogixExtensionTests
    {
        [Test]
        public void ToType_ValidTypes_ShouldBeOfExpectedType()
        {
            var type = (LogixType)new BOOL();

            var result = type.To<BOOL>();

            result.Should().BeOfType<BOOL>();
        }
        
        [Test]
        public void ToType_InvalidTypes_ShouldThrowException()
        {
            var type = (LogixType)new BOOL();

            FluentActions.Invoking(() => type.To<DINT>()).Should().Throw<InvalidCastException>();
        }
        
        [Test]
        public void AsType_InvalidTypes_ShouldBeNull()
        {
            var type = (LogixType)new BOOL();

            var result = type.As<DINT>();

            result.Should().BeNull();
        }
    }
}