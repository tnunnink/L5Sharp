using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Serialization.Data;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Tests.Serialization
{
    [TestFixture]
    public class ArraySerializerTests
    {
        private ArraySerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new ArraySerializer();
        }
        
        [Test]
        public void Serialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Serialize(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var component = Logix.Array<BOOL>(10);
            
            var xml = _serializer.Serialize(component);

            xml.Should().NotBeNull();
        }

        [Test]
        
        public Task Serialize_ValueTypeArray_ShouldBeApproved()
        {
            var element = Logix.Array<BOOL>(10);
            
            var xml = _serializer.Serialize(element);

            return Verify(xml.ToString());
        }
        
        [Test]
        
        public Task Serialize_StructureTypeArray_ShouldBeApproved()
        {
            var element = Logix.Array<TIMER>(10);
            
            var xml = _serializer.Serialize(element);

            return Verify(xml.ToString());
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
            var element = XElement.Parse(GetValueArrayXml());
            
            var component = _serializer.Deserialize(element);

            component.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_ValueTypeArray_ShouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetValueArrayXml());

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("REAL");
            component.Dimensions.Should().Be(new Dimensions(5));
            component.Class.Should().Be(DataTypeClass.Atomic);
            component.Family.Should().Be(DataTypeFamily.None);
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

            component.Name.Should().Be("TIMER");
            component.Dimensions.Should().Be(new Dimensions(5));
            component.Class.Should().Be(DataTypeClass.Unknown);
            component.Family.Should().Be(DataTypeFamily.None);
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