using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using L5Sharp.Comparers;
using L5Sharp.Core;
using L5SharpTests;
using NUnit.Framework;

namespace L5Sharp.Querying.Tests
{
    [TestFixture]
    public class RungQueryTests
    {
        private static readonly string RungExample1 =
            Path.Combine(Environment.CurrentDirectory, @"RungExamples\RungExample1.xml");

        [Test]
        public void Flatten_WhenCalled_ShouldNotBeACompleteDisaster()
        {
            var context = LogixContent.Load(Known.Test);

            var rungs = context.Rungs(q => q.InProgram("MainProgram").Flatten()).ToList();

            rungs.Should().NotBeEmpty();
        }

        [Test]
        public void Flatten_RungExample1_PleaseGod()
        {
            var context = LogixContent.Load(RungExample1);

            var rungs = context.Rungs(q => q.Flatten()).ToList();

            rungs.Should().NotBeEmpty();
        }

        [Test]
        public void InProgram_WhenCalled_ShouldNotBeEmpty()
        {
            var context = LogixContent.Load(Known.Test);

            var rungs = context.Rungs(q => q.InProgram("MainProgram")).ToList();

            rungs.Should().NotBeEmpty();
        }

        [Test]
        public void InProgram_NullName_ShouldBeEmpty()
        {
            var context = LogixContent.Load(Known.Test);

            var rungs = context.Rungs(q => q.InProgram(null!)).ToList();

            rungs.Should().BeEmpty();
        }

        [Test]
        public void InRange_RangeEqual_ShouldOnlyHaveRungsWithThatNumber()
        {
            var context = LogixContent.Load(Known.Test);

            var rungs = context.Rungs(q => q.InRange(1, 1)).ToList();

            rungs.Should().NotBeEmpty();
            rungs.Should().AllSatisfy(r => r.Number.Should().Be(1));
        }

        [Test]
        public void InRange_NegativeNumber_ShouldStillWorkAndReturnRungsInRange()
        {
            var context = LogixContent.Load(Known.Test);

            var rungs = context.Rungs(q => q.InRange(-1, 2)).ToList();

            rungs.Should().NotBeEmpty();
            rungs.Should().AllSatisfy(r => r.Number.Should().BeInRange(-1, 2));
        }

        [Test]
        public void InRange_LastLessThanFirst_ShouldBeEmptyBecauseThatMakesNoSense()
        {
            var context = LogixContent.Load(Known.Test);

            var rungs = context.Rungs(q => q.InRange(2, 1)).ToList();

            rungs.Should().BeEmpty();
        }

        [Test]
        public void InRange_ValidRange_NumbersShouldBeInRange()
        {
            var context = LogixContent.Load(Known.Test);

            var rungs = context.Rungs(q => q.InRange(1, 2)).ToList();

            rungs.Should().NotBeEmpty();
            rungs.Should().AllSatisfy(r => r.Number.Should().BeInRange(1, 2));
        }

        [Test]
        public void InRoutine_ValidRoutine_ShouldNotBeEmpty()
        {
            var context = LogixContent.Load(Known.Test);

            var rungs = context.Rungs(q => q.InRoutine("Main")).ToList();

            rungs.Should().NotBeEmpty();
        }

        [Test]
        public void InRoutine_NullName_ShouldBeEmpty()
        {
            var context = LogixContent.Load(Known.Test);

            var rungs = context.Rungs(q => q.InRoutine(null!)).ToList();

            rungs.Should().BeEmpty();
        }

        [Test]
        public void WithInstruction_NullName_ShouldBeEmpty()
        {
            var context = LogixContent.Load(Known.Test);

            var rungs = context.Rungs(q => q.WithInstruction(null!)).ToList();

            rungs.Should().BeEmpty();
        }

        [Test]
        public void WithInstruction_InvalidName_ShouldBeEmpty()
        {
            var context = LogixContent.Load(Known.Test);

            var rungs = context.Rungs(q => q.WithInstruction("Fake")).ToList();

            rungs.Should().BeEmpty();
        }

        [Test]
        public void WithInstruction_ValidName_ShouldBeEmpty()
        {
            var context = LogixContent.Load(Known.Test);

            var rungs = context.Rungs(q => q.WithInstruction("XIC")).ToList();

            rungs.Should().NotBeEmpty();
        }

        [Test]
        public void WithTag_BaseNameComparerValidName_ShouldNotBeEmpty()
        {
            var context = LogixContent.Load(Known.Test);

            var rungs = context.Rungs(q => q.WithTag("TestComplexTag", TagNameComparer.BaseName)).ToList();

            rungs.Should().NotBeEmpty();
            rungs.Should().AllSatisfy(r => r.Text.ToString().Should().Contain("TestComplexTag"));
        }
        
        [Test]
        public void WithTag_NullName_ShouldBeEmpty()
        {
            var context = LogixContent.Load(Known.Test);

            var rungs = context.Rungs(q => q.WithTag(null!));

            rungs.Should().BeEmpty();
        }

        [Test]
        public void WithTag_ValidName_ShouldNotBeEmpty()
        {
            var context = LogixContent.Load(Known.Test);

            var rungs = context.Rungs(q => q.WithTag("SimpleSint")).ToList();

            rungs.Should().NotBeEmpty();
            rungs.Should().AllSatisfy(r => r.Text.ToString().Should().Contain("SimpleSint"));
        }
        
        [Test]
        public void WithTag_ValidBaseNameWithOnRungsNoReferenceToJustBase_ShouldBeEmpty()
        {
            var context = LogixContent.Load(Known.Test);

            var rungs = context.Rungs(q => q.WithTag("TestComplexTag"));

            rungs.Should().BeEmpty();
        }
        
        [Test]
        public void WithTags_NullCollection_ShouldThrowArgumentNullException()
        {
            var context = LogixContent.Load(Known.Test);

            FluentActions.Invoking(() => context.Rungs(q => q.WithTags(null!))).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void WithTags_ValidTags_ShouldNotBeEmpty()
        {
            var context = LogixContent.Load(Known.Test);
            var tags = new List<TagName>
            {
                "SimpleSint",
                "TestComplexTag"
            };

            var rungs = context.Rungs(q => q.WithTags(tags)).ToList();

            rungs.Should().NotBeEmpty();
        }
    }
}