using AutoFixture;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class ProgramTests
    {
        [Test]
        public void New_InvalidName_ShouldThrowComponentNameInvalidException()
        {
            var fixture = new Fixture();

            FluentActions.Invoking(() => new Program(fixture.Create<string>())).Should()
                .Throw<ComponentNameInvalidException>();
        }
        
        [Test]
        public void New_ValidName_ShouldNotBeNull()
        {
            var program = new Program("Test");

            program.Should().NotBeNull();
        }
        
        [Test]
        public void New_ValidName_ShouldHaveExpectedDefaults()
        {
            var program = new Program("Test");

            program.Name.Should().Be("Test");
            program.Type.Should().Be(ProgramType.Normal);
            program.MainRoutineName.Should().BeNull();
            program.FaultRoutineName.Should().BeNull();
            program.UseAsFolder.Should().BeFalse();
            program.TestEdits.Should().BeFalse();
            program.Disabled.Should().BeFalse();
            program.Description.Should().BeNull();
            program.Tags.Should().BeEmpty();
            program.Routines.Should().BeEmpty();
        }
        
        [Test]
        public void New_Overridden_ShouldHaveExpectedValues()
        {
            var program = new Program("Test", "This is a test", "MainRoutine", "FaultRoutine", true, true, true);

            program.Name.Should().Be("Test");
            program.Type.Should().Be(ProgramType.Normal);
            program.MainRoutineName.Should().Be("MainRoutine");
            program.FaultRoutineName.Should().Be("FaultRoutine");
            program.UseAsFolder.Should().BeTrue();
            program.TestEdits.Should().BeTrue();
            program.Disabled.Should().BeTrue();
            program.Description.Should().Be("This is a test");
            program.Tags.Should().BeEmpty();
            program.Routines.Should().BeEmpty();
        }

        [Test]
        public void Enable_WhenCalled_DisabledShouldBeFalse()
        {
            var program = new Program("Test", disabled: true);
            
            program.Enable();

            program.Disabled.Should().BeFalse();
        }
        
        [Test]
        public void Disable_WhenCalled_DisabledShouldBeTrue()
        {
            var program = new Program("Test");
            
            program.Disable();

            program.Disabled.Should().BeTrue();
        }

        [Test]
        public void GetTag_TagExists_ShouldNotBeNull()
        {
            var program = new Program("Test");
            var tag = new Tag("Test", Logix.DataType.Bool);
            program.Tags.Add(tag);

            var result = program.Tags.Get("Test");

            result.Should().NotBeNull();
            result.Name.Should().Be("Test");
        }

        [Test]
        public void GetTag_DoesNotExists_ShouldBeNull()
        {
            var program = new Program("Test");

            var result = program.Tags.Get("Test");

            result.Should().BeNull();
        }

        [Test]
        public void AddTag_ValidName_ShouldNotBeNull()
        {
            var program = new Program("Test");
            var tag = new Tag("Test", Logix.DataType.Bool);

            program.Tags.Add(tag);

            program.Tags.Should().Contain(tag);
        }
        
        [Test]
        public void RemoveTag_Exists_ShouldRemoveFromCollection()
        {
            var program = new Program("Test");
            var tag = new Tag("Test", Logix.DataType.Bool);
            program.Tags.Add(tag);

            program.Tags.Remove(tag.Name);

            program.Tags.Should().NotContain(tag);
        }
    }
}