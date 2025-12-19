using System;
using System.Text;
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
            .Select(static (file, ct) =>
            {
                var text = file.GetText(ct);
                return new Project(file.Path, text?.ToString() ?? string.Empty);
            })
            .Where(static x => !string.IsNullOrWhiteSpace(x.Content))
            .Collect()
            .Combine(context.AnalyzerConfigOptionsProvider);

        context.RegisterSourceOutput(provider, (spc, tuple) =>
        {
            var (projects, options) = tuple;
            if (projects.IsDefaultOrEmpty) return;
            options.GlobalOptions.TryGetValue("build_property.RootNamespace", out var ns);

            foreach (var project in projects)
            {
                L5X content;

                try
                {
                    content = L5X.Parse(project.Content);
                }
                catch (Exception e)
                {
                    spc.ReportDiagnostic(Diagnostic.Create(
                        new DiagnosticDescriptor(
                            id: "L5XGEN001",
                            title: "Failed to parse L5X XML",
                            messageFormat: "Could not parse L5X file '{0}': {1}",
                            category: "L5XGen",
                            DiagnosticSeverity.Warning,
                            isEnabledByDefault: true),
                        Location.None,
                        project.Path,
                        e.Message));
                    continue;
                }

                foreach (var dataType in content.DataTypes)
                {
                    var hintName = dataType.Name.SanitizeName();
                    var info = LogixTypeInfo.From(dataType);
                    var code = info.GenerateSource(ns);
                    var source = SourceText.From(code, Encoding.UTF8);
                    spc.AddSource($"{hintName}.g.cs", source);
                }

                foreach (var aoi in content.AddOnInstructions)
                {
                    var hintName = aoi.Name.SanitizeName();
                    var info = LogixTypeInfo.From(aoi);
                    var code = info.GenerateSource(ns);
                    var source = SourceText.From(code, Encoding.UTF8);
                    spc.AddSource($"{hintName}.g.cs", source);
                }
            }
        });
    }

    /// <summary>
    /// Represents a project with associated file path and content data.
    /// This struct is used during the processing of L5X files in the LogixDataClassGenerator.
    /// </summary>
    private readonly struct Project(string path, string content)
    {
        public string Path { get; } = path;
        public string Content { get; } = content;
    }
}