using L5Sharp.Enums;

namespace L5Sharp
{
    public interface IRung
    {
        int Number { get; }
        RungType Type { get; }
        string Comment { get; }
        string Text { get; }
    }
}