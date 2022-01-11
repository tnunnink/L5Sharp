using System;
using System.Linq;
using System.Xml.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Serialization;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Internal.Tests.Serialization
{
    [TestFixture]
    public class ArrayElementSerializerTests
    {
        [Test]
        public void Serialize_Null_ShouldThrowArgumentNullException()
        {
            var serializer = new ArrayElementSerializer();

            FluentActions.Invoking(() => serializer.Serialize(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var element = new Member<Bool>("[1]", new Bool());
            var serializer = new ArrayElementSerializer();

            var xml = serializer.Serialize(element);

            xml.Should().NotBeNull();
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_ValueValueTypeMember_ShouldBeApproved()
        {
            var element = new Member<Bool>("[1]", new Bool());
            var serializer = new ArrayElementSerializer();

            var xml = serializer.Serialize(element);

            Approvals.VerifyXml(xml.ToString());
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_ValueStructureTypeMember_ShouldBeApproved()
        {
            var element = new Member<Timer>("[1]", new Timer());
            var serializer = new ArrayElementSerializer();

            var xml = serializer.Serialize(element);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        public void Deserialize_Null_ShouldThrowArgumentNullException()
        {
            var serializer = new ArrayElementSerializer();

            FluentActions.Invoking(() => serializer.Deserialize(null!)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Deserialize_InvalidElementName_ShouldThrowArgumentException()
        {
            const string xml = @"<Invalid></Invalid>";
            var element = XElement.Parse(xml);
            var serializer = new ArrayElementSerializer();

            FluentActions.Invoking(() => serializer.Deserialize(element)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Deserialize_ValidValueArrayElement_ShouldNotBeNull()
        {
            var element = XElement.Parse(GetValueArrayXml()).Elements().First();
            var serializer = new ArrayElementSerializer();

            var component = serializer.Deserialize(element);

            component.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_ValidValueArrayElement_ShouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetValueArrayXml()).Elements().First();
            var serializer = new ArrayElementSerializer();

            var component = serializer.Deserialize(element);

            component.Name.Should().Be("[0]");
            component.DataType.Should().BeOfType<Real>();
            ((Real)component.DataType).Value.Should().Be(0.0f);
            component.Dimensions.Should().Be(Dimensions.Empty);
            component.Radix.Should().Be(Radix.Float);
            component.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            component.Description.Should().BeEmpty();
        }
        
        [Test]
        public void Deserialize_ValidStructureArrayElement_ShouldNotBeNull()
        {
            var element = XElement.Parse(GetStructureArrayXml()).Elements().First();
            var serializer = new ArrayElementSerializer();

            var component = serializer.Deserialize(element);

            component.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_ValidStructureArrayElement_ShouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetStructureArrayXml()).Elements().First();
            var serializer = new ArrayElementSerializer();

            var component = serializer.Deserialize(element);

            component.Name.Should().Be("[0]");
            component.DataType.Should().BeOfType<StructureType>();
            ((IComplexType)component.DataType).Members.Should().HaveCount(5);
            component.Dimensions.Should().Be(Dimensions.Empty);
            component.Radix.Should().Be(Radix.Null);
            component.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            component.Description.Should().BeEmpty();
        }

        private static string GetValueArrayXml()
        {
            return @"<Array DataType=""REAL"" Dimensions=""5"" Radix=""Float"">
                <Element Index=""[0]"" Value=""0.0""/>
                <Element Index=""[1]"" Value=""1.1""/>
                <Element Index=""[2]"" Value=""2.2""/>
                <Element Index=""[3]"" Value=""3.3""/>
                <Element Index=""[4]"" Value=""4.4""/>
                </Array>";
        }
        
        private static string GetStructureArrayXml()
        {
            return @"<Array DataType=""TIMER"" Dimensions=""5"">
                <Element Index=""[0]"">
                <Structure DataType=""TIMER"">
                <DataValueMember Name=""PRE"" DataType=""DINT"" Radix=""Decimal"" Value=""0""/>
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
                </Array>";
        }
    }
}