using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Tests.Enums
{
    [TestFixture]
    public class DataTypeClassTests
    {
        [Test]
        public void New_User_ShouldNotBeNull()
        {
            var sut = DataTypeClass.User;

            sut.Should().NotBeNull();
        }
        
        [Test]
        public void New_Io_ShouldNotBeNull()
        {
            var sut = DataTypeClass.Module;

            sut.Should().NotBeNull();
        }
        
        [Test]
        public void New_Predefined_ShouldNotBeNull()
        {
            var sut = DataTypeClass.Predefined;

            sut.Should().NotBeNull();
        }
    }
}