using System.Xml.Linq;
using FluentAssertions;

// ReSharper disable UseObjectOrCollectionInitializer

namespace L5Sharp.Tests;

[TestFixture]
public class LogixElementTests
{
    [Test]
    public void New_Default_ShouldNotBeNull()
    {
        var element = new TestElement();

        element.Should().NotBeNull();
    }

    [Test]
    public void New_Default_ShouldHaveExpectedValues()
    {
        var element = new TestElement();

        element.L5XType.Should().Be("Test");
        element.L5X.Should().BeNull();
        element.Scope.IsScoped.Should().BeFalse();
        element.Scope.Container.Should().BeEmpty();
        element.Scope.Level.Should().Be(ScopeLevel.Null);
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
    public void GetSelectorValue_HasValue_ShouldBeExpectedValue()
    {
        var xml = new XElement("Test", new XElement("Child", new XAttribute("SelectorValue", "Value")));
        var element = new TestElement(xml);

        var value = element.SelectorValue;

        value.Should().Be("Value");
    }

    [Test]
    public void GetSelectorValue_NoValue_ShouldBeNull()
    {
        var xml = new XElement("Test", new XElement("Child"));
        var element = new TestElement(xml);

        var value = element.SelectorValue;

        value.Should().BeNull();
    }

    [Test]
    public Task SetSelectorValue_NoValueToValue_ShouldBeVerified()
    {
        var xml = new XElement("Test", new XElement("Child"));
        var element = new TestElement(xml);

        element.SelectorValue = "Value";

        return Verify(element.Serialize().ToString());
    }

    [Test]
    public Task SetSelectorValue_HasValueDifferentValue_ShouldBeVerified()
    {
        var xml = new XElement("Test", new XElement("Child", new XAttribute("SelectorValue", "Value")));
        var element = new TestElement(xml);

        element.SelectorValue = "NewValue";

        return Verify(element.Serialize().ToString());
    }

    [Test]
    public Task SetSelectorValue_HasValueToNull_ShouldBeVerified()
    {
        var xml = new XElement("Test", new XElement("Child", new XAttribute("SelectorValue", "Value")));
        var element = new TestElement(xml);

        element.SelectorValue = null;

        return Verify(element.Serialize().ToString());
    }

    [Test]
    public void GetChildValue_HasValue_ShouldBeExpectedValue()
    {
        var xml = new XElement("Test", new XElement("Child", new XAttribute("ChildValue", 123)));
        var element = new TestElement(xml);

        var value = element.ChildValue;

        value.Should().Be(123);
    }

    [Test]
    public void GetChildValue_NoValue_ShouldBeNull()
    {
        var xml = new XElement("Test", new XElement("Child"));
        var element = new TestElement(xml);

        var value = element.ChildValue;

        value.Should().BeNull();
    }

    [Test]
    public Task SetChildValue_NoValueToValue_ShouldBeVerified()
    {
        var xml = new XElement("Test", new XElement("Child"));
        var element = new TestElement(xml);

        element.ChildValue = 123;

        return Verify(element.Serialize().ToString());
    }

    [Test]
    public Task SetChildValue_HasValueDifferentValue_ShouldBeVerified()
    {
        var xml = new XElement("Test", new XElement("Child", new XAttribute("ChildValue", 123)));
        var element = new TestElement(xml);

        element.ChildValue = 321;

        return Verify(element.Serialize().ToString());
    }

    [Test]
    public Task SetChildValue_HasValueToNull_ShouldBeVerified()
    {
        var xml = new XElement("Test", new XElement("Child", new XAttribute("ChildValue", 123)));
        var element = new TestElement(xml);

        element.ChildValue = null;

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
        value?.SomeValue.Should().Be("Value");
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
    public Task AddAfter_AlternateType_ShouldBeVerified()
    {
        var container = new XElement("Container",
            new XElement("Test", new XAttribute("RequiredValue", "Test_1")),
            new XElement("Test", new XAttribute("RequiredValue", "Test_2"))
        );
        var xml = container.FirstNode as XElement;
        var element = new TestElement(xml!);

        element.AddAfter(new TestElement(new XElement("Alternate", new XAttribute("RequiredValue", "Test_3"))));

        return Verify(container.ToString());
    }

    [Test]
    public void AddAfter_InvalidType_ShouldThrowException()
    {
        var container = new XElement("Container",
            new XElement("Test", new XAttribute("RequiredValue", "Test_1")),
            new XElement("Test", new XAttribute("RequiredValue", "Test_2"))
        );
        var xml = container.FirstNode as XElement;
        var element = new TestElement(xml!);

        FluentActions.Invoking(() => element.AddAfter(new ChildElement())).Should()
            .Throw<InvalidOperationException>();
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
    public Task AddBefore_AlternateType_ShouldBeVerified()
    {
        var container = new XElement("Container",
            new XElement("Test", new XAttribute("RequiredValue", "Test_1")),
            new XElement("Test", new XAttribute("RequiredValue", "Test_2"))
        );
        var xml = container.FirstNode as XElement;
        var element = new TestElement(xml!);

        element.AddBefore(new TestElement(new XElement("Alternate", new XAttribute("RequiredValue", "Test_3"))));

        return Verify(container.ToString());
    }

    [Test]
    public void AddBefore_InvalidType_ShouldThrowException()
    {
        var container = new XElement("Container",
            new XElement("Test", new XAttribute("RequiredValue", "Test_1")),
            new XElement("Test", new XAttribute("RequiredValue", "Test_2"))
        );
        var xml = container.FirstNode as XElement;
        var element = new TestElement(xml!);

        FluentActions.Invoking(() => element.AddBefore(new ChildElement())).Should()
            .Throw<InvalidOperationException>();
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
    public Task Replace_AlternateType_ShouldBeVerified()
    {
        var container = new XElement("Container",
            new XElement("Test", new XAttribute("RequiredValue", "Test_1")),
            new XElement("Test", new XAttribute("RequiredValue", "Test_2"))
        );
        var xml = container.FirstNode as XElement;
        var element = new TestElement(xml!);

        element.Replace(new TestElement(new XElement("Alternate", new XAttribute("RequiredValue", "Test_3"))));

        return Verify(container.ToString());
    }

    [Test]
    public void Replace_InvalidType_ShouldThrowException()
    {
        var container = new XElement("Container",
            new XElement("Test", new XAttribute("RequiredValue", "Test_1")),
            new XElement("Test", new XAttribute("RequiredValue", "Test_2"))
        );
        var xml = container.FirstNode as XElement;
        var element = new TestElement(xml!);

        FluentActions.Invoking(() => element.Replace(new ChildElement())).Should()
            .Throw<InvalidOperationException>();
    }

    [Test]
    public void Remove_NoParent_ShouldThrowException()
    {
        var xml = new XElement("Test", new XAttribute("RequiredValue", "Test_1"));
        var element = new TestElement(xml);

        FluentActions.Invoking(() => element.Remove()).Should()
            .Throw<InvalidOperationException>();
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
    public void Convert_AsElementNullType_ShouldThrowException()
    {
        var element = new TestElement(new XElement("Test", new XAttribute("RequiredValue", "Test_1")));

        FluentActions.Invoking(() => element.Convert(null!)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void Convert_AsElementEmptyType_ShouldThrowException()
    {
        var element = new TestElement(new XElement("Test", new XAttribute("RequiredValue", "Test_1")));

        FluentActions.Invoking(() => element.Convert(string.Empty)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void Convert_AsElementInvalidType_ShouldThrowException()
    {
        var element = new TestElement(new XElement("Test", new XAttribute("RequiredValue", "Test_1")));

        FluentActions.Invoking(() => element.Convert("ChildElement")).Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void Convert_AsElementAlternateType_ShouldHaveExpectedTypeAndBeDifferentInstance()
    {
        var element = new TestElement(new XElement("Test", new XAttribute("RequiredValue", "Test_1")));

        var converted = element.Convert("Alternate");

        converted.L5XType.Should().Be("Alternate");
        converted.Should().BeOfType<TestElement>();
        converted.Should().NotBeSameAs(element);
    }

    [Test]
    public void Convert_AsElementSameType_ShouldHaveExpectedTypeAndBeSameInstance()
    {
        var element = new TestElement(new XElement("Test", new XAttribute("RequiredValue", "Test_1")));

        var converted = element.Convert("Test");

        converted.L5XType.Should().Be("Test");
        converted.Should().BeOfType<TestElement>();
        converted.Should().BeSameAs(element);
    }

    [Test]
    public void Convert_AsTypeInvalidType_ShouldThrowException()
    {
        var element = new TestElement(new XElement("Test", new XAttribute("RequiredValue", "Test_1")));

        FluentActions.Invoking(() => element.Convert<ChildElement>()).Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void Convert_AsTypeAlternateType_ShouldHaveExpectedTypeAndBeDifferentInstance()
    {
        var element = new TestElement(new XElement("Test", new XAttribute("RequiredValue", "Test_1")));

        var converted = element.Convert<TestElement>("Alternate");

        converted.L5XType.Should().Be("Alternate");
        converted.Should().BeOfType<TestElement>();
        converted.Should().NotBeSameAs(element);
    }

    [Test]
    public void Convert_AsTypeDefaultType_ShouldHaveExpectedTypeAndBeSameInstance()
    {
        var element = new TestElement(new XElement("Test", new XAttribute("RequiredValue", "Test_1")));

        var converted = element.Convert<TestElement>();

        converted.L5XType.Should().Be("Test");
        converted.Should().BeOfType<TestElement>();
        converted.Should().BeSameAs(element);
    }

    /*[Test]
    public void Parse_ValidXml_ShouldNotBeNull()
    {
        
        var element = LogixElement.Parse("");

        element.Should().NotBeNull();
    }*/

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
    public void EquivalentTo_DifferentType_ShouldBeFalse()
    {
        var first = new TestElement();
        var second = new ChildElement();

        var result = first.EquivalentTo(second);

        result.Should().BeFalse();
    }
}

[L5XType("Test")]
[L5XType("Alternate")]
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
        get => GetValue<string>();
        set => SetValue(value);
    }

    public string? SelectorValue
    {
        get => GetValue<string>(e => e.Element("Child"));
        set => SetValue(value, e => e.Element("Child"));
    }

    public string RequiredValue
    {
        get => GetRequiredValue<string>();
        set => SetRequiredValue(value);
    }

    public int? ChildValue
    {
        get => GetValue<int?>(XName.Get("Child"));
        set => SetValue(value, XName.Get("Child"));
    }

    public string? Property
    {
        get => GetProperty<string>();
        set => SetProperty(value);
    }

    public string? Description
    {
        get => GetProperty<string>();
        set => SetDescription(value);
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

public class ChildElement : LogixObject
{
    public ChildElement() : base(nameof(ChildElement))
    {
    }

    public ChildElement(XElement element) : base(element)
    {
    }

    public string? SomeValue
    {
        get => GetValue<string>();
        set => SetValue(value);
    }
}