using System;
using System.Linq;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Factories;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class ProgramTests
    {
        [Test]
        public void Create_NullName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new Program(null!))
                .Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void Create_ValidName_ShouldNotBeNull()
        {
            var program = new Program("Test");

            program.Should().NotBeNull();
        }
        
        [Test]
        public void Create_ValidName_ShouldHaveExpectedDefaults()
        {
            var program = new Program("Test");

            program.Name.Should().Be("Test");
            program.Description.Should().BeEmpty();
            program.Type.Should().Be(ProgramType.Normal);
            program.TestEdits.Should().BeFalse();
            program.Disabled.Should().BeFalse();
            program.MainRoutineName.Should().Be(string.Empty);
            program.FaultRoutineName.Should().Be(string.Empty);
            program.UseAsFolder.Should().BeFalse();
            program.Tags.Should().BeEmpty();
            program.Routines.Should().BeEmpty();
        }

        [Test]
        public void AddTag_ValidTag_ShouldHaveExpectedTag()
        {
            var program = new Program("Test");
            var tag = Tag.Create<Bool>("TestTag");
            
            program.Tags.Add(tag);

            program.Tags.Should().HaveCount(1);
            program.Tags.Should().Contain(tag);
        }

        [Test]
        public void AddTag_Duplicate_ShouldThrowException()
        {
            var program = new Program("Test");
            program.Tags.Add(Tag.Create<Counter>("Test"));

            FluentActions.Invoking(() => program.Tags.Add(Tag.Create<Bool>("Test")))
                .Should().Throw<ComponentNameCollisionException>();
        }

        [Test]
        public void AddRoutine_ValidRoutine_ShouldHaveExpectedRoutine()
        {
            var program = new Program("Test");
            var routine = Routine.Create<LadderLogic>("TestRoutine");
            
            program.Routines.Add(routine);

            program.Routines.Should().HaveCount(1);
            program.Routines.Should().Contain(routine);
        }

        [Test]
        public void AddRoutineWithRung_ShouldHaveExpected()
        {
            var program = new Program("Test");
            var routine = Routine.Create<LadderLogic>("TestRoutine");
            routine.Content.Add(new Rung("NOP();", "This is a test rung"));
            
            program.Routines.Add(routine);

            program.Routines.FirstOrDefault(r => r.Content.HasContent).Should().NotBeNull();
        }
    }
}