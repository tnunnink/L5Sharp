using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Types
{
    public sealed class Undefined : Predefined
    {
        public Undefined() : base(nameof(Undefined), DataTypeFamily.None)
        {
        }
    }
}