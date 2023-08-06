using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Serialization.Data;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Tests.Serialization
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
            FluentActions.Invoking(() => _serializer.Serialize(new Member("Test", new BOOL()))).Should()
                .Throw<InvalidCastException>();
        }

        [Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var element = new Member("Test", Logix.Array<BOOL>(5));

            var xml = _serializer.Serialize(element);

            xml.Should().NotBeNull();
        }

        [Test]
        
        public Task Serialize_ValueTypeArray_ShouldBeApproved()
        {
            var element = new Member("Test", Logix.Array<BOOL>(5));

            var xml = _serializer.Serialize(element);

            return Verify(xml.ToString());
        }

        [Test]
        
        public Task Serialize_StructureTypeArray_ShouldBeApproved()
        {
            var element = new Member("Test", Logix.Array<TIMER>(5));

            var xml = _serializer.Serialize(element);

            return Verify(xml.ToString());
        }

        [Test]
        public void Deserialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Deserialize(null!)).Should().Throw<ArgumentException>();
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
            component.DataType.Should().BeOfType<ArrayType<ILogixType>>();
        }

        [Test]
        public void Deserialize_SintMember_shouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetValueArrayMemberXml()).Elements().ToArray()[1];

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("SintArray");

            var array = component.DataType.As<ArrayType<ILogixType>>();
            array.Should().NotBeNull();
            
            array.Should().BeOfType<ArrayType<ILogixType>>();
            array.Dimensions.Should().Be(new Dimensions(5));
        }
        
        [Test]
        public void Deserialize_IntMember_ShouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetValueArrayMemberXml()).Elements().ToArray()[2];

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("IntArray");
            component.DataType.Should().BeOfType<ArrayType<ILogixType>>();
            component.DataType.As<ArrayType<ILogixType>>().Select(e => e).Should().AllBeOfType<INT>();
        }
        
        [Test]
        public void Deserialize_DintMember_ShouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetValueArrayMemberXml()).Elements().ToArray()[3];

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("DintArray");
            component.DataType.Should().BeOfType<ArrayType<ILogixType>>();
            component.DataType.As<ArrayType<ILogixType>>().Select(e => e).Should().AllBeOfType<DINT>();
        }
        
        [Test]
        public void Deserialize_LintMember_ShouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetValueArrayMemberXml()).Elements().ToArray()[4];

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("LintArray");
            component.DataType.Should().BeOfType<ArrayType<ILogixType>>();
            component.DataType.As<ArrayType<ILogixType>>().Select(e => e).Should().AllBeOfType<LINT>();
        }
        
        [Test]
        public void Deserialize_RealMember_ShouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetValueArrayMemberXml()).Elements().ToArray()[5];

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("RealArray");
            component.DataType.Should().BeOfType<ArrayType<ILogixType>>();
            component.DataType.As<ArrayType<ILogixType>>().Select(e => e).Should().AllBeOfType<REAL>();
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
            component.DataType.Should().BeOfType<ArrayType<ILogixType>>();
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