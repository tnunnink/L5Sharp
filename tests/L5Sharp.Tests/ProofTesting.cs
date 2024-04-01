using FluentAssertions;


namespace L5Sharp.Tests;

[TestFixture]
public class ProofTesting
{
   
    [Test]
    public void Scratch()
    {
        var content = Logix.Load(Known.Test);
        var sheet = content.Query<Sheet>().First();

        var references = sheet.References().Where(c => c.Type == nameof(Tag)).ToList();

        references.Should().NotBeEmpty();
    }

    [Test]
    public void ParserTypeTests()
    {
        var types = typeof(LogixParser).Assembly.GetTypes().Where(t =>
                t.GetInterfaces().Any(i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(ILogixParsable<>) &&
                    i.GetGenericArguments().All(a => !a.IsGenericTypeParameter)))
            .ToList();

        foreach (var type in types)
        {
            Console.WriteLine(type.FullName);
        }
    }
}