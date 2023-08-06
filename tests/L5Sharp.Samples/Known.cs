using System;
using System.IO;

namespace L5Sharp.Samples
{
    public static class Known
    {
        public static readonly string Test = Path.Combine(Environment.CurrentDirectory, "Test.xml");
        public static readonly string Empty = Path.Combine(Environment.CurrentDirectory, "Empty.xml");
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
        public static readonly string BoolTest = Path.Combine(Environment.CurrentDirectory, "BoolTest.xml");
    }
}