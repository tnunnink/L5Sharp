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
        public void Contains_ValidTask_ShouldBeTrue()
        {
            var context = LogixContext.Load(Known.L5X);

            var task = context.Tasks.Contains("Task_01");

            task.Should().BeTrue(); 
        }
        
        [Test]
        public void Get_ValidTask_ShouldNotBeNull()
        {
            var context = LogixContext.Load(Known.L5X);

            var task = context.Tasks.Get("Task_01");

            task.Should().NotBeNull();
        }

        [Test]
        public void GetAll_WhenCalled_ShouldHaveExpectedCount()
        {
            var context = LogixContext.Load(Known.L5X);

            var tasks = context.Tasks.GetAll();

            tasks.Should().HaveCount(3);
        }

        [Test]
        public void FindAll_PriorityValue_ShouldHaveExpectedCount()
        {
            var context = LogixContext.Load(Known.L5X);

            var results = context.Tasks.FindAll(t => t.Priority == 10);
            
            results.Should().HaveCount(4);
        }
        
        [Test]
        public void FindAll_RateValue_ShouldHaveExpectedCount()
        {
            var context = LogixContext.Load(Known.L5X);

            var results = context.Tasks.FindAll(t => t.Rate == 1000);
            
            results.Should().HaveCount(1);
        }
        
        [Test]
        public void FindAll_DescriptionEquals_ShouldHaveExpectedCount()
        {
            var context = LogixContext.Load(Known.L5X);

            var results = context.Tasks.FindAll(t => t.Description == "Test continuous task");
            
            results.Should().HaveCount(1);
        }
        
        [Test]
        public void FindAll_DescriptionContains_ShouldHaveExpectedCount()
        {
            var context = LogixContext.Load(Known.L5X);

            var results = context.Tasks.FindAll(t => t.Description.Contains("Test", StringComparison.OrdinalIgnoreCase));
            
            results.Should().HaveCount(4);
        }

        [Test]
        public void Find_ListContainsDescription_ShouldWork()
        {
            var context = LogixContext.Load(Known.L5X);

            var list = new List<string>
            {
                "This is a test task 01"
            };

            var results = context.Tasks.FindAll(t => list.Contains(t.Description));
            
            results.Should().HaveCount(1);
        }
    }
}