using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Serialization;
using L5Sharp.Types;
using NUnit.Framework;
using String = L5Sharp.Types.String;

namespace L5Sharp.Internal.Tests.Serialization
{
    [TestFixture]
    public class StructureSerializerTests
    {
        [Test]
        public void Serialize_Null_ShouldThrowArgumentNullException()
        {
            var serializer = new StructureSerializer();

            FluentActions.Invoking(() => serializer.Serialize(null!)).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var component = new StructureType("Test", DataTypeClass.Unknown);
            var serializer = new StructureSerializer();

            var xml = serializer.Serialize(component);

            xml.Should().NotBeNull();
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_EmptyStructure_ShouldBeApproved()
        {
            var component = new StructureType("Test", DataTypeClass.Unknown);
            var serializer = new StructureSerializer();

            var xml = serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_DataValueValueMembers_ShouldBeApproved()
        {
            var component = new StructureType("Test", DataTypeClass.Unknown, new List<IMember<IDataType>>
            {
                Member.Create<Bool>("BoolMember"),
                Member.Create<Sint>("SintMember"),
                Member.Create<Int>("IntMember"),
                Member.Create<Dint>("DintMember"),
                Member.Create<Lint>("LintMember"),
                Member.Create<Real>("RealMember"),
            });
            
            var serializer = new StructureSerializer();

            var xml = serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_StructureMembers_ShouldBeApproved()
        {
            var component = new StructureType("Test", DataTypeClass.Unknown, new List<IMember<IDataType>>
            {
                Member.Create<String>("StringMember"),
                Member.Create<Timer>("SintMember"),
                Member.Create<Counter>("IntMember"),
            });
            
            var serializer = new StructureSerializer();

            var xml = serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        public void Deserialize_Null_ShouldThrowArgumentNullException()
        {
            var serializer = new StructureSerializer();

            FluentActions.Invoking(() => serializer.Deserialize(null!)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Deserialize_InvalidElementName_ShouldThrowArgumentException()
        {
            const string xml = @"<Invalid></Invalid>";
            var element = XElement.Parse(xml);
            var serializer = new StructureSerializer();

            FluentActions.Invoking(() => serializer.Deserialize(element)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Deserialize_ValidElement_ShouldNotBeNull()
        {
            var element = XElement.Parse(GetTestXml());
            var serializer = new StructureSerializer();

            var component = serializer.Deserialize(element);

            component.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_ValidElement_ShouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetTestXml());
            var serializer = new StructureSerializer();

            var component = serializer.Deserialize(element);

            component.Name.Should().Be("AB:5000_AI8:I:0");
            component.Members.ToList().Should().NotBeNull();
        }
        
        private static string GetTestXml()
        {
            return @"<Structure DataType=""AB:5000_AI8:I:0"">
                        <DataValueMember Name=""RunMode"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""ConnectionFaulted"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""DiagnosticActive"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""DiagnosticSequenceCount"" DataType=""SINT"" Radix=""Decimal"" Value=""0""/>
                        <StructureMember Name=""Ch00"" DataType=""CHANNEL_AI_DIAG:I:0"">
                        <DataValueMember Name=""Fault"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Uncertain"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""OpenWire"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""OverTemperature"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""FieldPowerOff"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""NotANumber"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Underrange"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Overrange"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""LLAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""LAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""HAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""HHAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""RateAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""CalFault"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Calibrating"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Data"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                        <DataValueMember Name=""RollingTimestamp"" DataType=""INT"" Radix=""Decimal"" Value=""0""/>
                        </StructureMember>
                        <StructureMember Name=""Ch01"" DataType=""CHANNEL_AI_DIAG:I:0"">
                        <DataValueMember Name=""Fault"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Uncertain"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""OpenWire"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""OverTemperature"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""FieldPowerOff"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""NotANumber"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Underrange"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Overrange"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""LLAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""LAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""HAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""HHAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""RateAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""CalFault"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Calibrating"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Data"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                        <DataValueMember Name=""RollingTimestamp"" DataType=""INT"" Radix=""Decimal"" Value=""0""/>
                        </StructureMember>
                        <StructureMember Name=""Ch02"" DataType=""CHANNEL_AI_DIAG:I:0"">
                        <DataValueMember Name=""Fault"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Uncertain"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""OpenWire"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""OverTemperature"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""FieldPowerOff"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""NotANumber"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Underrange"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Overrange"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""LLAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""LAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""HAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""HHAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""RateAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""CalFault"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Calibrating"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Data"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                        <DataValueMember Name=""RollingTimestamp"" DataType=""INT"" Radix=""Decimal"" Value=""0""/>
                        </StructureMember>
                        <StructureMember Name=""Ch03"" DataType=""CHANNEL_AI_DIAG:I:0"">
                        <DataValueMember Name=""Fault"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Uncertain"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""OpenWire"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""OverTemperature"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""FieldPowerOff"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""NotANumber"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Underrange"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Overrange"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""LLAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""LAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""HAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""HHAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""RateAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""CalFault"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Calibrating"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Data"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                        <DataValueMember Name=""RollingTimestamp"" DataType=""INT"" Radix=""Decimal"" Value=""0""/>
                        </StructureMember>
                        <StructureMember Name=""Ch04"" DataType=""CHANNEL_AI_DIAG:I:0"">
                        <DataValueMember Name=""Fault"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Uncertain"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""OpenWire"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""OverTemperature"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""FieldPowerOff"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""NotANumber"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Underrange"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Overrange"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""LLAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""LAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""HAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""HHAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""RateAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""CalFault"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Calibrating"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Data"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                        <DataValueMember Name=""RollingTimestamp"" DataType=""INT"" Radix=""Decimal"" Value=""0""/>
                        </StructureMember>
                        <StructureMember Name=""Ch05"" DataType=""CHANNEL_AI_DIAG:I:0"">
                        <DataValueMember Name=""Fault"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Uncertain"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""OpenWire"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""OverTemperature"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""FieldPowerOff"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""NotANumber"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Underrange"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Overrange"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""LLAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""LAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""HAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""HHAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""RateAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""CalFault"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Calibrating"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Data"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                        <DataValueMember Name=""RollingTimestamp"" DataType=""INT"" Radix=""Decimal"" Value=""0""/>
                        </StructureMember>
                        <StructureMember Name=""Ch06"" DataType=""CHANNEL_AI_DIAG:I:0"">
                        <DataValueMember Name=""Fault"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Uncertain"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""OpenWire"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""OverTemperature"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""FieldPowerOff"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""NotANumber"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Underrange"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Overrange"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""LLAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""LAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""HAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""HHAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""RateAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""CalFault"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Calibrating"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Data"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                        <DataValueMember Name=""RollingTimestamp"" DataType=""INT"" Radix=""Decimal"" Value=""0""/>
                        </StructureMember>
                        <StructureMember Name=""Ch07"" DataType=""CHANNEL_AI_DIAG:I:0"">
                        <DataValueMember Name=""Fault"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Uncertain"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""OpenWire"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""OverTemperature"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""FieldPowerOff"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""NotANumber"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Underrange"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Overrange"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""LLAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""LAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""HAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""HHAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""RateAlarm"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""CalFault"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Calibrating"" DataType=""BOOL"" Value=""0""/>
                        <DataValueMember Name=""Data"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                        <DataValueMember Name=""RollingTimestamp"" DataType=""INT"" Radix=""Decimal"" Value=""0""/>
                        </StructureMember>
                        </Structure>";
        }
    }
}