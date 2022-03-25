using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Context.Tests
{
    [TestFixture]
    public class TaskRepositoryTests
    {
        [Test]
        public void All_WhenCalled_ShouldHaveExpectedCount()
        {
            var context = L5XContext.Load(Known.L5X);

            var tasks = context.Tasks().All();

            tasks.Should().HaveCount(3);
        }

        [Test]
        public void Any_ValidTask_ShouldBeTrue()
        {
            var context = L5XContext.Load(Known.L5X);

            var task = context.Tasks().Any("Continuous");

            task.Should().BeTrue(); 
        }

        [Test]
        public void Named_ValidTask_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.L5X);

            var task = context.Tasks().Named("Continuous");

            task.Should().NotBeNull();
        }

        [Test]
        public void Where_PriorityValue_ShouldHaveExpectedCount()
        {
            var context = L5XContext.Load(Known.L5X);

            var results = context.Tasks().Where(t => t.Priority == 10);
            
            results.Should().NotBeEmpty();
        }
        
        [Test]
        public void Where_RateValue_ShouldHaveExpectedCount()
        {
            var context = L5XContext.Load(Known.L5X);

            var results = context.Tasks().Where(t => t.Rate >= 1000);
            
            results.Should().HaveCount(0);
        }
        
        [Test]
        public void Where_DescriptionEquals_ShouldHaveExpectedCount()
        {
            var context = L5XContext.Load(Known.L5X);

            var results = context.Tasks().Where(t => t.Description == "Test continuous task");
            
            results.Should().HaveCount(1);
        }
        
        [Test]
        public void Where_DescriptionContains_ShouldHaveExpectedCount()
        {
            var context = L5XContext.Load(Known.L5X);

            var results = context.Tasks().Where(t => t.Description.Contains("Test", StringComparison.OrdinalIgnoreCase));
            
            results.Should().HaveCount(2);
        }

        [Test]
        public void Where_ListContainsDescription_ShouldWork()
        {
            var context = L5XContext.Load(Known.L5X);

            var list = new List<string>
            {
                "TestEvent Task"
            };

            var results = context.Tasks().Where(t => list.Contains(t.Description));
            
            results.Should().HaveCount(1);
        }
    }
}