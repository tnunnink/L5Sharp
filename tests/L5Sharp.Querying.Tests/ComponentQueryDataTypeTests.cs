using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.L5X;
using L5SharpTests;
using NUnit.Framework;

namespace L5Sharp.Querying.Tests
{
    [TestFixture]
    public class ComponentQueryDataTypeTests
    {
        [Test]
        public void All_WhenCalled_ShouldNotBeEmpty()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.DataTypes().All().ToList();

            results.Should().NotBeNull();
            results.Should().NotBeEmpty();
        }

        [Test]
        public void Any_HasComponents_ShouldBeTrue()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.DataTypes().Any();

            result.Should().BeTrue();
        }

        [Test]
        public void Any_NullName_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.DataTypes().Any(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Any_InvalidName_ShouldBeFalse()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.DataTypes().Any("FakeType");

            result.Should().BeFalse();
        }

        [Test]
        public void Any_ValidName_ShouldBeTrue()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.DataTypes().Any("SimpleType");

            result.Should().BeTrue();
        }

        [Test]
        public void First_WhenCalled_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.DataTypes().First();

            result.Should().NotBeNull();
        }

        [Test]
        public void FirstOrDefault_NonEmpty_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.DataTypes().FirstOrDefault();

            result.Should().NotBeNull();
        }

        [Test]
        public void Find_NullName_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.DataTypes().Find(((ComponentName)null)!))
                .Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Find_NonExistingName_ShouldBeNull()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.DataTypes().Find("FakeType");

            result.Should().BeNull();
        }

        [Test]
        public void Find_ExistingName_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.DataTypes().Find("SimpleType");

            result.Should().NotBeNull();
        }

        [Test]
        public void Find_SimpleType_ShouldBeExpected()
        {
            var context = L5XContext.Load(Known.Test);

            var component = context.DataTypes().Find("SimpleType");

            component?.Name.Should().Be("SimpleType");
            component?.Description.Should()
                .Be("This is a test data type that contains simple atomic types with an updated description");
            component?.Class.Should().Be(DataTypeClass.User);
            component?.Family.Should().Be(DataTypeFamily.None);
            component?.Members.Should().NotBeEmpty();
        }

        [Test]
        public void Find_ComplexType_ShouldBeExpected()
        {
            var context = L5XContext.Load(Known.Test);

            var component = context.DataTypes().Find("ComplexType");

            component?.Name.Should().Be("ComplexType");
            component?.Description.Should().Be("Test data type with more complex members");
            component?.Class.Should().Be(DataTypeClass.User);
            component?.Family.Should().Be(DataTypeFamily.None);
            component?.Members.Should().NotBeEmpty();
        }

        [Test]
        public void Find_NullNameCollection_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.DataTypes().Find(((IEnumerable<string>)null)!))
                .Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Find_ExistingNameCollection_ShouldHaveExpectedCount()
        {
            var context = L5XContext.Load(Known.Test);
            var names = new List<string> { "SimpleType", "ComplexType", "NestedType" };

            var results = context.DataTypes().Find(names);

            results.Should().HaveCount(3);
        }

        [Test]
        public void Find_NonExistingNameCollection_ShouldBeEmpty()
        {
            var context = L5XContext.Load(Known.Test);
            var names = new List<string> { "FakeType", "DoesNotExist", "NotRealType" };

            var results = context.DataTypes().Find(names);

            results.Should().BeEmpty();
        }

        [Test]
        public void Names_WhenCalled_ShouldNotBeEmpty()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.DataTypes().Names().ToList();

            results.Should().NotBeEmpty();
        }

        [Test]
        public void Take_Zero_ShouldBeEmpty()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.DataTypes().Take(0);

            results.Should().BeEmpty();
        }

        [Test]
        public void Take_One_ShouldHaveCountTen()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.DataTypes().Take(1);

            results.Should().HaveCount(1);
        }
    }
}