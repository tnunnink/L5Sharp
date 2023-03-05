using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Serialization.Data;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Tests.Serialization
{
    [TestFixture]
    public class DecoratedDataSerializerTests
    {
        private DecoratedDataSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new DecoratedDataSerializer();
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
        
        public Task Serialize_Dint_ShouldBeApproved()
        {
            var component = new DINT();

            var xml = _serializer.Serialize(component);

            return Verify(xml.ToString());
        }

        [Test]
        
        public Task Serialize_Timer_ShouldBeApproved()
        {
            var component = new TIMER();

            var xml = _serializer.Serialize(component);

            return Verify(xml.ToString());
        }

        [Test]
        
        public Task Serialize_RealArray_ShouldBeApproved()
        {
            var component = Logix.Array<REAL>(10);

            var xml = _serializer.Serialize(component);

            return Verify(xml.ToString());
        }

        [Test]
        
        public Task Serialize_CounterArray_ShouldBeApproved()
        {
            var component = Logix.Array<COUNTER>(5);

            var xml = _serializer.Serialize(component);

            return Verify(xml.ToString());
        }

        [Test]
        public void Deserialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Deserialize(null!)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Deserialize_GetDataValueXml_ShouldNotBeNull()
        {
            var element = XElement.Parse(GetDataValueXml());

            var component = _serializer.Deserialize(element);

            component.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_GetDataValueXml_ShouldBeExpected()
        {
            var element = XElement.Parse(GetDataValueXml());

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("DINT");
            component.Class.Should().Be(DataTypeClass.Atomic);
            component.Family.Should().Be(DataTypeFamily.None);
            component.As<DINT>().Should().Be(0);
        }

        [Test]
        public void Deserialize_GetArrayElementXml_ShouldNotBeNull()
        {
            var element = XElement.Parse(GetArrayElementXml());

            var component = _serializer.Deserialize(element);

            component.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_GetArrayElementXml_ShouldBeExpected()
        {
            var element = XElement.Parse(GetArrayElementXml());

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("REAL");
            component.As<ArrayType<ILogixType>>().Should().HaveCount(5);
            component.As<ArrayType<ILogixType>>().Dimensions.Should().BeEquivalentTo(new Dimensions(5));
        }

        [Test]
        public void Deserialize_GetStructureElementXml_ShouldNotBeNull()
        {
            var element = XElement.Parse(GetStructureElementXml());

            var component = _serializer.Deserialize(element);

            component.Should().NotBeNull();
        }


        private static string GetDataValueXml()
        {
            return "<DataValue DataType=\"DINT\" Radix=\"Decimal\" Value=\"0\"/>";
        }

        private static string GetArrayElementXml()
        {
            return @"<Array DataType=""REAL"" Dimensions=""5"" Radix=""Float"">
                <Element Index=""[0]"" Value=""0.0""/>
                <Element Index=""[1]"" Value=""0.0""/>
                <Element Index=""[2]"" Value=""0.0""/>
                <Element Index=""[3]"" Value=""0.0""/>
                <Element Index=""[4]"" Value=""0.0""/>
                </Array>";
        }

        private static string GetStructureElementXml()
        {
            return @"<Structure DataType=""SimpleType"">
                <DataValueMember Name=""BoolMember"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""SintMember"" DataType=""SINT"" Radix=""Hex"" Value=""16#00""/>
                <DataValueMember Name=""IntMember"" DataType=""INT"" Radix=""Octal"" Value=""8#000_016""/>
                <DataValueMember Name=""DintMember"" DataType=""DINT"" Radix=""ASCII"" Value=""'$00$00$00$01'""/>
                <DataValueMember Name=""LintMember"" DataType=""LINT"" Radix=""Decimal"" Value=""0""/>
                </Structure>";
        }
    }
}