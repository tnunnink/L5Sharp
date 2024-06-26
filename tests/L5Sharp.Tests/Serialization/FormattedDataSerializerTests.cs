﻿using System;
using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Serialization.Data;
using L5Sharp.Types.Predefined;
using NUnit.Framework;
using VerifyNUnit;

namespace L5Sharp.Tests.Serialization
{
    [TestFixture]
    public class FormattedDataSerializerTests
    {
        private FormattedDataSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new FormattedDataSerializer();
        }

        [Test]
        public void Serialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Serialize(null!)).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void Serialize_ComplexType_ShouldNotBeNull()
        {
            var component = new TIMER();

            var xml = _serializer.Serialize(component);

            xml.Should().NotBeNull();
        }

        [Test]
        public void Serialize_StringType_ShouldNotBeNull()
        {
            var component = new STRING();

            var xml = _serializer.Serialize(component);

            xml.Should().NotBeNull();
        }

        [Test]
        public void Serialize_AlarmType_ShouldNotBeNull()
        {
            var component = new ALARM_ANALOG();

            var xml = _serializer.Serialize(component);

            xml.Should().NotBeNull();
        }
        
        [Test]
        public Task Serialize_ComplexType_ShouldBeApproved()
        {
            var component = new TIMER();

            var xml = _serializer.Serialize(component);

            return Verify(xml.ToString());
        }
        
        [Test]
        public Task Serialize_StringType_ShouldBeApproved()
        {
            var component = new STRING("This is test string");

            var xml = _serializer.Serialize(component);

            return Verify(xml.ToString());
        }
        
        [Test]
        
        public Task Serialize_AlarmType_ShouldBeApproved()
        {
            var component = new ALARM_ANALOG();

            var xml = _serializer.Serialize(component);

            return Verify(xml.ToString());
        }

        [Test]
        public void Deserialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Deserialize(null!)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Deserialize_DecoratedData_ShouldNotBeNull()
        {
            var xml = XElement.Parse(GetDecoratedData());

            var component = _serializer.Deserialize(xml);

            component.Should().NotBeNull();
        }
        
        [Test]
        public void Deserialize_StringData_ShouldNotBeNull()
        {
            var xml = XElement.Parse(GetStringData());

            var component = _serializer.Deserialize(xml);

            component.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_AlarmDigital_ShouldNotBeNull()
        {
            var xml = XElement.Parse(GetAlarmDigitalData());
            
            var component = _serializer.Deserialize(xml);

            component.Should().NotBeNull();
        }
        
        private static string GetNoFormatData()
        {
            return @"<Data Length=""17"">
                <![CDATA['This is a tests']]>
                </Data>";
        }

        private static string GetDecoratedData()
        {
            return @"<Data Format=""Decorated"">
                <Structure DataType=""SimpleType"">
                <DataValueMember Name=""BoolMember"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""SintMember"" DataType=""SINT"" Radix=""Hex"" Value=""16#00""/>
                <DataValueMember Name=""IntMember"" DataType=""INT"" Radix=""Octal"" Value=""8#000_016""/>
                <DataValueMember Name=""DintMember"" DataType=""DINT"" Radix=""ASCII"" Value=""'$00$00$00$01'""/>
                <DataValueMember Name=""LintMember"" DataType=""LINT"" Radix=""Decimal"" Value=""0""/>
                </Structure>
                </Data>";
        }

        private static string GetStringData()
        {
            return @"<Data Format=""String"" Length=""17"">
                <![CDATA['This is a tests']]>
                </Data>";
        }

        private static string GetAlarmDigitalData()
        {
            return @"<Data Format=""Alarm"">
                <AlarmDigitalParameters Severity=""500"" MinDurationPRE=""0"" ShelveDuration=""0"" MaxShelveDuration=""0"" ProgTime=""DT#1970-01-01-00:00:00.000_000Z"" EnableIn=""false"" In=""false"" InFault=""false"" Condition=""true"" AckRequired=""true"" Latched=""false""
            ProgAck=""false"" OperAck=""false"" ProgReset=""false"" OperReset=""false"" ProgSuppress=""false"" OperSuppress=""false"" ProgUnsuppress=""false"" OperUnsuppress=""false"" OperShelve=""false"" ProgUnshelve=""false"" OperUnshelve=""false""
            ProgDisable=""false"" OperDisable=""false"" ProgEnable=""false"" OperEnable=""false"" AlarmCountReset=""false"" UseProgTime=""false""/>
                <AlarmConfig/>
                </Data>";
        }
    }
}