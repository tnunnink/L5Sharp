using System.Runtime.InteropServices;
using System.Xml.Linq;
using FluentAssertions;

namespace L5Sharp.Tests.Types;

[TestFixture]
public class ComplexTypeTests
{
    [Test]
    public void New_NullElement_ShouldThrowArgumentNullException()
    {
        FluentActions.Invoking(() => new ComplexType(((XElement)null!)!)).Should().Throw<ArgumentNullException>();
    }
    
    [Test]
    public void New_NullMembers_ShouldThrowArgumentNullException()
    {
        FluentActions.Invoking(() => new ComplexType("Test", null!)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void New_Default_ShouldNotBeNull()
    {
        var type = new ComplexType();

        type.Should().NotBeNull();
    }

    [Test]
    public void New_Default_ShouldHaveExpectedValues()
    {
        var type = new ComplexType();

        type.Name.Should().Be(nameof(ComplexType));
        type.Members.Should().BeEmpty();
    }

    [Test]
    public void New_ValidArguments_ShouldNotBeNull()
    {
        var type = new ComplexType("Test", new List<Member>());

        type.Should().NotBeNull();
    }

    [Test]
    public void New_NullName_ShouldThrowArgumentException()
    {
        FluentActions.Invoking(() => new ComplexType(null!, new List<Member>())).Should().Throw<ArgumentException>();
    }

    [Test]
    public void New_NullMembers_ShouldThrowArgumentException()
    {
        FluentActions.Invoking(() => new ComplexType("Test", null!)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void New_EmptyMembers_ShouldHaveExpectedValues()
    {
        var type = new ComplexType("Test", new List<Member>());

        type.Should().NotBeNull();
        type.Name.Should().Be("Test");
        type.Members.Should().BeEmpty();
    }

    [Test]
    public void New_WithMembers_ShouldHaveExpectedValues()
    {
        var type = new ComplexType("Test", new List<Member>
        {
            new("Member1", true),
            new("Member2", (byte)255),
            new("Member3", 1000),
            new("Member4", 4.5f),
            new("Member5", new TIMER())
        });


        type.Should().NotBeNull();
        type.Name.Should().Be("Test");
        type.Members.Should().HaveCount(5);
    }

    [Test]
    public void ToString_WhenCalled_ShouldBeName()
    {
        var type = new ComplexType("Test", new List<Member>());

        var name = type.ToString();

        name.Should().Be(type.Name);
    }

    [Test]
    public void Clone_WhenCalled_ShouldNotBeSameAsButEqual()
    {
        var type = new ComplexType("MyComplex", new List<Member>
        {
            new("Member1", true),
            new("Member2", (byte)255),
            new("Member3", 1000),
            new("Member4", 4.5f),
            new("Member5", new TIMER())
        });

        var clone = type.Clone<ComplexType>();

        clone.Should().BeOfType<ComplexType>();
        clone.Should().NotBeSameAs(type);
        clone.Name.Should().Be(type.Name);
        clone.Members.Should().HaveCount(type.Members.Count());
    }

    [Test]
    public void Add_ValidMember_ShouldHaveExpectedCount()
    {
        var type = new ComplexType();

        type.Add(new Member("Member", 123));

        type.Members.Should().HaveCount(1);
    }

    [Test]
    public void Add_ValidMember_ShouldHaveExpectedMember()
    {
        var type = new ComplexType();

        type.Add(new Member("Member", 123));

        var result = type.Members.First();

        result.Name.Should().Be("Member");
        result.Value.Should().BeOfType<DINT>();
        result.Value.Should().Be(123);
    }

    [Test]
    public void Add_Null_ShouldThrowArgumentNullException()
    {
        var type = new ComplexType();

        FluentActions.Invoking(() => type.Add(null!)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void AddRange_ValidMembers_ShouldHaveExpectedCount()
    {
        var expected = new List<Member>
        {
            new("Atomic", 1),
            new("String", "Test Value"),
            new("Structure", new TIMER { PRE = 2000 }),
        };
        
        var type = new ComplexType();

        type.AddRange(expected);

        type.Members.Should().HaveCount(3);
    }

    [Test]
    public void AddRange_ValidMembers_ShouldHaveExpectedMember()
    {
        var expected = new List<Member>
        {
            new("Atomic", 123),
            new("String", "Test Value"),
            new("Structure", new TIMER { PRE = 2000 }),
        };
        var type = new ComplexType();

        type.AddRange(expected);

        var a = type.Member("Atomic");
        a?.Name.Should().Be("Atomic");
        a?.Value.Should().BeOfType<DINT>();
        a?.Value.Should().Be(123);

        var b = type.Member("String");
        b?.Name.Should().Be("String");
        b?.Value.Should().BeOfType<STRING>();
        b?.Value.Should().Be("Test Value");

        var c = type.Member("Structure");
        c?.Name.Should().Be("Structure");
        c?.Value.Should().BeOfType<TIMER>();
        c?.Value.As<TIMER>().PRE.Should().Be(2000);
    }

    [Test]
    public void AddRange_Null_ShouldThrowArgumentNullException()
    {
        var type = new ComplexType();

        FluentActions.Invoking(() => type.AddRange(null!)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void AddRange_NullMembers_ShouldThrowArgumentNullException()
    {
        var members = new Member[] { null!, null!, null! };
        var type = new ComplexType("Test");

        FluentActions.Invoking(() => type.AddRange(members)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Clear_WhenCalled_ShouldHaveExpectedCount()
    {
        var type = new ComplexType("Test", new List<Member> { new("Test", 123) });

        type.Clear();

        type.Members.Should().BeEmpty();
    }

    [Test]
    public void Insert_ValidIndexAndMember_ShouldHaveExpectedCount()
    {
        var type = new ComplexType("Test", new List<Member>
        {
            new("Atomic", 1),
            new("String", "Test Value"),
            new("Structure", new TIMER { PRE = 2000 }),
        });

        type.Insert(1, new Member("Insert", -123));

        type.Members.Should().HaveCount(4);
    }

    [Test]
    public void Insert_ValidIndexAndMember_ShouldHaveExpectedMember()
    {
        var type = new ComplexType("Test", new List<Member>
        {
            new("Atomic", 1),
            new("String", "Test Value"),
            new("Structure", new TIMER { PRE = 2000 }),
        });

        type.Insert(1, new Member("Insert", -123));

        var result = type.Member("Insert");

        result?.Should().NotBeNull();
        result?.Name.Should().Be("Insert");
        result?.Value.Should().BeOfType<DINT>();
        result?.Value.Should().Be(-123);
    }

    [Test]
    public void Insert_NullMember_ShouldThrowArgumentNullException()
    {
        var type = new ComplexType("Test", new List<Member>
        {
            new("Atomic", 1),
            new("String", "Test Value"),
            new("Structure", new TIMER { PRE = 2000 }),
        });

        FluentActions.Invoking(() => type.Insert(1, null!)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Insert_InvalidIndex_ShouldThrowArgumentOutOfRangeException()
    {
        var type = new ComplexType("Test", new List<Member>
        {
            new("Atomic", 1),
            new("String", "Test Value"),
            new("Structure", new TIMER { PRE = 2000 })
        });

        FluentActions.Invoking(() => type.Insert(4, new Member("Insert", 123))).Should()
            .Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void Remove_ByName_ShouldHaveExpectedCount()
    {
        var type = new ComplexType("Test", new List<Member>
        {
            new("Atomic", 1),
            new("String", "Test Value"),
            new("Structure", new TIMER { PRE = 2000 })
        });

        type.Remove("String");

        type.Members.Should().HaveCount(2);
        type.Member("Atomic").Should().NotBeNull();
        type.Member("Structure").Should().NotBeNull();
    }

    [Test]
    public void Remove_NonExistingName_ShouldHaveExpectedCount()
    {
        var type = new ComplexType("Test", new List<Member>
        {
            new("Atomic", 1),
            new("String", "Test Value"),
            new("Structure", new TIMER { PRE = 2000 })
        });

        type.Remove("Fake");

        type.Members.Should().HaveCount(3);
        type.Member("Atomic").Should().NotBeNull();
        type.Member("String").Should().NotBeNull();
        type.Member("Structure").Should().NotBeNull();
    }

    [Test]
    public void Remove_ByIndex_ShouldHaveExpectedCount()
    {
        var type = new ComplexType("Test", new List<Member>
        {
            new("Atomic", 1),
            new("String", "Test Value"),
            new("Structure", new TIMER { PRE = 2000 })
        });

        type.Remove(0);

        type.Members.Should().HaveCount(2);
        type.Member("String").Should().NotBeNull();
        type.Member("Structure").Should().NotBeNull();
    }

    [Test]
    public void Remove_InvalidIndex_ShouldThrowArgumentOutOfRangeException()
    {
        var type = new ComplexType("Test", new List<Member>
        {
            new("Atomic", 1),
            new("String", "Test Value"),
            new("Structure", new TIMER { PRE = 2000 })
        });

        FluentActions.Invoking(() => type.Remove(4)).Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void Replace_ByNameValidNameAndMember_ShouldHaveExpectedCount()
    {
        var type = new ComplexType("Test", new List<Member>
        {
            new("Atomic", 1),
            new("String", "Test Value"),
            new("Structure", new TIMER { PRE = 2000 })
        });

        type.Replace(1, new Member("String", "Different Value"));

        type.Members.Should().HaveCount(3);
        type.Member("String").Should().NotBeNull();
        type.Member("String")?.Value.Should().Be("Different Value");
    }

    [Test]
    public void Replace_ByNameValidNameAndMember_ShouldHaveExpectedMember()
    {
        var type = new ComplexType("Test", new List<Member>
        {
            new("Atomic", 1),
            new("String", "Test Value"),
            new("Structure", new TIMER { PRE = 2000 })
        });

        type.Replace("Structure", new Member("Structure", new COUNTER { PRE = 3000 }));

        var result = type.Member("Insert");

        result?.Should().NotBeNull();
        result?.Name.Should().Be("Insert");
        result?.Value.Should().BeOfType<COUNTER>();
        result?.Value.As<COUNTER>().PRE.Should().Be(3000);
    }

    [Test]
    public void Replace_ByNameNullMember_ShouldThrowArgumentNullException()
    {
        var type = new ComplexType("Test", new List<Member>
        {
            new("Atomic", 1),
            new("String", "Test Value"),
            new("Structure", new TIMER { PRE = 2000 }),
        });

        FluentActions.Invoking(() => type.Replace("Atomic", ((Member)null!)!)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Replace_ByNameInvalidName_ShouldThrowArgumentException()
    {
        var type = new ComplexType("Test", new List<Member>
        {
            new("Atomic", 1),
            new("String", "Test Value"),
            new("Structure", new TIMER { PRE = 2000 })
        });

        FluentActions.Invoking(() => type.Replace("Fake", new Member("Replaced", 123))).Should()
            .Throw<ArgumentException>();
    }

    [Test]
    public void Replace_ByIndexValidIndexAndMember_ShouldHaveExpectedCount()
    {
        var type = new ComplexType("Test", new List<Member>
        {
            new("Atomic", 1),
            new("String", "Test Value"),
            new("Structure", new TIMER { PRE = 2000 }),
        });

        type.Replace(1, new Member("String", "Different Value"));

        type.Members.Should().HaveCount(3);
        type.Member("String").Should().NotBeNull();
        type.Member("String")?.Value.Should().Be("Different Value");
    }

    [Test]
    public void Replace_ByIndexValidIndexAndMember_ShouldHaveExpectedMember()
    {
        var type = new ComplexType("Test", new List<Member>
        {
            new("Atomic", 1),
            new("String", "Test Value"),
            new("Structure", new TIMER { PRE = 2000 }),
        });

        type.Replace(1, new Member("Insert", -123));

        var result = type.Member("Insert");

        result?.Should().NotBeNull();
        result?.Name.Should().Be("Insert");
        result?.Value.Should().BeOfType<DINT>();
        result?.Value.Should().Be(-123);
    }

    [Test]
    public void Replace_ByIndexNullMember_ShouldThrowArgumentNullException()
    {
        var type = new ComplexType("Test", new List<Member>
        {
            new("Atomic", 1),
            new("String", "Test Value"),
            new("Structure", new TIMER { PRE = 2000 }),
        });

        FluentActions.Invoking(() => type.Replace(1, null!)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Replace_ByIndexInvalidIndex_ShouldThrowArgumentOutOfRangeException()
    {
        var type = new ComplexType("Test", new List<Member>
        {
            new("Atomic", 1),
            new("String", "Test Value"),
            new("Structure", new TIMER { PRE = 2000 })
        });

        FluentActions.Invoking(() => type.Insert(4, new Member("Insert", 123))).Should()
            .Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void Replace_ByNameValidNameAndType_ShouldHaveExpectedCount()
    {
        var type = new ComplexType("Test", new List<Member>
        {
            new("Atomic", 1),
            new("String", "Test Value"),
            new("Structure", new TIMER { PRE = 2000 })
        });

        type.Replace("String", "Different Value");

        type.Members.Should().HaveCount(3);
        type.Member("String").Should().NotBeNull();
        type.Member("String")?.Value.Should().Be("Different Value");
    }

    [Test]
    public void Replace_ByNameValidNameAndType_ShouldHaveExpectedMember()
    {
        var type = new ComplexType("Test", new List<Member>
        {
            new("Atomic", 1),
            new("String", "Test Value"),
            new("Structure", new TIMER { PRE = 2000 })
        });

        type.Replace("Structure", new COUNTER { PRE = 3000 });

        var result = type.Member("Insert");

        result?.Should().NotBeNull();
        result?.Name.Should().Be("Insert");
        result?.Value.Should().BeOfType<COUNTER>();
        result?.Value.As<COUNTER>().PRE.Should().Be(3000);
    }

    [Test]
    public void Replace_ByNameAndTypeNullMember_ShouldThrowArgumentNullException()
    {
        var type = new ComplexType("Test", new List<Member>
        {
            new("Atomic", 1),
            new("String", "Test Value"),
            new("Structure", new TIMER { PRE = 2000 }),
        });

        FluentActions.Invoking(() => type.Replace("Atomic", ((LogixType)null!)!)).Should()
            .Throw<ArgumentNullException>();
    }

    [Test]
    public void Replace_ByNameAndTypeInvalidName_ShouldThrowArgumentException()
    {
        var type = new ComplexType("Test", new List<Member>
        {
            new("Atomic", 1),
            new("String", "Test Value"),
            new("Structure", new TIMER { PRE = 2000 })
        });

        FluentActions.Invoking(() => type.Replace("Fake", 123)).Should()
            .Throw<ArgumentException>();
    }

    [Test]
    public Task Serialize_Default_ShouldBeVerified()
    {
        var type = new ComplexType();

        var xml = type.Serialize().ToString();

        return Verify(xml);
    }

    [Test]
    public Task Serialize_Test_ShouldBeVerified()
    {
        var type = new ComplexType("Test", new List<Member>
        {
            new("Atomic", 1),
            new("String", "Test Value"),
            new("Structure", new TIMER { PRE = 2000 })
        });

        var xml = type.Serialize().ToString();

        return Verify(xml);
    }
    
    [Test]
    public void IsEquivalent_AreEqual_ShouldBeTrue()
    {
        var first = new ComplexType("Test", new List<Member>
        {
            new("Member1", true),
            new("Member2", (byte)255),
            new("Member3", 1000),
            new("Member4", 4.5f),
            new("Member5", new TIMER())
        });
        
        var second = new ComplexType("Test", new List<Member>
        {
            new("Member1", true),
            new("Member2", (byte)255),
            new("Member3", 1000),
            new("Member4", 4.5f),
            new("Member5", new TIMER())
        });


        var result = first.IsEquivalent(second);

        result.Should().BeTrue();
    }
    
    [Test]
    public void IsEquivalent_AreNotEqualByValue_ShouldBeFalse()
    {
        var first = new ComplexType("Test", new List<Member>
        {
            new("Member1", true),
            new("Member2", (byte)255),
            new("Member3", 1000),
            new("Member4", 4.5f),
            new("Member5", new TIMER())
        });
        
        var second = new ComplexType("Test", new List<Member>
        {
            new("Member1", true),
            new("Member2", (byte)255),
            new("Member3", 1000),
            new("Member4", 4.45f),
            new("Member5", new TIMER())
        });


        var result = first.IsEquivalent(second);

        result.Should().BeFalse();
    }
    
    [Test]
    public void IsEquivalent_AreNotEqualByName_ShouldBeFalse()
    {
        var first = new ComplexType("Test", new List<Member>
        {
            new("Member1", true),
            new("Member2", (byte)255),
            new("Member3", 1000),
            new("Member4", 4.5f),
            new("Member5", new TIMER())
        });
        
        var second = new ComplexType("Test", new List<Member>
        {
            new("Member1", true),
            new("Member2", (byte)255),
            new("Member3", 1000),
            new("Member4", 4.5f),
            new("Member6", new TIMER())
        });


        var result = first.IsEquivalent(second);

        result.Should().BeFalse();
    }
}