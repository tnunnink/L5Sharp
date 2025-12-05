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

    /// <summary>
    /// Retrieves an incremental provider that generates registrations for types annotated with the [LogixElement] attribute.
    /// </summary>
    /// <param name="context">
    /// The initialization context provided by the incremental generator, used to configure and register syntax providers.
    /// </param>
    /// <returns>
    /// An incremental values provider that produces registrations associating type names with their corresponding element attributes.
    /// </returns>
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

    /// <summary>
    /// Generates the source code for registering LogixElement types in the current assembly.
    /// </summary>
    /// <param name="registrations">
    /// A dictionary containing the type names as keys and their associated element names as values.
    /// </param>
    /// <param name="nameSpace">
    /// The root namespace in which the generated code should be placed.
    /// </param>
    /// <returns>
    /// A string containing the generated source code.
    /// </returns>
    private static string GenerateSource(IEnumerable<Registration> registrations, string nameSpace)
    {
        var builder = new StringBuilder();

        builder.AppendLine("using L5Sharp.Core;");
        builder.AppendLine("using System.Xml.Linq;");
        builder.AppendLine("using System.Runtime.CompilerServices;");
        builder.AppendLine();
        builder.AppendLine($"namespace {nameSpace};");
        builder.AppendLine();
        builder.AppendLine("/// <summary>");
        builder.AppendLine("/// Generated class for registering decorated types with the LogixSerializer.");
        builder.AppendLine("/// </summary>");
        builder.AppendLine("internal static class LogixSerializerRegistration");
        builder.AppendLine("{");
        builder.AppendLine("    /// <summary>");
        builder.AppendLine(
            "    /// Automatically registers all found LogixElement and LogixData types when the module is loaded.");
        builder.AppendLine("    /// </summary>");
        builder.AppendLine("    [ModuleInitializer]");
        builder.AppendLine("    internal static void Register()");
        builder.AppendLine("    {");

        foreach (var registration in registrations)
        {
            var type = registration.Type;
            var names = string.Join(", ", registration.Arguments.Select(n => $"\"{n}\""));
            var code = $"LogixSerializer.Register<{type}>(e => new {type}(e), {names});";
            builder.AppendLine($"        {code}");
        }

        builder.AppendLine("    }");
        builder.AppendLine("}");

        return builder.ToString();
    }

    /// <summary>
    /// Represents a registration that associates a type name with its corresponding element names.
    /// This struct is primarily used for handling serializer registration in Logix generation processes.
    /// </summary>
    private readonly struct Registration(string type, string[] arguments)
    {
        public string Type { get; } = type;
        public string[] Arguments { get; } = arguments;
    }
}