
using System.IO;

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
        public static readonly string Empty = Path.Combine("Projects", "Empty.L5X");
        public static readonly string LotOfTags = Path.Combine("Projects", "LotOfTags.L5X");
        public static readonly string Simple = Path.Combine("Projects", "Simple.L5X");
        public static readonly string Test = Path.Combine("Projects", "Test.L5X");
    }

    /// <summary>
    /// Contains relative paths to data type L5X sample files used in tests.
    /// </summary>
    public static class DataTypes
    {
        public static readonly string AlarmType = Path.Combine("DataTypes", "AlarmType.L5X");
        public static readonly string AoiTest = Path.Combine("DataTypes", "aoi_Test.L5X");
        public static readonly string ArrayType = Path.Combine("DataTypes", "ArrayType.L5X");
        public static readonly string BoolTest = Path.Combine("DataTypes", "BoolTest.L5X");
        public static readonly string ComplexType = Path.Combine("DataTypes", "ComplexType.L5X");
        public static readonly string InvalidType = Path.Combine("DataTypes", "InvalidType.L5X");
        public static readonly string ModuleType = Path.Combine("DataTypes", "ModuleType.L5X");
        public static readonly string MultipleDataTypes = Path.Combine("DataTypes", "MultipleDataTypes.L5X");
        public static readonly string NestedType = Path.Combine("DataTypes", "NestedType.L5X");
        public static readonly string SimpleType = Path.Combine("DataTypes", "SimpleType.L5X");
    }

    /// <summary>
    /// Contains relative paths to instruction L5X sample files used in tests.
    /// </summary>
    public static class Aoi
    {
        public static readonly string AoiSigned = Path.Combine("Instructions", "aoiSigned_AOI.L5X");
        public static readonly string AoiTest = Path.Combine("Instructions", "aoi_Test.L5X");
    }

    /// <summary>
    /// Contains relative paths to module L5X sample files used in tests.
    /// </summary>
    public static class Modules
    {
        public static readonly string FlexBus = Path.Combine("Modules", "FlexBus.L5X");
        public static readonly string FlexMod1 = Path.Combine("Modules", "Flex_Mod_1_Module.L5X");
        public static readonly string FlexMod8 = Path.Combine("Modules", "Flex_Mod_8.L5X");
        public static readonly string L1M1 = Path.Combine("Modules", "L1M1.L5X");
        public static readonly string L1M1D1 = Path.Combine("Modules", "L1M1D1.L5X");
        public static readonly string LM6C1 = Path.Combine("Modules", "LM6C1.L5X");
        public static readonly string LocalMod1 = Path.Combine("Modules", "Local_Mod_1.L5X");
        public static readonly string LocalMod4 = Path.Combine("Modules", "Local_Mod_4.L5X");
        public static readonly string LocalMod9 = Path.Combine("Modules", "Local_Mod_9.L5X");
        public static readonly string RackIo = Path.Combine("Modules", "RackIO.L5X");
        public static readonly string RackMode3 = Path.Combine("Modules", "Rack_Mode_3.L5X");
        public static readonly string RackMod2 = Path.Combine("Modules", "Rack_Mod_2.L5X");
        public static readonly string TestCard = Path.Combine("Modules", "TestCard.L5X");
        public static readonly string TestEtap = Path.Combine("Modules", "TestEtap.L5X");
        public static readonly string TestImport = Path.Combine("Modules", "TestImport.L5X");
    }

    /// <summary>
    /// Contains relative paths to program L5X sample files used in tests.
    /// </summary>
    public static class Programs
    {
        public static readonly string Empty = Path.Combine("Programs", "Empty.L5X");
        public static readonly string TestProgram = Path.Combine("Programs", "TestProgram.L5X");
    }

    /// <summary>
    /// Contains relative paths to routine L5X sample files used in tests.
    /// </summary>
    public static class Routines
    {
        public static readonly string Fbd = Path.Combine("Routines", "FBD.L5X");
        public static readonly string Main = Path.Combine("Routines", "Main.L5X");
        public static readonly string Sfc = Path.Combine("Routines", "SFC.L5X");
        public static readonly string St = Path.Combine("Routines", "ST.L5X");
    }

    /// <summary>
    /// Contains relative paths to rung L5X sample files used in tests.
    /// </summary>
    public static class Rungs
    {
        public static readonly string MessageRung = Path.Combine("Rungs", "Message_Rung.L5X");
        public static readonly string Rung0 = Path.Combine("Rungs", "Rung0_from_Main.L5X");
        public static readonly string Rung1 = Path.Combine("Rungs", "Rung1_from_Main.L5X");
        public static readonly string Rung2 = Path.Combine("Rungs", "Rung2_from_Main.L5X");
    }

    /// <summary>
    /// Contains relative paths to trend L5X sample files used in tests.
    /// </summary>
    public static class Trends
    {
        public static readonly string Test = Path.Combine("Trends", "Test.L5X");
        public static readonly string TriggerTrend = Path.Combine("Trends", "TriggerTrend.L5X");
    }
}