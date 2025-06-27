using FluentAssertions;


namespace L5Sharp.Tests.Core.Components
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
        public void New_Default_ShouldHaveDefaultValues()
        {
            var dataType = new DataType();

            dataType.Name.Should().BeEmpty();
            dataType.Description.Should().BeNull();
            dataType.Family.Should().Be(DataTypeFamily.None);
            dataType.Class.Should().Be(DataTypeClass.User);
            dataType.Members.Should().BeEmpty();
            dataType.Use.Should().BeNull();
            dataType.Scope.Level.Should().Be(ScopeLevel.Null);
        }

        [Test]
        public void New_Overriden_ShouldHaveExpectedValues()
        {
            var dataType = new DataType
            {
                Name = "Test",
                Description = "This is a test type",
                Family = DataTypeFamily.String,
                Class = DataTypeClass.Predefined,
                Members =
                [
                    new DataTypeMember { Name = "Member01" },
                    new DataTypeMember { Name = "Member02" },
                    new DataTypeMember { Name = "Member03" }
                ]
            };

            dataType.Name.Should().Be("Test");
            dataType.Description.Should().Be("This is a test type");
            dataType.Members.Should().HaveCount(3);
            dataType.Members.Should().Contain(m => m.Name == "Member01");
            dataType.Members.Should().Contain(m => m.Name == "Member02");
            dataType.Members.Should().Contain(m => m.Name == "Member03");
        }

        [Test]
        public void New_Name_ShouldBeExpected()
        {
            var dataType = new DataType("Test");

            dataType.Name.Should().Be("Test");
        }

        [Test]
        public Task Serialize_Default_ShouldBeVerified()
        {
            var component = new DataType();

            var xml = component.Serialize().ToString();

            return Verify(xml);
        }

        [Test]
        public Task Serialize_Parameterized_ShouldBeVerified()
        {
            var dataType = new DataType
            {
                Name = "Test",
                Description = "This is a test type",
                Family = DataTypeFamily.String,
                Class = DataTypeClass.Predefined,
                Members = new LogixContainer<DataTypeMember>
                {
                    new() { Name = "Member01" },
                    new() { Name = "Member02" },
                    new() { Name = "Member03" }
                }
            };

            var xml = dataType.Serialize().ToString();

            return Verify(xml);
        }

        [Test]
        public void AddMember_ValidParameters_ShouldContainMember()
        {
            var type = new DataType("Test");

            type.AddMember("MemberName", "DINT");

            type.Members.Should().HaveCount(1);
        }

        [Test]
        public void ToData_SimpleType_ShouldHaveExpectedStructure()
        {
            var content = L5X.Load(Known.Test);
            var simpleType = content.Get<DataType>(Known.DataType);

            var data = simpleType.ToData();

            data.Should().NotBeNull();
            data.Name.Should().Be("SimpleType");
            data.Members.Should().HaveCount(6);
        }

        [Test]
        public void ToData_ComplexType_ShouldHaveExpectedMembers()
        {
            var content = L5X.Load(Known.Test);
            var simpleType = content.Get<DataType>("ComplexType");

            var data = simpleType.ToData();

            data.Should().NotBeNull();
            data.Name.Should().Be("ComplexType");
            data.Members.Should().HaveCount(9);
            data.Member("SimpleMember")?.Value.Should().BeOfType<ComplexData>();
            data.Member("TimeMember")?.Value.Should().BeOfType<TIMER>();
            data.Member("CounterMember")?.Value.Should().BeOfType<COUNTER>();
        }

        [Test]
        public Task ToTag_SimpleType_ShouldBeVerified()
        {
            var content = L5X.Load(Known.Test);
            var type = content.Get<DataType>(Known.DataType);

            var tag = type.ToTag("MySimpleTag");

            return Verify(tag.Serialize().ToString());
        }

        [Test]
        public Task ToTag_ComplexType_ShouldBeVerified()
        {
            var content = L5X.Load(Known.Test);
            var type = content.Get<DataType>("ComplexType");

            var tag = type.ToTag("MyComplexTag");

            return Verify(tag.Serialize().ToString());
        }
    }
}