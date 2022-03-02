using System;
using FluentAssertions;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class AddOnInstructionTests
    {
        [Test]
        public void New_Valid_ShouldNotBeNull()
        {
            var instruction = new AddOnInstruction("Test");

            instruction.Should().NotBeNull();
        }

        [Test]
        public void New_NullName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new AddOnInstruction(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void New_Overloaded_ShouldHaveExpectedProperties()
        {
            var instruction = new AddOnInstruction("Test", RoutineType.Rll,
                new Revision(1, 2), "BETA", "This is the beta revision", "ENE",
                true, true, true, 
                DateTime.Today, "Some Person", DateTime.Today.AddDays(-1), "Some Other Person",
                new Revision(32, 1), "This is a test for the addition help text", true, "This is a test");

            instruction.Name.Should().Be("Test");
            instruction.Description.Should().Be("This is a test");
            instruction.Type.Should().Be(RoutineType.Rll);
            instruction.Class.Should().Be(DataTypeClass.AddOnDefined);
            instruction.Family.Should().Be(DataTypeFamily.None);
            instruction.Revision.Should().BeEquivalentTo(new Revision(1, 2));
            instruction.RevisionExtension.Should().Be("BETA");
            instruction.RevisionNote.Should().Be("This is the beta revision");
            instruction.Vendor.Should().Be("ENE");
            instruction.ExecutePreScan.Should().BeTrue();
            instruction.ExecutePostScan.Should().BeTrue();
            instruction.ExecuteEnableInFalse.Should().BeTrue();
            instruction.CreatedDate.Should().Be(DateTime.Today);
            instruction.CreatedBy.Should().Be("Some Person");
            instruction.EditedDate.Should().Be(DateTime.Today.AddDays(-1));
            instruction.EditedBy.Should().Be("Some Other Person");
            instruction.SoftwareRevision.Should().BeEquivalentTo(new Revision(32, 1));
            instruction.AdditionalHelpText.Should().Be("This is a test for the addition help text");
            instruction.IsEncrypted.Should().BeTrue();
            instruction.Parameters.Should().HaveCount(2);
            instruction.LocalTags.Should().NotBeNull();
            instruction.LocalTags.Should().BeEmpty();
            instruction.Routines.Should().HaveCount(1);
            instruction.Logic.Should().NotBeNull();
            instruction.Members.Should().HaveCount(2);
        }
    }
}