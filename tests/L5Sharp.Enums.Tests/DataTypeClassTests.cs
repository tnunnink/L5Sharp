using FluentAssertions;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Enums.Tests
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

            sut.Should().HaveCount(5);
        }

        [Test]
        public void FromType_IAtomic_ShouldBeAtomic()
        {
            var sut = DataTypeClass.FromType<IAtomic>();

            sut.Should().Be(DataTypeClass.Atomic);
        }
        
        [Test]
        public void FromType_IPredefined_ShouldBePredefined()
        {
            var sut = DataTypeClass.FromType<IPredefined>();

            sut.Should().Be(DataTypeClass.Predefined);
        }
        
        [Test]
        public void FromType_IUserDefined_ShouldBeUser()
        {
            var sut = DataTypeClass.FromType<IUserDefined>();

            sut.Should().Be(DataTypeClass.User);
        }
        
        [Test]
        public void FromType_IModuleDefined_ShouldBeIo()
        {
            var sut = DataTypeClass.FromType<IModuleDefined>();

            sut.Should().Be(DataTypeClass.Io);
        }
        
        [Test]
        public void FromType_IAddOnDefined_ShouldBeAddOn()
        {
            var sut = DataTypeClass.FromType<IAddOnDefined>();

            sut.Should().Be(DataTypeClass.AddOnDefined);
        }
        
        [Test]
        public void FromType_IAddOnDefined_ShouldBeNull()
        {
            var sut = DataTypeClass.FromType<IDataType>();

            sut.Should().Be(null);
        }
    }
}