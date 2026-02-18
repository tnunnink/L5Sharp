using System.IO;
using System.Runtime.CompilerServices;

namespace L5Sharp.Samples
{
    public static class Known
    {
        private static string GetThisFilePath([CallerFilePath] string? path = null) => path ?? string.Empty;
        public static readonly string Directory = Path.GetDirectoryName(GetThisFilePath())!;

        public static readonly string Test = Path.Combine(Directory, "Test.L5X");
        public static readonly string Empty = Path.Combine(Directory, "Empty.L5X");
        public static readonly string Simple = Path.Combine(Directory, "Simple.L5X");
        public static readonly string LotOfTags = Path.Combine(Directory, "LotOfTags.L5X");
        public static readonly string AoiSignedExport = Path.Combine(Directory, "Instructions", "aoiSigned_AOI.L5X");
        public static readonly string ModuleExport = Path.Combine(Directory, "Modules", "TestCard.L5X");
        public static readonly string ModuleRackIoExport = Path.Combine(Directory, "Modules", "RackIO.L5X");
        public static readonly string DataTypeExport = Path.Combine(Directory, "DataTypes", "ComplexType.L5X");
        public static readonly string ProgramExport = Path.Combine(Directory, "Programs", "TestProgram.L5X");
        public static readonly string RoutineExport = Path.Combine(Directory, "Routines", "Main.L5X");
        public const string Example = @"C:\Users\tnunnink\Documents\Rockwell\Example.L5X";

        public const string DataType = "SimpleType";
        public const string AddOnInstruction = "aoi_Test";
        public const string Module = "TestCard";
        public const string Tag = "TestSimpleTag";
        public const string Program = "MainProgram";
        public const string Task = "Periodic";
    }

    public static class Export
    {
        public static readonly string BoolTest = Path.Combine(Known.Directory, "DataTypes\\BoolTest.L5X");
    }
}