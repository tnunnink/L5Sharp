using L5Sharp.Enums;

namespace L5Sharp.Core
{
    public class Rung : IRung
    {
        public Rung(int number, string comment = null, string text = null, RungType type = null)
        {
            Number = number;
            Type = type;
            Comment = comment;
            Text = text;
        }
        public int Number { get; }
        public RungType Type { get; }
        public string Comment { get; }
        public string Text { get; }
    }
}