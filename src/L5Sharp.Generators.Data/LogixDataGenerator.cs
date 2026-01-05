using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using L5Sharp.Core;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace L5Sharp.Generators.Data;

[Generator]
public class LogixDataGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var provider = context.AdditionalTextsProvider
            .Where(static file => file.Path.IsL5XPath())
            .Select(static (file, ct) => Project.ReadFrom(file, ct))
            .Where(static x => !string.IsNullOrWhiteSpace(x.Content))
            .Collect()
            .Combine(context.AnalyzerConfigOptionsProvider);

        context.RegisterSourceOutput(provider, (spc, tuple) =>
        {
            var (projects, options) = tuple;
            if (projects.IsDefaultOrEmpty) return;

            options.GlobalOptions.TryGetValue("build_property.RootNamespace", out var ns);

            var types = IndexTypeDefinitions(projects, spc);

            foreach (var type in types)
            {
                var code = type.Value.GenerateSource(ns, types);
                var source = SourceText.From(code, Encoding.UTF8);
                spc.AddSource($"{type.Value.TypeName}.g.cs", source);
            }

            if (types.Count == 0) return;

            //We have to generate a registration initializer file because the core source generator won't see these
            //generated types with the registration attribute until it's too late.
            spc.AddSource(
                "LogixDataRegistration.g.cs",
                SourceText.From(GenerateRegistration(types.Values, ns ?? "L5Sharp.Data.Generation"), Encoding.UTF8)
            );
        });
    }

    /// <summary>
    /// Reads types from a collection of projects and returns a collection of LogixTypeInfo objects.
    /// </summary>
    /// <param name="projects">
    /// A collection of projects containing files and content for type parsing.
    /// </param>
    /// <param name="context">
    /// The source production context used to report diagnostics and manage source generation.
    /// </param>
    /// <returns>
    /// A collection of LogixTypeInfo objects representing the types extracted from the provided projects.
    /// </returns>
    private static Dictionary<string, LogixTypeInfo> IndexTypeDefinitions(IEnumerable<Project> projects,
        SourceProductionContext context)
    {
        var types = new Dictionary<string, LogixTypeInfo>();

        foreach (var project in projects)
        {
            if (!TryParseContent(context, project, out var content))
                continue;

            content.DataTypes
                .Where(x => x.Serialize().Element(L5XName.Members) is not null
                            && !LogixType.IsAtomic(x.Name))
                //&& !LogixType.IsRegistered(x.Name))
                .Select(LogixTypeInfo.From)
                .ToList().ForEach(t =>
                {
                    if (types.ContainsKey(t.Name))
                    {
                        context.ReportDiagnostic(Diagnostic.Create(DuplicateTypeWarning, Location.None, t.Name));
                        return;
                    }

                    types.Add(t.Name, t);
                });

            content.AddOnInstructions
                .Where(x => !LogixType.IsRegistered(x.Name))
                .Select(LogixTypeInfo.From)
                .ToList().ForEach(t =>
                {
                    if (types.ContainsKey(t.Name))
                    {
                        context.ReportDiagnostic(Diagnostic.Create(DuplicateTypeWarning, Location.None, t.Name));
                        return;
                    }

                    types.Add(t.Name, t);
                });
        }

        return types;
    }

    /// <summary>
    /// Attempts to parse the content of a given project into an L5X object.
    /// </summary>
    /// <param name="context">
    /// The source production context used to report diagnostics and manage source generation.
    /// </param>
    /// <param name="project">
    /// The project containing the file path and content to be parsed.
    /// </param>
    /// <param name="content">
    /// When this method returns, contains the parsed L5X object if the parsing succeeds,
    /// or null if the parsing fails.
    /// </param>
    /// <returns>
    /// True if the content was successfully parsed into an L5X object; otherwise, false.
    /// </returns>
    private static bool TryParseContent(SourceProductionContext context, Project project, out L5X content)
    {
        try
        {
            content = L5X.Parse(project.Content);
            return true;
        }
        catch (Exception e)
        {
            context.ReportDiagnostic(Diagnostic.Create(L5XParseWarning, Location.None, project.Path, e.Message));
            content = null!;
            return false;
        }
    }

    /// <summary>
    /// Generates a source string for registering Logix data types within a specified namespace.
    /// </summary>
    /// <param name="types">
    /// A collection of LogixTypeInfo objects representing the Logix data types to be registered.
    /// </param>
    /// <param name="nameSpace">
    /// The namespace in which the generated registration code will reside.
    /// </param>
    /// <returns>
    /// A string containing the generated source code for registering the provided Logix data types.
    /// </returns>
    private static string GenerateRegistration(IEnumerable<LogixTypeInfo> types, string nameSpace)
    {
        var builder = new StringBuilder();

        foreach (var type in types)
        {
            builder.Append($"LogixType.Register<{type.TypeName}>(\"{type.Name}\");");
            builder.Append("\n        ");
        }

        var registrations = builder.ToString().TrimEnd();

        return
            $$"""
              using L5Sharp.Core;
              using System.Xml.Linq;
              using System.Runtime.CompilerServices;

              namespace {{nameSpace}};

              /// <summary>
              /// Generated class for registering types decorated with the LogixData attribute.
              /// </summary>
              internal static class LogixDataRegistration
              {
                  /// <summary>
                  /// Automatically registers all found LogixData types when the module is loaded.
                  /// </summary>
                  [ModuleInitializer]
                  internal static void Register()
                  {
                      {{registrations}}
                  }
              }
              """;
    }

    /// <summary>
    /// Represents a project with associated file path and content data.
    /// This struct is used during the processing of L5X files in the LogixDataClassGenerator.
    /// </summary>
    private readonly struct Project(string path, string? content)
    {
        public string Path { get; } = path;
        public string Content { get; } = content ?? string.Empty;

        /// <summary>
        /// Creates a new <c>Project</c> instance by reading the content of the specified file asynchronously.
        /// </summary>
        /// <param name="file">
        /// The <c>AdditionalText</c> instance representing the file to be read.
        /// </param>
        /// <param name="token">
        /// A <c>CancellationToken</c> used to observe cancellation requests during the read operation.
        /// </param>
        /// <returns>
        /// A <c>Project</c> instance containing the file's path and its content as a string.
        /// </returns>
        public static Project ReadFrom(AdditionalText file, CancellationToken token)
        {
            var content = file.GetText(token);
            return new Project(file.Path, content?.ToString());
        }
    }

    private static readonly DiagnosticDescriptor L5XParseWarning = new(
        id: "L5G001",
        title: "Failed to parse L5X file",
        messageFormat: "Could not parse L5X file '{0}': {1}",
        category: "L5Sharp.Generation",
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    private static readonly DiagnosticDescriptor DuplicateTypeWarning = new(
        id: "L5G002",
        title: "Duplicate Logix Type",
        messageFormat:
        "The type '{0}' was found in multiple L5X files. The first definition found will be used, and others will be ignored.",
        category: "L5Sharp.Generation",
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true);
}