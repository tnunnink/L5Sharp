using System;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Factories;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;
using NUnit.Framework;

namespace L5Sharp.Factory.Tests
{
    public class TagFactoryTests
    {
        [Test]
        public void Create_NullName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Tag.Create(null!, new Bool())).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Create_EmptyName_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => Tag.Create(string.Empty, new Bool())).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Create_NullType_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Tag.Create("Test", (IDataType)null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Create_NameAndGenericType_ShouldNotBeNull()
        {
            var dataType = (IDataType)new Timer();
            
            var tag = Tag.Create("Test", dataType);

            tag.Should().NotBeNull();
        }

        [Test]
        public void Create_NameAndType_ShouldNotBeNull()
        {
            var tag = Tag.Create("Test", new Bool());

            tag.Should().NotBeNull();
        }

        [Test]
        public void Create_Name_ShouldNotBeNull()
        {
            var tag = Tag.Create<Bool>("Test");

            tag.Should().NotBeNull();
        }
        
        [Test]
        public void CreateArray_NullName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Tag.Create(null!, new Bool(), 10)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void CreateArray_EmptyName_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => Tag.Create(string.Empty, new Bool(), 10)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void CreateArray_NullType_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Tag.Create("Test", (IDataType)null!, 10)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void CreateArray_NameAndTypeAndDimensions_ShouldNotBeNull()
        {
            var tag = Tag.Create("Test", new Bool(), new Dimensions(5));

            tag.Should().NotBeNull();
        }

        [Test]
        public void CreateArray_NameAndTypeAndDimensions_DataTypeShouldBeArrayType()
        {
            var tag = Tag.Create("Test", new Bool(), new Dimensions(5));

            tag.DataType.Should().BeOfType<ArrayType<Bool>>();
        }
        
        [Test]
        public void CreateArray_NameAndGenericTypeAndDimensions_DataTypeShouldBeArrayIDataType()
        {
            var tag = Tag.Create("Test", (IDataType)new Bool(), new Dimensions(5));

            tag.DataType.Should().BeOfType<ArrayType<IDataType>>();
        }

        [Test]
        public void CreateArray_NameAndDimensions_ShouldNotBeNull()
        {
            var tag = Tag.Create<Bool>("Test", 10);

            tag.Should().NotBeNull();
        }

        [Test]
        public void CreateArray_NameAndDimensions_ShouldHaveExpectedProperties()
        {
            var tag = Tag.Create<Bool>("Test", 10);

            tag.Name.Should().Be("Test");
            tag.DataType.Name.Should().Be("BOOL");
            tag.DataType.Dimensions.Should().BeEquivalentTo(new Dimensions(10));
            tag.Dimensions.Should().BeEquivalentTo(new Dimensions(10));
            tag.Radix.Should().Be(Radix.Decimal);
            tag.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
        }
    }
}