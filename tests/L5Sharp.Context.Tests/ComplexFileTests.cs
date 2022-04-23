using System.Diagnostics;
using System.Linq;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.L5X;
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

            var context = LogixContext.Load(Known.Template);

            stopwatch.Stop();

            context.Should().NotBeNull();
        }

        [Test]
        public void DataTypes_GetAll_ShouldNotBeEmpty()
        {
            var context = LogixContext.Load(Known.Template);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var dataTypes = context.DataTypes().All().ToList();

            stopwatch.Stop();

            dataTypes.Should().NotBeEmpty();
        }

        [Test]
        public void Tags_GetAll_ShouldNotBeEmpty()
        {
            var context = LogixContext.Load(Known.Template);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var tags = context.Tags().All().ToList();

            stopwatch.Stop();

            tags.Should().NotBeEmpty();
        }

        [Test]
        public void Modules_GetAll_ShouldNotBeEmpty()
        {
            var context = LogixContext.Load(Known.Template);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var modules = context.Modules().All().ToList();

            stopwatch.Stop();

            modules.Should().NotBeEmpty();
        }

        [Test]
        public void Modules_GetAllTagMembers_ShouldNotBeNull()
        {
            var context = LogixContext.Load(Known.Template);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var module = context.Modules().Find("INJ_Module_3");

            var tags = module?.Tags;

            var members = tags?.SelectMany(t => t.Members());

            stopwatch.Stop();

            members.Should().NotBeNull();
        }

        [Test]
        public void Modules_GetSpecificTagMember_ShouldNotBeNull()
        {
            var context = LogixContext.Load(Known.Template);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var inputTag = context.Modules().Find("INJ_Module_3")?.Input;

            var ch01 = inputTag?.Member("Ch01");

            stopwatch.Stop();

            ch01.Should().NotBeNull();

            ch01?.Description.Should().NotBeEmpty();
        }

        [Test]
        public void Rungs_WithInstruction_ShouldBeSomewhatFastHopefully()
        {
            var context = LogixContext.Load(Known.Template);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var rungs = context.Rungs(q => q.WithInstruction("XIC")).ToList();

            stopwatch.Stop();

            rungs.Should().NotBeEmpty();
        }

        [Test]
        public void Rungs_WithTag_ShouldBeSomewhatFastHopefully()
        {
            var context = LogixContext.Load(Known.Template);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var rungs = context.Rungs(q => q.WithTag("FIO_MPLC:4:I")).ToList();

            stopwatch.Stop();

            rungs.Should().NotBeEmpty();
        }

        [Test]
        public void BufferTagTesting()
        {
            var context = LogixContext.Load(Known.Template);

            var bufferPairs = context.Rungs(q => q
                    .WithTag("FIO_MPLC:4:I")
                    .Flatten()
                    .WithInstruction("MOV"))
                .ToList()
                .SelectMany(r => r.Text.Instructions().Where(i => i.Name == "MOV"))
                .Select(i => new
                {
                    IOTag = new TagName(i.Operands.ToList()[0].ToString()),
                    BufferTag = new TagName(i.Operands.ToList()[1].ToString()),
                }).ToList();

            bufferPairs.Should().NotBeEmpty();
        }
    }
}