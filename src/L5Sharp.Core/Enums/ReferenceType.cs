using System.Linq;
using System.Text;

namespace L5Sharp.Core;

/// <summary>
/// Represents an abstract base type for a Logix reference type. This class provides
/// a framework for defining specific types of Logix references such as tags,
/// programs, routines, and other classifications.
/// </summary>
public abstract class ReferenceType : LogixEnum<ReferenceType, string>
{
    private ReferenceType(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Indicates whether the reference type is associated with a tag element.
    /// </summary>
    public bool IsTag => this == Tag || this == Parameter || this == LocalTag;

    /// <summary>
    /// Indicates whether the reference type is related to logic elements such as Rung, Line, or Sheet.
    /// </summary>
    public bool IsLogic => this == Rung || this == Line || this == Sheet;

    /// <summary>
    /// Determines whether the reference type is contextual, meaning it represents an element that can be contained
    /// in different scopes within an L5X project (Tag, Routine, Rung, Line, or Sheet).
    /// </summary>
    public bool IsContextual => this == Tag || this == Routine || IsLogic;

    /// <summary>
    /// Generates an XPath expression based on the current reference type and the provided identifier.
    /// </summary>
    /// <param name="identifier">The identifier to include in the XPath expression. For logic types, this corresponds to a number,
    /// whereas for non-logic types, it corresponds to a name.</param>
    /// <returns>A string representing the XPath expression for the specified identifier and reference type.</returns>
    public string GetXPath(string identifier)
    {
        var builder = new StringBuilder();

        var container = GetContainer();
        var attribute = IsLogic ? "@Number" : "@Name";
        builder.Append($"/{container}/{Value}[{attribute}='{identifier}']");

        return builder.ToString();
    }

    /// <summary>
    /// Retrieves a <see cref="ReferenceType"/> based on the specified entity type.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The entity type for which the <see cref="ReferenceType"/> is to be determined.
    /// Must implement <see cref="ILogixEntity"/>.
    /// </typeparam>
    /// <returns>The <see cref="ReferenceType"/> corresponding to the entity type <typeparamref name="TEntity"/>.</returns>
    public static ReferenceType FromType<TEntity>() where TEntity : ILogixEntity
    {
        return Parse(LogixSerializer.NamesFor(typeof(TEntity)).First());
    }

    /// <summary>
    /// Retrieves the element name that contains the reference type in the L5X schema. This is typically the pluralized
    /// reference value, but different for some types like code references of AOIs.
    /// </summary>
    protected abstract string GetContainer();

    /// <summary>
    /// Represents a Null <see cref="ReferenceType"/> value.
    /// </summary>
    /// <remarks>A <c>Null</c> scope will occur on element objects that have not been added to a container.</remarks>
    public static readonly ReferenceType Null = new NullReferenceType();

    /// <summary>
    /// Represents a DataType <see cref="ReferenceType"/> value.
    /// </summary>
    public static readonly ReferenceType DataType = new DataTypeReferenceType();

    /// <summary>
    /// Represents a Aoi (AddOnInstruction) <see cref="ReferenceType"/> value.
    /// </summary>
    public static readonly ReferenceType Aoi = new AoiReferenceType();

    /// <summary>
    /// Represents a Parameter <see cref="ReferenceType"/> value.
    /// </summary>
    public static readonly ReferenceType Parameter = new ParameterReferenceType();

    /// <summary>
    /// Represents a LocalTag <see cref="ReferenceType"/> value.
    /// </summary>
    public static readonly ReferenceType LocalTag = new LocalTagReferenceType();

    /// <summary>
    /// Represents a Module <see cref="ReferenceType"/> value.
    /// </summary>
    public static readonly ReferenceType Module = new ModuleReferenceType();

    /// <summary>
    /// Represents a Tag <see cref="ReferenceType"/> value.
    /// </summary>
    public static readonly ReferenceType Tag = new TagReferenceType();

    /// <summary>
    /// Represents a Program <see cref="ReferenceType"/> value.
    /// </summary>
    public static readonly ReferenceType Program = new ProgramReferenceType();

    /// <summary>
    /// Represents a Routine <see cref="ReferenceType"/> value.
    /// </summary>
    public static readonly ReferenceType Routine = new RoutineReferenceType();

    /// <summary>
    /// Represents a Task <see cref="ReferenceType"/> value.
    /// </summary>
    public static readonly ReferenceType Task = new TaskReferenceType();

    /// <summary>
    /// Represents a Rung <see cref="ReferenceType"/> value.
    /// </summary>
    public static readonly ReferenceType Rung = new RungReferenceType();

    /// <summary>
    /// Represents a Line <see cref="ReferenceType"/> value.
    /// </summary>
    public static readonly ReferenceType Line = new LineReferenceType();

    /// <summary>
    /// Represents a Sheet <see cref="ReferenceType"/> value.
    /// </summary>
    public static readonly ReferenceType Sheet = new SheetReferenceType();


    private class NullReferenceType() : ReferenceType(nameof(Null), string.Empty)
    {
        protected override string GetContainer() => string.Empty;
    }

    private class DataTypeReferenceType() : ReferenceType("datatype", "DataType")
    {
        protected override string GetContainer() => "DataTypes";
    }

    private class AoiReferenceType() : ReferenceType("aoi", "AddOnInstructionDefinition")
    {
        protected override string GetContainer() => "AddOnInstructionDefinitions";
    }

    private class ParameterReferenceType() : ReferenceType("parameter", "Parameter")
    {
        protected override string GetContainer() => "Parameters";
    }

    private class LocalTagReferenceType() : ReferenceType("localtag", "LocalTag")
    {
        protected override string GetContainer() => "LocalTags";
    }

    private class ModuleReferenceType() : ReferenceType("module", "Module")
    {
        protected override string GetContainer() => "Modules";
    }

    private class TagReferenceType() : ReferenceType("tag", "Tag")
    {
        protected override string GetContainer() => "Tags";
    }

    private class ProgramReferenceType() : ReferenceType("program", "Program")
    {
        protected override string GetContainer() => "Programs";
    }

    private class RoutineReferenceType() : ReferenceType("routine", "Routine")
    {
        protected override string GetContainer() => "Routines";
    }

    private class TaskReferenceType() : ReferenceType("task", "Task")
    {
        protected override string GetContainer() => "Tasks";
    }

    private class RungReferenceType() : ReferenceType("rung", "Rung")
    {
        protected override string GetContainer() => "RLLContent";
    }

    private class LineReferenceType() : ReferenceType("line", "Line")
    {
        protected override string GetContainer() => "STContent";
    }

    private class SheetReferenceType() : ReferenceType("sheet", "Sheet")
    {
        protected override string GetContainer() => "FBDContent";
    }
}