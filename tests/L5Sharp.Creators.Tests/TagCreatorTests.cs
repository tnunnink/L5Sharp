using System;
using System.Collections.Generic;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;
using NUnit.Framework;

namespace L5Sharp.Creators.Tests
{
    public class TagCreatorTests
    {
        [Test]
        public void Create_NullName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Tag.Create(null!, new BOOL())).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Create_EmptyName_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => Tag.Create(string.Empty, new BOOL())).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Create_NullType_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Tag.Create("Test", (IDataType)null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Create_NameAndGenericType_ShouldNotBeNull()
        {
            var dataType = (IDataType)new TIMER();

            var tag = Tag.Create("Test", dataType);

            tag.Should().NotBeNull();
        }

        [Test]
        public void Create_NameAndType_ShouldNotBeNull()
        {
            var tag = Tag.Create("Test", new BOOL());

            tag.Should().NotBeNull();
        }

        [Test]
        public void Create_Name_ShouldNotBeNull()
        {
            var tag = Tag.Create<BOOL>("Test");

            tag.Should().NotBeNull();
        }

        [Test]
        public void CreateArray_NullName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Tag.Create(null!, new BOOL(), 10)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void CreateArray_EmptyName_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => Tag.Create(string.Empty, new BOOL(), 10)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void CreateArray_NullType_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Tag.Create("Test", (IDataType)null!, 10)).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void CreateArray_NameAndTypeAndDimensions_ShouldNotBeNull()
        {
            var tag = Tag.Create("Test", new BOOL(), new Dimensions(5));

            tag.Should().NotBeNull();
        }

        [Test]
        public void CreateArray_NameAndTypeAndDimensions_DataTypeShouldBeArrayType()
        {
            var tag = Tag.Create("Test", new BOOL(), new Dimensions(5));

            tag.DataType.Should().BeOfType<ArrayType<BOOL>>();
        }

        [Test]
        public void CreateArray_NameAndGenericTypeAndDimensions_DataTypeShouldBeArrayIDataType()
        {
            var tag = Tag.Create("Test", (IDataType)new BOOL(), new Dimensions(5));

            tag.DataType.Should().BeOfType<ArrayType<IDataType>>();
        }

        [Test]
        public void CreateArray_NameAndDimensions_ShouldNotBeNull()
        {
            var tag = Tag.Create<BOOL>("Test", 10);

            tag.Should().NotBeNull();
        }

        [Test]
        public void CreateArray_NameAndDimensions_ShouldHaveExpectedProperties()
        {
            var tag = Tag.Create<BOOL>("Test", 10);

            tag.Name.Should().Be("Test");
            tag.DataType.Name.Should().Be("BOOL");
            tag.DataType.Dimensions.Should().BeEquivalentTo(new Dimensions(10));
            tag.Dimensions.Should().BeEquivalentTo(new Dimensions(10));
            tag.Radix.Should().Be(Radix.Decimal);
            tag.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
        }

        [Test]
        public void Build_Valid_ShouldNotBeNull()
        {
            var tag = Tag.Build<DINT>("Test").Create();

            tag.Should().NotBeNull();
        }

        [Test]
        public void Build_Valid_PropertiesShouldBeExpected()
        {
            var tag = Tag.Build<DINT>("Test").Create();

            tag.Name.Should().Be("Test");
            tag.DataType.Should().BeOfType<DINT>();
            tag.Dimensions.Should().Be(Dimensions.Empty);
            tag.Radix.Should().Be(Radix.Decimal);
            tag.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            tag.Description.Should().BeEmpty();
            tag.Usage.Should().Be(TagUsage.Null);
            tag.TagType.Should().Be(TagType.Base);
            tag.Constant.Should().BeFalse();
        }

        [Test]
        public void Build_WithDimensions_ShouldHaveExpectedValue()
        {
            var tag = Tag.Build<DINT>("Test").WithDimensions(10).Create();

            tag.DataType.Should().BeOfType<ArrayType<DINT>>();
            tag.Dimensions.Should().Be(new Dimensions(10));
        }

        [Test]
        public void Build_WithRadix_ShouldHaveExpectedValue()
        {
            var tag = Tag.Build<DINT>("Test").WithRadix(Radix.Binary).Create();

            tag.Radix.Should().Be(Radix.Binary);
        }

        [Test]
        public void Build_WithAccess_ShouldHaveExpectedValue()
        {
            var tag = Tag.Build<DINT>("Test").WithAccess(ExternalAccess.ReadOnly).Create();

            tag.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
        }

        [Test]
        public void Build_WithDescription_ShouldHaveExpectedValue()
        {
            var tag = Tag.Build<DINT>("Test").WithDescription("This is a test").Create();

            tag.Description.Should().Be("This is a test");
        }

        [Test]
        public void Build_WithUsage_ShouldHaveExpectedValue()
        {
            var tag = Tag.Build<DINT>("Test").WithUsage(TagUsage.Input).Create();

            tag.Usage.Should().Be(TagUsage.Input);
        }

        [Test]
        public void Build_WithData_ShouldHaveExpectedValue()
        {
            var tag = Tag.Build<DINT>("Test").WithData(new DINT(10)).Create();

            tag.Value.Should().Be(10);
        }

        [Test]
        public void Build_WithDataTimer_ShouldHaveExpectedValue()
        {
            var tag = Tag.Build<TIMER>("Test").WithData(new TIMER(5000)).Create();

            tag.Member(m => m.PRE).Value.Should().Be(5000);
        }

        [Test]
        public void Build_IsAliasFor_ShouldHaveExpectedValue()
        {
            var tag = Tag.Build<DINT>("Test").IsAliasFor("SomeOtherTagName").Create();

            tag.Alias.Should().Be("SomeOtherTagName");
        }

        [Test]
        public void Build_IsConstant_ShouldHaveExpectedValue()
        {
            var tag = Tag.Build<DINT>("Test").IsConstant().Create();

            tag.Constant.Should().BeTrue();
        }

        [Test]
        public void Build_ArrayWithRadix_ShouldHaveExpectedValue()
        {
            var tag = Tag.Build<DINT>("Test")
                .WithDimensions(5)
                .WithRadix(Radix.Binary)
                .Create();

            tag.Radix.Should().Be(Radix.Binary);
        }

        [Test]
        public void Build_ArrayWithAccess_ShouldHaveExpectedValue()
        {
            var tag = Tag.Build<DINT>("Test")
                .WithDimensions(5)
                .WithAccess(ExternalAccess.ReadOnly)
                .Create();

            tag.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
        }

        [Test]
        public void Build_ArrayWithDescription_ShouldHaveExpectedValue()
        {
            var tag = Tag.Build<DINT>("Test")
                .WithDimensions(5)
                .WithDescription("This is a test")
                .Create();

            tag.Description.Should().Be("This is a test");
        }

        [Test]
        public void Build_ArrayWithUsage_ShouldHaveExpectedValue()
        {
            var tag = Tag.Build<DINT>("Test")
                .WithDimensions(5)
                .WithUsage(TagUsage.Input)
                .Create();

            tag.Usage.Should().Be(TagUsage.Input);
        }

        [Test]
        public void Build_ArrayWithData_ShouldHaveExpectedValue()
        {
            var data = new List<DINT> { new(1), new(2), new(3) }.ToArrayType();

            var tag = Tag.Build<DINT>("Test")
                .WithDimensions(5)
                .WithData(data)
                .Create();

            tag[0].Value.Should().Be(1);
            tag[1].Value.Should().Be(2);
            tag[2].Value.Should().Be(3);
        }

        [Test]
        public void Build_ArrayIsAliasFor_ShouldHaveExpectedValue()
        {
            var tag = Tag.Build<DINT>("Test")
                .WithDimensions(5)
                .IsAliasFor("SomeOtherTagName")
                .Create();

            tag.Alias.Should().Be("SomeOtherTagName");
        }

        [Test]
        public void Build_ArrayIsConstant_ShouldHaveExpectedValue()
        {
            var tag = Tag.Build<DINT>("Test")
                .WithDimensions(5)
                .IsConstant()
                .Create();

            tag.Constant.Should().BeTrue();
        }
    }
}