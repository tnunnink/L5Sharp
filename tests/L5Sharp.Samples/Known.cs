using System;
using System.IO;
using System.Reflection;

namespace L5Sharp.Samples
{
    public static class Known
    {
        // Get the directory where the currently executing assembly is located
        public static readonly string SamplesDirectory =
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
            throw new InvalidOperationException("Can not find directory.");

        public static readonly string Test = Path.Combine(SamplesDirectory, "Test.xml");
        public static readonly string Empty = Path.Combine(SamplesDirectory, "Empty.xml");
        public const string Template = @"C:\Users\tnunnink\Local\Transfer\Template.L5X";

        public const string DataType = "SimpleType";
        public const string AddOnInstruction = "aoi_Test";
        public const string Module = "TestCard";
        public const string Tag = "TestSimpleTag";
        public const string Program = "MainProgram";
        public const string Task = "Periodic";
    }

    public static class Export
    {
        public static readonly string BoolTest = Path.Combine(Known.SamplesDirectory, "DataTypes\\BoolTest.L5X");
    }
}