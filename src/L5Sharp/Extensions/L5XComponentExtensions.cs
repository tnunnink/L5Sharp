using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Enumerations;
using L5Sharp.Utilities;

namespace L5Sharp.Extensions
{
    public static class L5XComponentExtensions
    {
        public static IDataType GetDataType(this XElement element)
        {
            if (element == null) throw new ArgumentNullException(nameof(element));
            
            var typeName = element.GetDataTypeName();
            if (typeName == null)
                throw new InvalidOperationException($"Element '{element.Name}' does not have data type name");
                
            if (Predefined.ContainsType(typeName))
                return Predefined.FromName(typeName);
            
            var typeElement = element.FindDataTypeElement(typeName);
            return typeElement?.Deserialize<DataType>();
        }
        public static XElement FindDataTypeElement(this XElement element, string name)
        {
            return element.Document?.Descendants(L5XNames.Components.DataType)
                .SingleOrDefault(x => x.Attribute(L5XNames.Attributes.Name)?.Value == name);
        }
        
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