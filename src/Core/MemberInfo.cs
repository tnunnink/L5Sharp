using System;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    public class MemberInfo : Attribute
    {
        public MemberRadix Radix { get; set; }
        public string ExternalAccess { get; set; }
        public string Description { get; set; }
    }

    public enum MemberRadix
    {
        Decimal,
        Ascii
    }
}