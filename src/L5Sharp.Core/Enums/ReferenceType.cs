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
    /// The name of the element that represents the container element for this element/scope type.
    /// This is required to help with build valid scope paths.
    /// </summary>
    public abstract string Container { get; }

    /// <summary>
    /// Gets the property used to uniquely identify a specific instance within its scope or context.
    /// The value of this property is critical for defining the identity of the object in the hierarchy.
    /// </summary>
    public abstract string Identifier { get; }

    /// <summary>
    /// Gets a value indicating whether the reference type can be defined in different scopes/containers within
    /// the L5X context (e.g., Tag, Routine, Rung).
    /// </summary>
    public virtual bool SupportsScope => false;

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
    /// Represents a Module <see cref="ReferenceType"/> value.
    /// </summary>
    public static readonly ReferenceType Aoi = new AoiReferenceType();

    /// <summary>
    /// Represents a Module <see cref="ReferenceType"/> value.
    /// </summary>
    public static readonly ReferenceType Module = new ModuleReferenceType();

    /// <summary>
    /// Represents a Program <see cref="ReferenceType"/> value.
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
        public override string Container => string.Empty;
        public override string Identifier => string.Empty;
    }

    private class DataTypeReferenceType() : ReferenceType(nameof(DataType), "DataType")
    {
        public override string Container => "DataTypes";
        public override string Identifier => "@Name";
    }

    private class AoiReferenceType() : ReferenceType("AddOnInstruction", "AddOnInstructionDefinition")
    {
        public override string Container => "AddOnInstructionDefinitions";
        public override string Identifier => "@Name";
    }

    private class ModuleReferenceType() : ReferenceType(nameof(Module), "Module")
    {
        public override string Container => "Modules";
        public override string Identifier => "@Name";
    }

    private class TagReferenceType() : ReferenceType(nameof(Tag), "Tag")
    {
        public override string Container => "Tags";

        public override string Identifier => "@Name";
        public override bool SupportsScope => true;
    }

    private class ProgramReferenceType() : ReferenceType(nameof(Program), "Program")
    {
        public override string Container => "Programs";
        public override string Identifier => "@Name";
    }

    private class RoutineReferenceType() : ReferenceType(nameof(Routine), "Routine")
    {
        public override string Container => "Routines";
        public override string Identifier => "@Name";
        public override bool SupportsScope => true;
    }

    private class TaskReferenceType() : ReferenceType(nameof(Task), "Task")
    {
        public override string Container => "Tasks";
        public override string Identifier => "@Name";
    }

    private class RungReferenceType() : ReferenceType(nameof(Rung), "Rung")
    {
        public override string Container => "RLLContent";
        public override string Identifier => "@Number";
        public override bool SupportsScope => true;
    }

    private class LineReferenceType() : ReferenceType(nameof(Line), "Line")
    {
        public override string Container => "STContent";
        public override string Identifier => "@Number";
        public override bool SupportsScope => true;
    }

    private class SheetReferenceType() : ReferenceType(nameof(Sheet), "Sheet")
    {
        public override string Container => "FBDContent";
        public override string Identifier => "@Number";

        public override bool SupportsScope => true;
    }
}