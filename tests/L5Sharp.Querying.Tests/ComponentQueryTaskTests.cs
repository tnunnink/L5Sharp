using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using L5Sharp.Core;
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
        public void Any_WhenCalled_ShouldBeTrue()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.Tasks().Any();

            result.Should().BeTrue();
        }

        [Test]
        public void Any_ValidName_ShouldBeTrue()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.Tasks().Any(ValidName);

            result.Should().BeTrue();
        }

        [Test]
        public void Any_InvalidName_ShouldBeFalse()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.Tasks().Any(FakeName);

            result.Should().BeFalse();
        }

        [Test]
        public void Any_NullName_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.Tasks().Any(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void First_WhenCalled_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.Tasks().First();

            result.Should().NotBeNull();
        }

        [Test]
        public void FirstOrDefault_NonEmpty_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.Tasks().FirstOrDefault();

            result.Should().NotBeNull();
        }
        
        [Test]
        public void Find_NullName_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.Tasks().Find(((ComponentName)null)!))
                .Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Find_ExistingName_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.Tasks().Find(ValidName);

            result.Should().NotBeNull();
        }

        [Test]
        public void Find_NonExistingName_ShouldBeNull()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.Tasks().Find(FakeName);

            result.Should().BeNull();
        }

        [Test]
        public void Find_SimpleType_ShouldBeExpected()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.Tasks().Find(ValidName);

            result?.Name.Should().Be(ValidName);
        }

        [Test]
        public void Find_NullNameCollection_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.Tasks().Find((ICollection<string>)((List<ComponentName>)null)!))
                .Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Find_ExistingNameCollection_ShouldHaveExpectedCount()
        {
            var context = L5XContext.Load(Known.Test);
            var names = new List<ComponentName> { ValidName, FakeName };

            var results = context.Tasks().Find((ICollection<string>)names);

            results.Should().HaveCount(1);
        }

        [Test]
        public void Names_WhenCalled_ShouldNotBeEmpty()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.Tasks().Names().ToList();

            results.Should().NotBeEmpty();
        }

        [Test]
        public void Take_Zero_ShouldBeEmpty()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.Tasks().Take(0);

            results.Should().BeEmpty();
        }

        [Test]
        public void Take_One_ShouldHaveCountOne()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.Tasks().Take(1);

            results.Should().HaveCount(1);
        }
    }
}