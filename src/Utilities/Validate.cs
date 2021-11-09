using System;
using System.Linq;
using System.Text.RegularExpressions;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Exceptions;

namespace L5Sharp.Utilities
{
    public static class Validate
    {
        public static void Name(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Name property can not be null or empty");

            if (!Regex.IsMatch(name, @"^[a-zA-Z_][a-zA-Z0-9_]{0,39}$"))
                throw new ComponentNameInvalidException(name);
        }
        
        public static void DataTypeName(string name)
        {
            if (Logix.ContainsType(name))
                throw new ComponentNameCollisionException(name, typeof(Predefined));
        }
        
        public static void MemberDataType(IMember<IDataType> member, IDataType dataType)
        {
            if (member.DataType.Equals(dataType))
                throw new CircularReferenceException(
                    $"Member can not be same type as parent type '{member.DataType.Name}'");
        }
    }
}