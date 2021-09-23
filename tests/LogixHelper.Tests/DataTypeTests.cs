using FluentAssertions;
using NUnit.Framework;

namespace LogixHelper.Tests
{
    [TestFixture]
    public class DataTypeTests
    {
        [Test]
        public void New_ValidArguments_ShouldNotBeNull()
        {
            var type = new DataType(string.Empty);

            type.Should().NotBeNull();
        }

        [Test]
        public void METHOD()
        {
            var controller = new Controller();
        }
    }
}