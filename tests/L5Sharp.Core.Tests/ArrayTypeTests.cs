using System;
using System.Collections;
using System.Linq;
using FluentAssertions;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Types;
using NUnit.Framework;
using String = L5Sharp.Types.String;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class ArrayTypeTests
    {
        [Test]
        public void New_NullDimensions_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new ArrayType<Bool>(null!, new Bool())).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void New_EmptyDimensions_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new ArrayType<Bool>(Dimensions.Empty))
                .Should().Throw<ArgumentException>().WithMessage("The provided dimensions can not be empty.");
        }

        [Test]
        public void New_AbstractType_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => new ArrayType<ComplexType>(10))
                .Should().Throw<ArgumentException>().WithMessage(
                    $"The specified type '{typeof(ComplexType)} is abstract. Abstract types can not be instantiated.");
        }

        [Test]
        public void New_NonDefaultConstructorType_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => new ArrayType<UserDefined>(10))
                .Should().Throw<ArgumentException>().WithMessage(
                    $"The specified type '{typeof(UserDefined)} does not have a default parameterless constructor.");
        }

        [Test]
        public void New_ValidTypeAndDimensions_ShouldNotBeNull()
        {
            var array = new ArrayType<Bool>(10);

            array.Should().NotBeNull();
        }

        [Test]
        public void New_ValidCustomType_ShouldNotBeNull()
        {
            var userType = new UserDefined("Test");

            var array = new ArrayType<IDataType>(10, userType);

            array.Should().NotBeNull();
        }

        [Test]
        public void New_AtomicType_ShouldHaveExpectedProperties()
        {
            var array = new ArrayType<Bool>(10);

            array.Name.Should().Be("BOOL[10]");
            array.Description.Should().BeEmpty();
            array.Class.Should().Be(DataTypeClass.Atomic);
            array.Family.Should().Be(DataTypeFamily.None);
            array.Dimensions.Length.Should().Be(10);
        }

        [Test]
        public void New_StringType_ShouldHaveExpectedProperties()
        {
            var array = new ArrayType<String>(10);

            array.Name.Should().Be("STRING[10]");
            array.Description.Should().BeEmpty();
            array.Class.Should().Be(DataTypeClass.Predefined);
            array.Family.Should().Be(DataTypeFamily.String);
            array.Dimensions.Length.Should().Be(10);
        }

        [Test]
        public void New_UserType_ShouldHaveExpectedProperties()
        {
            var userType = new UserDefined("Test");

            var array = new ArrayType<IUserDefined>(10, userType);

            array.Name.Should().Be("Test[10]");
            array.Description.Should().BeEmpty();
            array.Class.Should().Be(DataTypeClass.User);
            array.Family.Should().Be(DataTypeFamily.None);
            array.Dimensions.Length.Should().Be(10);
        }

        [Test]
        public void New_OverloadedParameters_AllMembersShouldHaveExpectedValues()
        {
            var type = new ArrayType<Int>(10, new Int(), Radix.Binary, ExternalAccess.ReadOnly, "This is a test");

            type.Select(e => e.Radix).Should().AllBeEquivalentTo(Radix.Binary);
            type.Select(e => e.ExternalAccess).Should().AllBeEquivalentTo(ExternalAccess.ReadOnly);
            type.Select(e => e.Description).Should().AllBeEquivalentTo("This is a test");
        }

        [Test]
        public void IndexGetter_OneDimensionInvalidIndex_ShouldThrowArgumentOutOfRangeException()
        {
            var array = new ArrayType<Bool>(5);

            FluentActions.Invoking(() => array[6]).Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage($"The provided index '[6]' is outside the bounds of the array. (Parameter 'index')");
        }

        [Test]
        public void IndexGetter_TwoDimensionInvalidIndex_ShouldThrowArgumentOutOfRangeException()
        {
            var array = new ArrayType<Bool>(new Dimensions(5, 5));

            FluentActions.Invoking(() => array[6, 0]).Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage($"The provided index '[6,0]' is outside the bounds of the array. (Parameter 'index')");
        }

        [Test]
        public void IndexGetter_ThreeDimensionInvalidIndex_ShouldThrowArgumentOutOfRangeException()
        {
            var array = new ArrayType<Bool>(new Dimensions(5, 5, 5));

            FluentActions.Invoking(() => array[1, 2, 6]).Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage($"The provided index '[1,2,6]' is outside the bounds of the array. (Parameter 'index')");
        }

        [Test]
        public void IndexGetter_OneDimensionsValidIndex_ShouldReturnExpectedMember()
        {
            var array = new ArrayType<Bool>(5);

            var first = array[0];

            first.Should().NotBeNull();
            first.Name.Should().Be("[0]");
            first.DataType.Should().BeOfType<Bool>();
            first.Dimensions.Should().Be(Dimensions.Empty);
            first.Radix.Should().Be(Radix.Decimal);
            first.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            first.Description.Should().BeEmpty();
        }

        [Test]
        public void IndexGetter_TwoDimensionsValidIndex_ShouldReturnExpectedMember()
        {
            var array = new ArrayType<Bool>(new Dimensions(5, 5));

            var first = array[1, 2];

            first.Should().NotBeNull();
            first.Name.Should().Be("[1,2]");
            first.DataType.Should().BeOfType<Bool>();
            first.Dimensions.Should().Be(Dimensions.Empty);
            first.Radix.Should().Be(Radix.Decimal);
            first.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            first.Description.Should().BeEmpty();
        }

        [Test]
        public void Instantiate_WhenCalled_ShouldBeDifferent()
        {
            var array = new ArrayType<Bool>(5);

            var instance = array.Instantiate();

            instance.Should().NotBeSameAs(array);
        }

        [Test]
        public void Instantiate_WhenCalled_ShouldBeSameType()
        {
            var array = new ArrayType<Bool>(5);

            var instance = array.Instantiate();

            instance.Should().BeOfType<ArrayType<Bool>>();
        }

        [Test]
        public void GetEnumerator_WhenCalled_ShouldNotBeNull()
        {
            var array = new ArrayType<Bool>(5);

            array.GetEnumerator().Should().NotBeNull();
        }


        [Test]
        public void GetEnumerator_AsEnumerable_ShouldNotBeNull()
        {
            var array = (IEnumerable)new ArrayType<Bool>(5);

            array.GetEnumerator().Should().NotBeNull();
        }
    }
}