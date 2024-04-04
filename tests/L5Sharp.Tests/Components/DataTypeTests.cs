using FluentAssertions;


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
        public Task Serialize_Default_ShouldBeVerified()
        {
            var component = new DataType();

            var xml = component.Serialize().ToString();
            
            return Verify(xml);
        }

        [Test]
        public void New_Default_ShouldHaveDefaultValues()
        {
            var dataType = new DataType();

            dataType.Name.Should().BeEmpty();
            dataType.Description.Should().BeNull();
            dataType.Family.Should().Be(DataTypeFamily.None);
            dataType.Class.Should().Be(DataTypeClass.User);
            dataType.Members.Should().NotBeNull();
            dataType.Members.Should().BeEmpty();
            dataType.Key.Should().BeEquivalentTo(new ComponentKey("DataType", ""));
            dataType.IsAttached.Should().BeFalse();
            dataType.L5X.Should().BeNull();
            dataType.Use.Should().BeNull();
            dataType.Container.Should().BeEmpty();
            dataType.Scope.Should().Be(Scope.Null);
        }

        [Test]
        public void DataType_ShouldSetPropertiesCorrectly()
        {
            var name = "MyDataType";
            var description = "This is my data type";
            var family = DataTypeFamily.String;
            var dataType = new DataType
            {
                Name = name,
                Description = description,
                Family = family,
                Class = DataTypeClass.User,
                
            };

            dataType.Name.Should().Be(name);
            dataType.Description.Should().Be(description);
            dataType.Family.Should().Be(family);
            dataType.Class.Should().Be(DataTypeClass.User);
            dataType.Members.Should().NotBeNull();
            dataType.Members.Should().BeEmpty();
        }

        [Test]
        public void New_Parameterized_ShouldHaveExpectedValues()
        {
            var dataType = new DataType
            {
                Name = "MyType",
                Description = "This is a test type",
                Members = new LogixContainer<DataTypeMember>
                {
                    new() { Name = "Member01", DataType = "DINT", Radix = Radix.Hex, Description = "A test member" },
                    new() { Name = "Member02", DataType = "REAL", Dimension = new Dimensions(10), Description = "A test member" },
                    new() { Name = "Member03", DataType = "TIME", ExternalAccess = ExternalAccess.ReadOnly }
                }
            };

            dataType.Name.Should().Be("MyType");
            dataType.Description.Should().Be("This is a test type");
            dataType.Members.Should().HaveCount(3);
            dataType.Members.Should().Contain(m => m.Name == "Member01");
            dataType.Members.Should().Contain(m => m.Name == "Member02");
            dataType.Members.Should().Contain(m => m.Name == "Member03");
        }

        [Test]
        public void DataType_Members_ShouldBeInitialized()
        {
            var dataType = new DataType();

            dataType.Members.Should().NotBeNull();
            dataType.Members.Should().BeEmpty();
        }

        [Test]
        public void L5X_AttachedElement_ShouldNotBeNull()
        {
            var content = L5X.Load(Known.Test);
            var component = content.DataTypes.Get(Known.DataType);
            
            var l5X = component.L5X;
            
            l5X.Should().NotBeNull();
        }
    }
}