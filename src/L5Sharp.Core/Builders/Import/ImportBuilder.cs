namespace L5Sharp.Core;

internal class ImportBuilder : IImportSourceBuilder, IImportTypeBuilder
{
    private readonly Import _import = new();

    public IImportTypeBuilder From(string fileName)
    {
        _import.Source = L5X.Load(fileName);
        return this;
    }

    public IImportTypeBuilder From(L5X content)
    {
        _import.Source = content;
        return this;
    }

    public IImportDefaultBuilder DataType(string? name = null)
    {
        if (name is not null && !name.IsEmpty())
        {
            _import.Target = Reference.To<DataType>(name);
        }

        return new ImportDefaultBuilder(_import);
    }

    public IImportDefaultBuilder Instruction(string? name = null)
    {
        if (name is not null && !name.IsEmpty())
        {
            _import.Target = Reference.To<AddOnInstruction>(name);
        }

        return new ImportDefaultBuilder(_import);
    }

    public IImportModuleBuilder Module(string? name = null)
    {
        if (name is not null && !name.IsEmpty())
        {
            _import.Target = Reference.To<Module>(name);
        }

        return new ImportModuleBuilder(_import);
    }

    public IImportProgramBuilder Program(string? name = null)
    {
        if (name is not null && !name.IsEmpty())
        {
            _import.Target = Reference.To<Program>(name);
        }

        return new ImportProgramBuilder(_import);
    }

    public IImportRoutineBuilder Routine(Reference? scope = null)
    {
        if (scope is not null)
        {
            _import.Target = scope;
        }

        return new ImportRoutineBuilder(_import);
    }

    public Import Build()
    {
        return _import;
    }
}