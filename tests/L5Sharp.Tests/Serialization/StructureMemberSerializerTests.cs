using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Serialization.Data;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Tests.Serialization
{
    [TestFixture]
    public class StructureMemberSerializerTests
    {
        private StructureMemberSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new StructureMemberSerializer();
        }

        [Test]
        public void Serialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Serialize(null!)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var component = new Member { Name = "Test", DataType = new StructureType("Test", new List<Member>()) };

            var xml = _serializer.Serialize(component);

            xml.Should().NotBeNull();
        }

        [Test]
        public Task Serialize_DataValueValueMembers_ShouldBeApproved()
        {
            var type = new StructureType("Test", new List<Member>
            {
                Logix.Member<BOOL>("BoolMember"),
                Logix.Member<SINT>("SintMember"),
                Logix.Member<INT>("IntMember"),
                Logix.Member<DINT>("DintMember"),
                Logix.Member<LINT>("LintMember"),
                Logix.Member<REAL>("RealMember")
            });

            var component = new Member("Test", type);

            var xml = _serializer.Serialize(component);

            return Verify(xml.ToString());
        }

        [Test]
        public Task SerializeArrayMembers_ShouldBeApproved()
        {
            var type = new StructureType("Test", new List<Member>
            {
                new("SimpleMember", Logix.Array<DINT>(10)),
                new("ComplexMember", Logix.Array<TIMER>(5)),
                new("ComplexMember", Logix.Array<STRING>(2)),
            });

            var component = new Member("Test", type);

            var xml = _serializer.Serialize(component);

            return Verify(xml.ToString());
        }

        [Test]
        public Task Serialize_StructureMembers_ShouldBeApproved()
        {
            var type = new StructureType("Test", new List<Member>
            {
                new("StringMember", new STRING()),
                new("TimerMember", new TIMER()),
                new("CounterMember", new COUNTER()),
            });

            var component = new Member("Test", type);

            var xml = _serializer.Serialize(component);

            return Verify(xml.ToString());
        }

        [Test]
        public void Deserialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Deserialize(null!)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Deserialize_InvalidElementName_ShouldThrowArgumentException()
        {
            const string xml = @"<Invalid></Invalid>";
            var element = XElement.Parse(xml);

            FluentActions.Invoking(() => _serializer.Deserialize(element)).Should().Throw<ArgumentException>()
                .WithMessage($"Element 'Invalid' not valid for the serializer {_serializer.GetType()}.");
        }

        [Test]
        public void Deserialize_SimpleStructureMember_ShouldNotBeNull()
        {
            var xml = XElement.Parse(GetSimpleStructureMember());

            var component = _serializer.Deserialize(xml);

            component.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_SimpleStructureMember_ShouldHaveExpectedProperties()
        {
            var xml = XElement.Parse(GetSimpleStructureMember());

            var component = _serializer.Deserialize(xml);

            component.Name.Should().Be("SimpleMember");
            component.DataType.Name.Should().Be("SimpleType");
            component.DataType.Should().BeOfType<StructureType>();
            component.DataType.As<StructureType>().Members.Should().HaveCount(5);
        }

        private static string GetSimpleStructureMember()
        {
            return @"<StructureMember Name=""SimpleMember"" DataType=""SimpleType"">
                <DataValueMember Name=""BoolMember"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""SintMember"" DataType=""SINT"" Radix=""Hex"" Value=""16#00""/>
                <DataValueMember Name=""IntMember"" DataType=""INT"" Radix=""Octal"" Value=""8#000_000""/>
                <DataValueMember Name=""DintMember"" DataType=""DINT"" Radix=""ASCII"" Value=""'$00$00$00$00'""/>
                <DataValueMember Name=""LintMember"" DataType=""LINT"" Radix=""Decimal"" Value=""0""/>
                </StructureMember>";
        }
    }
}