using Microsoft.CodeAnalysis;

namespace L5Sharp.Core.Generators;

/// <summary>
/// A source generator that adds the <c>ModuleInitializerAttribute</c> to assemblies that do not have it.
/// This "polyfills" the attribute, allowing it to be used in projects targeting older frameworks like .NET Standard 2.0.
/// </summary>
[Generator]
public class ModuleInitializerGenerator : IIncrementalGenerator
{
    private const string ModuleInitializerAttributeSource =
        """
        #if !NET5_0_OR_GREATER
        namespace System.Runtime.CompilerServices
        {
            [AttributeUsage(AttributeTargets.Method, Inherited = false)]
            internal sealed class ModuleInitializerAttribute : Attribute { }
        }
        #endif
        """;

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(c =>
        {
            // The source generator will conditionally emit this attribute. If the compilation already has a
            // ModuleInitializerAttribute, this will do nothing. If it doesn't, it will add our internal one.
            c.AddSource("ModuleInitializerAttribute.g.cs", ModuleInitializerAttributeSource);
        });
    }
}