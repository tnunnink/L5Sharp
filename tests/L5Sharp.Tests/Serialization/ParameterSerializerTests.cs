using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Serialization;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Tests.Serialization
{
    [TestFixture]
    public class ParameterSerializerTests
    {
        private ParameterSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new ParameterSerializer();
        }

        [Test]
        public void Serialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Serialize(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var parameter = new Parameter { Name = "Test", DataType = nameof(BOOL) };

            var xml = _serializer.Serialize(parameter);

            xml.Should().NotBeNull();
        }

        [Test]
        public Task Serialize_Basic_ShouldBeApproved()
        {
            var parameter = new Parameter { Name = "Test", DataType = nameof(BOOL) };

            var xml = _serializer.Serialize(parameter);

            return Verify(xml.ToString());
        }

        [Test]
        public Task Serialize_OverLoaded_ShouldBeApproved()
        {
            var parameter = new Parameter
            {
                Name = "Test",
                DataType = nameof(BOOL),
                Radix = Radix.Binary,
                ExternalAccess = ExternalAccess.ReadOnly,
                Usage = TagUsage.Output,
                Required = true, Visible = true, Constant = true,
                Description = "This is a test"
            };

            var xml = _serializer.Serialize(parameter);

            return Verify(xml.ToString());
        }

        [Test]
        
        public void Serialize_Complex_ShouldBeApproved()
        {
            var parameter = new Parameter { Name = "Test", DataType = nameof(TIMER) };

            var xml = _serializer.Serialize(parameter);

            Verifier.Verify(xml.ToString());
        }

        [Test]
        
        public void Serialize_AtomicArray_ShouldBeApproved()
        {
            var parameter = new Parameter { Name = "Test", DataType = nameof(DINT), Dimension = 10};

            var xml = _serializer.Serialize(parameter);

            Verifier.Verify(xml.ToString());
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

            component.Name.Should().Be("InputTest");
            component.DataType.Should().BeOfType<BOOL>();
            component.Dimension.Should().Be(Dimensions.Empty);
            component.Radix.Should().Be(Radix.Decimal);
            component.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            component.Usage.Should().Be(TagUsage.Input);
            component.Required.Should().BeFalse();
            component.Visible.Should().BeTrue();
            component.Constant.Should().BeFalse();
            component.Description.Should().BeEmpty();
        }

        [Test]
        public void Deserialize_ArrayMember_ShouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetArrayMemberXml());

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("Array");
            component.Dimension.Should().Be(new Dimensions(5));
            component.Radix.Should().Be(Radix.Float);
            component.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            component.Usage.Should().Be(TagUsage.InOut);
            component.Required.Should().BeTrue();
            component.Visible.Should().BeTrue();
            component.Constant.Should().BeFalse();
            component.Description.Should().BeEmpty();
        }

        [Test]
        public void Deserialize_SimpleMember_ShouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetSimpleMemberXml());

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("InOutTest");
            component.DataType.Should().Be("SimpleType");
            component.Dimension.Should().Be(Dimensions.Empty);
            component.Radix.Should().Be(Radix.Null);
            component.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            component.Usage.Should().Be(TagUsage.InOut);
            component.Required.Should().BeTrue();
            component.Visible.Should().BeTrue();
            component.Constant.Should().BeFalse();
            component.Description.Should().BeEmpty();
        }

        private static string GetAtomicMemberXml()
        {
            return
                @"<Parameter Name=""InputTest"" TagType=""Base"" DataType=""BOOL"" Usage=""Input"" Radix=""Decimal""
                    Required=""false"" Visible=""true"" ExternalAccess=""Read/Write"">
                <DefaultData Format=""L5K"">
                <![CDATA[0]]>
                </DefaultData>
                <DefaultData Format=""Decorated"">
                <DataValue DataType=""BOOL"" Radix=""Decimal"" Value=""0""/>
                </DefaultData>
                </Parameter>";
        }

        private static string GetArrayMemberXml()
        {
            return
                "<Parameter Name=\"Array\" TagType=\"Base\" DataType=\"REAL\" Dimensions=\"5\" Usage=\"InOut\" Radix=\"Float\" Required=\"true\" Visible=\"true\" Constant=\"false\"/>";
        }

        private static string GetSimpleMemberXml()
        {
            return
                "<Parameter Name=\"InOutTest\" TagType=\"Base\" DataType=\"SimpleType\" Usage=\"InOut\" Required=\"true\" Visible=\"true\" Constant=\"false\"/>";
        }
    }
}