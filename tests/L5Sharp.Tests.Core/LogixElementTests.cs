using System.Xml.Linq;
using FluentAssertions;

// ReSharper disable UseObjectOrCollectionInitializer

namespace L5Sharp.Tests.Core;

[TestFixture]
public class LogixElementTests
{
    [Test]
    public void ShouldBeRegisteredInSerializerClass()
    {
        var result = LogixSerializer.IsRegistered(typeof(TestElement));

        result.Should().BeTrue();
    }

    [Test]
    public void New_Default_ShouldNotBeNull()
    {
        var element = new TestElement();

        element.Should().NotBeNull();
    }

    [Test]
    public void New_Default_ShouldHaveExpectedElementName()
    {
        var element = new TestElement();

        element.Serialize().Name.LocalName.Should().Be("Test");
    }

    [Test]
    public void New_XElement_ShouldNotBeNull()
    {
        var element = new TestElement(new XElement("Test"));

        element.Should().NotBeNull();
    }

    [Test]
    public void New_NullXElement_ShouldThrowException()
    {
        FluentActions.Invoking(() => new TestElement(null!)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Serialize_WhenCalled_ShouldNotBeNull()
    {
        var element = new TestElement();

        var xml = element.Serialize();

        xml.Should().NotBeNull();
    }

    [Test]
    public void Serialize_WhenCalled_ShouldHaveExpectedElementName()
    {
        var element = new TestElement();

        var xml = element.Serialize();

        xml.Name.LocalName.Should().Be("Test");
    }

    [Test]
    public void As_TestElementAsBaseInterface_ShouldBeExpected()
    {
        var element = new TestElement();

        var casted = element.As<ILogixElement>();

        casted.Should().NotBeNull();
    }

    [Test]
    public void As_TestElementAsLogixObject_ShouldBeExpected()
    {
        var element = new TestElement();

        var casted = element.As<LogixObject<TestElement>>();

        casted.Should().NotBeNull();
    }

    [Test]
    public void As_LogixElementAsTestElement_ShouldNotBeNull()
    {
        var element = XElement.Parse("<Test/>").Deserialize<ILogixElement>();

        var casted = element.As<TestElement>();

        casted.Should().NotBeNull();
    }

    [Test]
    public void As_InvalidType_ShouldThrowInvalidCastException()
    {
        var element = XElement.Parse("<Test/>").Deserialize<ILogixElement>();

        FluentActions.Invoking(() => element.As<ChildElement>()).Should().Throw<InvalidCastException>();
    }

    [Test]
    public void Clone_WhenCalled_ShouldNotBeNull()
    {
        var element = new TestElement();

        var clone = element.Clone();

        clone.Should().NotBeNull();
    }

    [Test]
    public void Clone_WhenCalled_ShouldBeDifferentInstance()
    {
        var element = new TestElement();

        var clone = element.Clone();

        clone.Should().NotBeSameAs(element);
    }

    [Test]
    public void Clone_WhenCalled_ShouldBeOfSameType()
    {
        var element = new TestElement();

        var clone = element.Clone();

        clone.Should().BeOfType<TestElement>();
    }

    [Test]
    public void Clone_WhenCalled_ShouldBeDifferentXElementInstance()
    {
        var element = new TestElement();
        var xml = element.Serialize();

        var clone = element.Clone();
        var copy = clone.Serialize();

        xml.Should().NotBeSameAs(copy);
    }

    [Test]
    public void GetValue_HasValue_ShouldHaveExpectedValue()
    {
        var xml = new XElement("Test", new XAttribute("OptionalValue", "Value"));
        var element = new TestElement(xml);

        var value = element.OptionalValue;

        value.Should().Be("Value");
    }

    [Test]
    public void GetValue_NoValue_ShouldBeNull()
    {
        var xml = new XElement("Test");
        var element = new TestElement(xml);

        var value = element.OptionalValue;

        value.Should().BeNull();
    }

    [Test]
    public Task SetValue_ToValueHasValue_ShouldBeVerified()
    {
        var xml = new XElement("Test", new XAttribute("OptionalValue", "Value"));
        var element = new TestElement(xml);

        element.OptionalValue = "This is a test";

        return Verify(element.Serialize().ToString());
    }

    [Test]
    public Task SetValue_ToValueEmptyValue_ShouldBeVerified()
    {
        var xml = new XElement("Test", new XAttribute("OptionalValue", ""));
        var element = new TestElement(xml);

        element.OptionalValue = "This is a test";

        return Verify(element.Serialize().ToString());
    }

    [Test]
    public Task SetValue_ToEmptyHasValue_ShouldBeVerified()
    {
        var xml = new XElement("Test", new XAttribute("OptionalValue", "Value"));
        var element = new TestElement(xml);

        element.OptionalValue = string.Empty;

        return Verify(element.Serialize().ToString());
    }

    [Test]
    public Task SetValue_ToValueFromNull_ShouldBeVerified()
    {
        var xml = new XElement("Test");
        var element = new TestElement(xml);

        element.OptionalValue = "This is a test";

        return Verify(element.Serialize().ToString());
    }

    [Test]
    public Task SetValue_ToNullHasValue_ShouldBeVerified()
    {
        var xml = new XElement("Test", new XAttribute("OptionalValue", "Value"));
        var element = new TestElement(xml);

        element.OptionalValue = null;

        return Verify(element.Serialize().ToString());
    }

    [Test]
    public void GetRequiredValue_HasValue_ShouldBeExpectedValue()
    {
        var xml = new XElement("Test", new XAttribute("RequiredValue", "Value"));
        var element = new TestElement(xml);

        var value = element.RequiredValue;

        value.Should().Be("Value");
    }

    [Test]
    public void GetRequiredValue_NullValue_ShouldBeThrowException()
    {
        var xml = new XElement("Test");
        var element = new TestElement(xml);

        FluentActions.Invoking(() => element.RequiredValue).Should().Throw<InvalidOperationException>();
    }

    [Test]
    public Task SetRequiredValue_NoValueToValue_ShouldBeVerified()
    {
        var xml = new XElement("Test");
        var element = new TestElement(xml);

        element.RequiredValue = "Value";

        return Verify(element.Serialize().ToString());
    }

    [Test]
    public Task SetRequiredValue_HasValueDifferentValue_ShouldBeVerified()
    {
        var xml = new XElement("Test", new XAttribute("RequiredValue", "Value"));
        var element = new TestElement(xml);

        element.RequiredValue = "NewValue";

        return Verify(element.Serialize().ToString());
    }

    [Test]
    public void SetRequiredValue_HasValueToNull_ShouldBeVerified()
    {
        var xml = new XElement("Test", new XAttribute("RequiredValue", "Value"));
        var element = new TestElement(xml);

        FluentActions.Invoking(() => element.RequiredValue = null!).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void GetProperty_HasValue_ShouldBeExpectedValue()
    {
        var xml = new XElement("Test", new XElement("Property", "This is the value"));
        var element = new TestElement(xml);

        var value = element.Property;

        value.Should().Be("This is the value");
    }

    [Test]
    public void GetProperty_NoValue_ShouldBeNull()
    {
        var xml = new XElement("Test");
        var element = new TestElement(xml);

        var value = element.Property;

        value.Should().BeNull();
    }

    [Test]
    public Task SetProperty_NoValueToValue_ShouldBeVerified()
    {
        var xml = new XElement("Test");
        var element = new TestElement(xml);

        element.Property = "This is a new value";

        return Verify(element.Serialize().ToString());
    }

    [Test]
    public Task SetProperty_HasValueDifferentValue_ShouldBeVerified()
    {
        var xml = new XElement("Test", new XElement("Property", "This is the value"));
        var element = new TestElement(xml);

        element.Property = "This is the new value";

        return Verify(element.Serialize().ToString());
    }

    [Test]
    public Task SetProperty_HasValueToNull_ShouldBeVerified()
    {
        var xml = new XElement("Test", new XElement("Property", "This is the value"));
        var element = new TestElement(xml);

        element.Property = null;

        return Verify(element.Serialize().ToString());
    }

    [Test]
    public void GetComplex_HasValue_ShouldBeExpectedValue()
    {
        var xml = new XElement("Test", new XElement("ChildElement", new XAttribute("SomeValue", "Value")));
        var element = new TestElement(xml);

        var value = element.ChildElement;

        value.Should().NotBeNull();
        value.Should().BeOfType<ChildElement>();
        value.SomeValue.Should().Be("Value");
    }

    [Test]
    public void GetComplex_NoValue_ShouldBeNull()
    {
        var xml = new XElement("Test");
        var element = new TestElement(xml);

        var value = element.ChildElement;

        value.Should().BeNull();
    }

    [Test]
    public Task SetComplex_NoValueToValue_ShouldBeVerified()
    {
        var xml = new XElement("Test");
        var element = new TestElement(xml);

        element.ChildElement = new ChildElement { SomeValue = "Test Value" };

        return Verify(element.Serialize().ToString());
    }

    [Test]
    public Task SetComplex_HasValueDifferentValue_ShouldBeVerified()
    {
        var xml = new XElement("Test", new XElement("ChildElement", new XAttribute("SomeValue", "Value")));
        var element = new TestElement(xml);

        element.ChildElement = new ChildElement { SomeValue = "Replacement object" };

        return Verify(element.Serialize().ToString());
    }

    [Test]
    public Task SetComplex_HasValueToNull_ShouldBeVerified()
    {
        var xml = new XElement("Test", new XElement("ChildElement", new XAttribute("SomeValue", "Value")));
        var element = new TestElement(xml);

        element.ChildElement = null;

        return Verify(element.Serialize().ToString());
    }

    [Test]
    public void GetContainer_HasValue_ShouldBeExpectedValue()
    {
        var xml = new XElement("Test",
            new XElement("ChildElements",
                new XElement("ChildElement", new XAttribute("SomeValue", "Child_1")),
                new XElement("ChildElement", new XAttribute("SomeValue", "Child_2")),
                new XElement("ChildElement", new XAttribute("SomeValue", "Child_3"))
            ));
        var element = new TestElement(xml);

        var value = element.ChildElements;

        value.Should().NotBeNull();
        value.Should().BeOfType<LogixContainer<ChildElement>>();
        value.Should().HaveCount(3);
    }

    [Test]
    public void GetContainer_NoContainerElement_ShouldThrowException()
    {
        var xml = new XElement("Test");
        var element = new TestElement(xml);

        FluentActions.Invoking(() => element.ChildElements).Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void GetContainer_NoElements_ShouldBeEmpty()
    {
        var xml = new XElement("Test", new XElement("ChildElements"));
        var element = new TestElement(xml);

        var value = element.ChildElements;

        value.Should().BeEmpty();
    }

    [Test]
    public Task SetContainer_NoValueToEmptyCollection_ShouldBeVerified()
    {
        var xml = new XElement("Test");
        var element = new TestElement(xml);

        element.ChildElements = [];

        return Verify(element.Serialize().ToString());
    }

    [Test]
    public Task SetContainer_NoValueCollectionWithElements_ShouldBeVerified()
    {
        var xml = new XElement("Test");
        var element = new TestElement(xml);

        element.ChildElements =
        [
            new ChildElement { SomeValue = "Child_1" },
            new ChildElement { SomeValue = "Child_2" },
            new ChildElement { SomeValue = "Child_3" }
        ];

        return Verify(element.Serialize().ToString());
    }

    [Test]
    public Task SetContainer_HasValueDifferentValue_ShouldBeVerified()
    {
        var xml = new XElement("Test",
            new XElement("ChildElements",
                new XElement("ChildElement", new XAttribute("SomeValue", "Child_1")),
                new XElement("ChildElement", new XAttribute("SomeValue", "Child_2")),
                new XElement("ChildElement", new XAttribute("SomeValue", "Child_3"))
            ));
        var element = new TestElement(xml);

        element.ChildElements =
        [
            new ChildElement { SomeValue = "Child_3" },
            new ChildElement { SomeValue = "Child_2" },
            new ChildElement { SomeValue = "Child_1" }
        ];

        return Verify(element.Serialize().ToString());
    }

    [Test]
    public Task SetContainer_HasValueToNull_ShouldBeVerified()
    {
        var xml = new XElement("Test",
            new XElement("ChildElements",
                new XElement("ChildElement", new XAttribute("SomeValue", "Child_1")),
                new XElement("ChildElement", new XAttribute("SomeValue", "Child_2")),
                new XElement("ChildElement", new XAttribute("SomeValue", "Child_3"))
            ));
        var element = new TestElement(xml);

        element.ChildElements = null!;

        return Verify(element.Serialize().ToString());
    }

    [Test]
    public void GetDateTime_HasValue_ShouldBeExpectedValue()
    {
        var xml = new XElement("Test", new XAttribute("Date", "Mon Sep 27 15:23:27 2021"));
        var element = new TestElement(xml);

        var value = element.Date;

        value.Should().Be(new DateTime(2021, 9, 27, 15, 23, 27));
    }

    [Test]
    public void GetDateTime_NoValue_ShouldBeNull()
    {
        var xml = new XElement("Test");
        var element = new TestElement(xml);

        var value = element.Date;

        value.Should().BeNull();
    }

    [Test]
    public Task SetDateTime_NoValueToValue_ShouldBeVerified()
    {
        var xml = new XElement("Test");
        var element = new TestElement(xml);

        element.Date = new DateTime(2021, 9, 27, 15, 23, 27);

        return Verify(element.Serialize().ToString());
    }

    [Test]
    public Task SetDateTime_HasValueDifferentValue_ShouldBeVerified()
    {
        var xml = new XElement("Test", new XAttribute("Date", "Mon Sep 27 15:23:27 2021"));
        var element = new TestElement(xml);

        element.Date = new DateTime(2023, 9, 27, 15, 23, 27);

        return Verify(element.Serialize().ToString());
    }

    [Test]
    public Task SetDateTime_HasValueToNull_ShouldBeVerified()
    {
        var xml = new XElement("Test", new XAttribute("Date", "Mon Sep 27 15:23:27 2021"));
        var element = new TestElement(xml);

        element.Date = null;

        return Verify(element.Serialize().ToString());
    }

    [Test]
    public Task SetDescription_NoValueToValue_ShouldBeVerified()
    {
        var xml = new XElement("Test");
        var element = new TestElement(xml);

        element.Description = "This is a description test";

        return Verify(element.Serialize().ToString());
    }

    [Test]
    public Task SetDescription_HasValueDifferentValue_ShouldBeVerified()
    {
        var xml = new XElement("Test", new XElement("Description", new XCData("This is a test")));
        var element = new TestElement(xml);

        element.Description = "This is an updated description test";

        return Verify(element.Serialize().ToString());
    }

    [Test]
    public Task SetDescription_HasValueToNull_ShouldBeVerified()
    {
        var xml = new XElement("Test", new XElement("Description", new XCData("This is a test")));
        var element = new TestElement(xml);

        element.Description = null;

        return Verify(element.Serialize().ToString());
    }

    [Test]
    public void AddAfter_Null_ShouldThrowException()
    {
        var xml = new XElement("Test", new XAttribute("RequiredValue", "Test_1"));
        var element = new TestElement(xml);

        FluentActions.Invoking(() => element.AddAfter(null!)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void AddAfter_NoParent_ShouldThrowException()
    {
        var xml = new XElement("Test", new XAttribute("RequiredValue", "Test_1"));
        var element = new TestElement(xml);

        FluentActions.Invoking(() => element.AddAfter(new TestElement { RequiredValue = "Test_3" })).Should()
            .Throw<InvalidOperationException>();
    }

    [Test]
    public Task AddAfter_ValidElement_ShouldBeVerified()
    {
        var container = new XElement("Container",
            new XElement("Test", new XAttribute("RequiredValue", "Test_1")),
            new XElement("Test", new XAttribute("RequiredValue", "Test_2"))
        );
        var xml = container.FirstNode as XElement;
        var element = new TestElement(xml!);

        element.AddAfter(new TestElement { RequiredValue = "Test_3" });

        return Verify(container.ToString());
    }

    [Test]
    public void AddBefore_Null_ShouldThrowException()
    {
        var xml = new XElement("Test", new XAttribute("RequiredValue", "Test_1"));
        var element = new TestElement(xml);

        FluentActions.Invoking(() => element.AddBefore(null!)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void AddBefore_NoParent_ShouldThrowException()
    {
        var xml = new XElement("Test", new XAttribute("RequiredValue", "Test_1"));
        var element = new TestElement(xml);

        FluentActions.Invoking(() => element.AddBefore(new TestElement { RequiredValue = "Test_3" })).Should()
            .Throw<InvalidOperationException>();
    }

    [Test]
    public Task AddBefore_ValidElement_ShouldBeVerified()
    {
        var container = new XElement("Container",
            new XElement("Test", new XAttribute("RequiredValue", "Test_1")),
            new XElement("Test", new XAttribute("RequiredValue", "Test_2"))
        );
        var xml = container.FirstNode as XElement;
        var element = new TestElement(xml!);

        element.AddBefore(new TestElement { RequiredValue = "Test_3" });

        return Verify(container.ToString());
    }

    [Test]
    public void Replace_Null_ShouldThrowException()
    {
        var xml = new XElement("Test", new XAttribute("RequiredValue", "Test_1"));
        var element = new TestElement(xml);

        FluentActions.Invoking(() => element.Replace(null!)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Replace_NoParent_ShouldThrowException()
    {
        var xml = new XElement("Test", new XAttribute("RequiredValue", "Test_1"));
        var element = new TestElement(xml);

        FluentActions.Invoking(() => element.Replace(new TestElement { RequiredValue = "Test_3" })).Should()
            .Throw<InvalidOperationException>();
    }

    [Test]
    public Task Replace_ValidElement_ShouldBeVerified()
    {
        var container = new XElement("Container",
            new XElement("Test", new XAttribute("RequiredValue", "Test_1")),
            new XElement("Test", new XAttribute("RequiredValue", "Test_2"))
        );
        var xml = container.FirstNode as XElement;
        var element = new TestElement(xml!);

        element.Replace(new TestElement { RequiredValue = "Test_3" });

        return Verify(container.ToString());
    }

    [Test]
    public void Remove_NoParent_ShouldBeVerified()
    {
        var xml = new XElement("Test", new XAttribute("RequiredValue", "Test_1"));
        var element = new TestElement(xml);

        element.Remove();

        element.Serialize().Parent.Should().BeNull();
    }

    [Test]
    public Task Remove_HasParent_ShouldBeVerified()
    {
        var container = new XElement("Container",
            new XElement("Test", new XAttribute("RequiredValue", "Test_1")),
            new XElement("Test", new XAttribute("RequiredValue", "Test_2"))
        );
        var xml = container.FirstNode as XElement;
        var element = new TestElement(xml!);

        element.Remove();

        return Verify(container.ToString());
    }

    [Test]
    public void EquivalentTo_AreEquivalent_ShouldBeTrue()
    {
        var first = new TestElement();
        var second = new TestElement();

        var result = first.EquivalentTo(second);

        result.Should().BeTrue();
    }

    [Test]
    public void EquivalentTo_AreEquivalentWithSetProperties_ShouldBeTrue()
    {
        var first = new TestElement
        {
            OptionalValue = "Testing",
            Property = "SomeValue",
            Description = "This is a test",
            Date = new DateTime(2024, 1, 1)
        };

        var second = new TestElement
        {
            OptionalValue = "Testing",
            Property = "SomeValue",
            Description = "This is a test",
            Date = new DateTime(2024, 1, 1)
        };

        var result = first.EquivalentTo(second);

        result.Should().BeTrue();
    }

    [Test]
    public void EquivalentTo_AreNotEquivalentWithOneDifferent_ShouldBeFalse()
    {
        var first = new TestElement
        {
            OptionalValue = "Testing",
            Property = "SomeValue",
            Description = "This is a  test",
            Date = new DateTime(2024, 1, 1)
        };

        var second = new TestElement
        {
            OptionalValue = "Testing",
            Property = "SomeValue",
            Description = "This is a test",
            Date = new DateTime(2024, 1, 1)
        };

        var result = first.EquivalentTo(second);

        result.Should().BeFalse();
    }

    [Test]
    public void EquivalentTo_AreNotEquivalentOneUnsetProperty_ShouldBeFalse()
    {
        var first = new TestElement
        {
            Property = "SomeValue",
            Description = "This is a test",
            Date = new DateTime(2024, 1, 1)
        };

        var second = new TestElement
        {
            OptionalValue = "Testing",
            Property = "SomeValue",
            Description = "This is a test",
            Date = new DateTime(2024, 1, 1)
        };

        var result = first.EquivalentTo(second);

        result.Should().BeFalse();
    }

    [Test]
    public void Metadata_AddAndGet_ShouldBeExpected()
    {
        var element = new TestElement();

        element.Metadata.Add("Test", "Value");

        var result = element.Metadata.Get<string>("Test");

        result.Should().Be("Value");
    }

    [Test]
    public void Metadata_SetAndGet_ShouldBeExpected()
    {
        var element = new TestElement();

        element.Metadata["Test"] = 123;

        var result = element.Metadata.Get<int>("Test");

        result.Should().Be(123);
    }

    [Test]
    public void Metadata_Remove_ShouldBeExpected()
    {
        var element = new TestElement();
        element.Metadata.Add("Test", "Value");

        var result = element.Metadata.Remove("Test");

        result.Should().BeTrue();
        element.Metadata.Contains("Test").Should().BeFalse();
    }

    [Test]
    public void Metadata_Clear_ShouldBeEmpty()
    {
        var element = new TestElement();
        element.Metadata.Add("Test1", 1);
        element.Metadata.Add("Test2", 2);

        element.Metadata.Clear();

        element.Metadata.Count.Should().Be(0);
    }

    [Test]
    public void Metadata_Contains_ShouldBeExpected()
    {
        var element = new TestElement();
        element.Metadata.Add("Test", "Value");

        element.Metadata.Contains("Test").Should().BeTrue();
        element.Metadata.Contains("Invalid").Should().BeFalse();
    }

    [Test]
    public void Metadata_GetInvalidKey_ShouldThrowKeyNotFoundException()
    {
        var element = new TestElement();

        FluentActions.Invoking(() => element.Metadata.Get<string>("Invalid")).Should().Throw<KeyNotFoundException>();
    }

    [Test]
    public void Metadata_GetInvalidCast_ShouldThrowInvalidCastException()
    {
        var element = new TestElement();
        element.Metadata.Add("Test", 123);

        FluentActions.Invoking(() => element.Metadata.Get<string>("Test")).Should().Throw<InvalidCastException>();
    }

    [Test]
    public void Metadata_TryGetValue_ShouldBeExpected()
    {
        var element = new TestElement();
        element.Metadata.Add("Test", "Value");

        var result = element.Metadata.TryGetValue("Test", out var value);

        result.Should().BeTrue();
        value.Should().Be("Value");
    }

    [Test]
    public void Metadata_TryGetValueGeneric_ShouldBeExpected()
    {
        var element = new TestElement();
        element.Metadata.Add("Test", 123);

        var result = element.Metadata.TryGetValue<int>("Test", out var value);

        result.Should().BeTrue();
        value.Should().Be(123);
    }

    [Test]
    public void Metadata_TryGetValueGenericInvalidCast_ShouldReturnFalse()
    {
        var element = new TestElement();
        element.Metadata.Add("Test", 123);

        var result = element.Metadata.TryGetValue<string>("Test", out var value);

        result.Should().BeFalse();
        value.Should().BeNull();
    }

    [Test]
    public void Metadata_GetEnumerator_ShouldHaveItems()
    {
        var element = new TestElement();
        element.Metadata.Add("Test1", 1);
        element.Metadata.Add("Test2", 2);

        var list = element.Metadata.ToList();

        list.Should().HaveCount(2);
        list.Should().Contain(kv => kv.Key == "Test1" && (int)kv.Value == 1);
        list.Should().Contain(kv => kv.Key == "Test2" && (int)kv.Value == 2);
    }

    [Test]
    public void Metadata_LazyInitialization_ShouldNotHaveAnnotationInitially()
    {
        var element = new TestElement();
        var xml = element.Serialize();

        xml.Annotation<Dictionary<string, object>>().Should().BeNull();

        // Accessing count or other members should trigger initialization
        var count = element.Metadata.Count;

        count.Should().Be(0);
        xml.Annotation<Dictionary<string, object>>().Should().NotBeNull();
    }
}

[LogixElement("Test")]
[LogixElement("Alternate")]
public class TestElement : LogixObject<TestElement>
{
    public TestElement() : base("Test")
    {
    }

    public TestElement(XElement element) : base(element)
    {
    }

    public string? OptionalValue
    {
        get => GetValue();
        set => SetValue(value);
    }

    public string RequiredValue
    {
        get => GetRequiredValue();
        set => SetRequiredValue(value);
    }

    public string? Property
    {
        get => GetProperty();
        set => SetProperty(value);
    }

    public string? Description
    {
        get => GetProperty();
        set => SetProperty(value);
    }

    public DateTime? Date
    {
        get => GetDateTime();
        set => SetDateTime(value);
    }

    public ChildElement? ChildElement
    {
        get => GetComplex<ChildElement>();
        set => SetComplex(value);
    }

    public LogixContainer<ChildElement> ChildElements
    {
        get => GetContainer<ChildElement>();
        set => SetContainer(value);
    }
}

[LogixElement(nameof(ChildElement))]
public class ChildElement : LogixObject<ChildElement>
{
    public ChildElement() : base(nameof(ChildElement))
    {
    }

    public ChildElement(XElement element) : base(element)
    {
    }

    public string? SomeValue
    {
        get => GetValue();
        init => SetValue(value);
    }
}