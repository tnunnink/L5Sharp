using System.Collections;
using System.Xml.Linq;
using FluentAssertions;

// ReSharper disable UseObjectOrCollectionInitializer

namespace L5Sharp.Tests.Core.Data
{
    [TestFixture]
    public class ArrayDataTests
    {
        [Test]
        public void Constructor_NullArray_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new ArrayData(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void New_EmptyArray_ShouldHaveExpectedPropertyValues()
        {
            var array = new ArrayData<DINT>([]);

            array.Name.Should().Be("DINT");
            array.Radix.Should().Be(Radix.Decimal);
            array.Members.Should().BeEmpty();
            array.Dimensions.IsEmpty.Should().BeTrue();
        }

        [Test]
        public void New_OutOfRangeLength_ShouldThrowArgumentOutOfRangeException()
        {
            var array = Enumerable.Range(1, 1000000).Select(i => new DINT(i)).ToArray();

            FluentActions.Invoking(() => new ArrayData<DINT>(array))
                .Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void New_AtomicTypes_ShouldHaveExpectedValues()
        {
            var array = new ArrayData<DINT>([1, 2, 3, 4]);

            array.Name.Should().Be("DINT");
            array.Dimensions.Length.Should().Be(4);
            array.Radix.Should().Be(Radix.Decimal);
            array.Members.Should().HaveCount(4);
            array[0].As<DINT>().Should().Be(1);
            array[1].As<DINT>().Should().Be(2);
            array[2].As<DINT>().Should().Be(3);
            array[3].As<DINT>().Should().Be(4);
        }

        [Test]
        public void New_StructureTypes_ShouldHaveExpectedValues()
        {
            var array = new ArrayData<TIMER>([
                new TIMER { PRE = 1000 },
                new TIMER { PRE = 2000 },
                new TIMER { PRE = 3000 }
            ]);

            array.Name.Should().Be("TIMER");
            array.Dimensions.Length.Should().Be(3);
            array.Radix.Should().Be(Radix.Null);
            array.Members.Should().HaveCount(3);
            array[0].As<TIMER>().PRE.Should().Be(1000);
            array[1].As<TIMER>().PRE.Should().Be(2000);
            array[2].As<TIMER>().PRE.Should().Be(3000);
        }

        [Test]
        public void New_StringTypes_ShouldHaveExpectedValues()
        {
            var array = new ArrayData<STRING>(["Test", "Test", "Test"]);

            array.Name.Should().Be("STRING");
            array.Dimensions.Length.Should().Be(3);
            array.Radix.Should().Be(Radix.Null);
            array.Members.Should().HaveCount(3);
            array[0].ToString().Should().Be("Test");
            array[1].ToString().Should().Be("Test");
            array[2].ToString().Should().Be("Test");
        }

        [Test]
        public void New_TwoDimensional_ShouldHaveExpectedLength()
        {
            var type = new ArrayData<DINT>(new DINT[,]
            {
                { 1, 2 },
                { 3, 4 },
                { 5, 6 },
                { 7, 8 }
            });

            type.Dimensions.Length.Should().Be(8);
        }

        [Test]
        public void New_ThreeDimensional_ShouldHaveExpectedLength()
        {
            var type = new ArrayData<DINT>(new DINT[,,]
            {
                { { 1, 2, 3 }, { 4, 5, 6 } },
                { { 7, 8, 9 }, { 10, 11, 12 } }
            });

            type.Dimensions.Length.Should().Be(12);
        }

        [Test]
        public void New_ValidDimensions_ShouldBeExpected()
        {
            var array = new ArrayData<DINT>(100);

            array.Should().NotBeNull();
            array.Name.Should().Be("DINT");
            array.Dimensions.Length.Should().Be(100);
            array.Members.Should().HaveCount(100);
            array.Members.Should().AllSatisfy(m => m.Value.Should().BeOfType<DINT>());
        }

        [Test]
        public void New_OneDimensional_ShouldBeExpected()
        {
            var array = new ArrayData<TIMER>(10);

            array.Should().NotBeNull();
            array.Should().NotBeEmpty();
            array.Dimensions.Length.Should().Be(10);
        }

        [Test]
        public void New_TwoDimensional_ShouldBeExpected()
        {
            var array = new ArrayData<TIMER>(new Dimensions(5, 5));

            array.Should().NotBeNull();
            array.Should().NotBeEmpty();
            array.Dimensions.Length.Should().Be(25);
        }

        [Test]
        public void New_ThreeDimensional_ShouldBeExpected()
        {
            var array = new ArrayData<TIMER>(new Dimensions(2, 2, 2));

            array.Should().NotBeNull();
            array.Should().NotBeEmpty();
            array.Dimensions.Length.Should().Be(8);
        }

        [Test]
        public void GetIndex_OneDimensionalValidIndex_ShouldReturnExpected()
        {
            var data = new ArrayData<DINT>([1, 2, 3, 4]);

            var index = data[2];

            index.As<DINT>().Should().Be(3);
        }

        [Test]
        public void GetIndexOfArray_OneDimensional_ShouldReturnExpected()
        {
            var data = new ArrayData<DINT>([1, 2, 3, 4]);

            var index = data[2];

            index.As<DINT>().Should().Be(3);
        }

        [Test]
        public void GetIndex_OneDimensionalInValidIndex_ShouldThrowArgumentOutOfRangeException()
        {
            var array = new ArrayData<DINT>([1, 2, 3, 4]);

            FluentActions.Invoking(() => array[5]).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void SetIndex_OneDimensionalValidIndex_ShouldReturnExpected()
        {
            var array = new ArrayData<DINT>([1, 2, 3, 4]);

            array[2] = 100;

            array[2].As<DINT>().Should().Be(100);
        }

        [Test]
        public void SetIndex_OneDimensionalInValidIndex_ShouldThrowArgumentOutOfRangeException()
        {
            var array = new ArrayData<DINT>([1, 2, 3, 4]);

            FluentActions.Invoking(() => array[5] = 1).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void GetIndex_TwoDimensionalValidIndex_ShouldReturnExpected()
        {
            var array = new ArrayData<DINT>(new DINT[,]
            {
                { 1, 2 },
                { 3, 4 },
                { 5, 6 },
                { 7, 8 }
            });

            var index = array[2, 1];

            index.As<DINT>().Should().Be(6);
        }

        [Test]
        public void GetIndexOfArray_TwoDimensional_ShouldReturnExpected()
        {
            var array = new ArrayData<DINT>(new DINT[,]
            {
                { 1, 2 },
                { 3, 4 },
                { 5, 6 },
                { 7, 8 }
            });

            var index = array[2, 1];

            index.As<DINT>().Should().Be(6);
        }

        [Test]
        public void GetIndex_TwoDimensionalInValidIndex_ShouldThrowArgumentOutOfRangeException()
        {
            var array = new ArrayData<DINT>(new DINT[,]
            {
                { 1, 2 },
                { 3, 4 },
                { 5, 6 },
                { 7, 8 }
            });

            FluentActions.Invoking(() => array[1, 2]).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void SetIndex_TwoDimensionalValidIndex_ShouldReturnExpected()
        {
            var array = new ArrayData<DINT>(new DINT[,]
            {
                { 1, 2 },
                { 3, 4 },
                { 5, 6 },
                { 7, 8 }
            });

            array[1, 1] = 400;

            array[1, 1].As<DINT>().Should().Be(400);
        }

        [Test]
        public void SetIndex_TwoDimensionalInValidIndex_ShouldThrowArgumentOutOfRangeException()
        {
            var array = new ArrayData<DINT>(new DINT[,]
            {
                { 1, 2 },
                { 3, 4 },
                { 5, 6 },
                { 7, 8 }
            });

            FluentActions.Invoking(() => array[5, 2] = 1).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void GetIndex_ThreeDimensionalValidIndex_ShouldReturnExpected()
        {
            var array = new ArrayData<DINT>(new DINT[,,]
            {
                { { 1, 2, 3 }, { 4, 5, 6 } },
                { { 7, 8, 9 }, { 10, 11, 12 } }
            });

            var index = array[0, 1, 2];

            index.As<DINT>().Should().Be(6);
        }

        [Test]
        public void GetIndexOfArray_ThreeDimensional_ShouldReturnExpected()
        {
            var array = new ArrayData<DINT>(new DINT[,,]
            {
                { { 1, 2, 3 }, { 4, 5, 6 } },
                { { 7, 8, 9 }, { 10, 11, 12 } }
            });

            var index = array[0, 1, 2];

            index.As<DINT>().Should().Be(6);
        }

        [Test]
        public void GetIndex_ThreeDimensionalInValidIndex_ShouldThrowException()
        {
            var array = new ArrayData<DINT>(new DINT[,,]
            {
                { { 1, 2, 3 }, { 4, 5, 6 } },
                { { 7, 8, 9 }, { 10, 11, 12 } }
            });

            FluentActions.Invoking(() => array[2, 1, 2]).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void SetIndex_ThreeDimensionalValidIndex_ShouldReturnExpected()
        {
            var array = new ArrayData<DINT>(new DINT[,,]
            {
                { { 1, 2, 3 }, { 4, 5, 6 } },
                { { 7, 8, 9 }, { 10, 11, 12 } }
            });

            array[0, 1, 2] = 500;

            array[0, 1, 2].As<DINT>().Should().Be(500);
        }

        [Test]
        public void SetIndex_ThreeDimensionalInValidIndex_ShouldThrowArgumentOutOfRangeException()
        {
            var array = new ArrayData<DINT>(new DINT[,,]
            {
                { { 1, 2, 3 }, { 4, 5, 6 } },
                { { 7, 8, 9 }, { 10, 11, 12 } }
            });

            FluentActions.Invoking(() => array[2, 1, 2] = 1).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void SetIndex_Null_ShouldThrowArgumentNullException()
        {
            var array = new ArrayData<DINT>([1, 2, 3, 4]);

            FluentActions.Invoking(() => array[0] = null!).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void SetIndex_DifferentAtomicType_ShouldHaveExpectedValues()
        {
            var array = new ArrayData<DINT>([1, 2, 3, 4]);

            array[0] = (short)new INT(10);

            array[0].Should().BeOfType<DINT>();
            array[0].As<DINT>().Should().Be(10);
        }

        [Test]
        public void ImplicitOperator_OneDimensional_ShouldNotBeNull()
        {
            var array = new ArrayData<DINT>([1, 2, 3, 4]);

            array.Should().NotBeNull();
            array.Dimensions.Length.Should().Be(4);
            array.Dimensions.Rank.Should().Be(1);
        }

        [Test]
        public void Members_WhenCalled_ShouldNotBeEmpty()
        {
            var array = new ArrayData<DINT>([100, 200, 300]);

            var elements = array.Members.ToArray();

            elements.Should().NotBeEmpty();
            elements.Select(e => e.Value).Should().AllBeOfType<DINT>();
        }

        [Test]
        public void ToString_WhenCalled_ShouldBeExpected()
        {
            var array = new ArrayData<DINT>([100, 200, 300]);

            var name = array.ToString();

            name.Should().Be("DINT[3]");
        }

        [Test]
        public void GetEnumerator_WhenCalled_ShouldBeNull()
        {
            var array = new ArrayData<DINT>([100, 200, 300]);

            using var enumerator = array.GetEnumerator();

            enumerator.Should().NotBeNull();
        }

        [Test]
        public void Iterate_WhenPerformed_AllShouldNotBeNull()
        {
            var array = new ArrayData<DINT>([1, 2, 3, 4]);

            foreach (var type in array)
            {
                type.Should().NotBeNull();
                type.Should().BeOfType<DINT>();
            }
        }

        [Test]
        public Task Serialize_EmptyArray_ShouldBeVerified()
        {
            var array = new ArrayData<DINT>([]);

            var xml = array.Serialize().ToString();

            return Verify(xml);
        }

        [Test]
        public Task Serialize_SimpleOneDimensional_ShouldBeVerified()
        {
            var array = new ArrayData<DINT>([100, 200, 300]);

            var xml = array.Serialize().ToString();

            return Verify(xml);
        }

        [Test]
        public Task Serialize_SimpleTwoDimensional_ShouldBeVerified()
        {
            var array = new ArrayData<DINT>(new DINT[,]
            {
                { 1, 2 },
                { 3, 4 },
                { 5, 6 },
                { 7, 8 }
            });

            var xml = array.Serialize().ToString();

            return Verify(xml);
        }

        [Test]
        public Task Serialize_SimpleThreeDimensional_ShouldBeVerified()
        {
            var array = new ArrayData<DINT>(new DINT[,,]
            {
                { { 1, 2, 3 }, { 4, 5, 6 } },
                { { 7, 8, 9 }, { 10, 11, 12 } }
            });

            var xml = array.Serialize().ToString();

            return Verify(xml);
        }

        [Test]
        public Task Serialize_StructureTypeArray_ShouldBeVerified()
        {
            var array = new ArrayData<TIMER>(
            [
                new TIMER { PRE = 1000 },
                new TIMER { PRE = 2000 },
                new TIMER { PRE = 3000 }
            ]);

            var xml = array.Serialize().ToString();

            return Verify(xml);
        }

        [Test]
        public Task Serialize_StringTypeArray_ShouldBeVerified()
        {
            var array = new ArrayData<STRING>(["Test Value 1", "Test Value 2", "Test Value 3"]);

            var xml = array.Serialize().ToString();

            return Verify(xml);
        }

        [Test]
        public void Deserialize_NullElement_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new ArrayData(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Deserialize_ValueTypeArray_ShouldNotBeNull()
        {
            var element = XElement.Parse(GetValueArrayXml());

            var array = new ArrayData(element);

            array.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_ValueTypeArray_ShouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetValueArrayXml());

            var array = new ArrayData(element);

            array.Name.Should().Be("REAL");
            array.Dimensions.Should().Be(new Dimensions(5));

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

            var array = new ArrayData(element);

            array.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_StructureTypeArray_ShouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetStructureArrayXml());

            var array = new ArrayData<TIMER>(element);

            array.Name.Should().Be("TIMER");
            array.Dimensions.Should().Be(new Dimensions(5));
            array.Should().AllBeOfType<TIMER>();

            array[1].PRE.Should().Be(1000);
            array[1].EN.Should().Be(true);
        }

        [Test]
        public void GetEnumerator_WhenCalled_ShouldNotBeNull()
        {
            IEnumerable array = new ArrayData<DINT>([1, 2, 3, 4]);

            using var enumerator = array.GetEnumerator() as IDisposable;

            enumerator.Should().NotBeNull();

            foreach (var item in array)
            {
                item.Should().BeOfType<DINT>();
            }
        }

        private static string GetValueArrayXml()
        {
            return
                """
                <Array DataType="REAL" Dimensions="5" Radix="Float">
                    <Element Index="[0]" Value="0.0"/>
                    <Element Index="[1]" Value="1.1"/>
                    <Element Index="[2]" Value="2.2"/>
                    <Element Index="[3]" Value="3.3"/>
                    <Element Index="[4]" Value="4.4"/>
                </Array>
                """;
        }

        private static string GetStructureArrayXml()
        {
            return """
                   <Array DataType="TIMER" Dimensions="5">
                       <Element Index="[0]">
                           <Structure DataType="TIMER">
                               <DataValueMember Name="PRE" DataType="DINT" Radix="Decimal" Value="0"/>
                               <DataValueMember Name="ACC" DataType="DINT" Radix="Decimal" Value="0"/>
                               <DataValueMember Name="EN" DataType="BOOL" Value="0"/>
                               <DataValueMember Name="TT" DataType="BOOL" Value="0"/>
                               <DataValueMember Name="DN" DataType="BOOL" Value="0"/>
                           </Structure>
                       </Element>
                       <Element Index="[1]">
                           <Structure DataType="TIMER">
                               <DataValueMember Name="PRE" DataType="DINT" Radix="Decimal" Value="1000"/>
                               <DataValueMember Name="ACC" DataType="DINT" Radix="Decimal" Value="0"/>
                               <DataValueMember Name="EN" DataType="BOOL" Value="1"/>
                               <DataValueMember Name="TT" DataType="BOOL" Value="0"/>
                               <DataValueMember Name="DN" DataType="BOOL" Value="0"/>
                           </Structure>
                       </Element>
                       <Element Index="[2]">
                           <Structure DataType="TIMER">
                               <DataValueMember Name="PRE" DataType="DINT" Radix="Decimal" Value="0"/>
                               <DataValueMember Name="ACC" DataType="DINT" Radix="Decimal" Value="0"/>
                               <DataValueMember Name="EN" DataType="BOOL" Value="0"/>
                               <DataValueMember Name="TT" DataType="BOOL" Value="0"/>
                               <DataValueMember Name="DN" DataType="BOOL" Value="0"/>
                           </Structure>
                       </Element>
                       <Element Index="[3]">
                           <Structure DataType="TIMER">
                               <DataValueMember Name="PRE" DataType="DINT" Radix="Decimal" Value="0"/>
                               <DataValueMember Name="ACC" DataType="DINT" Radix="Decimal" Value="0"/>
                               <DataValueMember Name="EN" DataType="BOOL" Value="0"/>
                               <DataValueMember Name="TT" DataType="BOOL" Value="0"/>
                               <DataValueMember Name="DN" DataType="BOOL" Value="0"/>
                           </Structure>
                       </Element>
                       <Element Index="[4]">
                           <Structure DataType="TIMER">
                               <DataValueMember Name="PRE" DataType="DINT" Radix="Decimal" Value="0"/>
                               <DataValueMember Name="ACC" DataType="DINT" Radix="Decimal" Value="0"/>
                               <DataValueMember Name="EN" DataType="BOOL" Value="0"/>
                               <DataValueMember Name="TT" DataType="BOOL" Value="0"/>
                               <DataValueMember Name="DN" DataType="BOOL" Value="0"/>
                           </Structure>
                       </Element>
                    </Array>
                   """;
        }
    }
}