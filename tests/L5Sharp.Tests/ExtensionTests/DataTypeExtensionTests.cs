using FluentAssertions;
using L5Sharp.Extensions;
using NUnit.Framework;

namespace L5Sharp.Tests.ExtensionTests
{
    [TestFixture]
    public class DataTypeExtensionTests
    {
        [Test]
        public void GetDependentTypes_TypeWithMembers_ShouldNotBeEmpty()
        {
            var type = Logix.DataType.Timer;

            type.GetDependentTypes().Should().NotBeEmpty();
        }

        [Test]
        public void GetDependentTypes_TypeWithNoMembers_ShouldBeEmpty()
        {
            var type = Logix.DataType.Undefined;

            type.GetDependentTypes().Should().BeEmpty();
        }
    }
}