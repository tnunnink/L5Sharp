// ReSharper disable InconsistentNaming we want to match the XMl naming to prevent comparison issues.
namespace L5Sharp.L5X
{
    /// <summary>
    /// An enumeration of L5X element names for ease of referencing these value without using magic strings.
    /// </summary>
    public enum L5XElement
    {
        
        /// <summary>
        /// Represents the <see cref="AdditionalHelpText"/> L5X element.
        ///</summary>
        AdditionalHelpText,
        
        /// <summary>
        /// Represents the <see cref="AddOnInstructionDefinition"/> L5X element.
        ///</summary>
        AddOnInstructionDefinition,
        
        /// <summary>
        /// Represents the <see cref="AddOnInstructionDefinitions"/> L5X element.
        ///</summary>
        AddOnInstructionDefinitions,
        
        /// <summary>
        /// Represents the <see cref="AlarmAnalogParameters"/> L5X element.
        ///</summary>
        AlarmAnalogParameters,
        
        /// <summary>
        /// Represents the <see cref="AlarmConfig"/> L5X element.
        ///</summary>
        AlarmConfig,
        
        /// <summary>
        /// Represents the <see cref="AlarmDigitalParameters"/> L5X element.
        ///</summary>
        AlarmDigitalParameters,
        
        /// <summary>
        /// Represents the <see cref="Array"/> L5X element.
        ///</summary>
        Array,
        
        /// <summary>
        /// Represents the <see cref="ArrayMember"/> L5X element.
        ///</summary>
        ArrayMember,
        
        /// <summary>
        /// Represents the <see cref="Bus"/> L5X element.
        ///</summary>
        Bus,
        
        /// <summary>
        /// Represents the <see cref="CatNum"/> L5X element.
        ///</summary>
        CatNum,
        
        /// <summary>
        /// Represents the <see cref="ChildProgram"/> L5X element.
        ///</summary>
        ChildProgram,
        
        /// <summary>
        /// Represents the <see cref="ChildPrograms"/> L5X element.
        ///</summary>
        ChildPrograms,
        
        /// <summary>
        /// Represents the <see cref="Comment"/> L5X element.
        ///</summary>
        Comment,
        
        /// <summary>
        /// Represents the <see cref="Comments"/> L5X element.
        ///</summary>
        Comments,
        
        /// <summary>
        /// Represents the <see cref="Communications"/> L5X element.
        ///</summary>
        Communications,
        
        /// <summary>
        /// Represents the <see cref="ConfigData"/> L5X element.
        ///</summary>
        ConfigData,
        
        /// <summary>
        /// Represents the <see cref="ConfigID"/> L5X element.
        ///</summary>
        ConfigID,
        
        /// <summary>
        /// Represents the <see cref="ConfigScript"/> L5X element.
        ///</summary>
        ConfigScript,
        
        /// <summary>
        /// Represents the <see cref="ConfigTag"/> L5X element.
        ///</summary>
        ConfigTag,
        
        /// <summary>
        /// Represents the <see cref="Connection"/> L5X element.
        ///</summary>
        Connection,
        
        /// <summary>
        /// Represents the <see cref="Connections"/> L5X element.
        ///</summary>
        Connections,
        
        /// <summary>
        /// Represents the <see cref="Controller"/> L5X element.
        ///</summary>
        Controller,
        
        /// <summary>
        /// Represents the <see cref="CST"/> L5X element.
        ///</summary>
        CST,
        
        /// <summary>
        /// Represents the <see cref="Data"/> L5X element.
        ///</summary>
        Data,
        
        /// <summary>
        /// Represents the <see cref="DataLogs"/> L5X element.
        ///</summary>
        DataLogs,
        
        /// <summary>
        /// Represents the <see cref="DataType"/> L5X element.
        ///</summary>
        DataType,
        
        /// <summary>
        /// Represents the <see cref="DataTypes"/> L5X element.
        ///</summary>
        DataTypes,
        
        /// <summary>
        /// Represents the <see cref="DataValue"/> L5X element.
        ///</summary>
        DataValue,
        
        /// <summary>
        /// Represents the <see cref="DataValueMember"/> L5X element.
        ///</summary>
        DataValueMember,
        
        /// <summary>
        /// Represents the <see cref="DefaultData"/> L5X element.
        ///</summary>
        DefaultData,
        
        /// <summary>
        /// Represents the <see cref="Description"/> L5X element.
        ///</summary>
        Description,
        
        /// <summary>
        /// Represents the <see cref="EKey"/> L5X element.
        ///</summary>
        EKey,
        
        /// <summary>
        /// Represents the <see cref="Element"/> L5X element.
        ///</summary>
        Element,
        
        /// <summary>
        /// Represents the <see cref="EncodedData"/> L5X element.
        ///</summary>
        EncodedData,
        
        /// <summary>
        /// Represents the <see cref="EngineeringUnit"/> L5X element.
        ///</summary>
        EngineeringUnit,
        
        /// <summary>
        /// Represents the <see cref="EngineeringUnits"/> L5X element.
        ///</summary>
        EngineeringUnits,
        
        /// <summary>
        /// Represents the <see cref="EthernetPort"/> L5X element.
        ///</summary>
        EthernetPort,
        
        /// <summary>
        /// Represents the <see cref="EthernetPorts"/> L5X element.
        ///</summary>
        EthernetPorts,
        
        /// <summary>
        /// Represents the <see cref="EventInfo"/> L5X element.
        ///</summary>
        EventInfo,
        
        /// <summary>
        /// Represents the <see cref="ExtendedProperties"/> L5X element.
        ///</summary>
        ExtendedProperties,
        
        /// <summary>
        /// Represents the <see cref="InputTag"/> L5X element.
        ///</summary>
        InputTag,
        
        /// <summary>
        /// Represents the <see cref="LocalTag"/> L5X element.
        ///</summary>
        LocalTag,
        
        /// <summary>
        /// Represents the <see cref="LocalTags"/> L5X element.
        ///</summary>
        LocalTags,
        
        /// <summary>
        /// Represents the <see cref="Member"/> L5X element.
        ///</summary>
        Member,
        
        /// <summary>
        /// Represents the <see cref="Members"/> L5X element.
        ///</summary>
        Members,
        
        /// <summary>
        /// Represents the <see cref="MessageParameters"/> L5X element.
        ///</summary>
        MessageParameters,
        
        /// <summary>
        /// Represents the <see cref="Module"/> L5X element.
        ///</summary>
        Module,
        
        /// <summary>
        /// Represents the <see cref="Modules"/> L5X element.
        ///</summary>
        Modules,
        
        /// <summary>
        /// Represents the <see cref="OutputTag"/> L5X element.
        ///</summary>
        OutputTag,
        
        /// <summary>
        /// Represents the <see cref="Parameter"/> L5X element.
        ///</summary>
        Parameter,
        
        /// <summary>
        /// Represents the <see cref="Parameters"/> L5X element.
        ///</summary>
        Parameters,
        
        /// <summary>
        /// Represents the <see cref="PL"/> L5X element.
        ///</summary>
        PL,
        
        /// <summary>
        /// Represents the <see cref="Port"/> L5X element.
        ///</summary>
        Port,
        
        /// <summary>
        /// Represents the <see cref="Ports"/> L5X element.
        ///</summary>
        Ports,
        
        /// <summary>
        /// Represents the <see cref="Program"/> L5X element.
        ///</summary>
        Program,
        
        /// <summary>
        /// Represents the <see cref="Programs"/> L5X element.
        ///</summary>
        Programs,
        
        /*/// <summary>
        /// Represents the <see cref="public"/> L5X element.
        ///</summary>
        public,*/
        
        /// <summary>
        /// Represents the <see cref="QuickWatchList"/> L5X element.
        ///</summary>
        QuickWatchList,
        
        /// <summary>
        /// Represents the <see cref="QuickWatchLists"/> L5X element.
        ///</summary>
        QuickWatchLists,
        
        /// <summary>
        /// Represents the <see cref="RedundancyInfo"/> L5X element.
        ///</summary>
        RedundancyInfo,
        
        /// <summary>
        /// Represents the <see cref="RevisionNote"/> L5X element.
        ///</summary>
        RevisionNote,
        
        /// <summary>
        /// Represents the <see cref="RLLContent"/> L5X element.
        ///</summary>
        RLLContent,
        
        /// <summary>
        /// Represents the <see cref="Routine"/> L5X element.
        ///</summary>
        Routine,
        
        /// <summary>
        /// Represents the <see cref="Routines"/> L5X element.
        ///</summary>
        Routines,
        
        /// <summary>
        /// Represents the <see cref="RSLogix5000Content"/> L5X element.
        ///</summary>
        RSLogix5000Content,
        
        /// <summary>
        /// Represents the <see cref="Rung"/> L5X element.
        ///</summary>
        Rung,
        
        /// <summary>
        /// Represents the <see cref="SafetyInfo"/> L5X element.
        ///</summary>
        SafetyInfo,
        
        /// <summary>
        /// Represents the <see cref="ScheduledProgram"/> L5X element.
        ///</summary>
        ScheduledProgram,
        
        /// <summary>
        /// Represents the <see cref="ScheduledPrograms"/> L5X element.
        ///</summary>
        ScheduledPrograms,
        
        /// <summary>
        /// Represents the <see cref="Security"/> L5X element.
        ///</summary>
        Security,
        
        /// <summary>
        /// Represents the <see cref="Structure"/> L5X element.
        ///</summary>
        Structure,
        
        /// <summary>
        /// Represents the <see cref="StructureMember"/> L5X element.
        ///</summary>
        StructureMember,
        
        /// <summary>
        /// Represents the <see cref="Tag"/> L5X element.
        ///</summary>
        Tag,
        
        /// <summary>
        /// Represents the <see cref="Tags"/> L5X element.
        ///</summary>
        Tags,
        
        /// <summary>
        /// Represents the <see cref="Task"/> L5X element.
        ///</summary>
        Task,
        
        /// <summary>
        /// Represents the <see cref="Tasks"/> L5X element.
        ///</summary>
        Tasks,
        
        /// <summary>
        /// Represents the <see cref="Text"/> L5X element.
        ///</summary>
        Text,
        
        /// <summary>
        /// Represents the <see cref="TimeSyncEnabled"/> L5X element.
        ///</summary>
        TimeSyncEnabled,
        
        /// <summary>
        /// Represents the <see cref="TimeSynchronize"/> L5X element.
        ///</summary>
        TimeSynchronize,
        
        /// <summary>
        /// Represents the <see cref="Trends"/> L5X element.
        ///</summary>
        Trends,
        
        /// <summary>
        /// Represents the <see cref="Vendor"/> L5X element.
        ///</summary>
        Vendor,
        
        /// <summary>
        /// Represents the <see cref="Version"/> L5X element.
        ///</summary>
        Version,
        
        /// <summary>
        /// Represents the <see cref="WallClockTime"/> L5X element.
        ///</summary>
        WallClockTime,
        
        /// <summary>
        /// Represents the <see cref="WatchTag"/> L5X element.
        ///</summary>
        WatchTag,
    }
}