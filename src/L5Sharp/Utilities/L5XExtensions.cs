using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Linq;

namespace L5Sharp.Utilities
{
    public static class L5XExtensions
    {
        //this need work. too many assumptions
        public static string GetOperandOfTag(this XElement element)
        {
            var operandName = element.Attribute("Name")?.Value ?? string.Empty;

            var parent = element.Parent;

            if (parent == null) return operandName;
            
            while (parent.Name != "Tag")
            {
                var name = parent.Attribute("Name")?.Value;
                var index = parent.Attribute("Name")?.Value;

                if (name != null)
                    operandName = operandName.StartsWith('[') ? $"{name}{operandName}" : $"{name}.{operandName}";
                if (index != null)
                    operandName = $"{index}.{operandName}";

                if (parent.Parent == null) break;
                parent = parent.Parent;
            }

            return $".{operandName}";
        }
    }
}