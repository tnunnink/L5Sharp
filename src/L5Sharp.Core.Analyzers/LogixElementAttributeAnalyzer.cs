using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace L5Sharp.Core.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class LogixElementAttributeAnalyzer : DiagnosticAnalyzer
    {
        private const string LogixElementTypeName = "L5Sharp.Core.LogixElement";
        private const string LogixElementAttributeName = "L5Sharp.Core.LogixElementAttribute";
        private const string LogixDataTypeName = "L5Sharp.Core.LogixData";

        private static readonly DiagnosticDescriptor Rule = new(
            id: "L5S001",
            title: "LogixElement missing attribute",
            messageFormat:
            "The non-abstract LogixElement derivative '{0}' must be decorated with the [LogixElement] attribute",
            category: "L5Sharp.Design",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description:
            "All non-abstract types that derive from LogixElement must be decorated with a LogixElementAttribute to be registered for serialization.");

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();
            context.RegisterSymbolAction(AnalyzeNamedType, SymbolKind.NamedType);
        }

        private static void AnalyzeNamedType(SymbolAnalysisContext context)
        {
            var namedTypeSymbol = (INamedTypeSymbol)context.Symbol;

            // We only care about non-abstract non-generic classes.
            if (namedTypeSymbol.IsAbstract || 
                namedTypeSymbol.IsGenericType ||
                namedTypeSymbol.TypeKind != TypeKind.Class) return;

            // Check if the class inherits from LogixElement.
            if (!InheritsFrom(namedTypeSymbol, LogixElementTypeName)) return;

            // All types inheriting from LogixData are handled with different attribute.
            if (InheritsFrom(namedTypeSymbol, LogixDataTypeName)) return;

            // For now, make an exception for L5X since I don't know how else to handle it.
            if (namedTypeSymbol.Name == "L5X") return;

            // Check if the class already has the attribute. If so, it's valid.
            if (namedTypeSymbol.GetAttributes()
                .Any(attr => attr.AttributeClass?.ToDisplayString() == LogixElementAttributeName)) return;

            // If we get here, it's a non-abstract derivative without the attribute. Report an error.
            var diagnostic = Diagnostic.Create(Rule, namedTypeSymbol.Locations[0], namedTypeSymbol.Name);
            context.ReportDiagnostic(diagnostic);
        }

        private static bool InheritsFrom(INamedTypeSymbol typeSymbol, string baseTypeName)
        {
            var baseType = typeSymbol.BaseType;
            while (baseType != null)
            {
                if (baseType.ToDisplayString() == baseTypeName)
                {
                    return true;
                }

                baseType = baseType.BaseType;
            }

            return false;
        }
    }
}