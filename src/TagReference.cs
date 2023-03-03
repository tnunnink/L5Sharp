using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp;

public class TagReference
{
    public TagName TagName { get; set; }
    public NeutralText Reference { get; set; }
    public string Container { get; set; }
    public string Routine { get; set; }
    public RoutineType RoutineType { get; set; }
    public string Location { get; set; }
}