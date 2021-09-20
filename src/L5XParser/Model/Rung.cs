using System;
using System.Xml.Linq;

namespace L5XParser.Model
{
    public class Rung
    {
        private Rung(XElement element)
        {
            Number = Convert.ToInt32(element.Attribute("Number")?.Value);
            Type = element.Attribute("Type")?.Value;
            Comment = element.Element("Comment")?.Value;
            Text = element.Element("Text")?.Value;
        }
        public int Number { get; set; }
        public string Type { get; set; }
        public string Comment { get; set; }
        public string Text { get; set; }
        public static Rung Parse(XElement element)
        {
            return new Rung(element);
        }

        public bool ContainsTagName(string tagName)
        {
            return Text.Contains(tagName, StringComparison.OrdinalIgnoreCase);
        }
    }
}