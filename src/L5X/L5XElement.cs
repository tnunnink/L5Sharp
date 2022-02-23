// ReSharper disable InconsistentNaming we want to match the XMl naming to prevent comparison issues.
namespace L5Sharp.L5X
{
    /// <summary>
    /// An enumeration of L5X element names for ease of referencing these value without using magic strings.
    /// </summary>
    public enum L5XElement
    {
        /// <summary>
        /// Represents the <see cref="RsLogix5000Content"/> L5X element name.
        /// </summary>
        RsLogix5000Content,

        /// <summary>
        /// Represents the <see cref="Controller"/> L5X element name.
        /// </summary>
        Controller,

        /// <summary>
        /// Represents the <see cref="DataTypes"/> L5X element name.
        /// </summary>
        DataTypes,

        /// <summary>
        /// Represents the <see cref="DataType"/> L5X element name.
        /// </summary>
        DataType,

        /// <summary>
        /// Represents the <see cref="Members"/> L5X element name.
        /// </summary>
        Members,

        /// <summary>
        /// Represents the <see cref="Member"/> L5X element name.
        /// </summary>
        Member,

        /// <summary>
        /// Represents the <see cref="Modules"/> L5X element name.
        /// </summary>
        Modules,

        /// <summary>
        /// Represents the <see cref="Module"/> L5X element name.
        /// </summary>
        Module,

        /// <summary>
        /// Represents the <see cref="Ports"/> L5X element name.
        /// </summary>
        Ports,

        /// <summary>
        /// Represents the <see cref="Port"/> L5X element name.
        /// </summary>
        Port,

        /// <summary>
        /// Represents the <see cref="Bus"/> L5X element name.
        /// </summary>
        Bus,

        /// <summary>
        /// Represents the <see cref="Connections"/> L5X element name.
        /// </summary>
        Connections,

        /// <summary>
        /// Represents the <see cref="Connection"/> L5X element name.
        /// </summary>
        Connection,

        /// <summary>
        /// Represents the <see cref="AddOnInstructionDefinitions"/> L5X element name.
        /// </summary>
        AddOnInstructionDefinitions,

        /// <summary>
        /// Represents the <see cref="AddOnInstructionDefinition"/> L5X element name.
        /// </summary>
        AddOnInstructionDefinition,

        /// <summary>
        /// Represents the <see cref="Parameters"/> L5X element name.
        /// </summary>
        Parameters,

        /// <summary>
        /// Represents the <see cref="Parameter"/> L5X element name.
        /// </summary>
        Parameter,

        /// <summary>
        /// Represents the <see cref="Tags"/> L5X element name.
        /// </summary>
        Tags,

        /// <summary>
        /// Represents the <see cref="Tag"/> L5X element name.
        /// </summary>
        Tag,

        /// <summary>
        /// Represents the <see cref="Programs"/> L5X element name.
        /// </summary>
        Programs,

        /// <summary>
        /// Represents the <see cref="Program"/> L5X element name.
        /// </summary>
        Program,

        /// <summary>
        /// Represents the <see cref="Routines"/> L5X element name.
        /// </summary>
        Routines,

        /// <summary>
        /// Represents the <see cref="Routine"/> L5X element name.
        /// </summary>
        Routine,

        /// <summary>
        /// Represents the <see cref="RLLContent"/> L5X element name.
        /// </summary>
        RLLContent,

        /// <summary>
        /// Represents the <see cref="StContent"/> L5X element name.
        /// </summary>
        StContent,

        /// <summary>
        /// Represents the <see cref="Rungs"/> L5X element name.
        /// </summary>
        Rungs,

        /// <summary>
        /// Represents the <see cref="Rung"/> L5X element name.
        /// </summary>
        Rung,

        /// <summary>
        /// Represents the <see cref="Tasks"/> L5X element name.
        /// </summary>
        Tasks,

        /// <summary>
        /// Represents the <see cref="Task"/> L5X element name.
        /// </summary>
        Task,
        
        /// <summary>
        /// Represent the <see cref="ScheduledProgram"/> L5X element name.
        /// </summary>
        ScheduledProgram,

        /// <summary>
        /// Represents the <see cref="Data"/> L5X element name.
        /// </summary>
        Data,

        /// <summary>
        /// Represents the <see cref="DataValue"/> L5X element name.
        /// </summary>
        DataValue,

        /// <summary>
        /// Represents the <see cref="Array"/> L5X element name.
        /// </summary>
        Array,

        /// <summary>
        /// Represents the <see cref="Structure"/> L5X element name.
        /// </summary>
        Structure,

        /// <summary>
        /// Represents the <see cref="Element"/> L5X element name.
        /// </summary>
        Element,

        /// <summary>
        /// Represents the <see cref="ArrayMember"/> L5X element name.
        /// </summary>
        ArrayMember,

        /// <summary>
        /// Represents the <see cref="DataValueMember"/> L5X element name.
        /// </summary>
        DataValueMember,

        /// <summary>
        /// Represents the <see cref="StructureMember"/> L5X element name.
        /// </summary>
        StructureMember,
        
        /// <summary>
        /// Represents the <see cref="EventInfo"/> L5X element name.
        /// </summary>
        EventInfo,
        
        /// <summary>
        /// Represents the <see cref="Comment"/> L5X element name.
        /// </summary>
        Comment,
        
        /// <summary>
        /// Represents the <see cref="Comments"/> L5X element name.
        /// </summary>
        Comments,
        
        /// <summary>
        /// Represents the <see cref="InputTag"/> L5X element name.
        /// </summary>
        InputTag,
        
        /// <summary>
        /// Represents the <see cref="OutputTag"/> L5X element name.
        /// </summary>
        OutputTag,
        
        /// <summary>
        /// Represents the <see cref="AlarmDigitalParameters"/> L5X element name.
        /// </summary>
        AlarmDigitalParameters,
        
        /// <summary>
        /// Represents the <see cref="AlarmAnalogParameters"/> L5X element name.
        /// </summary>
        AlarmAnalogParameters,
    }
}