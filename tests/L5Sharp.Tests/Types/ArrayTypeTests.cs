using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;
using NUnit.Framework;

namespace L5Sharp.Tests.Types
{
    [TestFixture]
    public class ArrayTypeTests
    {
        private static readonly DINT[] SingleDintArray = { new(100), new(200), new(300) };
        private static readonly TIMER[] SingleTimerArray = new TIMER[10];
            
        [Test]
        public void New_ValidArray_ShouldNotBeNull()
        {
            var array = new ArrayType<DINT>(SingleDintArray);

            array.Should().NotBeNull();
        }

        [Test]
        public void New_SimpleArray_ShouldHaveExpectedProperties()
        {
            var array = new ArrayType<DINT>(SingleDintArray);

            array.Dimensions.Length.Should().Be(3);
            array.Name.Should().Be("DINT[3]");
            array.Family.Should().Be(DataTypeFamily.None);
            array.Class.Should().Be(DataTypeClass.Atomic);
            array.IsAtomic.Should().BeTrue();
            array.IsStructure.Should().BeFalse();
        }

        [Test]
        public void New_NullArray_ShouldNotWork()
        {
            FluentActions.Invoking(() => new ArrayType<ILogixType>(new ILogixType[10])).Should().Throw<ArgumentException>();
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
    }
}