using System.Runtime.CompilerServices;
using FluentAssertions;

namespace L5Sharp.Tests.Components;

[TestFixture]
public class AddOnInstructionTests
{

    [Test]
    public void New_Default_ShouldHaveExpectedValues()
    {
        var instruction = new AddOnInstruction();

        instruction.Name.Should().BeEmpty();
        instruction.Description.Should().BeNull();
        instruction.Class.Should().BeNull();
        instruction.Revision.Should().BeEquivalentTo(new Revision());
        instruction.RevisionExtension.Should().BeNull();
        instruction.RevisionNote.Should().BeNull();
        instruction.Vendor.Should().BeNull();
        instruction.ExecutePreScan.Should().BeFalse();
        instruction.ExecutePostScan.Should().BeFalse();
        instruction.ExecuteEnableInFalse.Should().BeFalse();
        instruction.CreatedDate.Should().BeWithin(TimeSpan.FromSeconds(1));
        instruction.CreatedBy.Should().Be(Environment.UserName);
        instruction.EditedDate.Should().BeWithin(TimeSpan.FromSeconds(1));
        instruction.EditedBy.Should().Be(Environment.UserName);
        instruction.SoftwareRevision.Should().BeNull();
        instruction.AdditionalHelpText.Should().BeNull();
        instruction.IsEncrypted.Should().BeFalse();
        instruction.Parameters.Should().BeEmpty();
        instruction.LocalTags.Should().BeEmpty();
        instruction.Routines.Should().BeEmpty();
    }

    [Test]
    public Task New_Default_ShouldBeValid()
    {
        var instruction = new AddOnInstruction();

        var xml = instruction.Serialize().ToString();

        return VerifyXml(xml);
    }

    [Test]
    public Task Export_WhenCalled_ShouldBeVerified()
    {
        VerifierSettings.AddExtraDatetimeFormat(L5X.DateTimeFormat);
        
        var instruction = new AddOnInstruction();

        var content = instruction.Export();

        return VerifyXml(content.Serialize().ToString());
    }
}