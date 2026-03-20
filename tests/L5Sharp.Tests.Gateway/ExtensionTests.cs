using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Gateway;
using L5Sharp.Gateway.Extensions;
using L5Sharp.Tests.Samples;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.Gateway;

[TestFixture]
public class ExtensionTests : PlcTestBase
{
    [Test]
    public async Task Upload_AllTags_ShouldHaveExpectedResponse()
    {
        var content = TestContent.Simple;
        //To ensure we read updated data, clear all the in memory tag values first.
        content.Tags.Update(t => t.Value.ClearData());

        using var client = CreateClient();

        var result = await content.Upload(client);

        result.Success.Should().BeTrue();
        result.Tags.Should().HaveCountGreaterThan(1000);
        result.Errors.Should().BeEmpty();
        result.Duration.Should().BeLessOrEqualTo(TimeSpan.FromSeconds(3));

        //Get the 1000 auto-generated tags to verify they read a value.
        var tags = content.Query<Tag>()
            .Where(t => t.IsPublic() && t.TagName.Base.StartsWith("Tag_") && t.TagName != "Tag_0")
            .SelectMany(t => t.Members())
            .ToList();

        tags.Should().AllSatisfy(t => t.Value.Should().NotBe(0));
    }
}