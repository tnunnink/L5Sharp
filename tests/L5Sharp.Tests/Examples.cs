using FluentAssertions;
using L5Sharp.Common;
using L5Sharp.Components;
using L5Sharp.Elements;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;

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
        public void QueryForTimerTagsAndReturnTheirTagNameDescriptionAndPresetValue()
        {
            var content = LogixContent.Load(Known.Test);

            var results = content.Find<Tag>()
                .SelectMany(t => t.Members())
                .Where(t => t.DataType == "TIMER")
                .Select(t => new { t.TagName, t.Comment, t.Value.As<TIMER>().PRE })
                .ToList();

            results.Should().NotBeEmpty();
        }

        [Test]
        public void METHOD()
        {
            var content = LogixContent.Load(Known.Test);

            content.Tags.AddRange(new List<Tag>
            {
                new() { Name = "tag01", Value = 100 },
                new() { Name = "tag02", Value = new TIMER { PRE = 2000 } },
                new() { Name = "tag03", Value = "This is a string tag value" }
            });
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
        public void FindArrayTagByNameAndCastItsDataToRetrieveElements()
        {
            var content = LogixContent.Load(Known.Test);

            var tags = content.Tags.Get("MultiDimensionalArray");

            var array = tags.Value.As<ArrayType<DINT>>();

            array.Should().NotBeNull();
            array.Should().BeOfType<ArrayType<DINT>>();
            array.Members.ToList().Should().NotBeEmpty();
            array[0, 0].Should().NotBeNull();
            array[0, 0].Should().Be(0);
            array[2, 4].Should().NotBeNull();
            array[2, 4].Should().Be(0);
        }

        [Test]
        public void GetComponentByNameUsingExtension()
        {
            var content = LogixContent.Load(Known.Test);

            var type = content.DataTypes.Get("AlarmType");

            type.Should().NotBeNull();
            type.Name.Should().Be("AlarmType");
            type.Class.Should().Be(DataTypeClass.User);
            type.Members.Should().NotBeEmpty();
        }

        [Test]
        public void PerformQueryOnComponentToFilterResults()
        {
            var content = LogixContent.Load(Known.Test);

            var results = content.DataTypes.Where(d => d.Members.Any(m => m.DataType == "BOOL")).ToList();

            results.Should().NotBeEmpty();
        }

        [Test]
        public void GetTagsOfASpecificDataType()
        {
            var content = LogixContent.Load(Known.Test);

            //This would return tags that are arrays of timers too
            var timers = content.Tags.Where(t => t.DataType == "TIMER");

            timers.Should().NotBeEmpty();
        }

        [Test]
        public void SearchAcrossL5XForNestedComponentWithSpecificName()
        {
            var content = LogixContent.Load(Known.Test);

            var results = content.Find<Routine>().Where(r => r.Name == "Main").ToList();

            results.Should().NotBeEmpty();
            results.Should().Contain(r => r.Name == "Main");
        }

        [Test]
        public void QueryAllRungsAndSelectDistinctTagNamesFromTheNeutralTextValue()
        {
            var content = LogixContent.Load(Known.Test);

            var results = content.Find<Rung>().SelectMany(t => t.Text.Tags()).Distinct().ToList();

            results.Should().NotBeEmpty();
        }

        [Test]
        public void QueryAllRungsAndGetTagsInMovInstruction()
        {
            var content = LogixContent.Load(Known.Test);

            var results = content.Find<Rung>()
                .SelectMany(r => r.Text.TagsIn("MOV"))
                .ToList();

            results.Should().NotBeEmpty();
        }
    }
}