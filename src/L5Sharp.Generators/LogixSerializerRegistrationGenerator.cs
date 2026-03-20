using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace L5Sharp.Generators;

[Generator]
public class LogixSerializerRegistrationGenerator : IIncrementalGenerator
{
    private const string LogixElementAttribute = "L5Sharp.Core.LogixElementAttribute";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var provider = GetElementRegistrations(context).Collect().Combine(context.AnalyzerConfigOptionsProvider);

        context.RegisterSourceOutput(provider, (ctx, source) =>
        {
            var (registrations, options) = source;
            if (registrations.IsDefaultOrEmpty) return;

            options.GlobalOptions.TryGetValue("build_property.RootNamespace", out var nameSpace);

            var code = GenerateSource(registrations, nameSpace);
            ctx.AddSource("LogixSerializer.Registration.g.cs", code);
        });
    }

    private static IncrementalValuesProvider<Registration> GetElementRegistrations(
        IncrementalGeneratorInitializationContext context)
    {
        return context.SyntaxProvider.ForAttributeWithMetadataName(
            LogixElementAttribute,
            static (node, _) => node is ClassDeclarationSyntax,
            static (ctx, _) =>
            {
                var symbol = (INamedTypeSymbol)ctx.TargetSymbol;
                var type = symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                var arguments = symbol.GetAttributeArguments(LogixElementAttribute).ToArray();
                return new Registration(type, arguments);
            });
    }

    private static string GenerateSource(IEnumerable<Registration> registrations, string nameSpace)
    {
        var builder = new StringBuilder();

        foreach (var registration in registrations)
        {
            var type = registration.Type;
            var names = string.Join(", ", registration.Arguments.Select(n => $"\"{n}\""));
            var register = $"LogixSerializer.Register<{type}>(e => new {type}(e), {names});";
            builder.AppendLine($"{register}");
            builder.Append("        ");
        }

        return
            $$"""
              using L5Sharp.Core;
              using System.Xml.Linq;
              using System.Runtime.CompilerServices;

              namespace {{nameSpace}};

              /// <summary>
              /// Generated class for registering decorated types with the LogixSerializer.
              /// </summary>
              internal static class LogixSerializerRegistration
              {
                  /// <summary>
                  /// Automatically registers all found LogixElement and LogixData types when the module is loaded.
                  /// </summary>
                  [ModuleInitializer]
                  internal static void Register()
                  {
                      {{builder}}
                  }
              }
              """;
    }

    private readonly struct Registration(string type, string[] arguments)
    {
        public string Type { get; } = type;
        public string[] Arguments { get; } = arguments;
    }
}