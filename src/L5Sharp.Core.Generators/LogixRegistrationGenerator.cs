using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace L5Sharp.Core.Generators;

[Generator]
public class LogixRegistrationGenerator : IIncrementalGenerator
{
    private const string LogixElementAttribute = "L5Sharp.Core.LogixElementAttribute";
    private const string LogixDataAttribute = "L5Sharp.Core.LogixDataAttribute";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var provider = GetElementRegistrations(context).Collect()
            .Combine(GetDataRegistrations(context).Collect())
            .Combine(context.AnalyzerConfigOptionsProvider);

        context.RegisterSourceOutput(provider, (ctx, source) =>
        {
            var ((elements, dataTypes), options) = source;
            if (elements.IsDefaultOrEmpty && dataTypes.IsDefaultOrEmpty) return;

            options.GlobalOptions.TryGetValue("build_property.RootNamespace", out var nameSpace);
            var registrations = elements.Concat(dataTypes);

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
                return new Registration(type, RegistrationType.Element, arguments);
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
                return new Registration(type, RegistrationType.DataType, arguments);
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
        builder.AppendLine("internal static class LogixRegistration");
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
            var typeName = registration.Name;
            string code;

            switch (registration.Type)
            {
                case RegistrationType.Element:
                {
                    //All the arguments we receive for element types are element names.
                    var elements = string.Join(", ", registration.Arguments.Select(n => $"\"{n}\""));
                    code = $"LogixSerializer.Register<{typeName}>(e => new {typeName}(e), {elements});";
                    break;
                }
                case RegistrationType.DataType:
                {
                    //The arguments for the logix data attribute are the data type name and the isAtomic flag
                    //We only need the data type name for serialization registration.
                    var dataType = registration.Arguments.FirstOrDefault();
                    code = $"LogixSerializer.Register<{typeName}>(e => new {typeName}(e), \"{dataType}\");";
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(registration.Type), "Registration type not defined.");
            }

            if (string.IsNullOrEmpty(code))
                continue;

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
    private readonly struct Registration(string name, RegistrationType type, string[] arguments)
    {
        public string Name { get; } = name;
        public RegistrationType Type { get; } = type;
        public string[] Arguments { get; } = arguments;
    }

    /// <summary>
    /// An enumeration to distinguish between element and data type registrations.
    /// </summary>
    private enum RegistrationType
    {
        Element,
        DataType
    }
}