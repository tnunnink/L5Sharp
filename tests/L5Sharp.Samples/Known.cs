using System.IO;
using System.Reflection;

namespace L5Sharp.Samples
{
    public static class Known
    {
        public static readonly string Directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

        public static readonly string Test = Path.Combine(Directory, "Test.xml");
        public static readonly string TestAcd = Path.Combine(Directory, "Test.ACD");
        public static readonly string Empty = Path.Combine(Directory, "Empty.xml");
        public static readonly string LotOfTags = Path.Combine(Directory, "LotOfTags.xml");
        public const string Example = @"C:\Users\tnunn\Documents\L5X\Example.L5X";

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