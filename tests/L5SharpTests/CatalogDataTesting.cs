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
        private const string FileName = @"C:\Users\tnunnink\dev\projects\L5Sharp\data\CatalogSvcsDatabase.xml";
        
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

            File.WriteAllLines(@"C:\Users\tnunnink\desktop\results.txt", categories);
        }
    }
}