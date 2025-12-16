using System.Xml.Linq;

namespace L5Sharp.Tests.Core;

[TestFixture]
public class ScratchTesting
{
    [Test]
    public void Scratch()
    {
        var element = new XElement("Test");
        
        element.Add(null!);
    }
}