using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Tests.Types.Custom;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Tests;

public class LogixSerializerTests
{
    [Test]
    public void Testing()
    {
        var timer = LogixSerializer.Deserialize("TIMER", new XElement(L5XName.Structure));

        timer.Should().NotBeNull();
        timer.Should().BeOfType<TIMER>();
    }

    [Test]
    public void TestingAutoScan()
    {
        LogixSerializer.Mode = ScanMode.None;

        FluentActions.Invoking(() => LogixSerializer.Deserialize("TIMER", new XElement(L5XName.Structure))).Should()
            .Throw<InvalidOperationException>();
    }
}