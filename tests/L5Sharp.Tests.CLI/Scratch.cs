namespace L5Sharp.Tests.CLI;

[TestFixture]
public class Scratch
{
    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase(@"C:\Users\tnunnink\Documents\Rockwell\")]
    [TestCase(@"C:\Users\tnunnink\Documents\Rockwell")]
    [TestCase(@"C:\Users\tnunnink\Documents\Rockwell\*.L5X")]
    public void Testing(string? source)
    {
        var isVoid = string.IsNullOrWhiteSpace(source);
        var isDirectory = Directory.Exists(source);

        var directory = isVoid ? Path.GetFullPath(".")
            : isDirectory ? Path.GetFullPath(source!)
            : Path.GetFullPath(Path.GetDirectoryName(source) ?? ".");

        var pattern = isVoid || isDirectory ? "*" : Path.GetFileName(source) ?? "*";

        var files = Directory.GetFiles(directory, pattern, SearchOption.TopDirectoryOnly);

        Assert.That(files, Is.Not.Empty);
    }

    [Test]
    public void ToTableTesting()
    {
        
    }
}