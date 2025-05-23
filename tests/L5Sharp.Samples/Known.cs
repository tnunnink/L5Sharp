﻿using System.IO;
using System.Reflection;

namespace L5Sharp.Samples
{
    public static class Known
    {
        public static readonly string Directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

        public static readonly string Test = Path.Combine(Directory, "Test.L5X");
        public static readonly string TestAcd = Path.Combine(Directory, "Test.ACD");
        public static readonly string Empty = Path.Combine(Directory, "Empty.L5X");
        public static readonly string LotOfTags = Path.Combine(Directory, "LotOfTags.L5X");
        public static readonly string ModuleExport = Path.Combine(Directory, "Modules", "TestCard.L5X");
        public static readonly string ModuleRackIoExport = Path.Combine(Directory, "Modules", "RackIO.L5X");
        public static readonly string DataTypeExport = Path.Combine(Directory, "DataTypes", "ComplexType.L5X");
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