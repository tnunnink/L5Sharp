using FluentAssertions;
using L5Sharp.Extensions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Tests.ExtensionTests
{
    [TestFixture]
    public class DataTypeExtensionTests
    {
        [Test]
        public void GetDependentTypes_TypeWithMembers_ShouldNotBeEmpty()
        {
            var type = new Timer();

            type.GetDependentTypes().Should().NotBeEmpty();
        }

        [Test]
        public void GetDependentTypes_TypeWithNoMembers_ShouldBeEmpty()
        {
            var type = new Undefined();

            type.GetDependentTypes().Should().BeEmpty();
        }
    }
}