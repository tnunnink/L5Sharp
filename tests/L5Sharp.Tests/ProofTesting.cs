using System.Reflection;
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
                    i.IsGenericType
                    && i.GetGenericTypeDefinition() == typeof(ILogixParsable<>)
                    && i.GetGenericArguments().All(a => !a.IsGenericParameter)
                )
            )
            .ToList();

        foreach (var type in types)
        {
            Console.WriteLine(type.FullName);
        }
    }

    [Test]
    public void GetParseMethod()
    {
        var type = Radix.Null.GetType();

        var flags = BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy;
        var method = type.GetMethods(flags).FirstOrDefault(m => IsParseFunctionFor(type, m));

        method.Should().NotBeNull();
    }
    
    private static bool IsParseFunctionFor(Type type, MethodInfo info)
    {
        var parameters = info.GetParameters();

        return info.Name.Equals("Parse")
               && info.ReturnType.IsAssignableFrom(type) 
               && parameters.Length == 1
               && parameters[0].ParameterType == typeof(string);
    }
}