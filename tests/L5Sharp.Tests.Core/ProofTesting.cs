using FluentAssertions;

namespace L5Sharp.Tests.Core;

[TestFixture]
public class ProofTesting
{
    [Test]
    public void Indexing()
    {
        var content = L5X.Load(Known.Example);
        var index = new LogixIndex(content);
        
        var result = index.GetEntity<Tag>(Known.Tag);

        result.Should().NotBeNull();
    }
}