using System.IO;
using System.Runtime.CompilerServices;

namespace L5Sharp.Tests.Samples;

public static class Known
{
    private static string GetThisFilePath([CallerFilePath] string? path = null) => path ?? string.Empty;
    public static readonly string Directory = Path.GetDirectoryName(GetThisFilePath())!;
    public static readonly string ModuleExport = Path.Combine(Directory, "Modules", "TestCard.L5X");

    public const string DataType = "SimpleType";
    public const string AddOnInstruction = "aoi_Test";
    public const string Module = "TestCard";
    public const string Tag = "TestSimpleTag";
    public const string Program = "MainProgram";
    public const string Task = "Periodic";
}