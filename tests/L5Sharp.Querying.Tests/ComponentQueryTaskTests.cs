using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using L5Sharp.L5X;
using L5SharpTests;
using NUnit.Framework;

namespace L5Sharp.Querying.Tests
{
    [TestFixture]
    public class ComponentQueryTaskTests
    {
        private const string ValidName = "Continuous";
        private const string FakeName = "Fake";
        
        [Test]
        public void All_WhenCalled_ShouldNotBeEmpty()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.Tasks().All().ToList();

            results.Should().NotBeNull();
            results.Should().NotBeEmpty();
        }

        [Test]
        public void Any_HasComponents_ShouldBeTrue()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.Tasks().Any();

            result.Should().BeTrue();
        }

        [Test]
        public void Any_NoComponents_ShouldBeFalse()
        {
            var context = L5XContext.Load(Known.Empty);

            var result = context.Tasks().Any();

            result.Should().BeFalse();
        }

        [Test]
        public void Contains_InvalidName_ShouldBeFalse()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.Tasks().Contains(FakeName);

            result.Should().BeFalse();
        }

        [Test]
        public void Contains_ValidName_ShouldBeTrue()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.Tasks().Contains(ValidName);

            result.Should().BeTrue();
        }

        [Test]
        public void Count_HasComponents_ShouldBeGreaterThanZero()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.Tasks().Count();

            result.Should().BeGreaterThan(0);
        }

        [Test]
        public void Count_NoComponents_ShouldBeZero()
        {
            var context = L5XContext.Load(Known.Empty);

            var result = context.Tasks().Count();

            result.Should().Be(0);
        }

        [Test]
        public void Find_NonExistingName_ShouldBeNull()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.Tasks().Find(FakeName);

            result.Should().BeNull();
        }

        [Test]
        public void Find_ExistingName_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.Tasks().Find(ValidName);

            result.Should().NotBeNull();
        }

        [Test]
        public void Find_HasAtLeastOnValidName_ShouldNotBeEmpty()
        {
            var context = L5XContext.Load(Known.Test);
            var names = new List<string> { ValidName, FakeName };

            var results = context.Tasks().Find(names);

            results.Should().NotBeEmpty();
        }

        [Test]
        public void Find_NonExistingNameCollection_ShouldBeEmpty()
        {
            var context = L5XContext.Load(Known.Test);
            var names = new List<string> { FakeName, "DoesNotExist", "NotReal" };

            var results = context.Tasks().Find(names);

            results.Should().BeEmpty();
        }

        [Test]
        public void Get_NullName_ShouldThrowInvalidOperationException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.Tasks().Get(null!)).Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Get_InvalidName_ShouldThrowInvalidOperationException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.Tasks().Get(FakeName)).Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Get_ValidName_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.Tasks().Get(ValidName);

            result.Should().NotBeNull();
        }

        [Test]
        public void Names_HasNames_ShouldNotBeEmpty()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.Tasks().Names().ToList();

            results.Should().NotBeEmpty();
        }

        [Test]
        public void Names_NoNames_ShouldBeEmpty()
        {
            var context = L5XContext.Load(Known.Empty);

            var results = context.Tasks().Names().ToList();

            results.Should().BeEmpty();
        }

        [Test]
        public void Take_Negative_ShouldBeEmpty()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.Tasks().Take(-1);

            results.Should().BeEmpty();
        }

        [Test]
        public void Take_Zero_ShouldBeEmpty()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.Tasks().Take(0);

            results.Should().BeEmpty();
        }

        [Test]
        public void Take_One_ShouldHaveCountTen()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.Tasks().Take(1);

            results.Should().HaveCount(1);
        }
    }
}