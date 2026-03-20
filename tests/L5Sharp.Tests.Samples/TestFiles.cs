
// ReSharper disable InconsistentNaming

namespace L5Sharp.Tests.Samples;

/// <summary>
/// Provides utilities for accessing test sample L5X files that are copied to the output directory.
/// </summary>
public static class TestFiles
{
    /// <summary>
    /// Contains relative paths to project-level L5X sample files used in tests.
    /// </summary>
    public static class Projects
    {
        public const string Empty = "Projects\\Empty.L5X";
        public const string LotOfTags = "Projects\\LotOfTags.L5X";
        public const string Simple = "Projects\\Simple.L5X";
        public const string Test = "Projects\\Test.L5X";
    }

    /// <summary>
    /// Contains relative paths to data type L5X sample files used in tests.
    /// </summary>
    public static class DataTypes
    {
        public const string AlarmType = "DataTypes\\AlarmType.L5X";
        public const string AoiTest = "DataTypes\\aoi_Test.L5X";
        public const string ArrayType = "DataTypes\\ArrayType.L5X";
        public const string BoolTest = "DataTypes\\BoolTest.L5X";
        public const string ComplexType = "DataTypes\\ComplexType.L5X";
        public const string InvalidType = "DataTypes\\InvalidType.L5X";
        public const string ModuleType = "DataTypes\\ModuleType.L5X";
        public const string MultipleDataTypes = "DataTypes\\MultipleDataTypes.L5X";
        public const string NestedType = "DataTypes\\NestedType.L5X";
        public const string SimpleType = "DataTypes\\SimpleType.L5X";
    }

    /// <summary>
    /// Contains relative paths to instruction L5X sample files used in tests.
    /// </summary>
    public static class Aoi
    {
        public const string AoiSigned = "Instructions\\aoiSigned_AOI.L5X";
        public const string AoiTest = "Instructions\\aoi_Test.L5X";
    }

    /// <summary>
    /// Contains relative paths to module L5X sample files used in tests.
    /// </summary>
    public static class Modules
    {
        public const string FlexBus = "Modules\\FlexBus.L5X";
        public const string FlexMod1 = "Modules\\Flex_Mod_1_Module.L5X";
        public const string FlexMod8 = "Modules\\Flex_Mod_8.L5X";
        public const string L1M1 = "Modules\\L1M1.L5X";
        public const string L1M1D1 = "Modules\\L1M1D1.L5X";
        public const string LM6C1 = "Modules\\LM6C1.L5X";
        public const string LocalMod1 = "Modules\\Local_Mod_1.L5X";
        public const string LocalMod4 = "Modules\\Local_Mod_4.L5X";
        public const string LocalMod9 = "Modules\\Local_Mod_9.L5X";
        public const string RackIo = "Modules\\RackIO.L5X";
        public const string RackMode3 = "Modules\\Rack_Mode_3.L5X";
        public const string RackMod2 = "Modules\\Rack_Mod_2.L5X";
        public const string TestCard = "Modules\\TestCard.L5X";
        public const string TestEtap = "Modules\\TestEtap.L5X";
        public const string TestImport = "Modules\\TestImport.L5X";
    }

    /// <summary>
    /// Contains relative paths to program L5X sample files used in tests.
    /// </summary>
    public static class Programs
    {
        public const string Empty = "Programs\\Empty.L5X";
        public const string TestProgram = "Programs\\TestProgram.L5X";
    }

    /// <summary>
    /// Contains relative paths to routine L5X sample files used in tests.
    /// </summary>
    public static class Routines
    {
        public const string Fbd = "Routines\\FBD.L5X";
        public const string Main = "Routines\\Main.L5X";
        public const string Sfc = "Routines\\SFC.L5X";
        public const string St = "Routines\\ST.L5X";
    }

    /// <summary>
    /// Contains relative paths to rung L5X sample files used in tests.
    /// </summary>
    public static class Rungs
    {
        public const string MessageRung = "Rungs\\Message_Rung.L5X";
        public const string Rung0 = "Rungs\\Rung0_from_Main.L5X";
        public const string Rung1 = "Rungs\\Rung1_from_Main.L5X";
        public const string Rung2 = "Rungs\\Rung2_from_Main.L5X";
    }

    /// <summary>
    /// Contains relative paths to trend L5X sample files used in tests.
    /// </summary>
    public static class Trends
    {
        public const string Test = "Trends\\Test.L5X";
        public const string TriggerTrend = "Trends\\TriggerTrend.L5X";
    }
}