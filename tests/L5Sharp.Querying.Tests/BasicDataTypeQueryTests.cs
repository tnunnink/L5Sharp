using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.L5X;
using L5Sharp.Querying.Tests.Content;
using NUnit.Framework;

namespace L5Sharp.Querying.Tests
{
    [TestFixture]
    public class BasicDataTypeQueryTests
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
        public void Any_WhenCalled_ShouldBeTrue()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.DataTypes().Any();

            result.Should().BeTrue();
        }

        [Test]
        public void Any_NullPredicate_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.DataTypes().Any(((Expression<Func<IComplexType, bool>>)null)!))
                .Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Any_UserClass_ShouldBeTrue()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.DataTypes().Any(d => d.Class == DataTypeClass.User);

            results.Should().BeTrue();
        }

        [Test]
        public void Any_AtomicClass_ShouldBeFalse()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.DataTypes().Any(d => d.Class == DataTypeClass.Atomic);

            results.Should().BeFalse();
        }

        [Test]
        public void Any_ValidName_ShouldBeTrue()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.DataTypes().Any("SimpleType");

            result.Should().BeTrue();
        }

        [Test]
        public void Any_InvalidName_ShouldBeFalse()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.DataTypes().Any("FakeType");

            result.Should().BeFalse();
        }

        [Test]
        public void Any_NullName_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.DataTypes().Any(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void First_WhenCalled_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.DataTypes().First();

            result.Should().NotBeNull();
        }

        [Test]
        public void First_NullPredicate_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.DataTypes().First(null!))
                .Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void First_KnownExisting_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.DataTypes().First(d => d.Class == DataTypeClass.User);

            result.Should().NotBeNull();
        }

        [Test]
        public void First_KnownNonExisting_ShouldThrowInvalidOperationException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.DataTypes().First(d => d.Class == DataTypeClass.Predefined))
                .Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void First_FamilyProperty_ShouldHaveExpectedFamily()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.DataTypes().First(d => d.Family == DataTypeFamily.String);

            result.Should().NotBeNull();
            result.Family.Should().Be(DataTypeFamily.String);
        }

        [Test]
        public void FirstOrDefault_NullPredicate_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.DataTypes().FirstOrDefault(null!))
                .Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void FirstOrDefault_NonEmpty_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.DataTypes().FirstOrDefault();

            result.Should().NotBeNull();
        }

        [Test]
        public void FirstOrDefault_KnownExisting_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.DataTypes().FirstOrDefault(d => d.Class == DataTypeClass.User);

            result.Should().NotBeNull();
        }

        [Test]
        public void FirstOrDefault_KnownNonExisting_ShouldBeNull()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.DataTypes().FirstOrDefault(d => d.Class == DataTypeClass.Predefined);

            result.Should().BeNull();
        }

        [Test]
        public void Named_NullName_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.DataTypes().Named(((ComponentName)null)!))
                .Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Named_ExistingName_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.DataTypes().Named("SimpleType");

            result.Should().NotBeNull();
        }

        [Test]
        public void Named_NonExistingName_ShouldBeNull()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.DataTypes().Named("FakeType");

            result.Should().BeNull();
        }

        [Test]
        public void Named_SimpleType_ShouldBeExpected()
        {
            var context = L5XContext.Load(Known.Test);

            var component = context.DataTypes().Named("SimpleType");

            component?.Name.Should().Be("SimpleType");
            component?.Description.Should()
                .Be("This is a test data type that contains simple atomic types with an updated description");
            component?.Class.Should().Be(DataTypeClass.User);
            component?.Family.Should().Be(DataTypeFamily.None);
            component?.Members.Should().NotBeEmpty();
        }

        [Test]
        public void Named_ComplexType_ShouldBeExpected()
        {
            var context = L5XContext.Load(Known.Test);

            var component = context.DataTypes().Named("ComplexType");

            component?.Name.Should().Be("ComplexType");
            component?.Description.Should().Be("Test data type with more complex members");
            component?.Class.Should().Be(DataTypeClass.User);
            component?.Family.Should().Be(DataTypeFamily.None);
            component?.Members.Should().NotBeEmpty();
        }

        [Test]
        public void Named_NullNameCollection_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.DataTypes().Named(((List<ComponentName>)null)!))
                .Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Named_ExistingNameCollection_ShouldHaveExpectedCount()
        {
            var context = L5XContext.Load(Known.Test);
            var names = new List<ComponentName> { "SimpleType", "ComplexType", "NestedType" };

            var results = context.DataTypes().Named(names);

            results.Should().HaveCount(3);
        }

        [Test]
        public void Named_NonExistingNameCollection_ShouldBeEmpty()
        {
            var context = L5XContext.Load(Known.Test);
            var names = new List<ComponentName> { "FakeType", "DoesNotExist", "NotRealType" };

            var results = context.DataTypes().Named(names);

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
        public void Take_Ten_ShouldHaveCountTen()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.DataTypes().Take(5);

            results.Should().HaveCount(5);
        }

        [Test]
        public void Where_NullPredicate_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.DataTypes().Where(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Where_ExistingElementThatSatisfyPredicate_ShouldNotBeEmpty()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.DataTypes().Where(d => d.Name.Contains("Type")).ToList();

            results.Should().NotBeEmpty();
        }

        [Test]
        public void Where_ExistingElementThatSatisfyPredicate_ShouldAllHaveExpectedFilterValue()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.DataTypes().Where(d => d.Name.Contains("Type")).ToList();

            results.All(s => s.Name.Contains("Type")).Should().BeTrue();
        }

        [Test]
        public void Where_DescriptionIsNotNullOrEmpty_ShouldNotBeEmpty()
        {
            var context = L5XContext.Load(Known.Test);

            var components = context.DataTypes().Where(t => !string.IsNullOrEmpty(t.Description)).ToList();

            components.Should().NotBeEmpty();
            components.All(c => !string.IsNullOrEmpty(c.Description)).Should().BeTrue();
        }

        [Test]
        public void DependingOn_Null_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.DataTypes().DependingOn(null!).All()).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void DependingOn_ValidDependentType_ShouldHaveMemberWithType()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.DataTypes().DependingOn("SimpleType").All().ToList();

            results.Should().NotBeEmpty();
            results.All(d => d.Members.Any(m => m.DataType.Name == "SimpleType")).Should().BeTrue();
        }
    }
}