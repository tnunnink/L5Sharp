namespace L5Sharp.Tests
{
    public static class Known
    {
        public static readonly string Test = Path.Combine(Environment.CurrentDirectory, @"TestFiles\Test.xml");
        public static readonly string Template = @"C:\Users\tnunnink\Local\Transfer\Template.L5X";
        public static readonly string Empty = Path.Combine(Environment.CurrentDirectory, @"TestFiles\Empty.xml");
        
        public static readonly string DataType = "SimpleType";
        public static readonly string AddOnInstruction = "aoi_Test";
        public static readonly string Module = "TestCard";
        public static readonly string Tag = "TestSimpleTag";
        public static readonly string Program = "MainProgram";
        public static readonly string Task = "Periodic";

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