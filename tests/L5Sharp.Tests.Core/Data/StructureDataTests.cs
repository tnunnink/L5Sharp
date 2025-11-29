using System.Xml.Linq;
using FluentAssertions;

namespace L5Sharp.Tests.Core.Data;

[TestFixture]
public class StructureDataTests
{
    [Test]
    public void New_NullElement_ShouldThrowArgumentNullException()
    {
        FluentActions.Invoking(() => new StructureData(((XElement)null!)!)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void New_NullName_ShouldThrowArgumentException()
    {
        FluentActions.Invoking(() => new StructureData(null!, new List<LogixMember>())).Should()
            .Throw<ArgumentException>();
    }

    [Test]
    public void New_NullMembers_ShouldThrowArgumentNullException()
    {
        FluentActions.Invoking(() => new StructureData("Test", null!)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void New_Default_ShouldHaveExpectedValues()
    {
        var data = new StructureData();

        data.Should().NotBeNull();
        data.Name.Should().Be(nameof(StructureData));
        data.Members.Should().BeEmpty();
        data.Keys.Should().BeEmpty();
        data.Values.Should().BeEmpty();
    }

    [Test]
    public void New_NameOverload_ShouldHaveExpectedName()
    {
        var data = new StructureData("MyType");

        data.Name.Should().Be("MyType");
    }

    [Test]
    public void New_EmptyMembers_ShouldBeEmpty()
    {
        var data = new StructureData("Test", []);

        data.Should().BeEmpty();
    }

    [Test]
    public void New_WithMembers_ShouldHaveExpectedValues()
    {
        var data = new StructureData("Test", new List<LogixMember>
        {
            new("Member1", true),
            new("Member2", (byte)255),
            new("Member3", 1000),
            new("Member4", 4.5f),
            new("Member5", new TIMER())
        });


        data.Name.Should().Be("Test");
        data.Keys.Should().HaveCount(5);
        data.Values.Should().HaveCount(5);
        data.Should().HaveCount(5);
    }

    [Test]
    public void New_CollectionInitializer_ShouldHaveExpectedCount()
    {
        var data = new StructureData("Test")
        {
            new LogixMember("First", true),
            new LogixMember("Second", 123),
            new LogixMember("Third", 1.345)
        };

        data.Should().HaveCount(3);
    }

    [Test]
    public void ToString_WhenCalled_ShouldBeName()
    {
        var data = new StructureData("Test", new List<LogixMember>());

        var name = data.ToString();

        name.Should().Be(data.Name);
    }

    [Test]
    public void Clone_WhenCalled_ShouldNotBeSameAsButEqual()
    {
        var data = new StructureData("MyComplex", new List<LogixMember>
        {
            new("Member1", true),
            new("Member2", (byte)255),
            new("Member3", 1000),
            new("Member4", 4.5f),
            new("Member5", new TIMER())
        });

        var clone = data.Clone().As<StructureData>();

        clone.Name.Should().Be(data.Name);
        clone.Should().BeOfType<StructureData>();
        clone.Should().NotBeSameAs(data);
        clone.Should().HaveCount(data.Count);
    }

    [Test]
    public void Add_ValidMember_ShouldHaveExpectedCount()
    {
        // ReSharper disable once UseObjectOrCollectionInitializer
        var data = new StructureData();

        data.Add(new LogixMember("Member", 123));

        data.Members.Should().HaveCount(1);
    }

    [Test]
    public void Add_ValidMember_ShouldHaveExpectedMember()
    {
        // ReSharper disable once UseObjectOrCollectionInitializer
        var data = new StructureData();

        data.Add(new LogixMember("Member", 123));

        var result = data.Members.First();

        result.Name.Should().Be("Member");
        result.Value.Should().BeOfType<DINT>();
        result.Value.Should().Be(123);
    }

    [Test]
    public void Add_Null_ShouldThrowArgumentNullException()
    {
        var data = new StructureData();

        FluentActions.Invoking(() => data.Add(null!)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Add_NameTypeOverload_ShouldHaveExpectedCount()
    {
        // ReSharper disable once UseObjectOrCollectionInitializer
        var data = new StructureData();

        data.Add("Member", 123);

        data.Should().HaveCount(1);
    }

    [Test]
    public void Add_NullName_ShouldThrowException()
    {
        // ReSharper disable once CollectionNeverQueried.Local
        var data = new StructureData();

        FluentActions.Invoking(() => data.Add(null!, 123)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void Add_NullValue_ShouldThrowException()
    {
        // ReSharper disable once CollectionNeverQueried.Local
        var data = new StructureData();

        FluentActions.Invoking(() => data.Add("test", null!)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void Add_DuplicateName_ShouldThrowException()
    {
        // ReSharper disable once CollectionNeverQueried.Local
        var data = new StructureData { new LogixMember("test", true) };

        FluentActions.Invoking(() => data.Add("test", 123)).Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void Add_KeyValuePairOverload_ShouldHaveExpectedCount()
    {
        // ReSharper disable once UseObjectOrCollectionInitializer
        var data = new StructureData();

        data.Add(new KeyValuePair<string, LogixData>("test", 1000));

        data.Should().HaveCount(1);
    }

    [Test]
    public void AddMany_ValidMembers_ShouldHaveExpectedCount()
    {
        var data = new StructureData();

        data.AddMany(new List<LogixMember>
        {
            new("Atomic", 1),
            new("String", "Test Value"),
            new("Structure", new TIMER { PRE = 2000 }),
        });

        data.Should().HaveCount(3);
    }

    [Test]
    public void AddMany_ValidMembers_ShouldHaveExpectedMembers()
    {
        var data = new StructureData();

        var expected = new List<LogixMember>
        {
            new("Atomic", 123),
            new("String", "Test Value"),
            new("Structure", new TIMER { PRE = 2000 })
        };

        data.AddMany(expected);

        var a = data.Member("Atomic");
        a?.Name.Should().Be("Atomic");
        a?.Value.Should().BeOfType<DINT>();
        a?.Value.Should().Be(123);

        var b = data.Member("String");
        b?.Name.Should().Be("String");
        b?.Value.Should().Be("Test Value");

        var c = data.Member("Structure");
        c?.Name.Should().Be("Structure");
        c?.Value.As<TIMER>().PRE.Should().Be(2000);
    }

    [Test]
    public void AddMany_NullMembers_ShouldThrowArgumentNullException()
    {
        var data = new StructureData();
        var members = new LogixMember[] { null!, null!, null! };

        FluentActions.Invoking(() => data.AddMany(members)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Clear_WhenCalled_ShouldHaveExpectedCount()
    {
        var data = new StructureData("Test", new List<LogixMember> { new("Test", 123) });

        data.Clear();

        data.Members.Should().BeEmpty();
    }

    [Test]
    public void Contains_ExistingKeyValuePair_ShouldBeTrue()
    {
        var data = new StructureData("MyType")
        {
            { "AtomicMember", 123 }
        };

        var result = data.Contains(new KeyValuePair<string, LogixData>("AtomicMember", 123));

        result.Should().BeTrue();
    }

    [Test]
    public void Contains_ExistingKeyDifferentValue_ShouldBeTrue()
    {
        var data = new StructureData("MyType")
        {
            { "AtomicMember", 123 }
        };

        var result = data.Contains(new KeyValuePair<string, LogixData>("AtomicMember", 321));

        result.Should().BeTrue();
    }

    [Test]
    public void Contains_NoMatchingKey_ShouldBeFalse()
    {
        var data = new StructureData("MyType")
        {
            { "AtomicMember", 123 }
        };

        var result = data.Contains(new KeyValuePair<string, LogixData>("Member", 321));

        result.Should().BeFalse();
    }

    [Test]
    public void ContainsKey_HasKey_ShouldBeTrue()
    {
        var data = new StructureData("Test") { { "MemberName", 123 } };

        var result = data.ContainsKey("MemberName");

        result.Should().BeTrue();
    }

    [Test]
    public void ContainsKey_HasNoKey_ShouldBeFalse()
    {
        var data = new StructureData("Test");

        var result = data.ContainsKey("MemberName");

        result.Should().BeFalse();
    }

    [Test]
    public void TryGetValue_ContainsKey_ShouldBeTrueAndHaveValue()
    {
        var data = new StructureData("Test") { { "Member", 123 } };

        var result = data.TryGetValue("Member", out var value);

        result.Should().BeTrue();
        value.Should().Be(123);
    }

    [Test]
    public void TryGetValue_NoMatchingKey_ShouldBeFalse()
    {
        var data = new StructureData("Test") { { "Member", 123 } };

        var result = data.TryGetValue("SomeName", out var _);

        result.Should().BeFalse();
    }

    [Test]
    public void Remove_ByName_ShouldHaveExpectedCount()
    {
        var data = new StructureData("Test", new List<LogixMember>
        {
            new("Atomic", 1),
            new("String", "Test Value"),
            new("Structure", new TIMER { PRE = 2000 })
        });

        var result = data.Remove("String");

        result.Should().BeTrue();
        data.Should().HaveCount(2);
    }

    [Test]
    public void Remove_NonExistingName_ShouldHaveExpectedCount()
    {
        var data = new StructureData("Test", new List<LogixMember>
        {
            new("Atomic", 1),
            new("String", "Test Value"),
            new("Structure", new TIMER { PRE = 2000 })
        });

        var result = data.Remove("Fake");

        result.Should().BeFalse();
        data.Should().HaveCount(3);
    }

    [Test]
    public void Remove_KeyValuePair_ShouldHaveExpectedCount()
    {
        var data = new StructureData("Test", new List<LogixMember>
        {
            new("Atomic", 1),
            new("String", "Test Value"),
            new("Structure", new TIMER { PRE = 2000 })
        });

        var result = data.Remove(new KeyValuePair<string, LogixData>("String", "Testing"));

        result.Should().BeTrue();
        data.Should().HaveCount(2);
    }

    [Test]
    public Task Serialize_Default_ShouldBeVerified()
    {
        var type = new StructureData();

        var xml = type.Serialize().ToString();

        return Verify(xml);
    }

    [Test]
    public Task Serialize_Test_ShouldBeVerified()
    {
        var type = new StructureData("Test", new List<LogixMember>
        {
            new("Atomic", 1),
            new("String", "Test Value"),
            new("Structure", new TIMER { PRE = 2000 })
        });

        var xml = type.Serialize().ToString();

        return Verify(xml);
    }

    [Test]
    public void EquivalentTo_AreEqual_ShouldBeTrue()
    {
        var first = new StructureData("Test", new List<LogixMember>
        {
            new("Member1", true),
            new("Member2", (byte)255),
            new("Member3", 1000),
            new("Member4", 4.5f),
            new("Member5", new TIMER())
        });

        var second = new StructureData("Test", new List<LogixMember>
        {
            new("Member1", true),
            new("Member2", (byte)255),
            new("Member3", 1000),
            new("Member4", 4.5f),
            new("Member5", new TIMER())
        });


        var result = first.EquivalentTo(second);

        result.Should().BeTrue();
    }

    [Test]
    public void EquivalentTo_AreNotEqualByValue_ShouldBeFalse()
    {
        var first = new StructureData("Test", new List<LogixMember>
        {
            new("Member1", true),
            new("Member2", (byte)255),
            new("Member3", 1000),
            new("Member4", 4.5f),
            new("Member5", new TIMER())
        });

        var second = new StructureData("Test", new List<LogixMember>
        {
            new("Member1", true),
            new("Member2", (byte)255),
            new("Member3", 1000),
            new("Member4", 4.45f),
            new("Member5", new TIMER())
        });


        var result = first.EquivalentTo(second);

        result.Should().BeFalse();
    }

    [Test]
    public void EquivalentTo_AreNotEqualByName_ShouldBeFalse()
    {
        var first = new StructureData("Test", new List<LogixMember>
        {
            new("Member1", true),
            new("Member2", (byte)255),
            new("Member3", 1000),
            new("Member4", 4.5f),
            new("Member5", new TIMER())
        });

        var second = new StructureData("Test", new List<LogixMember>
        {
            new("Member1", true),
            new("Member2", (byte)255),
            new("Member3", 1000),
            new("Member4", 4.5f),
            new("Member6", new TIMER())
        });


        var result = first.EquivalentTo(second);

        result.Should().BeFalse();
    }
}