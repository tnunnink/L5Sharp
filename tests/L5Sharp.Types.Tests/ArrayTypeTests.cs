using System;
using System.Linq;
using FluentAssertions;
using L5Sharp.Types.Atomics;
using NUnit.Framework;

namespace L5Sharp.Types.Tests
{
    [TestFixture]
    public class ArrayTypeTests
    {
        private static readonly DINT[] SingleDintArray = { new(100), new(200), new(300) };
            
        [Test]
        public void New_ValidArray_ShouldNotBeNull()
        {
            var array = new ArrayType(SingleDintArray.Cast<ILogixType>().ToArray());

            array.Should().NotBeNull();
        }

        [Test]
        public void New_NullElements_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new ArrayType(((ILogixType[])null)!)).Should().Throw<ArgumentNullException>();
        }
    }
}