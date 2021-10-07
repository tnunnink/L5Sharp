using L5Sharp.Core;

namespace L5Sharp.Types
{
    public sealed class Null : Predefined
    {
        public Null() : base(LoadElement(nameof(Null).ToUpper()))
        {
        }
    }
}