using System.Diagnostics;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Context.Tests
{
    [TestFixture]
    public class TemplateQueryTests
    {
        [Test]
        public void DataTypes_GetAll_ShouldNotBeEmpty()
        {
            var context = L5XContext.Load(Known.Template);

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            var dataTypes = context.DataTypes.GetAll().ToList();

            stopwatch.Stop();

            dataTypes.Should().NotBeEmpty();
        }

        [Test]
        public void Tags_GetAll_ShouldNotBeEmpty()
        {
            var context = L5XContext.Load(Known.Template);
            
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            var tags = context.Tags.GetAll().ToList();

            stopwatch.Stop();

            tags.Should().NotBeEmpty();
        }
    }
}