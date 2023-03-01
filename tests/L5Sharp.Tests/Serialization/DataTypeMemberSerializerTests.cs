using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Serialization;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Tests.Serialization
{
    [TestFixture]
    public class DataTypeMemberSerializerTests
    {
        private DataTypeMemberSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new DataTypeMemberSerializer();
        }
        
        [Test]
        public void Serialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Serialize(null!)).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var member = new DataTypeMember { Name = "Test", DataType = "BOOL"};

            var xml = _serializer.Serialize(member);

            xml.Should().NotBeNull();
        }
        
        [Test]
        public Task Serialize_Basic_ShouldBeApproved()
        {
            var member = new DataTypeMember { Name = "Test", DataType = "BOOL"};

            var xml = _serializer.Serialize(member);

            return Verify(xml.ToString());
        }

        [Test]
        
        public Task Serialize_OverLoaded_ShouldBeApproved()
        {
            var member = new DataTypeMember { Name = "Test", DataType = "BOOL"};

            var xml = _serializer.Serialize(member);

            return Verify(xml.ToString());
        }
        
        [Test]
        public Task Serialize_Complex_ShouldBeApproved()
        {
            var member = new DataTypeMember { Name = "Test", DataType = "TIMER"};

            var xml = _serializer.Serialize(member);

            return Verify(xml.ToString());
        }

        [Test]
        public Task Serialize_AtomicArray_ShouldBeApproved()
        {
            var member = new DataTypeMember { Name = "Test", DataType = "DINT", Dimension = new Dimensions(10)};

            var xml = _serializer.Serialize(member);

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

            FluentActions.Invoking(() => _serializer.Deserialize(element)).Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Deserialize_AtomicMember_ShouldNotBeNull()
        {
            var element = XElement.Parse(GetAtomicMemberXml());

            var component = _serializer.Deserialize(element);

            component.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_AtomicMember_ShouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetAtomicMemberXml());

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("Test");
            component.DataType.Should().Be("BOOL");
            component.Dimension.Should().Be(Dimensions.Empty);
            component.Radix.Should().Be(Radix.Decimal);
            component.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            component.Description.Should().Be("Simple Member");
        }

        [Test]
        public void Deserialize_ArrayMember_ShouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetArrayMemberXml());

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("IntArray");
            component.DataType.Should().Be("INT");
            component.Dimension.Should().Be(new Dimensions(5));
            component.Radix.Should().Be(Radix.Octal);
            component.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            component.Description.Should().Be("Test Int Array");
        }

        [Test]
        public void Deserialize_SimpleMember_ShouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetSimpleMemberXml());

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("SimpleMember");
            component.DataType.Should().Be("SimpleType");
            component.Dimension.Should().Be(Dimensions.Empty);
            component.Radix.Should().Be(Radix.Null);
            component.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            component.Description.Should().Be("User defined simple member type");
        }

        private static string GetAtomicMemberXml()
        {
            return @"<Member Name=""Test"" DataType=""BOOL"" Dimension=""0"" Radix=""Decimal"" Hidden=""false"" ExternalAccess=""Read/Write"">
                <Description>
                <![CDATA[Simple Member]]>
                </Description>
                </Member>";
        }

        private static string GetArrayMemberXml()
        {
            return @" <Member Name=""IntArray"" DataType=""INT"" Dimension=""5"" Radix=""Octal"" Hidden=""false"" ExternalAccess=""Read/Write"">
                <Description>
                <![CDATA[Test Int Array]]>
                </Description>
                </Member>";
        }

        private static string GetSimpleMemberXml()
        {
            return @"<Member Name=""SimpleMember"" DataType=""SimpleType"" Dimension=""0"" Radix=""NullType"" Hidden=""false"" ExternalAccess=""Read/Write"">
                <Description>
                <![CDATA[User defined simple member type]]>
                </Description>
                </Member>";
        }
    }
}