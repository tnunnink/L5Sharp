using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5XParser.Model
{
    public class Module
    {
        private Module(XElement element)
        {
            Name = element.Attribute("Name")?.Value;
            CatalogNumber = element.Attribute("CatalogNumber")?.Value;
            MajorVersion = Convert.ToInt32(element.Attribute("Major")?.Value);
            MinorVersion = Convert.ToInt32(element.Attribute("Minor")?.Value);
            ParentName = element.Attribute("ParentModule")?.Value;
            Port = element.Descendants("Port").FirstOrDefault()?.Attribute("Address")?.Value;
            Connections = new List<ConnectionTag>();
            
            var connection = element.Descendants("Connection").FirstOrDefault();
            if (connection != null)
                Connections.AddRange(ExtractConnectionTags(connection));
        }

        public string Name { get; set; }
        public string CatalogNumber { get; set; }
        public int MajorVersion { get; set; }
        public int MinorVersion { get; set; }
        public string ParentName { get; set; }
        public string Port { get; set; }
        public List<ConnectionTag> Connections { get; set; }

        public static Module Parse(XElement element)
        {
            return new Module(element);
        }

        private static IEnumerable<ConnectionTag> ExtractConnectionTags(XContainer connection)
        {
            var connections = new List<ConnectionTag>();

            var inputTag = connection.Element("InputTag");
            if (inputTag != null)
                connections.Add(ConnectionTag.Parse(inputTag));

            var outputTag = connection.Element("OutputTag");
            if (outputTag != null)
                connections.Add(ConnectionTag.Parse(outputTag));
            
            return connections;
        }

        public override string ToString()
        {
            return $"{Name},{CatalogNumber},{MajorVersion},{MinorVersion},{ParentName},{Port}";
        }
    }
}