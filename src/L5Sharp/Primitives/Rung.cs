using L5Sharp.Enumerations;

namespace L5Sharp.Primitives
{
    public class Rung
    {
        public int Number { get; set; }
        public RungType Type { get; set; }
        public string Comment { get; set; }
        public string Text { get; set; }
    }
}