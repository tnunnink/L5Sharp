using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Serialization;
using L5Sharp.Types;
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
            var tag = new Tag { Name = "Test", Data = new BOOL() };

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

            var tag = _serializer.Deserialize(element);

            tag.Should().NotBeNull();
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
            component.DataType.Should().Be("DINT");
            component.Description.Should().BeEmpty();
            component.TagType.Should().Be(TagType.Base);
            component.Constant.Should().BeFalse();
            component.Radix.Should().Be(Radix.Decimal);
            component.ExternalAccess.Should().Be(ExternalAccess.None);
            component.Dimensions.Should().Be(Dimensions.Empty);
            component.Usage.Should().Be(TagUsage.Normal);
            component.Data.Should().NotBeNull();
            component.Data.Should().BeOfType<DINT>();
            component.Data.As<DINT>().Should().Be(123456);
        }

        [Test]
        public void Deserialize_ComplexPredefined_ShouldBeExpected()
        {
            var element = XElement.Parse(GetPredefinedTagData());

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("TestTimer");
            component.DataType.Should().Be("TIMER");
            component.Description.Should().Be("Test Timer");
            component.TagType.Should().Be(TagType.Base);
            component.Constant.Should().BeFalse();
            component.Radix.Should().Be(Radix.Null);
            component.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
            component.Dimensions.Should().Be(Dimensions.Empty);
            component.Usage.Should().Be(TagUsage.Normal);
            component.Data.Should().NotBeNull();
            component.Data.Should().BeOfType<StructureType>();
            component.Members().Should().HaveCount(5);
            component.Member("PRE")?.Data.As<DINT>().Should().Be(1000);
            component.Member("ACC")?.Data.As<DINT>().Should().Be(0);
            component.Member("EN")?.Data.As<BOOL>().Should().Be(false);
            component.Member("TT")?.Data.As<BOOL>().Should().Be(false);
            component.Member("DN")?.Data.As<BOOL>().Should().Be(false);
        }
        
        [Test]
        public void Deserialize_GetTimerArrayWithComments_ShouldBeExpected()
        {
            var element = XElement.Parse(GetTimerArrayWithComments());

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("TimerArray");
            component.DataType.Should().Be("TIMER");
            component.Description.Should().Be("Base Timer");
            component.TagType.Should().Be(TagType.Base);
            component.Constant.Should().BeFalse();
            component.Radix.Should().Be(Radix.Null);
            component.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            component.Dimensions.Should().Be(new Dimensions(5));
            component.Usage.Should().Be(TagUsage.Normal);
            component.Data.Should().NotBeNull();
            component.Data.Should().BeOfType<ArrayType<ILogixType>>();
            component.Members().Should().HaveCount(30);
            component.Comments.Should().HaveCount(2);

            var first = component.Member("[1]");
            first?.As<TagMember>().Comment.Should().Be("Index 1");
            
            var firstPre = component.Member("[1].PRE");
            firstPre?.As<TagMember>().Comment.Should().Be("PRE 1");
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

        private static string GetTimerArrayWithComments()
        {
            return
                @"<Tag Name=""TimerArray"" TagType=""Base"" DataType=""TIMER"" Dimensions=""5"" Constant=""false""
                 ExternalAccess=""Read/Write"">
                <Description>
                    <![CDATA[Base Timer]]>
                </Description>
                <Comments>
                    <Comment Operand=""[1]"">
                        <![CDATA[Index 1]]>
                    </Comment>
                    <Comment Operand=""[1].PRE"">
                        <![CDATA[PRE 1]]>
                    </Comment>
                </Comments>
                <Data Format=""L5K"">
                    <![CDATA[[[0,5000,0],[0,0,0],[0,0,0],[0,0,0],[0,0,0]]]]>
                </Data>
                <Data Format=""Decorated"">
                    <Array DataType=""TIMER"" Dimensions=""5"">
                        <Element Index=""[0]"">
                            <Structure DataType=""TIMER"">
                                <DataValueMember Name=""PRE"" DataType=""DINT"" Radix=""Decimal"" Value=""5000""/>
                                <DataValueMember Name=""ACC"" DataType=""DINT"" Radix=""Decimal"" Value=""0""/>
                                <DataValueMember Name=""EN"" DataType=""BOOL"" Value=""0""/>
                                <DataValueMember Name=""TT"" DataType=""BOOL"" Value=""0""/>
                                <DataValueMember Name=""DN"" DataType=""BOOL"" Value=""0""/>
                            </Structure>
                        </Element>
                        <Element Index=""[1]"">
                            <Structure DataType=""TIMER"">
                                <DataValueMember Name=""PRE"" DataType=""DINT"" Radix=""Decimal"" Value=""0""/>
                                <DataValueMember Name=""ACC"" DataType=""DINT"" Radix=""Decimal"" Value=""0""/>
                                <DataValueMember Name=""EN"" DataType=""BOOL"" Value=""0""/>
                                <DataValueMember Name=""TT"" DataType=""BOOL"" Value=""0""/>
                                <DataValueMember Name=""DN"" DataType=""BOOL"" Value=""0""/>
                            </Structure>
                        </Element>
                        <Element Index=""[2]"">
                            <Structure DataType=""TIMER"">
                                <DataValueMember Name=""PRE"" DataType=""DINT"" Radix=""Decimal"" Value=""0""/>
                                <DataValueMember Name=""ACC"" DataType=""DINT"" Radix=""Decimal"" Value=""0""/>
                                <DataValueMember Name=""EN"" DataType=""BOOL"" Value=""0""/>
                                <DataValueMember Name=""TT"" DataType=""BOOL"" Value=""0""/>
                                <DataValueMember Name=""DN"" DataType=""BOOL"" Value=""0""/>
                            </Structure>
                        </Element>
                        <Element Index=""[3]"">
                            <Structure DataType=""TIMER"">
                                <DataValueMember Name=""PRE"" DataType=""DINT"" Radix=""Decimal"" Value=""0""/>
                                <DataValueMember Name=""ACC"" DataType=""DINT"" Radix=""Decimal"" Value=""0""/>
                                <DataValueMember Name=""EN"" DataType=""BOOL"" Value=""0""/>
                                <DataValueMember Name=""TT"" DataType=""BOOL"" Value=""0""/>
                                <DataValueMember Name=""DN"" DataType=""BOOL"" Value=""0""/>
                            </Structure>
                        </Element>
                        <Element Index=""[4]"">
                            <Structure DataType=""TIMER"">
                                <DataValueMember Name=""PRE"" DataType=""DINT"" Radix=""Decimal"" Value=""0""/>
                                <DataValueMember Name=""ACC"" DataType=""DINT"" Radix=""Decimal"" Value=""0""/>
                                <DataValueMember Name=""EN"" DataType=""BOOL"" Value=""0""/>
                                <DataValueMember Name=""TT"" DataType=""BOOL"" Value=""0""/>
                                <DataValueMember Name=""DN"" DataType=""BOOL"" Value=""0""/>
                            </Structure>
                        </Element>
                    </Array>
                </Data>
            </Tag>";
        }
    }
}