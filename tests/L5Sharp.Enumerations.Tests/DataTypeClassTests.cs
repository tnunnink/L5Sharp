using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Enumerations.Tests
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
            var sut = DataTypeClass.Io;

            sut.Should().NotBeNull();
        }
        
        [Test]
        public void New_Predefined_ShouldNotBeNull()
        {
            var sut = DataTypeClass.Predefined;

            sut.Should().NotBeNull();
        }
        
        [Test]
        public void List_WhenCalled_ShouldHaveExpectedCount()
        {
            var sut = DataTypeClass.List;

            sut.Should().HaveCount(3);
        }
    }
}