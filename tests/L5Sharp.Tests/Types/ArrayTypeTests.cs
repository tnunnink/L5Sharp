using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Tests.Types.Custom;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;
using NUnit.Framework.Internal;

namespace L5Sharp.Tests.Types
{
    [TestFixture]
    public class ArrayTypeTests
    {
        private static readonly DINT[] DintArray = { new(100), new(200), new(300) };
        private static readonly TIMER[] TimerArray = new TIMER[10];
        private static readonly STRING[] StringArray = Logix.Array<STRING>(50).ToArray();
        private static readonly MyNestedType[] CustomNestedArray = Logix.Array<MyNestedType>(5).ToArray();
            
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
            array.Name.Should().Be("DINT[3]");
            array.Family.Should().Be(DataTypeFamily.None);
            array.Class.Should().Be(DataTypeClass.Atomic);
            array.IsAtomic.Should().BeTrue();
            array.IsStructure.Should().BeFalse();
        }
        
        [Test]
        public void New_NullArray_ShouldHaveExpectedProperties()
        {
            var array = new ArrayType<DINT>(new DINT[10]);

            array.Dimensions.Length.Should().Be(10);
            array.Name.Should().Be("[10]");
            array.Family.Should().Be(DataTypeFamily.None);
            array.Class.Should().Be(DataTypeClass.Unknown);
            array.IsAtomic.Should().BeTrue();
            array.IsStructure.Should().BeFalse();
        }
        
        [Test]
        public void New_GenericType_ShouldHaveExpectedProperties()
        {
            var array = new ArrayType<ILogixType>(new DINT[10]);

            array.Dimensions.Length.Should().Be(10);
            array.Name.Should().Be("[10]");
            array.Family.Should().Be(DataTypeFamily.None);
            array.Class.Should().Be(DataTypeClass.Unknown);
            array.IsAtomic.Should().BeFalse();
            array.IsStructure.Should().BeFalse();
        }

        [Test]
        public void New_NullElements_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new ArrayType<ILogixType>(((ILogixType[])null)!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void New_OutOfRangeLength_ShouldThrowArgumentOutOfRangeException()
        {
            FluentActions.Invoking(() => new ArrayType<ILogixType>(new ILogixType[1000000])).Should().Throw<ArgumentOutOfRangeException>();
        }
        
        [Test]
        public void New_OneDimensional_ShouldNotBeNull()
        {
            var array = new ArrayType<ILogixType>(new ILogixType[10, 10]);

            array.Should().NotBeNull();
        }
        
        [Test]
        public void New_OneDimensional_ShouldHaveExpectedLength()
        {
            var array = new ArrayType<ILogixType>(new ILogixType[10, 10]);

            array.Dimensions.Length.Should().Be(100);
        }

        [Test]
        public void New_TwoDimensional_ShouldNotBeNull()
        {
            var array = new ArrayType<ILogixType>(new ILogixType[10, 10]);

            array.Should().NotBeNull();
        }
        
        [Test]
        public void New_TwoDimensional_ShouldHaveExpectedLength()
        {
            var array = new ArrayType<ILogixType>(new ILogixType[10, 10]);

            array.Dimensions.Length.Should().Be(100);
        }
        
        [Test]
        public void New_ThreeDimensional_ShouldNotBeNull()
        {
            var array = new ArrayType<ILogixType>(new ILogixType[10, 10]);

            array.Should().NotBeNull();
        }
        
        [Test]
        public void New_ThreeDimensional_ShouldHaveExpectedLength()
        {
            var array = new ArrayType<ILogixType>(new ILogixType[10, 10, 10]);

            array.Dimensions.Length.Should().Be(1000);
        }

        [Test]
        public void GetElementOneDimensional__ValidIndex_ShouldBeExpected()
        {
            var array = new ArrayType<DINT>(DintArray);

            var element = array[1];

            element.Should().Be(200);
        }
    }
}