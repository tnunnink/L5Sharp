using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.L5X;
using L5Sharp.Querying.Tests.Content;
using NUnit.Framework;

namespace L5Sharp.Querying.Tests
{
    [TestFixture]
    public class BasicInstructionQueryTests
    {
        private const string ValidName = "aoi_Test";
        private const string FakeName = "Fake";

        [Test]
        public void All_WhenCalled_ShouldNotBeEmpty()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.Instructions().All().ToList();

            results.Should().NotBeNull();
            results.Should().NotBeEmpty();
        }

        [Test]
        public void Any_WhenCalled_ShouldBeTrue()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.Instructions().Any();

            result.Should().BeTrue();
        }

        [Test]
        public void Any_NullPredicate_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.Instructions().Any(((Expression<Func<IAddOnInstruction, bool>>)null)!))
                .Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Any_UserClass_ShouldBeTrue()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.Instructions().Any(d => d.Name == ValidName);

            results.Should().BeTrue();
        }

        [Test]
        public void Any_AtomicClass_ShouldBeFalse()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.Instructions().Any(d => d.Name == FakeName);

            results.Should().BeFalse();
        }

        [Test]
        public void Any_ValidName_ShouldBeTrue()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.Instructions().Any(ValidName);

            result.Should().BeTrue();
        }

        [Test]
        public void Any_InvalidName_ShouldBeFalse()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.Instructions().Any(FakeName);

            result.Should().BeFalse();
        }

        [Test]
        public void Any_NullName_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.Instructions().Any(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void First_WhenCalled_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.Instructions().First();

            result.Should().NotBeNull();
        }

        [Test]
        public void First_NullPredicate_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.Instructions().First(null!))
                .Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void First_KnownExisting_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.Instructions().First(d => d.Name == ValidName);

            result.Should().NotBeNull();
        }

        [Test]
        public void First_KnownNonExisting_ShouldThrowInvalidOperationException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.Instructions().First(d => d.Name == FakeName))
                .Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void FirstOrDefault_NullPredicate_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.Instructions().FirstOrDefault(null!))
                .Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void FirstOrDefault_NonEmpty_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.Instructions().FirstOrDefault();

            result.Should().NotBeNull();
        }

        [Test]
        public void FirstOrDefault_KnownExisting_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.Instructions().FirstOrDefault(d => d.Name == ValidName);

            result.Should().NotBeNull();
        }

        [Test]
        public void FirstOrDefault_KnownNonExisting_ShouldBeNull()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.Instructions().FirstOrDefault(d => d.Name == FakeName);

            result.Should().BeNull();
        }

        [Test]
        public void Named_NullName_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.Instructions().Named(((ComponentName)null)!))
                .Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Named_ExistingName_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.Instructions().Named(ValidName);

            result.Should().NotBeNull();
        }

        [Test]
        public void Named_NonExistingName_ShouldBeNull()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.Instructions().Named(FakeName);

            result.Should().BeNull();
        }

        [Test]
        public void Named_SimpleType_ShouldBeExpected()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.Instructions().Named(ValidName);

            result?.Name.Should().Be(ValidName);
        }

        [Test]
        public void Named_NullNameCollection_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.Instructions().Named(((List<ComponentName>)null)!))
                .Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Named_ExistingNameCollection_ShouldHaveExpectedCount()
        {
            var context = L5XContext.Load(Known.Test);
            var names = new List<ComponentName> { ValidName, FakeName };

            var results = context.Instructions().Named(names);

            results.Should().HaveCount(1);
        }

        [Test]
        public void Names_WhenCalled_ShouldNotBeEmpty()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.Instructions().Names().ToList();

            results.Should().NotBeEmpty();
        }

        [Test]
        public void Take_Zero_ShouldBeEmpty()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.Instructions().Take(0);

            results.Should().BeEmpty();
        }

        [Test]
        public void Take_One_ShouldHaveCountOne()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.Instructions().Take(1);

            results.Should().HaveCount(1);
        }

        [Test]
        public void Where_NullPredicate_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.Instructions().Where(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Where_ExistingElementThatSatisfyPredicate_ShouldNotBeEmpty()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.Instructions().Where(d => d.Name.Contains(ValidName, StringComparison.OrdinalIgnoreCase));

            results.Should().NotBeEmpty();
        }

        [Test]
        public void Where_ExistingElementThatSatisfyPredicate_ShouldAllHaveExpectedFilterValue()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.Instructions().Where(d => d.Name.Contains(ValidName, StringComparison.OrdinalIgnoreCase));

            results.All(s => s.Name.Contains(ValidName)).Should().BeTrue();
        }
    }
}