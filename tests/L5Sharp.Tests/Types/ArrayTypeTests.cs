using System.Collections;
using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;

// ReSharper disable UseObjectOrCollectionInitializer

namespace L5Sharp.Tests.Types
{
    [TestFixture]
    public class ArrayTypeTests
    {
        [Test]
        public void Constructor_NullArray_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new ArrayType(((LogixType[])null)!)).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void Constructor_EmptyArray_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => new ArrayType(Array.Empty<DINT>())).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Constructor_ArrayOfNullTypes_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => new ArrayType(new LogixType[5])).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Constructor_OutOfRangeLength_ShouldThrowArgumentOutOfRangeException()
        {
            var array = Enumerable.Range(1, 1000000).Select(i => new DINT(i)).ToArray();
            FluentActions.Invoking(() => new ArrayType(array)).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Constructor_ArrayOfDifferentTypes_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => new ArrayType(new[] { new BOOL(), new DINT(), new TIMER(), LogixData.Null }))
                .Should().Throw<ArgumentException>();
        }

        [Test]
        public void Constructor_ValidArray_ShouldNotBeNull()
        {
            var array = new ArrayType(new DINT[] { 1, 2, 3, 4 });

            array.Should().NotBeNull();
        }

        [Test]
        public void Constructor_AtomicTypes_ShouldHaveExpectedValues()
        {
            var array = new ArrayType(new DINT[] { 1, 2, 3, 4 });

            array.Name.Should().Be("DINT[4]");
            array.Dimensions.Length.Should().Be(4);
            array.Radix.Should().Be(Radix.Decimal);
            array.Class.Should().Be(DataTypeClass.Atomic);
            array.Family.Should().Be(DataTypeFamily.None);
            array.Members.Should().HaveCount(4);
            array[0].As<DINT>().Should().Be(1);
            array[1].As<DINT>().Should().Be(2);
            array[2].As<DINT>().Should().Be(3);
            array[3].As<DINT>().Should().Be(4);
        }

        [Test]
        public void Constructor_StructureTypes_ShouldHaveExpectedValues()
        {
            var array = new ArrayType(new TIMER[] { new() { PRE = 1000 }, new() { PRE = 2000 }, new() { PRE = 3000 } });

            array.Name.Should().Be("TIMER[3]");
            array.Dimensions.Length.Should().Be(3);
            array.Radix.Should().Be(Radix.Null);
            array.Class.Should().Be(DataTypeClass.Predefined);
            array.Family.Should().Be(DataTypeFamily.None);
            array.Members.Should().HaveCount(3);
            array[0].As<TIMER>().PRE.Should().Be(1000);
            array[1].As<TIMER>().PRE.Should().Be(2000);
            array[2].As<TIMER>().PRE.Should().Be(3000);
        }

        [Test]
        public void Constructor_StringTypes_ShouldHaveExpectedValues()
        {
            var array = new ArrayType(new STRING[] { "Test", "Test", "Test" });

            array.Name.Should().Be("STRING[3]");
            array.Dimensions.Length.Should().Be(3);
            array.Radix.Should().Be(Radix.Null);
            array.Class.Should().Be(DataTypeClass.Predefined);
            array.Family.Should().Be(DataTypeFamily.String);
            array.Members.Should().HaveCount(3);
            array[0].ToString().Should().Be("Test");
            array[1].ToString().Should().Be("Test");
            array[2].ToString().Should().Be("Test");
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
        public void New_Empty_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => ArrayType.New<TIMER>(0)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void New_OneDimensional_ShouldBeExpected()
        {
            var array = ArrayType.New<TIMER>(10);

            array.Should().NotBeNull();
            array.Should<TIMER>().NotBeEmpty();
            array.Dimensions.Length.Should().Be(10);
            array.Should<TIMER>().AllBeOfType<TIMER>();
        }

        [Test]
        public void New_TwoDimensional_ShouldBeExpected()
        {
            var array = ArrayType.New<TIMER>(new Dimensions(5, 5));

            array.Should().NotBeNull();
            array.Should<TIMER>().NotBeEmpty();
            array.Dimensions.Length.Should().Be(25);
            array.Should<TIMER>().AllBeOfType<TIMER>();
        }

        [Test]
        public void New_ThreeDimensional_ShouldBeExpected()
        {
            var array = ArrayType.New<TIMER>(new Dimensions(2, 2, 2));

            array.Should().NotBeNull();
            array.Should<TIMER>().NotBeEmpty();
            array.Dimensions.Length.Should().Be(8);
            array.Should<TIMER>().AllBeOfType<TIMER>();
        }

        [Test]
        public void GetIndex_OneDimensionalValidIndex_ShouldReturnExpected()
        {
            ArrayType type = new DINT[] { 1, 2, 3, 4 };

            var index = type[2];

            index.As<DINT>().Should().Be(3);
        }

        [Test]
        public void GetIndex_OneDimensionalInValidIndex_ShouldThrowArgumentOutOfRangeException()
        {
            ArrayType array = new DINT[] { 1, 2, 3, 4 };

            FluentActions.Invoking(() => array[5]).Should().Throw<ArgumentOutOfRangeException>();
        }


        [Test]
        public void SetIndex_OneDimensionalValidIndex_ShouldReturnExpected()
        {
            ArrayType type = new DINT[] { 1, 2, 3, 4 };

            type[2] = 100;

            type[2].As<DINT>().Should().Be(100);
        }

        [Test]
        public void SetIndex_OneDimensionalInValidIndex_ShouldThrowArgumentOutOfRangeException()
        {
            ArrayType array = new DINT[] { 1, 2, 3, 4 };

            FluentActions.Invoking(() => array[5] = 1).Should().Throw<ArgumentOutOfRangeException>();
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
        public void GetIndex_TwoDimensionalInValidIndex_ShouldThrowArgumentOutOfRangeException()
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
        public void SetIndex_TwoDimensionalInValidIndex_ShouldThrowArgumentOutOfRangeException()
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
        public void SetIndex_ThreeDimensionalValidIndex_ShouldReturnExpected()
        {
            var array = new DINT[,,]
            {
                { { 1, 2, 3 }, { 4, 5, 6 } },
                { { 7, 8, 9 }, { 10, 11, 12 } }
            };

            var type = new ArrayType(array);

            type[0, 1, 2] = 500;

            type[0, 1, 2].As<DINT>().Should().Be(500);
        }

        [Test]
        public void SetIndex_ThreeDimensionalInValidIndex_ShouldThrowArgumentOutOfRangeException()
        {
            var array = new DINT[,,]
            {
                { { 1, 2, 3 }, { 4, 5, 6 } },
                { { 7, 8, 9 }, { 10, 11, 12 } }
            };

            var type = new ArrayType(array);

            FluentActions.Invoking(() => type[2, 1, 2] = 1).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void SetIndex_Null_ShouldThrowArgumentNullException()
        {
            ArrayType array = new DINT[] { 1, 2, 3, 4 };

            FluentActions.Invoking(() => array[0] = null!).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void SetIndex_InvalidType_ShouldThrowArgumentException()
        {
            ArrayType array = new DINT[] { 1, 2, 3, 4 };

            FluentActions.Invoking(() => array[0] = new TIMER()).Should().Throw<InvalidCastException>();
        }

        [Test]
        public void SetIndex_DifferentAtomicType_ShouldHaveExpectedValues()
        {
            ArrayType array = new DINT[] { 1, 2, 3, 4 };

            array[0] = new INT(10);

            array[0].Should().BeOfType<DINT>();
            array[0].As<DINT>().Should().Be(10);
        }

        [Test]
        public void ImplicitOperator_OneDimensional_ShouldNotBeNull()
        {
            ArrayType array = new DINT[] { 1, 2, 3, 4 };

            array.Should().NotBeNull();
            array.Dimensions.Length.Should().Be(4);
            array.Dimensions.Rank.Should().Be(1);
        }

        [Test]
        public void ImplicitOperator_TwoDimensional_ShouldNotBeNull()
        {
            ArrayType array = new DINT[,] { { 1, 2 }, { 3, 4 } };

            array.Should().NotBeNull();
            array.Dimensions.Length.Should().Be(4);
            array.Dimensions.Rank.Should().Be(2);
        }

        [Test]
        public void ImplicitOperator_ThreeDimensional_ShouldNotBeNull()
        {
            ArrayType array = new DINT[,,] { { { 1, 2 }, { 3, 4 } }, { { 5, 6 }, { 7, 8 } } };

            array.Should().NotBeNull();
            array.Dimensions.Length.Should().Be(8);
            array.Dimensions.Rank.Should().Be(3);
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

            name.Should().Be("DINT[3]");
        }

        [Test]
        public void GetEnumerator_WhenCalled_ShouldBeNull()
        {
            var array = new ArrayType(new DINT[] { new(100), new(200), new(300) });

            using var enumerator = array.GetEnumerator();

            enumerator.Should().NotBeNull();
        }

        [Test]
        public void GetEnumerator_AsEnumerable_ShouldBeNull()
        {
            var array = (IEnumerable)new ArrayType(new DINT[] { new(100), new(200), new(300) });

            var enumerator = array.GetEnumerator();

            enumerator.Should().NotBeNull();
        }

        [Test]
        public void Iterate_WhenPerformed_AllShouldNotBeNull()
        {
            var array = new ArrayType(new DINT[] { 1, 2, 3, 4 });

            foreach (var type in array)
            {
                type.Should().NotBeNull();
                type.Should().BeOfType<DINT>();
            }
        }

        [Test]
        public void Of_ValidType_ShouldNotBeNull()
        {
            var array = new ArrayType(new DINT[] { 1, 2, 3, 4 });

            var casted = array.Of<DINT>();
            casted.Should().NotBeNull();
            casted.Dimensions.Length.Should().Be(4);

            var element = casted[0];
            element.Should().NotBeNull();
            element.Should().BeOfType<DINT>();
        }

        [Test]
        public void AsArray_InvalidType_ShouldThrowInvalidCastException()
        {
            var array = new ArrayType(new DINT[] { 1, 2, 3, 4 });

            FluentActions.Invoking(() => array.OfType<INT>()).Should().Throw<InvalidCastException>();
        }

        [Test]
        public void AsArray_InvalidTypeUp_ShouldThrowInvalidCastException()
        {
            var array = new ArrayType(new INT[] { 1, 2, 3, 4 });

            FluentActions.Invoking(() => array.OfType<DINT>()).Should().Throw<InvalidCastException>();
        }

        [Test]
        public void AsArray_BaseTypeOfStructure_ShouldWork()
        {
            var array = new ArrayType(new TIMER[] { new(), new(), new() });

            var casted = array.OfType<StructureType>();

            casted.Should().NotBeNull();
            casted.Should<StructureType>().AllBeOfType<TIMER>();
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
        public void Deserialize_NullElement_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new ArrayType<LogixType>(((XElement)null)!)).Should().Throw<ArgumentNullException>();
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

            array.Name.Should().Be("REAL[5]");
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

            array.Name.Should().Be("TIMER[5]");
            array.Dimensions.Should().Be(new Dimensions(5));
            array.Class.Should().Be(DataTypeClass.Predefined);
            array.Family.Should().Be(DataTypeFamily.None);
            array.Should().AllBeOfType<TIMER>();

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