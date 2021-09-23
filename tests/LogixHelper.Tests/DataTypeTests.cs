using FluentAssertions;
using LogixHelper.Primitives;
using NUnit.Framework;

namespace LogixHelper.Tests
{
    [TestFixture]
    public class DataTypeTests
    {
        [Test]
        public void New_ValidArguments_ShouldNotBeNull()
        {
            var type = new DataType(string.Empty, string.Empty);

            type.Should().NotBeNull();
        }
    }
}