﻿using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Serialization.Data;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Tests.Serialization
{
    [TestFixture]
    public class StructureSerializerTests
    {
        private StructureSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new StructureSerializer();
        }

        [Test]
        public void Serialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Serialize(null!)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var component = new StructureType("Test", new List<Member>());

            var xml = _serializer.Serialize(component);

            xml.Should().NotBeNull();
        }
        
        [Test]
        public Task Serialize_EmptyStructure_ShouldBeApproved()
        {
            var component = new StructureType("Test", new List<Member>());

            var xml = _serializer.Serialize(component);

            return Verify(xml.ToString());
        }

        [Test]
        
        public Task Serialize_DataValueValueMembers_ShouldBeApproved()
        {
            var component = new StructureType("Test", new List<Member>()
            {
                Logix.Member<BOOL>("BoolMember"),
                Logix.Member<SINT>("SintMember"),
                Logix.Member<INT>("IntMember"),
                Logix.Member<DINT>("DintMember"),
                Logix.Member<LINT>("LintMember"),
                Logix.Member<REAL>("RealMember")
            });

            var xml = _serializer.Serialize(component);

            return Verify(xml.ToString());
        }
        
        [Test]
        
        public Task SerializeArrayMembers_ShouldBeApproved()
        {
            var type = new StructureType("Test", new List<Member>
            {
                new ("SimpleMember", Logix.Array<DINT>(10)),
                new ("ComplexMember", Logix.Array<TIMER>(5)),
                new ("ComplexMember", Logix.Array<STRING>(2)),
            });

            var xml = _serializer.Serialize(type);

            return Verify(xml.ToString());
        }
        
        [Test]
        
        public Task Serialize_StructureMembers_ShouldBeApproved()
        {
            var type = new StructureType("Test", new List<Member>
            {
                new ("StringMember", new STRING()),
                new ("TimerMember", new TIMER()),
                new ("CounterMember", new COUNTER()),
            });

            var xml = _serializer.Serialize(type);

            return Verify(xml.ToString());
        }

        [Test]
        public void Deserialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Deserialize(null!)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Deserialize_SimpleArrayStructure_ShouldNotBeNull()
        {
            var element = XElement.Parse(GetSimpleArrayStructure());

            var component = _serializer.Deserialize(element);

            component.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_SimpleArrayStructure_ShouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetSimpleArrayStructure());

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("ArrayType");
            component.Members.ToList().Should().HaveCount(5);
        }
        
        [Test]
        public void Deserialize_ComplexType_ShouldNotBeNull()
        {
            var element = XElement.Parse(GetComplexTypeStructure());

            var component = _serializer.Deserialize(element);

            component.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_ModuleType_ShouldNotBeNull()
        {
            var element = XElement.Parse(GetModuleTypeStructure());

            var component = _serializer.Deserialize(element);

            component.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_ModuleType_ShouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetModuleTypeStructure());

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("AB:5000_AI8:I:0");
            component.Members.ToList().Should().NotBeNull();
        }

        private static string GetComplexTypeStructure()
        {
            return @"<Structure DataType=""ComplexType"">
                <StructureMember Name=""SimpleMember"" DataType=""SimpleType"">
                    <DataValueMember Name=""BoolMember"" DataType=""BOOL"" Value=""0""/>
                    <DataValueMember Name=""SintMember"" DataType=""SINT"" Radix=""Hex"" Value=""16#00""/>
                    <DataValueMember Name=""IntMember"" DataType=""INT"" Radix=""Octal"" Value=""8#000_000""/>
                    <DataValueMember Name=""DintMember"" DataType=""DINT"" Radix=""ASCII""
                                     Value=""'$00$00$00$00'""/>
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
                    <DataValueMember Name=""HHLimit"" DataType=""REAL"" Radix=""Float""
                                     Value=""3.40282347e+038""/>
                    <DataValueMember Name=""HLimit"" DataType=""REAL"" Radix=""Float""
                                     Value=""3.40282347e+038""/>
                    <DataValueMember Name=""LLimit"" DataType=""REAL"" Radix=""Float""
                                     Value=""-3.40282347e+038""/>
                    <DataValueMember Name=""LLLimit"" DataType=""REAL"" Radix=""Float""
                                     Value=""-3.40282347e+038""/>
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
                <ArrayMember Name=""SimpleArray"" DataType=""SimpleType"" Dimensions=""5"">
                    <Element Index=""[0]"">
                        <Structure DataType=""SimpleType"">
                            <DataValueMember Name=""BoolMember"" DataType=""BOOL"" Value=""0""/>
                            <DataValueMember Name=""SintMember"" DataType=""SINT"" Radix=""Hex""
                                             Value=""16#00""/>
                            <DataValueMember Name=""IntMember"" DataType=""INT"" Radix=""Octal""
                                             Value=""8#000_000""/>
                            <DataValueMember Name=""DintMember"" DataType=""DINT"" Radix=""ASCII""
                                             Value=""'$00$00$00$00'""/>
                            <DataValueMember Name=""LintMember"" DataType=""LINT"" Radix=""Decimal""
                                             Value=""0""/>
                        </Structure>
                    </Element>
                    <Element Index=""[1]"">
                        <Structure DataType=""SimpleType"">
                            <DataValueMember Name=""BoolMember"" DataType=""BOOL"" Value=""0""/>
                            <DataValueMember Name=""SintMember"" DataType=""SINT"" Radix=""Hex""
                                             Value=""16#00""/>
                            <DataValueMember Name=""IntMember"" DataType=""INT"" Radix=""Octal""
                                             Value=""8#000_000""/>
                            <DataValueMember Name=""DintMember"" DataType=""DINT"" Radix=""ASCII""
                                             Value=""'$00$00$00$00'""/>
                            <DataValueMember Name=""LintMember"" DataType=""LINT"" Radix=""Decimal""
                                             Value=""0""/>
                        </Structure>
                    </Element>
                    <Element Index=""[2]"">
                        <Structure DataType=""SimpleType"">
                            <DataValueMember Name=""BoolMember"" DataType=""BOOL"" Value=""0""/>
                            <DataValueMember Name=""SintMember"" DataType=""SINT"" Radix=""Hex""
                                             Value=""16#00""/>
                            <DataValueMember Name=""IntMember"" DataType=""INT"" Radix=""Octal""
                                             Value=""8#000_000""/>
                            <DataValueMember Name=""DintMember"" DataType=""DINT"" Radix=""ASCII""
                                             Value=""'$00$00$00$00'""/>
                            <DataValueMember Name=""LintMember"" DataType=""LINT"" Radix=""Decimal""
                                             Value=""0""/>
                        </Structure>
                    </Element>
                    <Element Index=""[3]"">
                        <Structure DataType=""SimpleType"">
                            <DataValueMember Name=""BoolMember"" DataType=""BOOL"" Value=""0""/>
                            <DataValueMember Name=""SintMember"" DataType=""SINT"" Radix=""Hex""
                                             Value=""16#00""/>
                            <DataValueMember Name=""IntMember"" DataType=""INT"" Radix=""Octal""
                                             Value=""8#000_000""/>
                            <DataValueMember Name=""DintMember"" DataType=""DINT"" Radix=""ASCII""
                                             Value=""'$00$00$00$00'""/>
                            <DataValueMember Name=""LintMember"" DataType=""LINT"" Radix=""Decimal""
                                             Value=""0""/>
                        </Structure>
                    </Element>
                    <Element Index=""[4]"">
                        <Structure DataType=""SimpleType"">
                            <DataValueMember Name=""BoolMember"" DataType=""BOOL"" Value=""0""/>
                            <DataValueMember Name=""SintMember"" DataType=""SINT"" Radix=""Hex""
                                             Value=""16#00""/>
                            <DataValueMember Name=""IntMember"" DataType=""INT"" Radix=""Octal""
                                             Value=""8#000_000""/>
                            <DataValueMember Name=""DintMember"" DataType=""DINT"" Radix=""ASCII""
                                             Value=""'$00$00$00$00'""/>
                            <DataValueMember Name=""LintMember"" DataType=""LINT"" Radix=""Decimal""
                                             Value=""0""/>
                        </Structure>
                    </Element>
                </ArrayMember>
            </Structure>";
        }
        

        private static string GetModuleTypeStructure()
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
        
        private static string GetSimpleArrayStructure()
        {
            return @"<Structure DataType=""ArrayType"">
                        <ArrayMember Name=""BoolArray"" DataType=""BOOL"" Dimensions=""32"" Radix=""Decimal"">
                            <Element Index=""[0]"" Value=""0""/>
                            <Element Index=""[1]"" Value=""0""/>
                            <Element Index=""[2]"" Value=""0""/>
                            <Element Index=""[3]"" Value=""0""/>
                            <Element Index=""[4]"" Value=""0""/>
                            <Element Index=""[5]"" Value=""0""/>
                            <Element Index=""[6]"" Value=""0""/>
                            <Element Index=""[7]"" Value=""0""/>
                            <Element Index=""[8]"" Value=""0""/>
                            <Element Index=""[9]"" Value=""0""/>
                            <Element Index=""[10]"" Value=""0""/>
                            <Element Index=""[11]"" Value=""0""/>
                            <Element Index=""[12]"" Value=""0""/>
                            <Element Index=""[13]"" Value=""0""/>
                            <Element Index=""[14]"" Value=""0""/>
                            <Element Index=""[15]"" Value=""0""/>
                            <Element Index=""[16]"" Value=""0""/>
                            <Element Index=""[17]"" Value=""0""/>
                            <Element Index=""[18]"" Value=""0""/>
                            <Element Index=""[19]"" Value=""0""/>
                            <Element Index=""[20]"" Value=""0""/>
                            <Element Index=""[21]"" Value=""0""/>
                            <Element Index=""[22]"" Value=""0""/>
                            <Element Index=""[23]"" Value=""0""/>
                            <Element Index=""[24]"" Value=""0""/>
                            <Element Index=""[25]"" Value=""0""/>
                            <Element Index=""[26]"" Value=""0""/>
                            <Element Index=""[27]"" Value=""0""/>
                            <Element Index=""[28]"" Value=""0""/>
                            <Element Index=""[29]"" Value=""0""/>
                            <Element Index=""[30]"" Value=""0""/>
                            <Element Index=""[31]"" Value=""0""/>
                        </ArrayMember>
                        <ArrayMember Name=""SintArray"" DataType=""SINT"" Dimensions=""6"" Radix=""Octal"">
                            <Element Index=""[0]"" Value=""8#000""/>
                            <Element Index=""[1]"" Value=""8#000""/>
                            <Element Index=""[2]"" Value=""8#000""/>
                            <Element Index=""[3]"" Value=""8#000""/>
                            <Element Index=""[4]"" Value=""8#000""/>
                            <Element Index=""[5]"" Value=""8#000""/>
                        </ArrayMember>
                        <ArrayMember Name=""IntArray"" DataType=""INT"" Dimensions=""8"" Radix=""Decimal"">
                            <Element Index=""[0]"" Value=""0""/>
                            <Element Index=""[1]"" Value=""0""/>
                            <Element Index=""[2]"" Value=""0""/>
                            <Element Index=""[3]"" Value=""0""/>
                            <Element Index=""[4]"" Value=""0""/>
                            <Element Index=""[5]"" Value=""0""/>
                            <Element Index=""[6]"" Value=""0""/>
                            <Element Index=""[7]"" Value=""0""/>
                        </ArrayMember>
                        <ArrayMember Name=""DintArray"" DataType=""DINT"" Dimensions=""3"" Radix=""Decimal"">
                            <Element Index=""[0]"" Value=""0""/>
                            <Element Index=""[1]"" Value=""0""/>
                            <Element Index=""[2]"" Value=""0""/>
                        </ArrayMember>
                        <ArrayMember Name=""LintArray"" DataType=""LINT"" Dimensions=""2"" Radix=""ASCII"">
                            <Element Index=""[0]"" Value=""'$00$00$00$00$00$00$00$00'""/>
                            <Element Index=""[1]"" Value=""'$00$00$00$00$00$00$00$00'""/>
                        </ArrayMember>
                    </Structure>";
        }
    }
}