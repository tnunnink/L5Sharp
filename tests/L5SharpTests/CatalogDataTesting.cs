using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace L5SharpTests
{
    [TestFixture]
    public class CatalogDataTesting
    {
        private const string FileName =
            @"C:\Users\tnunnink\Local\Projects\L5Sharp\tests\Samples\CatalogData\CatalogSvcsDatabaseV2.xml";

        [Test]
        public void FileExists()
        {
            FileAssert.Exists(FileName);
        }

        [Test]
        public void GetCount_ShouldNotBeZero()
        {
            var document = XDocument.Load(FileName);

            var devices = document.Descendants("CatalogNumber").Select(e => e.Value).ToList();

            devices.Count.Should().BeGreaterThan(0);
        }


        [Test]
        public void GetAllCategories()
        {
            var document = XDocument.Load(FileName);

            var categories = document.Descendants("Category").Select(e => e.Attribute("Name")?.Value).Distinct();

            File.WriteAllLines(@"C:\Users\tnunnink\desktop\categories.txt", categories);
        }

        [Test]
        public void GetAllPorts()
        {
            var document = XDocument.Load(FileName);

            var ports = document.Descendants("Port").Select(e => e.Attribute("Type")?.Value).Distinct();

            File.WriteAllLines(@"C:\Users\tnunnink\desktop\PortTypes.txt", ports);
        }

        [Test]
        public void GetAllDevicesWithMultiplePortsWithoutExtensionProperties()
        {
            var document = XDocument.Load(FileName);

            var ports = document.Descendants("RADevice")
                .Where(e => e.Descendants("Port").Count() > 1
                            && e.Descendants("PortExtProperty").All(d => d.Value != "DownstreamOnly"))
                .Select(e =>
                {
                    return
                        $"{e.Descendants("CatalogNumber").First().Value};" +
                        $" {string.Join(",", e.Descendants("Category").Select(a => a.Attribute("Name")?.Value))}; " +
                        $"{string.Join(",", e.Descendants("Port").Select(a => a.Attribute("Type")?.Value))}";
                });

            File.WriteAllLines(@"C:\Users\tnunnink\desktop\MultiplePortsDevices.txt", ports);
        }

        [Test]
        public void GetWithMultiplePorts()
        {
            var document = XDocument.Load(FileName);

            var ports = document.Descendants("Ports").Where(x => x.Elements().Count() > 2);

            var parents = ports.Select(p => p.Parent?.ToString());

            File.WriteAllLines(@"C:\Users\tnunnink\desktop\MultiplePorts.txt", parents);
        }

        [Test]
        public void GetDevicesWithInputCategory()
        {
            var document = XDocument.Load(FileName);

            var ports = document.Descendants("Category").Where(x => x.Elements().Count() > 2);

            var parents = ports.Select(p => p.Parent?.ToString());

            File.WriteAllLines(@"C:\Users\tnunnink\desktop\MultiplePorts.txt", parents);
        }
    }
}