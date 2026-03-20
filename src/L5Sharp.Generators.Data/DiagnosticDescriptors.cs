using Microsoft.CodeAnalysis;

namespace L5Sharp.Generators.Data;

internal static class DiagnosticDescriptors
{
    internal static readonly DiagnosticDescriptor L5XParseWarning = new(
        id: "L5G001",
        title: "Failed to parse L5X file",
        messageFormat: "Could not parse L5X file '{0}': {1}",
        category: "Usage",
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    internal static readonly DiagnosticDescriptor DuplicateTypeWarning = new(
        id: "L5G002",
        title: "Duplicate Logix Type",
        messageFormat:
        "The type '{0}' was found in multiple L5X files. The first definition found will be used, and others will be ignored.",
        category: "Design",
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    internal static readonly DiagnosticDescriptor TypeDefinitionNotFoundWarning = new(
        id: "L5G003",
        title: "Type Definition Not Found",
        messageFormat: "Type definition for nested member '{0}' was not found in registry or context",
        category: "Usage",
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true);
}