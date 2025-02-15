using FluentAssertions;

namespace L5Sharp.Tests.Core
{
    [TestFixture]
    public class Examples
    {
        [Test]
        public void SampleQuery001()
        {
            var content = L5X.Load(Known.Test);

            var results = content.Query<Tag>()
                .SelectMany(t => t.Members())
                .Where(t => t.DataType == "TIMER" && t.Dimensions == Dimensions.Empty)
                .Select(t => new { t.TagName, t.Description, Preset = t["PRE"].Value })
                .OrderBy(v => v.TagName)
                .ToList();

            results.Should().NotBeEmpty();
        }

        [Test]
        public void SampleQuery002()
        {
            var content = L5X.Load(Known.Test);

            var results = content.Query<Tag>()
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

            var results = content.Query<Tag>().Where(t => t.Scope.IsLocal && t.DataType == "DINT");

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

            var array = tags.Value.As<ArrayData>().Cast<DINT>();

            array.Should().NotBeNull();
            array.Should().BeOfType<ArrayData<DINT>>();
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

            var results = content.Query<Rung>().SelectMany(t => t.Text.Tags()).Distinct().ToList();

            results.Should().NotBeEmpty();
        }

        [Test]
        public void QueryAllRungsAndGetTagsInMovInstruction()
        {
            var content = L5X.Load(Known.Test);

            var results = content.Query<Rung>()
                .SelectMany(r => r.Text.TagsIn("MOV"))
                .ToList();

            results.Should().NotBeEmpty();
        }

        [Test]
        public void SomeMoreTagQueriesAcrossTheL5XFile()
        {
            var content = L5X.Load(Known.Test);

            var allTags = content.Query<Tag>().ToList();

            var programTags = allTags.Where(t => t.Scope.IsLocal);
            var ioTags = allTags.Where(t => t.Name.Contains(':'));
            var readWriteTags = allTags.Where(t => t.ExternalAccess?.Equals(ExternalAccess.ReadWrite) is true);
            var timerTags = allTags.Where(t => t.DataType == "TIMER");

            programTags.Should().NotBeEmpty();
            ioTags.Should().NotBeEmpty();
            readWriteTags.Should().NotBeEmpty();
            timerTags.Should().NotBeEmpty();
        }
    }
}