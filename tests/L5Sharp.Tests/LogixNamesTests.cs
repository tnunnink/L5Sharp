using FluentAssertions;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Utilities;
using NUnit.Framework;

namespace L5Sharp.Tests
{
    [TestFixture]
    public class LogixNamesTests
    {
        [Test]
        public void GetContainerName_IDataType_ShouldBeDataTypes()
        {
            var name = LogixNames.GetContainerName<IDataType>();

            name.Should().Be("DataTypes");
        }
        
        [Test]
        public void GetContainerName_DataType_ShouldBeDataTypes()
        {
            var name = LogixNames.GetContainerName<DataType>();

            name.Should().Be("DataTypes");
        }
        
        [Test]
        public void GetContainerName_Predefined_ShouldBeDataTypes()
        {
            var name = LogixNames.GetContainerName<Predefined>();

            name.Should().Be("DataTypes");
        }
        
        [Test]
        public void GetContainerName_IMember_ShouldBeDataTypes()
        {
            var name = LogixNames.GetContainerName<IMember>();

            name.Should().Be("Members");
        }

        [Test]
        public void GetContainerName_Member_ShouldBeDataTypes()
        {
            var name = LogixNames.GetContainerName<DataTypeMember>();

            name.Should().Be("Members");
        }
        
        [Test]
        public void GetContainerName_ITag_ShouldBeDataTypes()
        {
            var name = LogixNames.GetContainerName<ITag>();

            name.Should().Be("Tags");
        }
        
        [Test]
        public void GetContainerName_Tag_ShouldBeDataTypes()
        {
            var name = LogixNames.GetContainerName<Tag>();

            name.Should().Be("Tags");
        }
    }
}