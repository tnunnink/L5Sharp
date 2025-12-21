using FluentAssertions;

namespace L5Sharp.Tests.Core.Enums
{
    [TestFixture]
    public class DataTypeClassTests
    {
        [Test]
        public void New_Unknown_ShouldNotBeNull()
        {
            var type = DataTypeClass.Unknown;

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_User_ShouldNotBeNull()
        {
            var type = DataTypeClass.User;

            type.Should().NotBeNull();
        }

        [Test]
        public void New_IO_ShouldNotBeNull()
        {
            var type = DataTypeClass.IO;

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_Predefined_ShouldNotBeNull()
        {
            var type = DataTypeClass.Predefined;

            type.Should().NotBeNull();
            type.Name.Should().Be("Predefined");
            type.Value.Should().Be("ProductDefined");
        }
    }
}