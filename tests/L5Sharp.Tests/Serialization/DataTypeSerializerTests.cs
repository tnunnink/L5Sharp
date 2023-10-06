using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Serialization;
using NUnit.Framework;
using VerifyNUnit;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.Serialization
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
            var type = new DataType();

            var xml = _serializer.Serialize(type);

            xml.Should().NotBeNull();
        }

        [Test]
        
        public void Serialize_BasicType_ShouldBeApproved()
        {
            var type = new DataType
            {
                Name = "Test", Description = "This is a test user defined type"
            };

            var xml = _serializer.Serialize(type);

            Verifier.Verify(xml.ToString());
        }

        [Test]
        public Task Serialize_AtomicType_ShouldBeApproved()
        {
            var type = new DataType
            {
                Name = "Test",
                Description = "This is a test user defined type",
                Members = new List<DataTypeMember>
                {
                    new()
                    {
                        Name = "Member01", DataType = "BOOL", Radix = Radix.Binary,
                        ExternalAccess = ExternalAccess.ReadOnly, Description = "This is a new test member"
                    },
                    new()
                    {
                        Name = "Member02", DataType = "INT", Radix = Radix.Octal,
                        ExternalAccess = ExternalAccess.None, Description = "This is a test member"
                    },
                    new()
                    {
                        Name = "Member03", DataType = "DINT", Radix = Radix.Decimal,
                        ExternalAccess = ExternalAccess.ReadOnly, Description = "This is a test member",
                    },
                    new()
                    {
                        Name = "Member04", DataType = "LINT", Radix = Radix.Hex,
                        Description = "This is a test member"
                    },
                    new() { Name = "Member04", DataType = "SINT", Radix = Radix.Ascii },
                    new() { Name = "Member05", DataType = "REAL" }
                }
            };

            var xml = _serializer.Serialize(type);
            
            return Verifier.VerifyXml(xml.ToString());
        }

        [Test]
        public void Deserialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Deserialize(null!)).Should().Throw<
                ArgumentException>();
        }
        
        [Test]
        public void Deserialize_SimpleType_ShouldNotBeNull()
        {
            var element = XElement.Parse(GetSimpleTypeXml());
            var component = _serializer.Deserialize(element);
            component.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_SimpleType_ShouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetSimpleTypeXml());

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("SimpleType");
            component.Family.Should().Be(DataTypeFamily.None);
            component.Class.Should().Be(DataTypeClass.User);
            component.Members.Should().HaveCount(5);
            component.Description.Should()
                .Be("This is a test data type that contains simple atomic types with an updated description");
        }

        [Test]
        public void Deserialize_SimpleType_ShouldHaveExpectedBoolMember()
        {
            var element = XElement.Parse(GetSimpleTypeXml());

            var component = _serializer.Deserialize(element);

            var boolMember = component.Members.FirstOrDefault(m => m.Name == "BoolMember");
            boolMember.Should().NotBeNull();
            boolMember?.Name.Should().Be("BoolMember");
            boolMember?.DataType.Should().Be("BIT");
            boolMember?.Dimension.Should().Be(Dimensions.Empty);
            boolMember?.Radix.Should().Be(Radix.Hex);
            boolMember?.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            boolMember?.Description.Should().Be("Test Boolean update");
        }

        [Test]
        public void Deserialize_SimpleType_ShouldHaveExpectedSintMember()
        {
            var element = XElement.Parse(GetSimpleTypeXml());

            var component = _serializer.Deserialize(element);

            var member = component.Members.FirstOrDefault(m => m.Name == "SintMember");
            member.Should().NotBeNull();
            member?.Name.Should().Be("SintMember");
            member?.DataType.Should().Be("SINT");
            member?.Dimension.Should().Be(Dimensions.Empty);
            member?.Radix.Should().Be(Radix.Decimal);
            member?.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            member?.Description.Should().Be("Test Sint");
        }

        [Test]
        public void Deserialize_SimpleType_ShouldHaveExpectedIntMember()
        {
            var element = XElement.Parse(GetSimpleTypeXml());

            var component = _serializer.Deserialize(element);

            var member = component.Members.FirstOrDefault(m => m.Name == "IntMember");
            member.Should().NotBeNull();
            member?.Name.Should().Be("IntMember");
            member?.DataType.Should().Be("INT");
            member?.Dimension.Should().Be(Dimensions.Empty);
            member?.Radix.Should().Be(Radix.Decimal);
            member?.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            member?.Description.Should().Be("Test Int");
        }

        [Test]
        public void Deserialize_SimpleType_ShouldHaveExpectedDintMember()
        {
            var element = XElement.Parse(GetSimpleTypeXml());

            var component = _serializer.Deserialize(element);

            var member = component.Members.FirstOrDefault(m => m.Name == "DintMember");
            member.Should().NotBeNull();
            member?.Name.Should().Be("DintMember");
            member?.DataType.Should().Be("DINT");
            member?.Dimension.Should().Be(Dimensions.Empty);
            member?.Radix.Should().Be(Radix.Octal);
            member?.ExternalAccess.Should().Be(ExternalAccess.None);
            member?.Description.Should().Be("Test Dint comment");
        }

        [Test]
        public void Deserialize_SimpleType_ShouldHaveExpectedLintMember()
        {
            var element = XElement.Parse(GetSimpleTypeXml());

            var component = _serializer.Deserialize(element);

            var member = component.Members.FirstOrDefault(m => m.Name == "LintMember");
            member.Should().NotBeNull();
            member?.Name.Should().Be("LintMember");
            member?.DataType.Should().Be("LINT");
            member?.Dimension.Should().Be(Dimensions.Empty);
            member?.Radix.Should().Be(Radix.Decimal);
            member?.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            member?.Description.Should().Be("Test Lint");
        }


        private static string GetSimpleTypeXml()
        {
            return @"<DataType Name=""SimpleType"" Family=""NoFamily"" Class=""User"">
                <Description>
                    <![CDATA[This is a test data type that contains simple atomic types with an updated description]]>
                </Description>
                <Members>
                    <Member Name=""ZZZZZZZZZZSimpleType0"" DataType=""SINT"" Dimension=""0"" Radix=""Decimal"" Hidden=""true""
                            ExternalAccess=""Read/Write""/>
                    <Member Name=""BoolMember"" DataType=""BIT"" Dimension=""0"" Radix=""Hex"" Hidden=""false""
                            Target=""ZZZZZZZZZZSimpleType0"" BitNumber=""0"" ExternalAccess=""Read/Write"">
                        <Description>
                            <![CDATA[Test Boolean update]]>
                        </Description>
                    </Member>
                    <Member Name=""SintMember"" DataType=""SINT"" Dimension=""0"" Radix=""Decimal"" Hidden=""false""
                            ExternalAccess=""Read/Write"">
                        <Description>
                            <![CDATA[Test Sint]]>
                        </Description>
                    </Member>
                    <Member Name=""IntMember"" DataType=""INT"" Dimension=""0"" Radix=""Decimal"" Hidden=""false""
                            ExternalAccess=""Read/Write"">
                        <Description>
                            <![CDATA[Test Int]]>
                        </Description>
                    </Member>
                    <Member Name=""ZZZZZZZZZZSimpleType4"" DataType=""SINT"" Dimension=""0"" Radix=""Decimal"" Hidden=""true""
                            ExternalAccess=""None""/>
                    <Member Name=""DintMember"" DataType=""DINT"" Dimension=""0"" Radix=""Octal"" Hidden=""false""
                            Target=""ZZZZZZZZZZSimpleType4"" BitNumber=""0"" ExternalAccess=""None"">
                        <Description>
                            <![CDATA[Test Dint comment]]>
                        </Description>
                    </Member>
                    <Member Name=""LintMember"" DataType=""LINT"" Dimension=""0"" Radix=""Decimal"" Hidden=""false""
                            ExternalAccess=""Read/Write"">
                        <Description>
                            <![CDATA[Test Lint]]>
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
                <![CDATA[Test BOOL Array]]>
                </Description>
                </Member>
                </Members>
                </DataType>";
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
    }
}