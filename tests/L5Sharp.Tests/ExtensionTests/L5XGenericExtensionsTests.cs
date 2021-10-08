using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Extensions;
using NUnit.Framework;

namespace L5Sharp.Tests.ExtensionTests
{
    [TestFixture]
    public class L5XGenericExtensionsTests
    {
        [Test]
        public void ToXAttribute_ValidComponent_ShouldHaveExpectedNameAndValue()
        {
            var component = new DataType("Test");

            var attribute = component.ToXAttribute(c => c.Name);

            attribute.Should().NotBeNull();
            attribute.Name.ToString().Should().Be("Name");
            attribute.Should().HaveValue("Test");
        }
    }
}