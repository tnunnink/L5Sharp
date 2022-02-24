using System;
using System.Linq;
using System.Xml.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Serialization.Data;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;
using NUnit.Framework;

namespace L5Sharp.Serialization.Tests
{
    [TestFixture]
    public class ArrayMemberSerializerTests
    {
        private ArrayMemberSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new ArrayMemberSerializer();
        }

        [Test]
        public void Serialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Serialize(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Serialize_NonArrayType_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => _serializer.Serialize(new Member<IDataType>("Test", new Bool()))).Should()
                .Throw<ArgumentException>();
        }

        [Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var element = new Member<ArrayType<Bool>>("Test", new ArrayType<Bool>(new Dimensions(5)));

            var xml = _serializer.Serialize(element);

            xml.Should().NotBeNull();
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_ValueTypeArray_ShouldBeApproved()
        {
            var element = new Member<ArrayType<Bool>>("Test", new ArrayType<Bool>(new Dimensions(5)));

            var xml = _serializer.Serialize(element);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_StructureTypeArray_ShouldBeApproved()
        {
            var element = new Member<ArrayType<Timer>>("Test", new ArrayType<Timer>(new Dimensions(5)));

            var xml = _serializer.Serialize(element);

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
        public void Deserialize_ValueTypeArray_ShouldNotBeNull()
        {
            var element = XElement.Parse(GetValueArrayMemberXml()).Elements().First();

            var component = _serializer.Deserialize(element);

            component.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_ValueTypeArray_ShouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetValueArrayMemberXml()).Elements().First();

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("BoolArray");
            component.DataType.Should().BeOfType<ArrayType<IDataType>>();
            component.Dimensions.Should().Be(new Dimensions(5));
            component.Radix.Should().Be(Radix.Binary);
            component.Description.Should().BeEmpty();
        }

        [Test]
        public void Deserialize_SintMember_shouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetValueArrayMemberXml()).Elements().ToArray()[1];

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("SintArray");
            component.DataType.Should().BeOfType<ArrayType<IDataType>>();
            component.Dimensions.Should().Be(new Dimensions(5));
            component.Radix.Should().Be(Radix.Ascii);
            component.Description.Should().BeEmpty();
        }
        
        [Test]
        public void Deserialize_IntMember_ShouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetValueArrayMemberXml()).Elements().ToArray()[2];

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("IntArray");
            component.DataType.Should().BeOfType<ArrayType<IDataType>>();
            component.Dimensions.Should().Be(new Dimensions(5));
            component.Radix.Should().Be(Radix.Octal);
            component.Description.Should().BeEmpty();
            component.DataType.As<ArrayType<IDataType>>().Select(e => e.DataType).Should().AllBeOfType<Int>();
        }
        
        [Test]
        public void Deserialize_DintMember_ShouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetValueArrayMemberXml()).Elements().ToArray()[3];

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("DintArray");
            component.DataType.Should().BeOfType<ArrayType<IDataType>>();
            component.Dimensions.Should().Be(new Dimensions(5));
            component.Radix.Should().Be(Radix.Hex);
            component.Description.Should().BeEmpty();
            component.DataType.As<ArrayType<IDataType>>().Select(e => e.DataType).Should().AllBeOfType<Dint>();
        }
        
        [Test]
        public void Deserialize_LintMember_ShouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetValueArrayMemberXml()).Elements().ToArray()[4];

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("LintArray");
            component.DataType.Should().BeOfType<ArrayType<IDataType>>();
            component.Dimensions.Should().Be(new Dimensions(5));
            component.Radix.Should().Be(Radix.DateTime);
            component.Description.Should().BeEmpty();
            component.DataType.As<ArrayType<IDataType>>().Select(e => e.DataType).Should().AllBeOfType<Lint>();
        }
        
        [Test]
        public void Deserialize_RealMember_ShouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetValueArrayMemberXml()).Elements().ToArray()[5];

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("RealArray");
            component.DataType.Should().BeOfType<ArrayType<IDataType>>();
            component.Dimensions.Should().Be(new Dimensions(5));
            component.Radix.Should().Be(Radix.Exponential);
            component.Description.Should().BeEmpty();
            component.DataType.As<ArrayType<IDataType>>().Select(e => e.DataType).Should().AllBeOfType<Real>();
        }

        [Test]
        public void Deserialize_StructureTypeArray_ShouldNotBeNull()
        {
            var element = XElement.Parse(GetStructureArrayXml());

            var component = _serializer.Deserialize(element);

            component.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_StructureTypeArray_ShouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetStructureArrayXml());

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("Test");
            component.DataType.Should().BeOfType<ArrayType<IDataType>>();
            component.Dimensions.Should().Be(new Dimensions(5));
            component.Radix.Should().Be(Radix.Null);
            component.Description.Should().BeEmpty();
        }

        private static string GetValueArrayMemberXml()
        {
            return @"<Structure DataType=""ArrayType"">
                <ArrayMember Name=""BoolArray"" DataType=""BOOL"" Dimensions=""5"" Radix=""Binary"">
                <Element Index=""[0]"" Value=""0""/>
                <Element Index=""[1]"" Value=""0""/>
                <Element Index=""[2]"" Value=""0""/>
                <Element Index=""[3]"" Value=""0""/>
                <Element Index=""[4]"" Value=""0""/>
                </ArrayMember>
                <ArrayMember Name=""SintArray"" DataType=""SINT"" Dimensions=""5"" Radix=""ASCII"">
                <Element Index=""[0]"" Value=""'$00'""/>
                <Element Index=""[1]"" Value=""'$00'""/>
                <Element Index=""[2]"" Value=""'$00'""/>
                <Element Index=""[3]"" Value=""'$00'""/>
                <Element Index=""[4]"" Value=""'$00'""/>
                </ArrayMember>
                <ArrayMember Name=""IntArray"" DataType=""INT"" Dimensions=""5"" Radix=""Octal"">
                <Element Index=""[0]"" Value=""8#000_000""/>
                <Element Index=""[1]"" Value=""8#000_000""/>
                <Element Index=""[2]"" Value=""8#000_000""/>
                <Element Index=""[3]"" Value=""8#000_000""/>
                <Element Index=""[4]"" Value=""8#000_000""/>
                </ArrayMember>
                <ArrayMember Name=""DintArray"" DataType=""DINT"" Dimensions=""5"" Radix=""Hex"">
                <Element Index=""[0]"" Value=""16#0000_0000""/>
                <Element Index=""[1]"" Value=""16#0000_0000""/>
                <Element Index=""[2]"" Value=""16#0000_0000""/>
                <Element Index=""[3]"" Value=""16#0000_0000""/>
                <Element Index=""[4]"" Value=""16#0000_0000""/>
                </ArrayMember>
                <ArrayMember Name=""LintArray"" DataType=""LINT"" Dimensions=""5"" Radix=""Date/Time"">
                <Element Index=""[0]"" Value=""DT#1970-01-01-00:00:00.000_000Z""/>
                <Element Index=""[1]"" Value=""DT#1970-01-01-00:00:00.000_000Z""/>
                <Element Index=""[2]"" Value=""DT#1970-01-01-00:00:00.000_000Z""/>
                <Element Index=""[3]"" Value=""DT#1970-01-01-00:00:00.000_000Z""/>
                <Element Index=""[4]"" Value=""DT#1970-01-01-00:00:00.000_000Z""/>
                </ArrayMember>
                <ArrayMember Name=""RealArray"" DataType=""REAL"" Dimensions=""5"" Radix=""Exponential"">
                <Element Index=""[0]"" Value=""0.00000000e+000""/>
                <Element Index=""[1]"" Value=""0.00000000e+000""/>
                <Element Index=""[2]"" Value=""0.00000000e+000""/>
                <Element Index=""[3]"" Value=""0.00000000e+000""/>
                <Element Index=""[4]"" Value=""0.00000000e+000""/>
                </ArrayMember>
                </Structure>";
        }

        private static string GetStructureArrayXml()
        {
            return @"<ArrayMember Name=""Test"" DataType=""TIMER"" Dimensions=""5"">
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
                </ArrayMember>";
        }
    }
}