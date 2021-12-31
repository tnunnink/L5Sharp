using System;
using System.Linq;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Types.Atomic;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class ProgramTests
    {
        [Test]
        public void Create_NullName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Program.Create(null!))
                .Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void Create_ValidName_ShouldNotBeNull()
        {
            var program = Program.Create("Test");

            program.Should().NotBeNull();
        }
        
        [Test]
        public void Create_ValidName_ShouldHaveExpectedDefaults()
        {
            var program = Program.Create("Test");

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
            var program = Program.Create("Test");
            var tag = Tag.Create<Bool>("TestTag");
            
            program.Tags.Add(tag);

            program.Tags.Should().HaveCount(1);
            program.Tags.Should().Contain(tag);
        }
        
        [Test]
        public void AddRoutine_ValidRoutine_ShouldHaveExpectedRoutine()
        {
            var program = Program.Create("Test");
            var routine = Routine.Create<LadderLogic>("TestRoutine");
            
            program.Routines.Add(routine);

            program.Routines.Should().HaveCount(1);
            program.Routines.Should().Contain(routine);
        }

        [Test]
        public void AddRoutineWithRung_ShouldHaveExpected()
        {
            var program = Program.Create("Test");
            var routine = Routine.Create<LadderLogic>("TestRoutine");
            routine.Content.Add(new Rung("NOP();", "This is a test rung"));
            
            program.Routines.Add(routine);

            program.Routines.FirstOrDefault(r => r.Content.HasContent).Should().NotBeNull();
        }
    }
}