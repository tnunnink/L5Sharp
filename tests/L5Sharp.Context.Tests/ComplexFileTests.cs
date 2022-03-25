using System.Diagnostics;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Context.Tests
{
    [TestFixture]
    public class ComplexFileTests
    {
        [Test]
        public void KnownTemplateFileShouldExists()
        {
            FileAssert.Exists(Known.Template);
        }
        
        [Test]
        public void Load_ValidFile_ShouldNotBeNull()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            var context = L5XContext.Load(Known.Template);
            
            stopwatch.Stop();

            context.Should().NotBeNull();
        }
        
        [Test]
        public void DataTypes_GetAll_ShouldNotBeEmpty()
        {
            var context = L5XContext.Load(Known.Template);

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            var dataTypes = context.DataTypes().All().ToList();

            stopwatch.Stop();

            dataTypes.Should().NotBeEmpty();
        }

        [Test]
        public void Tags_GetAll_ShouldNotBeEmpty()
        {
            var context = L5XContext.Load(Known.Template);
            
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            var tags = context.Tags().All();

            stopwatch.Stop();

            tags.Should().NotBeEmpty();
        }
        
        [Test]
        public void Modules_GetAll_ShouldNotBeEmpty()
        {
            var context = L5XContext.Load(Known.Template);
            
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            var modules = context.Modules().All().ToList();

            stopwatch.Stop();

            modules.Should().NotBeEmpty();
        }
        
        [Test]
        public void Modules_GetAllTagMembers_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.Template);
            
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var module = context.Modules().Named("INJ_Module_3"); 
            
            var tags = module?.Tags;

            var members = tags?.SelectMany(t => t.Members());

            stopwatch.Stop();

            members.Should().NotBeNull();
        }
        
        [Test]
        public void Modules_GetSpecificTagMember_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.Template);
            
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var inputTag = context.Modules().Named("INJ_Module_3")?.Tags.Input!;

            var ch01 = inputTag?.Member("Ch01");

            stopwatch.Stop();

            ch01.Should().NotBeNull();

            ch01?.Description.Should().NotBeEmpty();
        }
    }
}