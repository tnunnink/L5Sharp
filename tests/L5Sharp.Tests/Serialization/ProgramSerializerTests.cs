﻿using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Serialization;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.Serialization
{
    [TestFixture]
    public class ProgramSerializerTests
    {
        private ProgramSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new ProgramSerializer();
        }

        [Test]
        public void Serialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Serialize(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Serialize_BasicProgram_ShouldNotBeNull()
        {
            var component = new Program
            {
                Name = "Test",
                Description = "This is a test program"
            };

            var xml = _serializer.Serialize(component);

            xml.Should().NotBeNull();
        }

        [Test]
        public Task Serialize_BasicProgram_ShouldBeApproved()
        {
            var component = new Program
            {
                Name = "Test",
                Description = "This is a test program"
            };

            var xml = _serializer.Serialize(component);

            return Verify(xml.ToString());
        }

        [Test]
        public void Deserialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Deserialize(null!)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Deserialize_ValidElement_ShouldNotBeNull()
        {
            var xml = XElement.Parse(GetProgramData());

            var component = _serializer.Deserialize(xml);

            component.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_ValidElement_ShouldHaveExpectedProperties()
        {
            var xml = XElement.Parse(GetProgramData());

            var component = _serializer.Deserialize(xml);

            component.Name.Should().Be("NProgram");
            component.TestEdits.Should().BeFalse();
            component.MainRoutineName.Should().Be("Main");
            component.FaultRoutineName.Should().Be("Fault");
            component.Disabled.Should().BeFalse();
            component.UseAsFolder.Should().BeFalse();
        }


        private static string GetProgramData()
        {
            return
                @"<Program Use=""Target"" Name=""NProgram"" TestEdits=""false"" MainRoutineName=""Main"" FaultRoutineName=""Fault"" Disabled=""false"" UseAsFolder=""false"">
                    <Description>
                    <![CDATA[Test Program]]>
                    </Description>
                    <Tags>
                    <Tag Name=""InOutTag"" TagType=""Base"" DataType=""INT"" Radix=""Decimal"" Usage=""InOut"" Constant=""false"" ExternalAccess=""Read/Write""/>
                    <Tag Name=""InTag"" TagType=""Base"" DataType=""DINT"" Radix=""Decimal"" Usage=""Input"" Constant=""false"" ExternalAccess=""Read/Write"">
                    <Data Format=""L5K"">
                    <![CDATA[0]]>
                    </Data>
                    <Data Format=""Decorated"">
                    <DataValue DataType=""DINT"" Radix=""Decimal"" Value=""0""/>
                    </Data>
                    </Tag>
                    <Tag Name=""LocalBool"" TagType=""Base"" DataType=""BOOL"" Radix=""Decimal"" Constant=""false"" ExternalAccess=""Read/Write"">
                    <Data Format=""L5K"">
                    <![CDATA[0]]>
                    </Data>
                    <Data Format=""Decorated"">
                    <DataValue DataType=""BOOL"" Radix=""Decimal"" Value=""0""/>
                    </Data>
                    </Tag>
                    <Tag Name=""LocalComplex"" TagType=""Base"" DataType=""ComplexType"" Constant=""false"" ExternalAccess=""Read/Write"">
                    <Data Format=""L5K"">
                    <![CDATA[[[0,0,0,0,0],[0,0,0],[0,0,0],[1,0.00000000e+000,3.40282347e+038,3.40282347e+038,-3.40282347e+038,-3.40282347e+038
		                    ,0.00000000e+000,0.00000000e+000,0.00000000e+000,0.00000000e+000,0,0.00000000e+000,0,5.60519386e-045
		                    ,0.00000000e+000,0.00000000e+000,0.00000000e+000,0.00000000e+000,0.00000000e+000,0.00000000e+000
		                    ,0.00000000e+000,0.00000000e+000,0.00000000e+000,0.00000000e+000],[1,0,0],[[0,0,0,0,0],[0,0,0,0,0],[0,0,0,0,0],[0,0,0
		                    ,0,0],[0,0,0,0,0]]]]]>
                    </Data>
                    <Data Format=""Decorated"">
                    <Structure DataType=""ComplexType"">
                    <StructureMember Name=""SimpleMember"" DataType=""SimpleType"">
                    <DataValueMember Name=""BoolMember"" DataType=""BOOL"" Value=""0""/>
                    <DataValueMember Name=""SintMember"" DataType=""SINT"" Radix=""Hex"" Value=""16#00""/>
                    <DataValueMember Name=""IntMember"" DataType=""INT"" Radix=""Octal"" Value=""8#000_000""/>
                    <DataValueMember Name=""DintMember"" DataType=""DINT"" Radix=""ASCII"" Value=""'$00$00$00$00'""/>
                    <DataValueMember Name=""LintMember"" DataType=""LINT"" Radix=""Decimal"" Value=""0""/>
                    </StructureMember>
                    <StructureMember Name=""CounterMember"" DataType=""COUNTER"">
                    <DataValueMember Name=""PRE"" DataType=""DINT"" Radix=""Decimal"" Value=""0""/>
                    <DataValueMember Name=""ACC"" DataType=""DINT"" Radix=""Decimal"" Value=""0""/>
                    <DataValueMember Name=""CU"" DataType=""BOOL"" Value=""0""/>
                    <DataValueMember Name=""CD"" DataType=""BOOL"" Value=""0""/>
                    <DataValueMember Name=""DN"" DataType=""BOOL"" Value=""0""/>
                    <DataValueMember Name=""OV"" DataType=""BOOL"" Value=""0""/>
                    <DataValueMember Name=""UN"" DataType=""BOOL"" Value=""0""/>
                    </StructureMember>
                    <StructureMember Name=""TimeMember"" DataType=""TIMER"">
                    <DataValueMember Name=""PRE"" DataType=""DINT"" Radix=""Decimal"" Value=""0""/>
                    <DataValueMember Name=""ACC"" DataType=""DINT"" Radix=""Decimal"" Value=""0""/>
                    <DataValueMember Name=""EN"" DataType=""BOOL"" Value=""0""/>
                    <DataValueMember Name=""TT"" DataType=""BOOL"" Value=""0""/>
                    <DataValueMember Name=""DN"" DataType=""BOOL"" Value=""0""/>
                    </StructureMember>
                    <StructureMember Name=""AlarmMember"" DataType=""ALARM"">
                    <DataValueMember Name=""EnableIn"" DataType=""BOOL"" Value=""1""/>
                    <DataValueMember Name=""In"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                    <DataValueMember Name=""HHLimit"" DataType=""REAL"" Radix=""Float"" Value=""3.40282347e+038""/>
                    <DataValueMember Name=""HLimit"" DataType=""REAL"" Radix=""Float"" Value=""3.40282347e+038""/>
                    <DataValueMember Name=""LLimit"" DataType=""REAL"" Radix=""Float"" Value=""-3.40282347e+038""/>
                    <DataValueMember Name=""LLLimit"" DataType=""REAL"" Radix=""Float"" Value=""-3.40282347e+038""/>
                    <DataValueMember Name=""Deadband"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                    <DataValueMember Name=""ROCPosLimit"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                    <DataValueMember Name=""ROCNegLimit"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                    <DataValueMember Name=""ROCPeriod"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                    <DataValueMember Name=""EnableOut"" DataType=""BOOL"" Value=""0""/>
                    <DataValueMember Name=""HHAlarm"" DataType=""BOOL"" Value=""0""/>
                    <DataValueMember Name=""HAlarm"" DataType=""BOOL"" Value=""0""/>
                    <DataValueMember Name=""LAlarm"" DataType=""BOOL"" Value=""0""/>
                    <DataValueMember Name=""LLAlarm"" DataType=""BOOL"" Value=""0""/>
                    <DataValueMember Name=""ROCPosAlarm"" DataType=""BOOL"" Value=""0""/>
                    <DataValueMember Name=""ROCNegAlarm"" DataType=""BOOL"" Value=""0""/>
                    <DataValueMember Name=""ROC"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                    <DataValueMember Name=""Status"" DataType=""DINT"" Radix=""Hex"" Value=""16#0000_0000""/>
                    <DataValueMember Name=""InstructFault"" DataType=""BOOL"" Value=""0""/>
                    <DataValueMember Name=""DeadbandInv"" DataType=""BOOL"" Value=""0""/>
                    <DataValueMember Name=""ROCPosLimitInv"" DataType=""BOOL"" Value=""0""/>
                    <DataValueMember Name=""ROCNegLimitInv"" DataType=""BOOL"" Value=""0""/>
                    <DataValueMember Name=""ROCPeriodInv"" DataType=""BOOL"" Value=""0""/>
                    </StructureMember>
                    <StructureMember Name=""AOIType"" DataType=""aoi_Test"">
                    <DataValueMember Name=""EnableIn"" DataType=""BOOL"" Value=""1""/>
                    <DataValueMember Name=""EnableOut"" DataType=""BOOL"" Value=""0""/>
                    <DataValueMember Name=""InputTest"" DataType=""BOOL"" Value=""0""/>
                    <DataValueMember Name=""OutputTest"" DataType=""DINT"" Radix=""Decimal"" Value=""0""/>
                    <DataValueMember Name=""Config"" DataType=""INT"" Radix=""Decimal"" Value=""0""/>
                    <DataValueMember Name=""Test"" DataType=""BOOL"" Value=""0""/>
                    </StructureMember>
                    <ArrayMember Name=""SimplArray"" DataType=""SimpleType"" Dimensions=""5"">
                    <Element Index=""[0]"">
                    <Structure DataType=""SimpleType"">
                    <DataValueMember Name=""BoolMember"" DataType=""BOOL"" Value=""0""/>
                    <DataValueMember Name=""SintMember"" DataType=""SINT"" Radix=""Hex"" Value=""16#00""/>
                    <DataValueMember Name=""IntMember"" DataType=""INT"" Radix=""Octal"" Value=""8#000_000""/>
                    <DataValueMember Name=""DintMember"" DataType=""DINT"" Radix=""ASCII"" Value=""'$00$00$00$00'""/>
                    <DataValueMember Name=""LintMember"" DataType=""LINT"" Radix=""Decimal"" Value=""0""/>
                    </Structure>
                    </Element>
                    <Element Index=""[1]"">
                    <Structure DataType=""SimpleType"">
                    <DataValueMember Name=""BoolMember"" DataType=""BOOL"" Value=""0""/>
                    <DataValueMember Name=""SintMember"" DataType=""SINT"" Radix=""Hex"" Value=""16#00""/>
                    <DataValueMember Name=""IntMember"" DataType=""INT"" Radix=""Octal"" Value=""8#000_000""/>
                    <DataValueMember Name=""DintMember"" DataType=""DINT"" Radix=""ASCII"" Value=""'$00$00$00$00'""/>
                    <DataValueMember Name=""LintMember"" DataType=""LINT"" Radix=""Decimal"" Value=""0""/>
                    </Structure>
                    </Element>
                    <Element Index=""[2]"">
                    <Structure DataType=""SimpleType"">
                    <DataValueMember Name=""BoolMember"" DataType=""BOOL"" Value=""0""/>
                    <DataValueMember Name=""SintMember"" DataType=""SINT"" Radix=""Hex"" Value=""16#00""/>
                    <DataValueMember Name=""IntMember"" DataType=""INT"" Radix=""Octal"" Value=""8#000_000""/>
                    <DataValueMember Name=""DintMember"" DataType=""DINT"" Radix=""ASCII"" Value=""'$00$00$00$00'""/>
                    <DataValueMember Name=""LintMember"" DataType=""LINT"" Radix=""Decimal"" Value=""0""/>
                    </Structure>
                    </Element>
                    <Element Index=""[3]"">
                    <Structure DataType=""SimpleType"">
                    <DataValueMember Name=""BoolMember"" DataType=""BOOL"" Value=""0""/>
                    <DataValueMember Name=""SintMember"" DataType=""SINT"" Radix=""Hex"" Value=""16#00""/>
                    <DataValueMember Name=""IntMember"" DataType=""INT"" Radix=""Octal"" Value=""8#000_000""/>
                    <DataValueMember Name=""DintMember"" DataType=""DINT"" Radix=""ASCII"" Value=""'$00$00$00$00'""/>
                    <DataValueMember Name=""LintMember"" DataType=""LINT"" Radix=""Decimal"" Value=""0""/>
                    </Structure>
                    </Element>
                    <Element Index=""[4]"">
                    <Structure DataType=""SimpleType"">
                    <DataValueMember Name=""BoolMember"" DataType=""BOOL"" Value=""0""/>
                    <DataValueMember Name=""SintMember"" DataType=""SINT"" Radix=""Hex"" Value=""16#00""/>
                    <DataValueMember Name=""IntMember"" DataType=""INT"" Radix=""Octal"" Value=""8#000_000""/>
                    <DataValueMember Name=""DintMember"" DataType=""DINT"" Radix=""ASCII"" Value=""'$00$00$00$00'""/>
                    <DataValueMember Name=""LintMember"" DataType=""LINT"" Radix=""Decimal"" Value=""0""/>
                    </Structure>
                    </Element>
                    </ArrayMember>
                    </Structure>
                    </Data>
                    </Tag>
                    <Tag Name=""LocalDint"" TagType=""Base"" DataType=""DINT"" Radix=""Octal"" Constant=""false"" ExternalAccess=""Read/Write"">
                    <Data Format=""L5K"">
                    <![CDATA[1234]]>
                    </Data>
                    <Data Format=""Decorated"">
                    <DataValue DataType=""DINT"" Radix=""Octal"" Value=""8#00_000_002_322""/>
                    </Data>
                    </Tag>
                    <Tag Name=""LocalReal"" TagType=""Base"" DataType=""REAL"" Radix=""Float"" Constant=""false"" ExternalAccess=""Read/Write"">
                    <Data Format=""L5K"">
                    <![CDATA[0.00000000e+000]]>
                    </Data>
                    <Data Format=""Decorated"">
                    <DataValue DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                    </Data>
                    </Tag>
                    <Tag Name=""LocalSimlpe"" TagType=""Base"" DataType=""SimpleType"" Constant=""false"" ExternalAccess=""Read/Write"">
                    <Data Format=""L5K"">
                    <![CDATA[[0,0,0,0,0]]]>
                    </Data>
                    <Data Format=""Decorated"">
                    <Structure DataType=""SimpleType"">
                    <DataValueMember Name=""BoolMember"" DataType=""BOOL"" Value=""0""/>
                    <DataValueMember Name=""SintMember"" DataType=""SINT"" Radix=""Hex"" Value=""16#00""/>
                    <DataValueMember Name=""IntMember"" DataType=""INT"" Radix=""Octal"" Value=""8#000_000""/>
                    <DataValueMember Name=""DintMember"" DataType=""DINT"" Radix=""ASCII"" Value=""'$00$00$00$00'""/>
                    <DataValueMember Name=""LintMember"" DataType=""LINT"" Radix=""Decimal"" Value=""0""/>
                    </Structure>
                    </Data>
                    </Tag>
                    <Tag Name=""OutTag"" TagType=""Base"" DataType=""DINT"" Radix=""Decimal"" Usage=""Output"" Constant=""false"" ExternalAccess=""Read/Write"">
                    <Data Format=""L5K"">
                    <![CDATA[0]]>
                    </Data>
                    <Data Format=""Decorated"">
                    <DataValue DataType=""DINT"" Radix=""Decimal"" Value=""0""/>
                    </Data>
                    </Tag>
                    <Tag Name=""PublicInt"" TagType=""Base"" DataType=""INT"" Radix=""Decimal"" Usage=""Public"" Constant=""false"" ExternalAccess=""Read/Write"">
                    <Data Format=""L5K"">
                    <![CDATA[0]]>
                    </Data>
                    <Data Format=""Decorated"">
                    <DataValue DataType=""INT"" Radix=""Decimal"" Value=""0""/>
                    </Data>
                    </Tag>
                    </Tags>
                    <Routines>
                    <Routine Name=""Fault"" Type=""RLL""/>
                    <Routine Name=""Main"" Type=""RLL"">
                    <RLLContent>
                    <Rung Number=""0"" Type=""N"">
                    <Text>
                    <![CDATA[XIC(LocalBool)MOV(1234,LocalDint);]]>
                    </Text>
                    </Rung>
                    </RLLContent>
                    </Routine>
                    </Routines>
                    </Program>";
        }
    }
}