using System;
using System.Collections.Generic;
using System.Xml.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Serialization.Tests
{
    [TestFixture]
    public class AddOnInstructionSerializerTests
    {
        private AddOnInstructionSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new AddOnInstructionSerializer();
        }

        [Test]
        public void Serialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Serialize(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var instruction = new AddOnInstruction("Test");

            var xml = _serializer.Serialize(instruction);

            xml.Should().NotBeNull();
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_BasicType_ShouldBeApproved()
        {
            var instruction = new AddOnInstruction("Test");

            var xml = _serializer.Serialize(instruction);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_OverloadedInstruction_ShouldBeApproved()
        {
            var instruction = new AddOnInstruction("Test", RoutineType.Rll, new Revision(1, 1), "Rev1",
                "This is the revision note", "ME", true, true, true, 
                DateTime.Parse("1/1/2022 12:00:00"), "CreatedBy", DateTime.Parse("1/1/2022 12:00:00"), "EditedBy", new Revision(2, 1), 
                "this is additional help text", true, new List<IParameter<IDataType>>
                {
                    new Parameter<IDataType>("TestParameter1", new BOOL()),
                    new Parameter<IDataType>("TestParameter2", new REAL()),
                    new Parameter<IDataType>("TestParameter3", new TIMER())
                }, new List<ITag<IDataType>>
                {
                    new Tag<IDataType>("TestTag1", new ALARM()),
                    new Tag<IDataType>("TestTag2", new DINT()),
                    new Tag<IDataType>("TestTag3", new STRING())
                }, new List<IRoutine<ILogixContent>>
                {
                   new Routine<ILogixContent>("Main"),
                   new Routine<ILogixContent>("Test")
                });

            var xml = _serializer.Serialize(instruction);

            Approvals.VerifyXml(xml.ToString());
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
        public void Deserialize_ValidElement_ShouldNotBeNull()
        {
            var element = XElement.Parse(GetValidElement());

            var component = _serializer.Deserialize(element);

            component.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_ValidElement_ShouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetValidElement());

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("aoi_Test");
            component.Family.Should().Be(DataTypeFamily.None);
            component.Class.Should().Be(DataTypeClass.AddOnDefined);
            component.Description.Should().Be("Test AOI");
            component.Revision.Should().Be(new Revision());
            component.RevisionExtension.Should().Be("Rev1");
            component.RevisionNote.Should().Be("This revision not is a test note");
            component.Vendor.Should().Be("ENE");
            component.ExecutePreScan.Should().BeFalse();
            component.ExecutePostScan.Should().BeFalse();
            component.ExecuteEnableInFalse.Should().BeFalse();
            component.CreatedDate.Should().Be(DateTime.Parse("2021-10-22 15:27:48.120"));
            component.CreatedBy.Should().Be(@"ENE\tnunnink");
            component.EditedDate.Should().Be(DateTime.Parse("2022-03-01 03:17:54.746"));
            component.EditedBy.Should().Be(@"ENE\tnunnink");
            component.SoftwareRevision.Should().Be(new Revision(32, 2));
            component.AdditionalHelpText.Should().Be("This is a test aoi definitio that was built for purposes of exporting data to examine the structure of the L5X");
            component.IsEncrypted.Should().BeFalse();
            component.Parameters.Should().NotBeEmpty();
            component.LocalTags.Should().NotBeEmpty();
            component.Routines.Should().NotBeEmpty();
        }

        private static string GetValidElement()
        {
            return
                @"<AddOnInstructionDefinition Use=""Target"" Name=""aoi_Test"" Revision=""1.0"" RevisionExtension=""Rev1"" Vendor=""ENE"" ExecutePrescan=""false"" ExecutePostscan=""false"" ExecuteEnableInFalse=""false"" CreatedDate=""2021-10-22T15:27:48.120Z"" CreatedBy=""ENE\tnunnink"" EditedDate=""2022-03-01T03:17:54.746Z""
                         EditedBy=""ENE\tnunnink"" SoftwareRevision=""v32.02"">
                        <Description>
                        <![CDATA[Test AOI]]>
                        </Description>
                        <RevisionNote>
                        <![CDATA[This revision not is a test note]]>
                        </RevisionNote>
                        <SignatureHistory>
                        <HistoryEntry User=""ENE\tnunnink"" Timestamp=""2022-03-01T03:10:25.642Z"" SignatureID=""16#cae1_ac8c"">
                        <Description>
                        <![CDATA[This is a signature history description
                        ]]>
                        </Description>
                        </HistoryEntry>
                        </SignatureHistory>
                        <AdditionalHelpText>
                        <![CDATA[This is a test aoi definitio that was built for purposes of exporting data to examine the structure of the L5X]]>
                        </AdditionalHelpText>
                        <Parameters>
                        <Parameter Name=""EnableIn"" TagType=""Base"" DataType=""BOOL"" Usage=""Input"" Radix=""Decimal"" Required=""false"" Visible=""false"" ExternalAccess=""Read Only"">
                        <Description>
                        <![CDATA[Enable Input - System Defined Parameter]]>
                        </Description>
                        </Parameter>
                        <Parameter Name=""EnableOut"" TagType=""Base"" DataType=""BOOL"" Usage=""Output"" Radix=""Decimal"" Required=""false"" Visible=""false"" ExternalAccess=""Read Only"">
                        <Description>
                        <![CDATA[Enable Output - System Defined Parameter]]>
                        </Description>
                        </Parameter>
                        <Parameter Name=""InputTest"" TagType=""Base"" DataType=""BOOL"" Usage=""Input"" Radix=""Decimal"" Required=""false"" Visible=""true"" ExternalAccess=""Read/Write"">
                        <DefaultData Format=""L5K"">
                        <![CDATA[0]]>
                        </DefaultData>
                        <DefaultData Format=""Decorated"">
                        <DataValue DataType=""BOOL"" Radix=""Decimal"" Value=""0""/>
                        </DefaultData>
                        </Parameter>
                        <Parameter Name=""OutputTest"" TagType=""Base"" DataType=""DINT"" Usage=""Input"" Radix=""Decimal"" Required=""false"" Visible=""true"" ExternalAccess=""Read Only"">
                        <DefaultData Format=""L5K"">
                        <![CDATA[0]]>
                        </DefaultData>
                        <DefaultData Format=""Decorated"">
                        <DataValue DataType=""DINT"" Radix=""Decimal"" Value=""0""/>
                        </DefaultData>
                        </Parameter>
                        <Parameter Name=""InOutTest"" TagType=""Base"" DataType=""SimpleType"" Usage=""InOut"" Required=""true"" Visible=""true"" Constant=""false""/>
                        <Parameter Name=""Config"" TagType=""Base"" DataType=""INT"" Usage=""Input"" Radix=""Decimal"" Required=""true"" Visible=""true"" ExternalAccess=""None"">
                        <DefaultData Format=""L5K"">
                        <![CDATA[0]]>
                        </DefaultData>
                        <DefaultData Format=""Decorated"">
                        <DataValue DataType=""INT"" Radix=""Decimal"" Value=""0""/>
                        </DefaultData>
                        </Parameter>
                        <Parameter Name=""Test"" TagType=""Base"" DataType=""BOOL"" Usage=""Input"" Radix=""Decimal"" Required=""false"" Visible=""false"" ExternalAccess=""None"">
                        <DefaultData Format=""L5K"">
                        <![CDATA[0]]>
                        </DefaultData>
                        <DefaultData Format=""Decorated"">
                        <DataValue DataType=""BOOL"" Radix=""Decimal"" Value=""0""/>
                        </DefaultData>
                        </Parameter>
                        <Parameter Name=""Timers"" TagType=""Base"" DataType=""REAL"" Dimensions=""5"" Usage=""InOut"" Radix=""Float"" Required=""true"" Visible=""true"" Constant=""false""/>
                        </Parameters>
                        <LocalTags>
                        <LocalTag Name=""LocalBool"" DataType=""BOOL"" Radix=""Decimal"" ExternalAccess=""None"">
                        <DefaultData Format=""L5K"">
                        <![CDATA[0]]>
                        </DefaultData>
                        <DefaultData Format=""Decorated"">
                        <DataValue DataType=""BOOL"" Radix=""Decimal"" Value=""0""/>
                        </DefaultData>
                        </LocalTag>
                        </LocalTags>
                        <Routines>
                        <Routine Name=""Logic"" Type=""RLL"">
                        <RLLContent>
                        <Rung Number=""0"" Type=""N"">
                        <Text>
                        <![CDATA[OTE(InputTest);]]>
                        </Text>
                        </Rung>
                        </RLLContent>
                        </Routine>
                        <Routine Name=""Prescan"" Type=""RLL""/>
                        </Routines>
                        <Dependencies>
                        <Dependency Type=""DataType"" Name=""SimpleType""/>
                        </Dependencies>
                        </AddOnInstructionDefinition>";
        }
    }
}
