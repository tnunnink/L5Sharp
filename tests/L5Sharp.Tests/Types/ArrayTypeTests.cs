using FluentAssertions;
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
        private static readonly List<TIMER> TimerArray = Logix.Array<TIMER>(10).ToList();

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
            FluentActions.Invoking(() => new ArrayType<ILogixType>(((ILogixType[])null)!)).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void New_OutOfRangeLength_ShouldThrowArgumentOutOfRangeException()
        {
            FluentActions.Invoking(() => new ArrayType<ILogixType>(new ILogixType[1000000])).Should()
                .Throw<ArgumentOutOfRangeException>();
        }
        
        [Test]
        public void New_OneDimensionalNullArray_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => new ArrayType<ILogixType>(new ILogixType[10])).Should()
                .Throw<ArgumentException>();
        }

        [Test]
        public void New_TwoDimensionalNullArray_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => new ArrayType<ILogixType>(new ILogixType[10, 10])).Should()
                .Throw<ArgumentException>();
        }

        [Test]
        public void New_ThreeDimensionalNullArray_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => new ArrayType<ILogixType>(new ILogixType[10, 10, 10])).Should()
                .Throw<ArgumentException>();
        }
        
        [Test]
        public void New_OneDimensional_ShouldHaveExpectedLength()
        {
            var array = new DINT[] { 1, 2 , 3, 4 };

            var type = new ArrayType<DINT>(array);

            type.Dimensions.Length.Should().Be(4);
        }
        
        [Test]
        public void Index_OneDimensionalValidIndex_ShouldReturnExpected()
        {
            var array = new DINT[] { 1, 2 , 3, 4 };

            var type = new ArrayType<DINT>(array);

            var index = type[2];

            index.Should().Be(3);
        }
        
        [Test]
        public void Index_OneDimensionalInValidIndex_ShouldThrowException()
        {
            var array = new DINT[] { 1, 2 , 3, 4 };

            var type = new ArrayType<DINT>(array);

            FluentActions.Invoking(() => type[5]).Should().Throw<KeyNotFoundException>();
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
        public void Index_TwoDimensionalValidIndex_ShouldReturnExpected()
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
        public void Index_TwoDimensionalInValidIndex_ShouldThrowException()
        {
            var array = new DINT[,]
            {
                { 1, 2 },
                { 3, 4 },
                { 5, 6 },
                { 7, 8 }
            };

            var type = new ArrayType<DINT>(array);

            FluentActions.Invoking(() => type[1, 2]).Should().Throw<KeyNotFoundException>();
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
        public void Index_ThreeDimensionalValidIndex_ShouldReturnExpected()
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
        public void Index_ThreeDimensionalInValidIndex_ShouldThrowException()
        {
            var array = new DINT[,,]
            {
                { { 1, 2, 3 }, { 4, 5, 6 } },
                { { 7, 8, 9 }, { 10, 11, 12 } }
            };

            var type = new ArrayType<DINT>(array);

            FluentActions.Invoking(() => type[2, 1, 2]).Should().Throw<KeyNotFoundException>();
        }

        [Test]
        public void Elements_WhenCalled_ShouldNotBeEmpty()
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
            var array = new ArrayType<TIMER>(10, TimerArray);

            array.Should().NotBeNull();
            array.Should().NotBeEmpty();
            array.Dimensions.Length.Should().Be(10);
        }
    }
}