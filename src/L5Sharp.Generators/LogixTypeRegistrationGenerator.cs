using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace L5Sharp.Generators;

[Generator]
public class LogixTypeRegistrationGenerator : IIncrementalGenerator
{
    private const string LogixDataAttribute = "L5Sharp.Core.LogixDataAttribute";

    /// <inheritdoc />
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var provider = GetLogixTypeInfo(context)
            .Collect()
            .Combine(context.AnalyzerConfigOptionsProvider);

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
    private static IncrementalValuesProvider<LogixTypeInfo> GetLogixTypeInfo(
        IncrementalGeneratorInitializationContext context)
    {
        return context.SyntaxProvider.ForAttributeWithMetadataName(
            LogixDataAttribute,
            static (node, _) => node is ClassDeclarationSyntax,
            static (ctx, _) =>
            {
                var symbol = (INamedTypeSymbol)ctx.TargetSymbol;
                var typeName = symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                var arguments = symbol.GetAttributeArguments(LogixDataAttribute).ToArray();
                var dataType = arguments[0] ?? string.Empty;
                var isAtomic = arguments.Length == 2 ? arguments[1] : "false";
                return new LogixTypeInfo(typeName, dataType, isAtomic);
            });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="registrations"></param>
    /// <param name="nameSpace"></param>
    /// <returns></returns>
    private static string GenerateSource(IEnumerable<LogixTypeInfo> registrations, string nameSpace)
    {
        var builder = new StringBuilder();

        foreach (var registration in registrations)
        {
            var typeName = registration.TypeName;
            var dataType = registration.DataType;

            var register = registration.IsAtomic.ToLower() == "true"
                ? $"LogixType.RegisterAtomic<{typeName}>(\"{dataType}\");"
                : $"LogixType.Register<{typeName}>(\"{dataType}\");";

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
              /// Generated class for registering types decorated with the LogixData attribute.
              /// </summary>
              internal static class LogixTypeRegistration
              {
                  /// <summary>
                  /// Automatically registers all found LogixData types when the module is loaded.
                  /// </summary>
                  [ModuleInitializer]
                  internal static void Register()
                  {
                      {{builder}}
                  }
              }
              """;
    }

    /// <summary>
    /// Represents a data structure for storing information about a registration
    /// in the context of Logix data types within the L5Sharp.Generators namespace.
    /// </summary>
    /// <remarks>
    /// This struct encapsulates information about the type name, corresponding data type,
    /// and whether the type is considered atomic within the Logix data model.
    /// </remarks>
    private readonly struct LogixTypeInfo(string typeName, string dataType, string isAtomic)
    {
        public string TypeName { get; } = typeName;
        public string DataType { get; } = dataType;
        public string IsAtomic { get; } = isAtomic;
    }
}