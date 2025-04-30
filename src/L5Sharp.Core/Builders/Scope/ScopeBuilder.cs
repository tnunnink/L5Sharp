using System.Linq;
using System.Text;

namespace L5Sharp.Core;

/// <summary>
/// Internal class that implements the builder interface.
/// </summary>
internal class ScopeBuilder(string controller) : IScopeBuilder,
    IScopeProgramBuilder, IScopeRoutineBuilder, IScopeNameBuilder, IScopeTypeBuilder
{
    private const char PathSeparator = '/';
    private string _program = string.Empty;
    private string _routine = string.Empty;
    private string _type = string.Empty;
    private string _name = string.Empty;

    public IScopeProgramBuilder In(string program)
    {
        _program = program;
        return this;
    }

    IScopeRoutineBuilder IScopeProgramBuilder.In(string routine)
    {
        _routine = routine;
        return this;
    }

    public IScopeNameBuilder Type(string type)
    {
        _type = type;
        return this;
    }

    public Scope DataType(string name)
    {
        _type = L5XName.DataType;
        _name = name;
        return Build();
    }

    public Scope Module(string name)
    {
        _type = L5XName.Module;
        _name = name;
        return Build();
    }

    public Scope Instruction(string name)
    {
        _type = ScopeType.Instruction.Value;
        _name = name;
        return Build();
    }

    public Scope Tag(string name)
    {
        _type = L5XName.Tag;
        _name = name;
        return Build();
    }

    public Scope Routine(string name)
    {
        _type = L5XName.Routine;
        _name = name;
        return Build();
    }

    public Scope Program(string name)
    {
        _type = L5XName.Program;
        _name = name;
        return Build();
    }

    public Scope Task(string name)
    {
        _type = L5XName.Task;
        _name = name;
        return Build();
    }

    public Scope Rung(int number)
    {
        _type = L5XName.Rung;
        _name = number.ToString();
        return Build();
    }

    public Scope Line(int number)
    {
        _type = L5XName.Line;
        _name = number.ToString();
        return Build();
    }

    public Scope Sheet(int number)
    {
        _type = L5XName.Sheet;
        _name = number.ToString();
        return Build();
    }

    public Scope Named(string name)
    {
        _name = name;
        return Build();
    }

    private Scope Build()
    {
        var builder = new StringBuilder();

        builder.Append(controller);
        if (!_program.IsEmpty()) builder.Append(PathSeparator).Append(_program);
        if (!_routine.IsEmpty()) builder.Append(PathSeparator).Append(_routine);
        if (_type != ScopeType.Null) builder.Append(PathSeparator).Append(_type);
        if (!_name.IsEmpty()) builder.Append(PathSeparator).Append(_name);

        return builder.ToString();
    }
}