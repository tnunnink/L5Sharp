using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5XParser.Model
{
    public class ConnectionTag
    {
        private ConnectionTag(XElement element)
        {
            Name = element.Name.ToString();
            Type = element.Name.ToString() == "InputTag" 
                ? ConnectionType.Input 
                : element.Name.ToString() == "OutputTag" 
                    ? ConnectionType.Output 
                    : ConnectionType.Unknown;
            var parent = element.Parent;
            if (parent != null)
            {
                Suffix = Type == ConnectionType.Input
                    ? parent.Attribute("InputTagSuffix")?.Value
                    : parent.Attribute("OutputTagSuffix")?.Value;
            }
            
            ExtractTags(element);
        }

        public string Name { get; set; }
        public ConnectionType Type { get; set; }
        public string Suffix { get; set; }
        public List<TagMember> Tags { get; set; }
        
        public static ConnectionTag Parse(XElement element)
        {
            return new ConnectionTag(element);
        }
        
        private void ExtractTags(XContainer element)
        {
            Tags = new List<TagMember>();

            var tagNames = GetTagNames(element);

            Tags.AddRange(from tagName in tagNames
                let comment = FindCommentForTagName(element, tagName)
                let unit = FindUnitForTagName(element, tagName)
                select new TagMember
                {
                    Name = tagName,
                    Comment = comment,
                    Units = unit
                });
        }
        
        private static IEnumerable<string> GetTagNames(XContainer element)
        {
            return element?.Descendants("Data")
                .SingleOrDefault(e => e.FirstAttribute.Value == "Decorated")
                ?.Descendants("StructureMember")
                .Select(e => e.Attribute("Name")?.Value);
        }

        private static string FindCommentForTagName(XContainer element, string tagName)
        {
            var comments = element?.Descendants("Comment").Where(e => e.Attribute("Operand")?.Value != null);
            return comments == null 
                ? string.Empty 
                : comments.FirstOrDefault(e => OperandContainsName(e, tagName))?.Value;
        }

        private static string FindUnitForTagName(XContainer element, string tagName)
        {
            var units = element?.Descendants("EngineeringUnit").Where(e => e.Attribute("Operand")?.Value != null);
            return units == null 
                ? string.Empty 
                : units.FirstOrDefault(e => OperandContainsName(e, tagName))?.Value;
        }
        
        private static bool OperandContainsName(XElement element, string name)
        {
            var operand = element.Attribute("Operand")?.Value;
            return operand != null && operand.Contains(name, StringComparison.OrdinalIgnoreCase);
        }
    }
}