using System.Collections.Generic;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Tests.Components
{
    [TestFixture]
    public class DataTypeTests
    {
        [Test]
        public void New_Default_shouldNotBeNull()
        {
            var dataType = new DataType();

            dataType.Should().NotBeNull();
        }

        [Test]
        public void New_Default_shouldHaveDefaults()
        {
            var dataType = new DataType();

            dataType.Name.Should().BeEmpty();
            dataType.Description.Should().BeEmpty();
            dataType.Family.Should().Be(DataTypeFamily.None);
            dataType.Class.Should().Be(DataTypeClass.User);
            dataType.Members.Should().BeEmpty();
        }

        [Test]
        public void New_Parameterized_ShouldHaveExpectedValues()
        {
            var dataType = new DataType
            {
                Name = "MyType",
                Description = "This is a test type",
                Members = new List<DataTypeMember>
                {
                    new()
                    {
                        Name = "Member01", DataType = "DINT", Radix = Radix.Hex, Description = "A test member"
                    },
                    new()
                    {
                        Name = "Member02", DataType = "REAL", Dimensions = new Dimensions(10),
                        Description = "A test member"
                    },
                    new()
                    {
                        Name = "Member03", DataType = "TIME", ExternalAccess = ExternalAccess.ReadOnly
                    }
                }
            };

            dataType.Name.Should().Be("MyType");
            dataType.Description.Should().Be("This is a test type");
            dataType.Members.Should().HaveCount(3);
            dataType.Members.Should().Contain(m => m.Name == "Member01");
            dataType.Members.Should().Contain(m => m.Name == "Member02");
            dataType.Members.Should().Contain(m => m.Name == "Member03");
        }
    }
}