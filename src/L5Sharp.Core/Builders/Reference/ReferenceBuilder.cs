using System;
using System.Collections.Generic;
using System.Text;

namespace L5Sharp.Core;

/// <summary>
/// 
/// </summary>
public class ReferenceBuilder : IReferenceTypeBuilder, IReferenceLocationBuilder, IReferenceScopeBuilder
{
    private readonly List<string> _containers = [];
    private ReferenceType? _type;
    private string? _location;

    /// <inheritdoc />
    public IReferenceLocationBuilder Type(ReferenceType type)
    {
        _type = type;
        return this;
    }

    /// <inheritdoc />
    public IReferenceScopeBuilder DataType(string name)
    {
        _type = ReferenceType.DataType;
        _location = name;
        _containers.Clear();
        return this;
    }

    /// <inheritdoc />
    public IReferenceScopeBuilder Module(string name)
    {
        _type = ReferenceType.Module;
        _location = name;
        _containers.Clear();
        return this;
    }

    /// <inheritdoc />
    public IReferenceScopeBuilder Aoi(string name)
    {
        _type = ReferenceType.Aoi;
        _location = name;
        _containers.Clear();
        return this;
    }

    /// <inheritdoc />
    public IReferenceScopeBuilder Program(string name)
    {
        _type = ReferenceType.Program;
        _location = name;
        _containers.Clear();
        return this;
    }

    /// <inheritdoc />
    public IReferenceScopeBuilder Task(string name)
    {
        _type = ReferenceType.Task;
        _location = name;
        _containers.Clear();
        return this;
    }

    /// <inheritdoc />
    public IReferenceScopeBuilder Tag(string name)
    {
        _type = ReferenceType.Tag;
        _location = name;
        return this;
    }

    /// <inheritdoc />
    public IReferenceScopeBuilder Routine(string name)
    {
        _type = ReferenceType.Routine;
        _location = name;
        return this;
    }

    /// <inheritdoc />
    public IReferenceScopeBuilder Rung(int number)
    {
        _type = ReferenceType.Rung;
        _location = number.ToString();
        return this;
    }

    /// <inheritdoc />
    public IReferenceScopeBuilder Line(int number)
    {
        _type = ReferenceType.Line;
        _location = number.ToString();
        return this;
    }

    /// <inheritdoc />
    public IReferenceScopeBuilder Sheet(int number)
    {
        _type = ReferenceType.Sheet;
        _location = number.ToString();
        return this;
    }

    /// <inheritdoc />
    public IReferenceScopeBuilder In(params string[] containers)
    {
        _containers.AddRange(containers);
        return this;
    }

    /// <inheritdoc />
    public IReferenceScopeBuilder Named(string name)
    {
        _location = name;
        return this;
    }

    /// <inheritdoc />
    public IReferenceScopeBuilder Number(int number)
    {
        _location = number.ToString();
        return this;
    }

    /// <inheritdoc />
    public Reference Build()
    {
        if (_type is null)
            throw new InvalidOperationException("Can not build Reference without a configured reference type.");

        var builder = new StringBuilder();

        builder.Append("Controller");

        //We will always assume the first container is the program.
        if (_containers.Count >= 1)
        {
            builder.Append($"/Programs/Program[@Name='{_containers[0]}']");
        }

        //We will always assume the second container is the routine.
        if (_containers.Count >= 2)
        {
            builder.Append($"/Routines/Routine[@Name='{_containers[1]}']");
        }

        builder.Append($"/{_type.Container}/{_type}[{_type.Identifier}='{_location}']");

        return Reference.To(builder.ToString());
    }
}