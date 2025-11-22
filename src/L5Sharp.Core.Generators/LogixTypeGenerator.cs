using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace L5Sharp.Core.Generators;

[Generator]
public class LogixTypeGenerator : IIncrementalGenerator
{
    private const string LogixDataAttribute = "L5Sharp.Core.LogixDataAttribute";

    /// <inheritdoc />
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var provider = context.SyntaxProvider.ForAttributeWithMetadataName(
            fullyQualifiedMetadataName: LogixDataAttribute,
            predicate: static (node, _) => node is ClassDeclarationSyntax,
            transform: static (ctx, _) =>
            {
                var symbol = (INamedTypeSymbol)ctx.TargetSymbol;
                var attribute = symbol.GetAttributeByName(LogixDataAttribute);
                var arguments = attribute.ConstructorArguments;
                var dataType = arguments[0].Value as string ?? string.Empty;
                var isAtomic = arguments.Length > 1 && (bool)arguments[1].Value!;
                return new Registration(symbol.ToDisplayString(), dataType, isAtomic);
            }
        ).Collect();

        context.RegisterSourceOutput(provider, (ctx, types) =>
        {
            if (types.IsDefaultOrEmpty) return;
            ctx.AddSource("LogixType.g.cs", GenerateTypeSource(types!));
        });
    }

    private static string GenerateTypeSource(IEnumerable<Registration> registrations)
    {
        var builder = new StringBuilder();
        builder.AppendLine("using L5Sharp.Core;");
        builder.AppendLine();
        builder.AppendLine("namespace L5Sharp.Core;");
        builder.AppendLine();
        builder.AppendLine("public static partial class LogixType");
        builder.AppendLine("{");
        builder.AppendLine("    static partial void RegisterTypes()");
        builder.AppendLine("    {");

        registrations.ToList().ForEach(r =>
        {
            var isAtomic = r.IsAtomic.ToString().ToLower(); //get correct syntax
            var code = $"Register(typeof({r.TypeName}), \"{r.DataType}\", {isAtomic}, () => new {r.TypeName}());";
            builder.AppendLine($"        {code}");
        });

        builder.AppendLine("    }");
        builder.AppendLine("}");
        return builder.ToString();
    }

    private readonly struct Registration(string typeName, string dataType, bool isAtomic)
    {
        public string TypeName { get; } = typeName;
        public string DataType { get; } = dataType;
        public bool IsAtomic { get; } = isAtomic;
    }
}