using System;
using System.Linq;

namespace L5Sharp.Core;

internal class ImportTypeBuilder(string sourceFile, L5X content) : IImportTypeBuilder
{
    public IImportBaseBuilder DataType(string? name)
    {
        var source = L5X.Load(sourceFile);
        var target = !string.IsNullOrEmpty(name) ? Scope.Build().DataType(name) : GenerateTargetScope(source);
        return new ImportBaseBuilder(new ImportConfig(content, source, target));
    }

    public IImportBaseBuilder Instruction(string? name = null)
    {
        var source = L5X.Load(sourceFile);
        var target = !string.IsNullOrEmpty(name) ? Scope.Build().Instruction(name) : GenerateTargetScope(source);
        return new ImportBaseBuilder(new ImportConfig(content, source, target));
    }

    public IImportModuleBuilder Module(string? name = null)
    {
        var source = L5X.Load(sourceFile);
        var target = !string.IsNullOrEmpty(name) ? Scope.Build().Module(name) : GenerateTargetScope(source);
        return new ImportModuleBuilder(new ImportConfig(content, source, target));
    }

    public IImportProgramBuilder Program(string? name = null)
    {
        var source = L5X.Load(sourceFile);
        var target = !string.IsNullOrEmpty(name) ? Scope.Build().Program(name) : GenerateTargetScope(source);
        return new ImportProgramBuilder(new ImportConfig(content, source, target));
    }

    public IImportRoutineBuilder Routine(Scope? scope = null)
    {
        var source = L5X.Load(sourceFile);
        var target = scope ?? GenerateTargetScope(source);
        return new ImportRoutineBuilder(new ImportConfig(content, source, target));
    }

    private static Scope GenerateTargetScope(L5X source)
    {
        if (source.Info.TargetType is null || source.Info.TargetName is null)
            throw new InvalidOperationException(
                "No target type/name information is configured in the specified source.");

        if (source.Info.TargetType == nameof(Routine))
        {
            var program = source.Programs.FirstOrDefault(p => Use.Context.Equals(p.Use));

            if (program is null)
                throw new InvalidOperationException("Could not determine scope for import process.");

            return Scope.Build().In(program.Name).Routine(source.Info.TargetName);
        }

        return Scope.Build().Type(source.Info.TargetType).Named(source.Info.TargetName);
    }
}