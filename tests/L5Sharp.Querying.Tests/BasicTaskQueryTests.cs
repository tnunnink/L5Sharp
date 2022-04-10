using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.L5X;
using L5SharpTests;
using NUnit.Framework;

namespace L5Sharp.Querying.Tests
{
    [TestFixture]
    public class BasicTaskQueryTests
    {
        private const string ValidName = "Continuous";
        private const string FakeName = "Fake";
        
        [Test]
        public void All_WhenCalled_ShouldNotBeEmpty()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.Tasks().Select().ToList();

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
        public void Any_NullPredicate_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.Tasks().Any(((Expression<Func<ITask, bool>>)null)!))
                .Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Any_UserClass_ShouldBeTrue()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.Tasks().Any(d => d.Name == ValidName);

            results.Should().BeTrue();
        }

        [Test]
        public void Any_AtomicClass_ShouldBeFalse()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.Tasks().Any(d => d.Name == FakeName);

            results.Should().BeFalse();
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
        public void First_NullPredicate_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.Tasks().First(null!))
                .Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void First_KnownExisting_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.Tasks().First(d => d.Name == ValidName);

            result.Should().NotBeNull();
        }

        [Test]
        public void First_KnownNonExisting_ShouldThrowInvalidOperationException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.Tasks().First(d => d.Name == FakeName))
                .Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void FirstOrDefault_NullPredicate_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.Tasks().FirstOrDefault(null!))
                .Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void FirstOrDefault_NonEmpty_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.Tasks().FirstOrDefault();

            result.Should().NotBeNull();
        }

        [Test]
        public void FirstOrDefault_KnownExisting_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.Tasks().FirstOrDefault(d => d.Name == ValidName);

            result.Should().NotBeNull();
        }

        [Test]
        public void FirstOrDefault_KnownNonExisting_ShouldBeNull()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.Tasks().FirstOrDefault(d => d.Name == FakeName);

            result.Should().BeNull();
        }

        [Test]
        public void Named_NullName_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.Tasks().Named(((ComponentName)null)!))
                .Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Named_ExistingName_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.Tasks().Named(ValidName);

            result.Should().NotBeNull();
        }

        [Test]
        public void Named_NonExistingName_ShouldBeNull()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.Tasks().Named(FakeName);

            result.Should().BeNull();
        }

        [Test]
        public void Named_SimpleType_ShouldBeExpected()
        {
            var context = L5XContext.Load(Known.Test);

            var result = context.Tasks().Named(ValidName);

            result?.Name.Should().Be(ValidName);
        }

        [Test]
        public void Named_NullNameCollection_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.Tasks().Named(((List<ComponentName>)null)!))
                .Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Named_ExistingNameCollection_ShouldHaveExpectedCount()
        {
            var context = L5XContext.Load(Known.Test);
            var names = new List<ComponentName> { ValidName, FakeName };

            var results = context.Tasks().Named(names);

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

        [Test]
        public void Where_NullPredicate_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.Tasks().Where(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Where_ExistingElementThatSatisfyPredicate_ShouldNotBeEmpty()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.Tasks().Where(d => d.Name.Contains(ValidName, StringComparison.OrdinalIgnoreCase));

            results.Should().NotBeEmpty();
        }

        [Test]
        public void Where_ExistingElementThatSatisfyPredicate_ShouldAllHaveExpectedFilterValue()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.Tasks().Where(d => d.Name.Contains(ValidName, StringComparison.OrdinalIgnoreCase));

            results.All(s => s.Name.Contains(ValidName)).Should().BeTrue();
        }

        [Test]
        public void Where_DescriptionIsNotNullOrEmpty_ShouldNotBeEmpty()
        {
            var context = L5XContext.Load(Known.Test);

            var components = context.Tasks().Where(t => !string.IsNullOrEmpty(t.Description)).ToList();

            components.Should().NotBeEmpty();
            components.All(c => !string.IsNullOrEmpty(c.Description)).Should().BeTrue();
        }

        [Test]
        public void Where_PriorityValue_ShouldHaveExpectedCount()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.Tasks().Where(t => t.Priority == 10);
            
            results.Should().NotBeEmpty();
        }
        
        [Test]
        public void Where_RateValue_ShouldHaveExpectedCount()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.Tasks().Where(t => t.Rate >= 1000);
            
            results.Should().HaveCount(0);
        }
        
        [Test]
        public void Where_DescriptionEquals_ShouldHaveExpectedCount()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.Tasks().Where(t => t.Description == "Test continuous task");
            
            results.Should().HaveCount(1);
        }
        
        [Test]
        public void Where_DescriptionContains_ShouldHaveExpectedCount()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.Tasks().Where(t => t.Description.Contains("Test", StringComparison.OrdinalIgnoreCase));
            
            results.Should().HaveCount(2);
        }

        [Test]
        public void Where_ListContainsDescription_ShouldWork()
        {
            var context = L5XContext.Load(Known.Test);

            var list = new List<string>
            {
                "TestEvent Task"
            };

            var results = context.Tasks().Where(t => list.Contains(t.Description));
            
            results.Should().HaveCount(1);
        }
    }
}