using L5Sharp.Core;
using L5Sharp.Samples;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Console;

public class TestClass
{
    public async Task RunExample()
    {
        var content = await L5X.LoadAsync(Known.Example, CancellationToken.None);

        var tags = content.Query<Tag>().Where(t => t.TagName.Contains("Test"));

        foreach (var tag in tags)
        {
            if (tag.Description is not null)
            {
                System.Console.WriteLine($"{tag.TagName}: {tag.Description}");
            }
        }
    }
}