using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Serialization;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.Serialization
{
    [TestFixture]
    public class TagSerializerTests
    {
        private TagSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new TagSerializer();
        }

        [Test]
        public void Serialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Serialize(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void SerializeSimpleBool_ShouldNotBeNull()
        {
            var tag = new Tag { Name = "Test" };

            var xml = _serializer.Serialize(tag);

            xml.Should().NotBeNull();
        }

        [Test]
        public Task Serialize_SimpleBool_ShouldBeApproved()
        {
            var tag = new Tag { Name = "Test", Data = new BOOL() };

            var xml = _serializer.Serialize(tag);

            return Verify(xml.ToString());
        }

        [Test]
        public Task Serialize_SimpleSint_ShouldBeApproved()
        {
            var tag = Logix.Tag<SINT>("Test");

            var xml = _serializer.Serialize(tag);

            return Verify(xml.ToString());
        }

        [Test]
        public Task Serialize_SimpleInt_ShouldBeApproved()
        {
            var tag = Logix.Tag<INT>("Test");

            var xml = _serializer.Serialize(tag);

            return Verify(xml.ToString());
        }

        [Test]
        public Task Serialize_SimpleDint_ShouldBeApproved()
        {
            var tag = Logix.Tag<DINT>("Test");

            var xml = _serializer.Serialize(tag);

            return Verify(xml.ToString());
        }

        [Test]
        public Task Serialize_SimpleLint_ShouldBeApproved()
        {
            var tag = Logix.Tag<LINT>("Test");

            var xml = _serializer.Serialize(tag);

            return Verify(xml.ToString());
        }

        [Test]
        public Task Serialize_SimpleReal_ShouldBeApproved()
        {
            var tag = Logix.Tag<REAL>("Test");

            var xml = _serializer.Serialize(tag);

            return Verify(xml.ToString());
        }

        [Test]
        public Task Serialize_SimpleBoolArray_ShouldBeApproved()
        {
            var tag = Logix.TagArray<BOOL>("Test", 5);

            var xml = _serializer.Serialize(tag);

            return Verify(xml.ToString());
        }

        [Test]
        public Task Serialize_SimpleSintArray_ShouldBeApproved()
        {
            var tag = Logix.TagArray<SINT>("Test", 5);

            var xml = _serializer.Serialize(tag);

            return Verify(xml.ToString());
        }

        [Test]
        public Task Serialize_SimpleIntArray_ShouldBeApproved()
        {
            var tag = Logix.TagArray<INT>("Test", new Dimensions(5));

            var xml = _serializer.Serialize(tag);

            return Verify(xml.ToString());
        }

        [Test]
        public Task Serialize_SimpleDintArray_ShouldBeApproved()
        {
            var tag = Logix.TagArray<DINT>("Test", new Dimensions(5));

            var xml = _serializer.Serialize(tag);

            return Verify(xml.ToString());
        }

        [Test]
        public Task Serialize_SimpleLintArray_ShouldBeApproved()
        {
            var tag = Logix.TagArray<LINT>("Test", new Dimensions(5));

            var xml = _serializer.Serialize(tag);

            return Verify(xml.ToString());
        }

        [Test]
        public Task Serialize_SimpleRealArray_ShouldBeApproved()
        {
            var tag = Logix.TagArray<REAL>("Test", new Dimensions(5));

            var xml = _serializer.Serialize(tag);

            return Verify(xml.ToString());
        }

        [Test]
        public Task Serialize_String_ShouldBeApproved()
        {
            var tag = Logix.Tag<STRING>("Test");

            var xml = _serializer.Serialize(tag);

            return Verify(xml.ToString());
        }

        [Test]
        public Task Serialize_Timer_ShouldBeApproved()
        {
            var tag = Logix.Tag<TIMER>("Test");

            var xml = _serializer.Serialize(tag);

            return Verify(xml.ToString());
        }

        [Test]
        public Task Serialize_AlarmDigital_ShouldBeApproved()
        {
            var tag = Logix.Tag<ALARM_DIGITAL>("Test");

            var xml = _serializer.Serialize(tag);

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
        public void Deserialize_SimpleTagData_ShouldNotBeNull()
        {
            var element = XElement.Parse(GetSimpleTagData());

            var component = _serializer.Deserialize(element);

            component.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_SimpleTagData_ShouldBeExpected()
        {
            var element = XElement.Parse(GetSimpleTagData());

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("SimpleDint");
            component.DataType.Should().BeOfType<DINT>();
            component.Description.Should().BeEmpty();
            component.TagType.Should().Be(TagType.Base);
            component.Constant.Should().BeFalse();
            component.Radix.Should().Be(Radix.Decimal);
            component.ExternalAccess.Should().Be(ExternalAccess.None);
            component.Dimensions.Should().Be(Dimensions.Empty);
            component.Usage.Should().Be(TagUsage.Null);
            component.Data.Should().Be(123456);
        }

        [Test]
        public void Deserialize_ComplexPredefined_ShouldBeExpected()
        {
            var element = XElement.Parse(GetPredefinedTagData());

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("TestTimer");
            component.DataType.Should().BeOfType<TIMER>();
            component.Description.Should().Be("Test Timer");
            component.TagType.Should().Be(TagType.Base);
            component.Constant.Should().BeFalse();
            component.Radix.Should().Be(Radix.Null);
            component.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
            component.Dimensions.Should().Be(Dimensions.Empty);
            component.Usage.Should().Be(TagUsage.Null);
            component.Data.Should().BeNull();
            component.Members().Should().HaveCount(5);
            component.Member("PRE")?.Data.Should().Be(1000);
            component.Member("ACC")?.Data.Should().Be(0);
            component.Member("EN")?.Data.Should().Be(false);
            component.Member("TT")?.Data.Should().Be(false);
            component.Member("DN")?.Data.Should().Be(false);
        }

        private static string GetSimpleTagData()
        {
            return
                @"<Tag Name=""SimpleDint"" TagType=""Base"" DataType=""DINT"" Radix=""Decimal"" Constant=""false"" ExternalAccess=""None"">
                <Data Format=""L5K"">
                <![CDATA[123456]]>
                </Data>
                <Data Format=""Decorated"">
                <DataValue DataType=""DINT"" Radix=""Decimal"" Value=""123456""/>
                </Data>
                </Tag>";
        }

        private static string GetPredefinedTagData()
        {
            return
                @"<Tag Name=""TestTimer"" TagType=""Base"" DataType=""TIMER"" Constant=""false"" ExternalAccess=""Read Only"">
                <Description>
                <![CDATA[Test Timer]]>
                </Description>
                <Data Format=""L5K"">
                <![CDATA[[0,1000,0]]]>
                </Data>
                <Data Format=""Decorated"">
                <Structure DataType=""TIMER"">
                <DataValueMember Name=""PRE"" DataType=""DINT"" Radix=""Decimal"" Value=""1000""/>
                <DataValueMember Name=""ACC"" DataType=""DINT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""EN"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""TT"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""DN"" DataType=""BOOL"" Value=""0""/>
                </Structure>
                </Data>
                </Tag>";
        }
    }
}