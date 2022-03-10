using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;

namespace L5Sharp.Context.Tests
{
    [TestFixture]
    public class L5XTests
    {
        [Test]
        public void GetAllElementNames()
        {
            var document = XDocument.Load(Known.Template);

            var names = document.Descendants().Select(e => e.Name.ToString());

            var set = new HashSet<string>();

            foreach (var name in names)
                set.Add(name);

            File.WriteAllLines(@"C:\Users\tnunnink\desktop\L5XElements.txt", set.OrderBy(x => x));
        }
        
        [Test]
        public void GetAllAttributeNames()
        {
            var document = XDocument.Load(Known.Template);

            var names = document.Descendants().SelectMany(e => e.Attributes().Select(a => a.Name.ToString()));

            var set = new HashSet<string>();

            foreach (var name in names)
                set.Add(name);

            File.WriteAllLines(@"C:\Users\tnunnink\desktop\L5XAttribute.txt", set.OrderBy(x => x));
        }
    }
}