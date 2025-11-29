using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace L5Sharp.Core.Generators;

[Generator]
public class LogixTypeRegistrationGenerator : IIncrementalGenerator
{
    private const string LogixDataAttribute = "L5Sharp.Core.LogixDataAttribute";

    /// <inheritdoc />
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var provider = GetDataRegistrations(context).Collect().Combine(context.AnalyzerConfigOptionsProvider);

        context.RegisterSourceOutput(provider, (ctx, source) =>
        {
            var (registrations, options) = source;
            if (registrations.IsDefaultOrEmpty) return;

            options.GlobalOptions.TryGetValue("build_property.RootNamespace", out var nameSpace);

            var code = GenerateSource(registrations, nameSpace);
            ctx.AddSource("LogixType.Registration.g.cs", code);
        });
    }

    /// <summary>
    /// Retrieves an incremental provider that generates registrations for classes annotated with the [LogixData] attribute.
    /// </summary>
    /// <param name="context">
    /// The initialization context provided by the incremental generator, used to configure and register syntax providers for handling class declarations with relevant attributes.
    /// </param>
    /// <returns>
    /// An incremental values provider that produces registrations associating class names with their corresponding data attributes.
    /// </returns>
    private static IncrementalValuesProvider<Registration> GetDataRegistrations(
        IncrementalGeneratorInitializationContext context)
    {
        return context.SyntaxProvider.ForAttributeWithMetadataName(
            LogixDataAttribute,
            static (node, _) => node is ClassDeclarationSyntax,
            static (ctx, _) =>
            {
                var symbol = (INamedTypeSymbol)ctx.TargetSymbol;
                var type = symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                var arguments = symbol.GetAttributeArguments(LogixDataAttribute).ToArray();
                var dataType = arguments[0] ?? string.Empty;
                var isAtomic = arguments.Length == 2 ? arguments[1] : "false";
                return new Registration(type, dataType, isAtomic);
            });
    }

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
        builder.AppendLine("/// Generated class for registering types decorated with the LogixData attribute.");
        builder.AppendLine("/// </summary>");
        builder.AppendLine("internal static class LogixTypeRegistration");
        builder.AppendLine("{");
        builder.AppendLine("    /// <summary>");
        builder.AppendLine(
            "    /// Automatically registers all found LogixData types when the module is loaded.");
        builder.AppendLine("    /// </summary>");
        builder.AppendLine("    [ModuleInitializer]");
        builder.AppendLine("    internal static void Register()");
        builder.AppendLine("    {");

        registrations.ToList().ForEach(r =>
        {
            var code = r.IsAtomic.ToLower() == "true"
                ? $"LogixType.RegisterAtomic<{r.TypeName}>(\"{r.DataType}\", () => new {r.TypeName}());"
                : $"LogixType.Register<{r.TypeName}>(\"{r.DataType}\", () => new {r.TypeName}());";

            builder.AppendLine($"        {code}");
        });

        builder.AppendLine("    }");
        builder.AppendLine("}");

        return builder.ToString();
    }

    /// <summary>
    /// Represents a data structure for storing information about a registration
    /// in the context of Logix data types within the L5Sharp.Core.Generators namespace.
    /// </summary>
    /// <remarks>
    /// This struct encapsulates information about the type name, corresponding data type,
    /// and whether the type is considered atomic within the Logix data model.
    /// </remarks>
    private readonly struct Registration(string typeName, string dataType, string isAtomic)
    {
        public string TypeName { get; } = typeName;
        public string DataType { get; } = dataType;
        public string IsAtomic { get; } = isAtomic;
    }
}