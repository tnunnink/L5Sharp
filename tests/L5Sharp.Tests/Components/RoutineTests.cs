using System.Xml.Linq;
using FluentAssertions;

namespace L5Sharp.Tests.Components;

[TestFixture]
public class RoutineTests
{
    [Test]
    public void New_Default_ShouldNotBeNull()
    {
        var routine = new Routine();

        routine.Should().NotBeNull();
    }

    [Test]
    public void New_Default_ShouldHaveDefaultValues()
    {
        var routine = new Routine();

        routine.Name.Should().BeEmpty();
        routine.Description.Should().BeNull();
        routine.Type.Should().Be(RoutineType.RLL);
        routine.SheetOrientation.Should().BeNull();
        routine.SheetSize.Should().BeNull();
        routine.OnlineEditType.Should().BeNull();
        routine.Content<Rung>().Should().NotBeNull();
    }

    [Test]
    public void New_StructuredText_ShouldHaveExpectedValues()
    {
        var routine = new Routine(RoutineType.ST);

        routine.Name.Should().BeEmpty();
        routine.Description.Should().BeNull();
        routine.Type.Should().Be(RoutineType.ST);
        routine.Content<Line>().Should().NotBeNull();
        routine.Content<Line>().Should().BeEmpty();
    }

    [Test]
    public void New_FunctionBlockDiagram_ShouldHaveExpectedValues()
    {
        var routine = new Routine(RoutineType.FBD);

        routine.Name.Should().BeEmpty();
        routine.Description.Should().BeNull();
        routine.Type.Should().Be(RoutineType.FBD);
        routine.Content<Sheet>().Should().NotBeNull();
        routine.Content<Sheet>().Should().BeEmpty();
    }

    [Test]
    public void New_EmptyRoutineElement_ShouldNotBeNullAndAccessingContentShouldWork()
    {
        var element = XElement.Parse("<Routine Use=\"Target\" Name=\"Empty\" Type=\"RLL\"/>");
        var routine = new Routine(element);

        routine.Should().NotBeNull();
        routine.Name.Should().Be("Empty");
        routine.Type.Should().Be(RoutineType.RLL);
        routine.Content<Rung>().Should().NotBeNull();
        
        routine.Content<Rung>().Add(new Rung("XIC(Test);"));
        routine.Content<Rung>().Should().HaveCount(1);
    }

    [Test]
    public void New_RungCollection_ShouldHaveExpectedCount()
    {
        var routine = new Routine();

        var rungs = new List<Rung>
        {
            new("XIC(SomeTag)OTE(SomeOtherTag);"),
            new("XIC(SomeTag)OTE(SomeOtherTag);"),
            new("XIC(SomeTag)OTE(SomeOtherTag);")
        };

        routine.Content<Rung>().AddRange(rungs);

        routine.Content<Rung>().Count().Should().Be(3);
    }

    [Test]
    public void New_Name_ShouldBeExpected()
    {
        var routine = new Routine("Test");

        routine.Name.Should().Be("Test");
        routine.Type.Should().Be(RoutineType.RLL);
    }

    [Test]
    public void New_NameAndType_ShouldBeExpected()
    {
        var routine = new Routine("Test", RoutineType.ST);

        routine.Name.Should().Be("Test");
        routine.Type.Should().Be(RoutineType.ST);
    }

    #region FBDTests

    [Test]
    public Task AddSheet_ValidSheetObject_ShouldWork()
    {
        var program = new Program { Name = "TestProgram" };
        program.Tags.Add(new Tag { Name = "InputTag", Value = new DINT(101) });
        program.Tags.Add(new Tag { Name = "OutputTag", Value = 0 });
        program.Tags.Add(new Tag { Name = "Add_Block", Value = new ComplexData("FBD_MATH") });

        var routine = new Routine(RoutineType.FBD)
        {
            Name = "Test",
            SheetSize = SheetSize.A0,
            SheetOrientation = SheetOrientation.Landscape
        };

        var sheet = new Sheet { Number = 1 };
        var id1 = sheet.AddAt(100, 100, Block.IREF("InputTag"));
        var id2 = sheet.AddAt(100, 200, Block.IREF(100));
        var id3 = sheet.AddAt(200, 100, Block.ADD("Add_Block"));
        var id4 = sheet.AddAt(300, 100, Block.OREF("OutputTag"));
        sheet.Add(new Wire { FromID = id1, ToID = id3, ToParam = "SourceA" });
        sheet.Add(new Wire { FromID = id2, ToID = id3, ToParam = "SourceB" });
        sheet.Add(new Wire { FromID = id3, ToID = id4, FromParam = "Destination" });

        routine.Content<Sheet>().Add(sheet);
        program.Routines.Add(routine);

        /*var content = program.Export();
        content.Save(@"C:\Users\tnunnink\Documents\Temp\L5X\TestRoutine.L5X");*/
        return Verify(program.Serialize().ToString());
    }

    #endregion
}