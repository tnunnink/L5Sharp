using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Primitives;
using L5Sharp.Utilities;

namespace L5Sharp.Loaders
{
    public class DataValueMemberTransform : TagMemberTransform
    {
        public override XElement Normalize(XElement element)
        {
            if (!element.Name.ToString().Equals(L5XNames.DataValueMember))
                throw new InvalidOperationException(
                    $"XElement name is not expected value {L5XNames.DataValueMember}");

            var name = element.GetName() ?? throw new ArgumentNullException();
            var dataType = element.GetDataTypeName();
            var radix = element.GetRadix();
            var externalAccess = element.Ancestors(nameof(Tag))
                .FirstOrDefault()?.Attribute(nameof(L5XNames.ExternalAccess))?.Value;
            
            var operand = GetOperand(element);
            var description = element.Ancestors(nameof(Tag)).FirstOrDefault()?.Descendants("Comment")
                    .SingleOrDefault(c => c.Attribute("Operand")?.Value == operand)?.Value.ToString();
            
            var value = element.Attribute(L5XNames.Value)?.Value;

            var transformed = GenerateMember(name, dataType, radix: radix.Name, access: externalAccess, description: description,
                value: value);
            
            return transformed;
        }

        private static string GetOperand(XElement element)
        {
            var operandName = element.Attribute(L5XNames.Name)?.Value ?? string.Empty;

            var parent = element.Parent;

            if (parent == null) return operandName;
            
            while (parent.Name != nameof(Tag))
            {
                var name = parent.Attribute(L5XNames.Name)?.Value;
                var index = parent.Attribute(L5XNames.Index)?.Value;

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