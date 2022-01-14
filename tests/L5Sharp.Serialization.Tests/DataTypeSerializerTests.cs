using System;
using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Serialization.Tests
{
    [TestFixture]
    public class DataTypeSerializerTests
    {
        private DataTypeSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new DataTypeSerializer();
        }
        
        [Test]
        public void Serialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Serialize(null!)).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var type = new UserDefined("Test");

            var xml = _serializer.Serialize(type);

            xml.Should().NotBeNull();
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_BasicType_ShouldBeApproved()
        {
            var type = new UserDefined("Test", "This is a test user defined type");

            var xml = _serializer.Serialize(type);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_AtomicType_ShouldBeApproved()
        {
            var type = new UserDefined("Test", "This is a test user defined type", new List<IMember<IDataType>>
            {
                Member.Create<Bool>("Member01", Radix.Binary, ExternalAccess.ReadOnly, "This is a test member"),
                Member.Create<Int>("Member02", Radix.Octal, ExternalAccess.None, "This is a test member"),
                Member.Create<Dint>("Member03", Radix.Decimal, ExternalAccess.ReadOnly, "This is a test member"),
                Member.Create<Lint>("Member04", Radix.Hex, description: "This is a test member"),
                Member.Create<Sint>("Member04", Radix.Ascii),
                Member.Create<Real>("Member05")
            });

            var xml = _serializer.Serialize(type);

            Approvals.VerifyXml(xml.ToString());
        }

        
        private static string GetComplexTypeXml()
        {
            return @"<DataType Name=""ComplexType"" Family=""NoFamily"" Class=""User"">
                <Description>
                <![CDATA[Test data type with more complex members]]>
                </Description>
                <Members>
                <Member Name=""SimpleMember"" DataType=""SimpleType"" Dimension=""0"" Radix=""NullType"" Hidden=""false"" ExternalAccess=""Read/Write"">
                <Description>
                <![CDATA[User defined complex type]]>
                </Description>
                </Member>
                <Member Name=""CounterMember"" DataType=""COUNTER"" Dimension=""0"" Radix=""NullType"" Hidden=""false"" ExternalAccess=""Read/Write"">
                <Description>
                <![CDATA[Test counter member]]>
                </Description>
                </Member>
                <Member Name=""TimeMember"" DataType=""TIMER"" Dimension=""0"" Radix=""NullType"" Hidden=""false"" ExternalAccess=""Read/Write"">
                <Description>
                <![CDATA[Test Timer member]]>
                </Description>
                </Member>
                <Member Name=""AlarmMember"" DataType=""ALARM"" Dimension=""0"" Radix=""NullType"" Hidden=""false"" ExternalAccess=""Read/Write"">
                <Description>
                <![CDATA[Test Analog Alarm]]>
                </Description>
                </Member>
                <Member Name=""AOIType"" DataType=""aoi_Test"" Dimension=""0"" Radix=""NullType"" Hidden=""false"" ExternalAccess=""Read/Write"">
                <Description>
                <![CDATA[Test aoi]]>
                </Description>
                </Member>
                <Member Name=""SimpleArray"" DataType=""SimpleType"" Dimension=""5"" Radix=""NullType"" Hidden=""false"" ExternalAccess=""Read/Write"">
                <Description>
                <![CDATA[Test simple array type]]>
                </Description>
                </Member>
                </Members>
                </DataType>";
        }
        
        private static string GetArrayTypeXml()
        {
            return @"<DataType Name=""ArrayType"" Family=""NoFamily"" Class=""User"">
                <Description>
                <![CDATA[This is a test type]]>
                </Description>
                <Members>
                <Member Name=""SintArray"" DataType=""SINT"" Dimension=""5"" Radix=""ASCII"" Hidden=""false"" ExternalAccess=""Read/Write"">
                <Description>
                <![CDATA[Test Sint Array]]>
                </Description>
                </Member>
                <Member Name=""IntArray"" DataType=""INT"" Dimension=""5"" Radix=""Octal"" Hidden=""false"" ExternalAccess=""Read/Write"">
                <Description>
                <![CDATA[Test Int Array]]>
                </Description>
                </Member>
                <Member Name=""DintArray"" DataType=""DINT"" Dimension=""5"" Radix=""Hex"" Hidden=""false"" ExternalAccess=""Read/Write"">
                <Description>
                <![CDATA[Test 
            Dint Array]]>
                </Description>
                </Member>
                <Member Name=""LintArray"" DataType=""LINT"" Dimension=""5"" Radix=""Date/Time"" Hidden=""false"" ExternalAccess=""Read/Write"">
                <Description>
                <![CDATA[Test Lint Array]]>
                </Description>
                </Member>
                <Member Name=""RealArray"" DataType=""REAL"" Dimension=""5"" Radix=""Exponential"" Hidden=""false"" ExternalAccess=""Read/Write"">
                <Description>
                <![CDATA[Test Real Array]]>
                </Description>
                </Member>
                <Member Name=""BoolArray"" DataType=""BOOL"" Dimension=""32"" Radix=""Binary"" Hidden=""false"" ExternalAccess=""Read/Write"">
                <Description>
                <![CDATA[Test Bool Array]]>
                </Description>
                </Member>
                </Members>
                </DataType>";
        }
    }
}