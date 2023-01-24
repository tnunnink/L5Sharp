using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Tests.Extensions
{
    public class Reference
    {
        public int RungNumber { get; set; }
    }

    public static class CustomContentExtensions
    {
        public static IEnumerable<Reference> References(this LogixContent content, string programName)
        {
            var element = content.ToElement();

            var program = element.Descendants("Rung").FirstOrDefault(e => e.Attribute("Name")?.Value == programName);

            return program?.Descendants("Rung").Select(r => new Reference
                { RungNumber = int.Parse(r.Attribute("Number")!.Value) });
        }
        
        public static IEnumerable<Reference> References(this LogixContent content)
        {
            var element = XElement.Parse(content.ToString());

            throw new NotImplementedException();
        }
    }
}