namespace L5Sharp.Tests
{
    public static class Known
    {
        public static readonly string Test = Path.Combine(Environment.CurrentDirectory, @"TestFiles\Test.xml");
        public const string Template = @"C:\Users\tnunnink\Local\Transfer\Template.L5X";
        public static readonly string Empty = Path.Combine(Environment.CurrentDirectory, @"TestFiles\Empty.xml");

        public const string DataType = "SimpleType";
        public const string AddOnInstruction = "aoi_Test";
        public const string Module = "TestCard";
        public const string Tag = "TestSimpleTag";
        public const string Program = "MainProgram";
        public const string Task = "Periodic";

        /*public static DataType SimpleType = new()
        {
            Name = "SimpleType",
            Description = "This is a test data type that contains simple atomic types with an updated description",
            Members = new List<DataTypeMember>
            {
                new() { Name = "BoolMember", DataType = "BOOL", Description = "Test Bool", Radix = Radix.Hex},
                new() { Name = "SintMember", DataType = "SINT", Description = "Test Sint", Radix = Radix.Hex },
                new() { Name = "IntMember", DataType = "INT", Description = "Test Int", Radix = Radix.Octal },
                new() { Name = "DintMember", DataType = "DINT", Description = "Test Dint", Radix = Radix.Ascii },
                new() { Name = "LintMember", DataType = "LINT", Description = "Test Lint" },
                new() { Name = "RealMember", DataType = "REAL", Description = "Test Real" }
            }
        };*/
    }
}