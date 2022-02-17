using System;
using System.Collections.Generic;
using System.Xml.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Factories;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Serialization.Tests
{
    [TestFixture]
    public class UserDefinedSerializerTests
    {
        private UserDefinedSerializer _serializer;


        [SetUp]
        public void Setup()
        {
            var context = new LogixContext(TestFileTests.L5X);
            _serializer = new UserDefinedSerializer(context);
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

            FluentActions.Invoking(() => _serializer.Deserialize(element)).Should().Throw<ArgumentException>();
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

            var boolMember = component.Members.Get("BoolMember");
            boolMember.Should().NotBeNull();
            boolMember?.Name.Should().Be("BoolMember");
            boolMember?.DataType.Should().BeOfType<Bool>();
            boolMember?.Dimensions.Should().Be(Dimensions.Empty);
            boolMember?.Radix.Should().Be(Radix.Hex);
            boolMember?.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            boolMember?.Description.Should().Be("Test Boolean update");
        }
        
        [Test]
        public void Deserialize_SimpleType_ShouldHaveExpectedSintMember()
        {
            var element = XElement.Parse(GetSimpleTypeXml());

            var component = _serializer.Deserialize(element);

            var member = component.Members.Get("SintMember");
            member.Should().NotBeNull();
            member?.Name.Should().Be("SintMember");
            member?.DataType.Should().BeOfType<Sint>();
            member?.Dimensions.Should().Be(Dimensions.Empty);
            member?.Radix.Should().Be(Radix.Decimal);
            member?.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            member?.Description.Should().Be("Test Sint");
        }
        
        [Test]
        public void Deserialize_SimpleType_ShouldHaveExpectedIntMember()
        {
            var element = XElement.Parse(GetSimpleTypeXml());

            var component = _serializer.Deserialize(element);

            var member = component.Members.Get("IntMember");
            member.Should().NotBeNull();
            member?.Name.Should().Be("IntMember");
            member?.DataType.Should().BeOfType<Int>();
            member?.Dimensions.Should().Be(Dimensions.Empty);
            member?.Radix.Should().Be(Radix.Decimal);
            member?.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            member?.Description.Should().Be("Test Int");
        }
        
        [Test]
        public void Deserialize_SimpleType_ShouldHaveExpectedDintMember()
        {
            var element = XElement.Parse(GetSimpleTypeXml());

            var component = _serializer.Deserialize(element);

            var member = component.Members.Get("DintMember");
            member.Should().NotBeNull();
            member?.Name.Should().Be("DintMember");
            member?.DataType.Should().BeOfType<Dint>();
            member?.Dimensions.Should().Be(Dimensions.Empty);
            member?.Radix.Should().Be(Radix.Octal);
            member?.ExternalAccess.Should().Be(ExternalAccess.None);
            member?.Description.Should().Be("Test Dint comment");
        }
        
        [Test]
        public void Deserialize_SimpleType_ShouldHaveExpectedLintMember()
        {
            var element = XElement.Parse(GetSimpleTypeXml());

            var component = _serializer.Deserialize(element);

            var member = component.Members.Get("LintMember");
            member.Should().NotBeNull();
            member?.Name.Should().Be("LintMember");
            member?.DataType.Should().BeOfType<Lint>();
            member?.Dimensions.Should().Be(Dimensions.Empty);
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