using FluentAssertions;

namespace L5Sharp.Tests.Core.Components;

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
        instruction.Parameters.Should().HaveCount(2);
        instruction.LocalTags.Should().BeEmpty();
        instruction.Routines.Should().HaveCount(1);
    }

    [Test]
    public void New_Override_ShouldHaveExpectedValues()
    {
        var instruction = new AddOnInstruction()
        {
            Name = "Test",
            Description = "This is another test",
            Class = ComponentClass.Standard,
            Revision = new Revision(1, 2),
            RevisionExtension = "This is the revision extension",
            RevisionNote = "These are notes about the revision",
            Vendor = "Test Vendor",
            ExecutePreScan = true,
            ExecutePostScan = true,
            ExecuteEnableInFalse = true,
            CreatedDate = DateTime.Now,
            CreatedBy = "Test User",
            EditedDate = DateTime.Now,
            EditedBy = "Test User",
            SoftwareRevision = new Revision(2, 3),
            AdditionalHelpText = "This is additional help text",
            IsEncrypted = true,
            Parameters = new LogixContainer<Parameter>(),
            LocalTags = new LogixContainer<LocalTag>(),
            Routines = new LogixContainer<Routine>()
        };

        instruction.Name.Should().Be("Test");
        instruction.Description.Should().Be("This is another test");
        instruction.Class.Should().Be(ComponentClass.Standard);
        instruction.Revision.Should().BeEquivalentTo(new Revision(1, 2));
        instruction.RevisionExtension.Should().Be("This is the revision extension");
        instruction.RevisionNote.Should().Be("These are notes about the revision");
        instruction.Vendor.Should().Be("Test Vendor");
        instruction.ExecutePreScan.Should().BeTrue();
        instruction.ExecutePostScan.Should().BeTrue();
        instruction.ExecuteEnableInFalse.Should().BeTrue();
        instruction.CreatedDate.Should().BeWithin(TimeSpan.FromSeconds(1));
        instruction.CreatedBy.Should().Be("Test User");
        instruction.EditedDate.Should().BeWithin(TimeSpan.FromSeconds(1));
        instruction.EditedBy.Should().Be("Test User");
        instruction.SoftwareRevision.Should().BeEquivalentTo(new Revision(2, 3));
        instruction.AdditionalHelpText.Should().Be("This is additional help text");
        instruction.IsEncrypted.Should().BeTrue();
        instruction.Parameters.Should().BeOfType<LogixContainer<Parameter>>();
        instruction.LocalTags.Should().BeOfType<LogixContainer<LocalTag>>();
        instruction.Routines.Should().BeOfType<LogixContainer<Routine>>();
    }

    [Test]
    public Task New_Default_ShouldBeValid()
    {
        var instruction = new AddOnInstruction();

        var xml = instruction.Serialize().ToString();

        return VerifyXml(xml)
            .ScrubMember("CreatedDate")
            .ScrubMember("CreatedBy")
            .ScrubMember("EditedDate")
            .ScrubMember("EditedBy");
    }

    [Test]
    public Task Export_WhenCalled_ShouldBeVerified()
    {
        var instruction = new AddOnInstruction();

        var content = instruction.Export();

        return VerifyXml(content.Serialize().ToString())
            .ScrubMember("ProjectCreationDate")
            .ScrubMember("LastModifiedDate")
            .ScrubMember("CreatedDate")
            .ScrubMember("CreatedDate")
            .ScrubMember("CreatedBy")
            .ScrubMember("EditedDate")
            .ScrubMember("EditedBy")
            .ScrubMember("ExportDate")
            .ScrubMember("Owner");
    }

    [Test]
    public Task Serialize_WithRungsAndTag_ShouldBeVerified()
    {
        var aoi = new AddOnInstruction("Test");
        aoi.Logic.Content<Rung>().Add(new Rung("XIC(Test)OTL(Test);"));
        aoi.Logic.Content<Rung>().Add(new Rung("XIC(Test)OTL(Test);"));
        aoi.Logic.Content<Rung>().Add(new Rung("XIC(Test)OTL(Test);"));
        aoi.Logic.Content<Rung>().Add(new Rung("XIC(Test)OTL(Test);"));
        aoi.LocalTags.Add(new LocalTag { Name = "Test", Value = true });

        var xml = aoi.Serialize().ToString();

        return VerifyXml(xml)
            .ScrubMember("CreatedDate")
            .ScrubMember("CreatedBy")
            .ScrubMember("EditedDate")
            .ScrubMember("EditedBy");
    }

    [Test]
    public void ToTag_InstructionWithValidParameters_ShouldBeExpected()
    {
        var content = L5X.Empty();

        var aoi = new AddOnInstruction("MyAoi");
        aoi.Parameters.Add(new Parameter("Param01", new DINT(123)));
        aoi.Parameters.Add(new Parameter("Param02", new BOOL(true)));
        aoi.Parameters.Add(new Parameter("Param02", new REAL(1.23f)));

        var tag = aoi.ToTag("MyAoiTag");

        var referenceTags = aoi.Parameters
            .Where(p => p.Usage == TagUsage.InOut)
            .Select(p => new Tag(p.Name, new ComplexData(p.DataType!)));

        content.Tags.Add(tag);
        content.Tags.AddRange(referenceTags);


        tag.Should().NotBeNull();
        tag.Name.Should().Be("MyAoiTag");
        tag.Value.Should().BeOfType<ComplexData>();
        tag.Value.Name.Should().Be("MyAoi");
        tag.Members().ToList().Should().HaveCount(6);
    }

    [Test]
    public void ToText_ValidArguments_ShouldBeExpected()
    {
        var aoi = new AddOnInstruction("TestAOI");
        var text = aoi.ToText("TestAoiTag", "Param1", "Param2", 123, 0);
        text.Should().Be("TestAOI(TestAoiTag,Param1,Param2,123,0)");
    }

    /*[Test]
    public void Example()
    {
        var content = L5X.Load("PathToFile");

        var aoi = content.Instructions.Get("AOI_OB16_Mapping");

        var aoiTag = aoi.ToTag("AOI_OB16_Mapping_Tag");

        var aoiInOutTags = aoi.Parameters
            .Where(p => p.Usage == TagUsage.InOut)
            .Select(p => new Tag(p.Name, new ComplexData(p.DataType!)));

        content.Tags.Add(aoiTag);
        content.Tags.AddRange(aoiInOutTags);

        content.Save("PathToFile");
    }*/
}