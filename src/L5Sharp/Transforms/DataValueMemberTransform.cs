using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Primitives;

namespace L5Sharp.Transforms
{
    public class DataValueMemberTransform : BaseTransform
    {
        public override XElement Perform(XElement element)
        {
            if (!element.Name.ToString().Equals(ElementNames.DataValueMember))
                throw new InvalidOperationException(
                    $"XElement name is not expected value {ElementNames.DataValueMember}");

            var name = element.Attribute(ElementNames.Name)?.Value ?? throw new ArgumentNullException();
            var dataType = element.Attribute(nameof(ElementNames.DataType))?.Value;
            var radix = element.Attribute(nameof(ElementNames.Radix))?.Value;
            var externalAccess = element.Ancestors(nameof(Tag))
                .FirstOrDefault()?.Attribute(nameof(ElementNames.ExternalAccess))?.Value;
            
            var operand = GetOperand(element);
            var description = element.Ancestors(nameof(Tag)).FirstOrDefault()?.Descendants("Comment")
                    .SingleOrDefault(c => c.Attribute("Operand")?.Value == operand)?.Value.ToString();
            
            var value = element.Attribute(ElementNames.Value)?.Value;

            var transformed = Generate(name, dataType, radix: radix, access: externalAccess, description: description,
                value: value);
            
            return transformed;
        }

        private static string GetOperand(XElement element)
        {
            var operandName = element.Attribute(ElementNames.Name)?.Value ?? string.Empty;

            var parent = element.Parent;

            if (parent == null) return operandName;
            
            while (parent.Name != nameof(Tag))
            {
                var name = parent.Attribute(ElementNames.Name)?.Value;
                var index = parent.Attribute(ElementNames.Index)?.Value;

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