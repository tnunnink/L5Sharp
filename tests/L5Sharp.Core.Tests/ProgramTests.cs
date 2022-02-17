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
        public void AddTag_Duplicate_ShouldThrowException()
        {
            var program = Program.Create("Test");
            program.Tags.Add(Tag.Create<Counter>("Test"));

            FluentActions.Invoking(() => program.Tags.Add(Tag.Create<Bool>("Test")))
                .Should().Throw<ComponentNameCollisionException>();
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
        
        
        [Test]
        public void TypedEquals_AreEqual_ShouldBeTrue()
        {
            var first = Program.Create("Test");
            var second = Program.Create("Test");

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void TypedEquals_AreSame_ShouldBeTrue()
        {
            var first = Program.Create("Test");

            var result = first.Equals(first);

            result.Should().BeTrue();
        }


        [Test]
        public void TypedEquals_Null_ShouldBeFalse()
        {
            var first = Program.Create("Test");

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = Program.Create("Test");
            var second = Program.Create("Test");

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = Program.Create("Test");

            // ReSharper disable once EqualExpressionComparison for coverage purposes
            var result = first.Equals(first);

            result.Should().BeTrue();
        }


        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var first = Program.Create("Test");

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = (Program)Program.Create("Test");
            var second = (Program)Program.Create("Test");

            var result = first == second;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = (Program)Program.Create("Test");
            var second = (Program)Program.Create("Test");

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var first = (Program)Program.Create("Test");

            var hash = first.GetHashCode();

            hash.Should().NotBe(0);
        }
    }
}