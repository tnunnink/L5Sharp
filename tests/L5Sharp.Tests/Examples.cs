using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Entities;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Types;

namespace L5Sharp.Tests
{
    [TestFixture]
    public class Examples
    {
        [Test]
        public void New_ValidFile_ShouldNotBeNull()
        {
            var content = LogixContent.Load(Known.Test);

            content.Should().NotBeNull();
        }

        [Test]
        public void ApiTesting()
        {
            var content = LogixContent.Load(Known.Test);

            var routine = content.Programs["MyProgram"].Routines.Find("Setup");

            var dependents = content.DataTypes.DependentsOf("SimpleType");
        }

        [Test]
        public void New_ValidFile_ShouldHaveExpectedContent()
        {
            var content = LogixContent.Load(Known.Test);

            content.L5X.Should().NotBeNull();
            content.L5X.SchemaRevision.Should().Be(new Revision());
            content.L5X.SoftwareRevision.Should().Be(new Revision(32, 02));
            content.L5X.TargetName.Should().Be("TestController");
            content.L5X.TargetType.Should().Be("Controller");
            content.L5X.ContainsContext.Should().Be(false);
            content.L5X.Owner.Should().Be("tnunnink, EN Engineering");
            content.L5X.ExportDate.Should().NotBeNull();
        }

        [Test]
        public void Controller_WhenCalled_ReturnsControllerInstance()
        {
            var content = LogixContent.Load(Known.Test);

            var controller = content.Controller;

            controller.Name.Should().Be("TestController");
            controller.ProcessorType.Should().Be("1756-L83E");
            controller.Description.Should().Be("This is a test project");
            controller.Revision.Should().Be(new Revision(32, 11));
            controller.RedundancyInfo?.Enabled.Should().BeFalse();
        }

        [Test]
        public void FindAArrayTagByNameAndCastItsDataToRetrieveElements()
        {
            var content = LogixContent.Load(Known.Test);

            var tags = content.Tags.Find("MultiDimensionalArray");

            tags.Should().NotBeNull();

            var array = AssertionExtensions.As<ArrayType<LogixType>>(tags.Value);

            var elements = array.Members.ToList();
            elements.Should().NotBeEmpty();

            array.Should().NotBeNull();
        }

        [Test]
        public void FindDataTypeByName()
        {
            var content = LogixContent.Load(Known.Test);

            var type = content.DataTypes.Find("AlarmType");

            type.Should().NotBeNull();
            type.Name.Should().Be("AlarmType");
            type.Class.Should().Be(DataTypeClass.User);
            type.Members.Should().NotBeEmpty();
        }

        [Test]
        public void DataTypes_WithMembersOfTypeBool_ShouldNotBeEmpty()
        {
            var content = LogixContent.Load(Known.Test);

            var results = content.DataTypes.Where(d => d.Members.Any(m => m.DataType == "BOOL")).ToList();

            results.Should().NotBeEmpty();
        }

        [Test]
        public void Tags_OfType_ShouldNotBeEmpty()
        {
            var content = LogixContent.Load(Known.Test);

            var timers = content.Tags.Where(t => t.DataType == "TIMER");

            timers.Should().NotBeEmpty();
        }

        [Test]
        public void Find_RoutineWithName_ShouldHaveResultWithName()
        {
            var content = LogixContent.Load(Known.Test);

            var results = content.Find<Routine>().Where(r => r.Name == "Main").ToList();

            results.Should().NotBeEmpty();
            results.Should().Contain(r => r.Name == "Main");
        }

        [Test]
        public void Find_Rungs_ShouldReturnsRungs()
        {
            var content = LogixContent.Load(Known.Test);

            var results = content.Find<Rung>().SelectMany(t => t.Text.Tags()).Distinct().ToList();

            results.Should().NotBeEmpty();
        }

        [Test]
        public void Query_DistinctReferencedTagNames_ShouldReturnsLotsOfTagNames()
        {
            var content = LogixContent.Load(Known.Test);

            var results = content.Find<Rung>().SelectMany(t => t.Text.Tags()).Distinct().ToList();

            results.Should().NotBeEmpty();
        }

        [Test]
        public void Query_TagsInMovInstructions_ShouldReturnsLotsOfTagNames()
        {
            var content = LogixContent.Load(Known.Test);

            var results = content.Find<Rung>()
                .SelectMany(r => r.Text.TagsIn("MOV"))
                .ToList();

            results.Should().NotBeEmpty();
        }

        [Test]
        public void Text_InProgramWithTag_ShouldNotBeEmpty()
        {
            var content = LogixContent.Load(Known.Test);

            var logic = content.Text().In("MainProgram").WithTag("SimpleBool");

            logic.Should().NotBeEmpty();
        }
    }
}