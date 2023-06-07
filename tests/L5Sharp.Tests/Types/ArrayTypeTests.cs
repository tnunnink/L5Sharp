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
        [Test]
        public void Constructor_NullElements_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new ArrayType(((LogixType[])null)!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Constructor_OutOfRangeLength_ShouldThrowArgumentOutOfRangeException()
        {
            FluentActions.Invoking(() => new ArrayType(new LogixType[1000000])).Should()
                .Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Constructor_Dimensions_ShouldHaveExpected()
        {
            var array = new ArrayType(5);

            array.Should().NotBeNull();
            array.Name.Should().Be(string.Empty);
            array.Dimensions.Length.Should().Be(5);
            array.Radix.Should().Be(Radix.Null);
            array.Family.Should().Be(DataTypeFamily.None);
            array.Class.Should().Be(DataTypeClass.Unknown);

            array[0].Should().Be(LogixType.Null);
            array[1].Should().Be(LogixType.Null);
            array[2].Should().Be(LogixType.Null);
            array[3].Should().Be(LogixType.Null);
            array[4].Should().Be(LogixType.Null);
        }

        [Test]
        public void Constructor_ComplexType_ShouldBeExpectedValues()
        {
            var array = new ArrayType<TIMER>(new TIMER[]
                { new() { PRE = 1000 }, new() { PRE = 2000 }, new() { PRE = 3000 } });

            array.Should().NotBeNull();
            array.Name.Should().Be("TIMER");
            array.Dimensions.Length.Should().Be(3);
            array.Class.Should().Be(DataTypeClass.Predefined);
            array.Family.Should().Be(DataTypeFamily.None);
        }

        [Test]
        public void Constructor_OnDimensional_ShouldHaveExpectedProperties()
        {
            var array = new ArrayType(new DINT[] { new(100), new(200), new(300) });

            array.Should().NotBeNull();
            array.Name.Should().Be("DINT");
            array.Dimensions.Length.Should().Be(3);
            array.Radix.Should().Be(Radix.Decimal);
            array.Family.Should().Be(DataTypeFamily.None);
            array.Class.Should().Be(DataTypeClass.Atomic);

            array[0].As<DINT>().Should().Be(100);
            array[1].As<DINT>().Should().Be(200);
            array[2].As<DINT>().Should().Be(300);
        }

        [Test]
        public void Constructor_TwoDimensional_ShouldHaveExpectedLength()
        {
            var array = new DINT[,]
            {
                { 1, 2 },
                { 3, 4 },
                { 5, 6 },
                { 7, 8 }
            };

            var type = new ArrayType(array);

            type.Dimensions.Length.Should().Be(8);
        }

        [Test]
        public void Constructor_ThreeDimensional_ShouldHaveExpectedLength()
        {
            var array = new DINT[,,]
            {
                { { 1, 2, 3 }, { 4, 5, 6 } },
                { { 7, 8, 9 }, { 10, 11, 12 } }
            };

            var type = new ArrayType(array);

            type.Dimensions.Length.Should().Be(12);
        }

        [Test]
        public void New_ValidArguments_ShouldBeExpected()
        {
            var array = ArrayType.New<TIMER>(10);

            array.Should().NotBeNull();
            array.Should<TIMER>().NotBeEmpty();
            array.Dimensions.Length.Should().Be(10);

            array.Should<TIMER>().AllBeOfType<TIMER>();
        }

        [Test]
        public void GetIndex_OneDimensionalValidIndex_ShouldReturnExpected()
        {
            var array = new DINT[] { 1, 2, 3, 4 };

            var type = new ArrayType(array);

            var index = type[2];

            index.As<DINT>().Should().Be(3);
        }

        [Test]
        public void GetIndex_OneDimensionalInValidIndex_ShouldThrowException()
        {
            var array = new DINT[] { 1, 2, 3, 4 };

            var type = new ArrayType(array);

            FluentActions.Invoking(() => type[5]).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void SetIndex_OneDimensionalValidIndex_ShouldReturnExpected()
        {
            var type = new ArrayType(new DINT[] { 1, 2, 3, 4 });

            type[2] = 100;

            type[2].As<DINT>().Should().Be(100);
        }

        [Test]
        public void SetIndex_OneDimensionalInValidIndex_ShouldThrowException()
        {
            var array = new DINT[] { 1, 2, 3, 4 };

            var type = new ArrayType(array);

            FluentActions.Invoking(() => type[5] = 1).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void SetIndex_InvalidType_ShouldThrowArgumentException()
        {
            var array = new ArrayType(new DINT[] { 1, 2, 3, 4 });

            FluentActions.Invoking(() => array[0] = new TIMER()).Should().Throw<ArgumentException>();
        }

        [Test]
        public void SetIndex_ComplexTypeArray_ShouldThrowArgumentException()
        {
            var array = new ArrayType(new TIMER[] { new(), new(), new(), new() });

            array[0] = new TIMER { PRE = 5000 };

            array[0].To<TIMER>().PRE.Should().Be(new DINT(5000));
        }

        [Test]
        public void SetIndex_UninitializedArrayToAtomicValue_ShouldInitializeArrayValues()
        {
            var array = new ArrayType(10);

            array[0] = 10;

            array.Name.Should().Be(nameof(DINT));
            array.Class.Should().Be(DataTypeClass.Atomic);
            array.Radix.Should().Be(Radix.Decimal);
            array[0].As<DINT>().Should().Be(10);
        }

        [Test]
        public void SetIndex_UninitializedArrayToStructureType_ShouldInitializeArrayValues()
        {
            var array = new ArrayType(10);

            array[1] = new TIMER();

            array.Name.Should().Be(nameof(TIMER));
            array.Dimensions.Length.Should().Be(10);
            array.Class.Should().Be(DataTypeClass.Predefined);
            array.Radix.Should().Be(Radix.Null);
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

            var type = new ArrayType(array);

            var index = type[2, 1];

            index.As<DINT>().Should().Be(6);
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

            var type = new ArrayType(array);

            FluentActions.Invoking(() => type[1, 2]).Should().Throw<ArgumentOutOfRangeException>();
        }
        
        [Test]
        public void SetIndex_TwoDimensionalValidIndex_ShouldReturnExpected()
        {
            var array = new DINT[,]
            {
                { 1, 2 },
                { 3, 4 },
                { 5, 6 },
                { 7, 8 }
            };
            
            var type = new ArrayType(array);

            type[1, 1] = 400;

            type[1, 1].As<DINT>().Should().Be(400);
        }

        [Test]
        public void SetIndex_TwoDimensionalInValidIndex_ShouldThrowException()
        {
            var array = new DINT[,]
            {
                { 1, 2 },
                { 3, 4 },
                { 5, 6 },
                { 7, 8 }
            };

            var type = new ArrayType(array);

            FluentActions.Invoking(() => type[5, 2] = 1).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void GetIndex_ThreeDimensionalValidIndex_ShouldReturnExpected()
        {
            var array = new DINT[,,]
            {
                { { 1, 2, 3 }, { 4, 5, 6 } },
                { { 7, 8, 9 }, { 10, 11, 12 } }
            };

            var type = new ArrayType(array);

            var index = type[0, 1, 2];

            index.As<DINT>().Should().Be(6);
        }

        [Test]
        public void GetIndex_ThreeDimensionalInValidIndex_ShouldThrowException()
        {
            var array = new DINT[,,]
            {
                { { 1, 2, 3 }, { 4, 5, 6 } },
                { { 7, 8, 9 }, { 10, 11, 12 } }
            };

            var type = new ArrayType(array);

            FluentActions.Invoking(() => type[2, 1, 2]).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void SetIndex_InvalidAtomic_NotSure()
        {
            var array = new ArrayType(new SINT[] {1, 2, 3, 4});

            array[0] = 6;

            array[0].As<SINT>().Should().Be(6);
        }

        [Test]
        public void Members_WhenCalled_ShouldNotBeEmpty()
        {
            var array = new ArrayType(new DINT[] { new(100), new(200), new(300) });

            var elements = array.Members.ToArray();

            elements.Should().NotBeEmpty();
            elements.Select(e => e.DataType).Should().AllBeOfType<DINT>();
        }

        [Test]
        public void ToString_WhenCalled_ShouldBeExpected()
        {
            var array = new ArrayType(new DINT[] { new(100), new(200), new(300) });

            var name = array.ToString();

            name.Should().Be("DINT");
        }

        [Test]
        public void GetEnumerator_WhenCalled_ShouldBeNull()
        {
            var array = new ArrayType(new DINT[] { new(100), new(200), new(300) });

            using var enumerator = array.GetEnumerator();

            enumerator.Should().NotBeNull();
        }

        [Test]
        public void GetIndex_ComplexTypeArray_ShouldReturnExpectedValue()
        {
            var array = new ArrayType<TIMER>(new TIMER[]
                { new() { PRE = 1000 }, new() { PRE = 2000 }, new() { PRE = 3000 } });

            var timer = array[1];

            timer.Should().NotBeNull();
            timer.PRE.Should().Be(2000);
        }

        [Test]
        public void ToType_ValidType_ShouldNotBeNull()
        {
            var array = new ArrayType(new DINT[] { 1, 2, 3, 4 });

            var casted = array.ToType<DINT>();
            casted.Should().NotBeNull();
            casted.Dimensions.Length.Should().Be(4);

            var element = casted[0];
            element.Should().NotBeNull();
            element.Should().BeOfType<DINT>();
        }

        [Test]
        public void ToString_WhenCalled_ShouldBeTypeName()
        {
            var array = new ArrayType(new DINT[] { 1, 2, 3, 4, });

            array.ToString().Should().Be("DINT");
        }

        [Test]
        public void GetEnumerator_WhenCalled_ShouldBeTypeName()
        {
            var array = new ArrayType(new DINT[] { 1, 2, 3, 4, });

            using var enumerator = array.GetEnumerator();

            enumerator.Should().NotBeNull();
        }

        [Test]
        public void Iterate_WhenPerformed_AllShouldNotBeNull()
        {
            var array = new ArrayType(10);

            foreach (var type in array)
            {
                type.Should().NotBeNull();
                type.Should().BeOfType<NullType>();
            }
        }

        [Test]
        public Task Serialize_SimpleOneDimensional_ShouldBeVerified()
        {
            var type = new ArrayType(new DINT[] { new(100), new(200), new(300) });

            var xml = type.Serialize().ToString();

            return Verify(xml);
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

            var type = new ArrayType(array);

            var xml = type.Serialize().ToString();

            return Verify(xml);
        }

        [Test]
        public Task Serialize_SimpleThreeDimensional_ShouldBeVerified()
        {
            var array = new DINT[,,]
            {
                { { 1, 2, 3 }, { 4, 5, 6 } },
                { { 7, 8, 9 }, { 10, 11, 12 } }
            };

            var type = new ArrayType(array);

            var xml = type.Serialize().ToString();

            return Verify(xml);
        }

        [Test]
        public Task Serialize_ComplexTypeArray_ShouldBeVerified()
        {
            var array = new ArrayType<TIMER>(new TIMER[]
                { new() { PRE = 1000 }, new() { PRE = 2000 }, new() { PRE = 3000 } });

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
            array.Should().AllBeOfType<TIMER>();

            array[1].To<TIMER>().PRE.Should().Be(1000);
            array[1].To<TIMER>().EN.Should().Be(true);
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