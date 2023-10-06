using FluentAssertions;
using L5Sharp.Common;
using L5Sharp.Components;
using L5Sharp.Elements;
using L5Sharp.Enums;
using L5Sharp.Samples;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Tests
{
    [TestFixture]
    public class Examples
    {
        [Test]
        public void SampleQuery001()
        {
            var content = L5X.Load(Known.Test);

            var results = content.Find<Tag>()
                .SelectMany(t => t.Members())
                .Where(t => t.DataType == "TIMER")
                .Select(t => new {t.TagName, t.Description, Preset = t.Value.As<TIMER>().PRE})
                .OrderBy(v => v.TagName)
                .ToList();

            results.Should().NotBeEmpty();
        }

        [Test]
        public void SampleQuery002()
        {
            var content = L5X.Load(Known.Test);

            var results = content.Find<Tag>()
                .SelectMany(t => t.Members())
                .Where(t => t.DataType == "SimpleType")
                .OrderBy(v => v.TagName)
                .ToList();

            results.Should().NotBeEmpty();
        }

        [Test]
        public void SampleQuery003()
        {
            var content = L5X.Load(Known.Test);

            var results = content.Find<Tag>().Where(t => t.Scope == Scope.Program && t.DataType == "DINT");

            results.Should().NotBeEmpty();
        }

        [Test]
        public void AddComponents()
        {
            var content = L5X.Load(Known.Test);

            var count = content.Tags.Count();

            content.Tags.AddRange(new List<Tag>
            {
                new() { Name = "tag01", Value = 100 },
                new() { Name = "tag02", Value = new TIMER { PRE = 2000 } },
                new() { Name = "tag03", Value = "This is a string tag value" }
            });

            content.Tags.Count().Should().Be(count + 3);
        }

        [Test]
        public void GetControllerProperties()
        {
            var content = L5X.Load(Known.Test);

            var controller = content.Controller;

            controller.Name.Should().Be("TestController");
            controller.ProcessorType.Should().Be("1756-L83E");
            controller.Description.Should().Be("This is a test project");
            controller.Revision.Should().Be(new Revision(32, 11));
            controller.RedundancyInfo?.Enabled.Should().BeFalse();
        }

        [Test]
        public void AccessMultidimensionalArray()
        {
            var content = L5X.Load(Known.Test);

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
        public void GetComponentByName()
        {
            var content = L5X.Load(Known.Test);

            var type = content.DataTypes.Get("AlarmType");

            type.Should().NotBeNull();
            type.Name.Should().Be("AlarmType");
            type.Class.Should().Be(DataTypeClass.User);
            type.Members.Should().NotBeEmpty();
        }

        [Test]
        public void QueryAllRungsAndSelectDistinctTagNamesFromTheNeutralTextValue()
        {
            var content = L5X.Load(Known.Test);

            var results = content.Find<Rung>().SelectMany(t => t.Text.Tags()).Distinct().ToList();

            results.Should().NotBeEmpty();
        }

        [Test]
        public void QueryAllRungsAndGetTagsInMovInstruction()
        {
            var content = L5X.Load(Known.Test);

            var results = content.Find<Rung>()
                .SelectMany(r => r.Text.TagsIn("MOV"))
                .ToList();

            results.Should().NotBeEmpty();
        }
    }
}