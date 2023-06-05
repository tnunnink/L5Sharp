using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Tests.Types
{
    [TestFixture]
    public class ArrayTypeTests
    {
        private static readonly DINT[] DintArray = { new(100), new(200), new(300) };

        private static readonly TIMER[] TimerArray =
            { new() { PRE = 1000 }, new() { PRE = 2000 }, new() { PRE = 3000 } };

        [Test]
        public void New_InitializedArray_ShouldNotBeNull()
        {
            var array = new ArrayType<DINT>(DintArray);

            array.Should().NotBeNull();
        }

        [Test]
        public void New_InitializedArray_ShouldHaveExpectedProperties()
        {
            var array = new ArrayType<DINT>(DintArray);

            array.Dimensions.Length.Should().Be(3);
            array.Name.Should().Be("DINT");
            array.Family.Should().Be(DataTypeFamily.None);
            array.Class.Should().Be(DataTypeClass.Atomic);
        }

        [Test]
        public void New_NullElements_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new ArrayType<LogixType>(((LogixType[])null)!)).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void New_OutOfRangeLength_ShouldThrowArgumentOutOfRangeException()
        {
            FluentActions.Invoking(() => new ArrayType<LogixType>(new LogixType[1000000])).Should()
                .Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void New_OneDimensional_ShouldHaveExpectedLength()
        {
            var array = new DINT[] { 1, 2, 3, 4 };

            var type = new ArrayType<DINT>(array);

            type.Dimensions.Length.Should().Be(4);
        }

        [Test]
        public void GetIndex_OneDimensionalValidIndex_ShouldReturnExpected()
        {
            var array = new DINT[] { 1, 2, 3, 4 };

            var type = new ArrayType<DINT>(array);

            var index = type[2];

            index.Should().Be(3);
        }

        [Test]
        public void GetIndex_OneDimensionalInValidIndex_ShouldThrowException()
        {
            var array = new DINT[] { 1, 2, 3, 4 };

            var type = new ArrayType<DINT>(array);

            FluentActions.Invoking(() => type[5]).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void SetIndex_OneDimensionalValidIndex_ShouldReturnExpected()
        {
            var array = new DINT[] { 1, 2, 3, 4 };
            var type = new ArrayType<DINT>(array);

            type[2] = 100;

            var result = type[2];
            result.Should().Be(100);
        }

        [Test]
        public void SetIndex_OneDimensionalInValidIndex_ShouldThrowException()
        {
            var array = new DINT[] { 1, 2, 3, 4 };

            var type = new ArrayType<DINT>(array);

            FluentActions.Invoking(() => type[5] = 1).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public Task Serialize_SimpleOneDimensional_ShouldBeVerified()
        {
            var type = new ArrayType<DINT>(DintArray);

            var xml = type.Serialize().ToString();

            return Verify(xml);
        }

        [Test]
        public void New_TwoDimensional_ShouldHaveExpectedLength()
        {
            var array = new DINT[,]
            {
                { 1, 2 },
                { 3, 4 },
                { 5, 6 },
                { 7, 8 }
            };

            var type = new ArrayType<DINT>(array);

            type.Dimensions.Length.Should().Be(8);
        }

        [Test]
        public void GetIndex_TwoDimensionalValidIndex_ShouldReturnExpected()
        {
            var array = new DINT[,]
            {
                { 1, 2 },
                { 3, 4 },
                { 5, 6 },
                { 7, 8 }
            };

            var type = new ArrayType<DINT>(array);

            var index = type[2, 1];

            index.Should().Be(6);
        }

        [Test]
        public void GetIndex_TwoDimensionalInValidIndex_ShouldThrowException()
        {
            var array = new DINT[,]
            {
                { 1, 2 },
                { 3, 4 },
                { 5, 6 },
                { 7, 8 }
            };

            var type = new ArrayType<DINT>(array);

            FluentActions.Invoking(() => type[1, 2]).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public Task Serialize_SimpleTwoDimensional_ShouldBeVerified()
        {
            var array = new DINT[,]
            {
                { 1, 2 },
                { 3, 4 },
                { 5, 6 },
                { 7, 8 }
            };

            var type = new ArrayType<DINT>(array);

            var xml = type.Serialize().ToString();

            return Verify(xml);
        }

        [Test]
        public void New_ThreeDimensional_ShouldHaveExpectedLength()
        {
            var array = new DINT[,,]
            {
                { { 1, 2, 3 }, { 4, 5, 6 } },
                { { 7, 8, 9 }, { 10, 11, 12 } }
            };

            var type = new ArrayType<DINT>(array);

            type.Dimensions.Length.Should().Be(12);
        }

        [Test]
        public void GetIndex_ThreeDimensionalValidIndex_ShouldReturnExpected()
        {
            var array = new DINT[,,]
            {
                { { 1, 2, 3 }, { 4, 5, 6 } },
                { { 7, 8, 9 }, { 10, 11, 12 } }
            };

            var type = new ArrayType<DINT>(array);

            var index = type[0, 1, 2];

            index.Should().Be(6);
        }

        [Test]
        public void GetIndex_ThreeDimensionalInValidIndex_ShouldThrowException()
        {
            var array = new DINT[,,]
            {
                { { 1, 2, 3 }, { 4, 5, 6 } },
                { { 7, 8, 9 }, { 10, 11, 12 } }
            };

            var type = new ArrayType<DINT>(array);

            FluentActions.Invoking(() => type[2, 1, 2]).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public Task Serialize_SimpleThreeDimensional_ShouldBeVerified()
        {
            var array = new DINT[,,]
            {
                { { 1, 2, 3 }, { 4, 5, 6 } },
                { { 7, 8, 9 }, { 10, 11, 12 } }
            };

            var type = new ArrayType<DINT>(array);

            var xml = type.Serialize().ToString();

            return Verify(xml);
        }

        [Test]
        public void Members_WhenCalled_ShouldNotBeEmpty()
        {
            var array = new ArrayType<DINT>(DintArray);

            var elements = array.Members.ToArray();

            elements.Should().NotBeEmpty();
            elements.Select(e => e.DataType).Should().AllBeOfType<DINT>();
        }

        [Test]
        public void ToString_WhenCalled_ShouldBeExpected()
        {
            var array = new ArrayType<DINT>(DintArray);

            var name = array.ToString();

            name.Should().Be("DINT");
        }

        [Test]
        public void GetEnumerator_WhenCalled_ShouldBeNull()
        {
            var array = new ArrayType<DINT>(DintArray);

            using var enumerator = array.GetEnumerator();

            enumerator.Should().NotBeNull();
        }

        [Test]
        public void New_DimensionsAndCollection_ValidArgument_ShouldBeExpected()
        {
            var array = ArrayType.New<TIMER>(10);

            array.Should().NotBeNull();
            array.Should().NotBeEmpty();
            array.Dimensions.Length.Should().Be(10);
        }

        [Test]
        public void New_ComplexTypeArray_ShouldBeExpectedValues()
        {
            var array = new ArrayType<TIMER>(TimerArray);

            array.Should().NotBeNull();
            array.Name.Should().Be("TIMER");
            array.Dimensions.Length.Should().Be(3);
            array.Class.Should().Be(DataTypeClass.Predefined);
            array.Family.Should().Be(DataTypeFamily.None);
        }

        [Test]
        public void GetIndex_ComplexTypeArray_ShouldReturnExpectedValue()
        {
            var array = new ArrayType<TIMER>(TimerArray);

            var timer = array[1];

            timer.Should().NotBeNull();
            timer.PRE.Should().Be(2000);
        }

        [Test]
        public Task Serialize_ComplexTypeArray_ShouldBeVerified()
        {
            var array = new ArrayType<TIMER>(TimerArray);

            var xml = array.Serialize().ToString();
            
            return Verify(xml);
        }

        [Test]
        public void Deserialize_ValueTypeArray_ShouldNotBeNull()
        {
            var element = XElement.Parse(GetValueArrayXml());
            
            var array = new ArrayType<LogixType>(element);

            array.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_ValueTypeArray_ShouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetValueArrayXml());

            var array = new ArrayType<LogixType>(element);

            array.Name.Should().Be("REAL");
            array.Dimensions.Should().Be(new Dimensions(5));
            array.Class.Should().Be(DataTypeClass.Atomic);
            array.Family.Should().Be(DataTypeFamily.None);

            array[0].As<REAL>().Should().Be(0.0f);
            array[1].As<REAL>().Should().Be(1.1f);
            array[2].As<REAL>().Should().Be(2.2f);
            array[3].As<REAL>().Should().Be(3.3f);
            array[4].As<REAL>().Should().Be(4.4f);
        }
        
        [Test]
        public void Deserialize_StructureTypeArray_ShouldNotBeNull()
        {
            var element = XElement.Parse(GetStructureArrayXml());
            
            var array = new ArrayType<LogixType>(element);

            array.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_StructureTypeArray_ShouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetStructureArrayXml());
            
            var array = new ArrayType<LogixType>(element);

            array.Name.Should().Be("TIMER");
            array.Dimensions.Should().Be(new Dimensions(5));
            array.Class.Should().Be(DataTypeClass.Predefined);
            array.Family.Should().Be(DataTypeFamily.None);

            array[1].As<TIMER>().PRE.Should().Be(1000);
            array[1].As<TIMER>().EN.Should().Be(true);
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
                <DataValueMember Name=""PRE"" DataType=""DINT"" Radix=""Decimal"" Value=""1000""/>
                <DataValueMember Name=""ACC"" DataType=""DINT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""EN"" DataType=""BOOL"" Value=""1""/>
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