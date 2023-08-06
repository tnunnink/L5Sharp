using System.Diagnostics;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Samples;

namespace L5Sharp.Tests;

[TestFixture]
public class LogixContentTemplateTests
{
    [Test]
    public void DataTypes_WhenCalled_ShouldNotBeEmpty()
    {
        var content = LogixContent.Load(Known.Template);

        var components = content.DataTypes.ToList();

        components.Should().NotBeEmpty();
    }

    [Test]
    public void Modules_WhenCalled_ShouldNotBeEmpty()
    {
        var content = LogixContent.Load(Known.Template);

        var components = content.Modules.ToList();

        components.Should().NotBeEmpty();
    }

    [Test]
    public void Instructions_WhenCalled_ShouldNotBeEmpty()
    {
        var content = LogixContent.Load(Known.Template);

        var components = content.Instructions.ToList();

        components.Should().NotBeEmpty();
    }

    [Test]
    public void Tags_WhenCalled_ShouldNotBeEmpty()
    {
        var content = LogixContent.Load(Known.Template);

        var components = content.Tags.ToList();

        components.Should().NotBeEmpty();
    }

    [Test]
    public void TagsAll_WhenEnumerated_ShouldWork()
    {
        var content = LogixContent.Load(Known.Template);

        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var components = content.Find<Tag>().SelectMany(t => t.Members()).ToList();
        stopwatch.Stop();

        Console.WriteLine(stopwatch.Elapsed);
        components.Should().NotBeEmpty();
    }

    [Test]
    public void Programs_WhenCalled_ShouldNotBeEmpty()
    {
        var content = LogixContent.Load(Known.Template);

        var components = content.Programs.ToList();

        components.Should().NotBeEmpty();
    }

    [Test]
    public void Tasks_WhenCalled_ShouldNotBeEmpty()
    {
        var content = LogixContent.Load(Known.Template);

        var components = content.Tasks.ToList();

        components.Should().NotBeEmpty();
    }
}