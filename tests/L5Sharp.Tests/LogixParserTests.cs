using FluentAssertions;

namespace L5Sharp.Tests;

[TestFixture]
public class LogixParserTests
{
    [Test]
    [TestCase(typeof(bool))]
    [TestCase(typeof(int))]
    [TestCase(typeof(uint))]
    [TestCase(typeof(float))]
    [TestCase(typeof(double))]
    [TestCase(typeof(string))]
    [TestCase(typeof(DateTime))]
    public void IsParsable_NativeType_ShouldBeTrue(Type type)
    {
        type.IsParsable().Should().BeTrue();
    }
    
    [Test]
    [TestCase(typeof(TagName))]
    [TestCase(typeof(Dimensions))]
    [TestCase(typeof(Radix))]
    [TestCase(typeof(ExternalAccess))]
    [TestCase(typeof(DINT))]
    [TestCase(typeof(REAL))]
    [TestCase(typeof(AtomicType))]
    [TestCase(typeof(STRING))]
    public void IsParsable_LogixType_ShouldBeTrue(Type type)
    {
        type.IsParsable().Should().BeTrue();
    }
    
    [Test]
    [TestCase("true", true, typeof(bool))]
    [TestCase("123", 123, typeof(int))]
    [TestCase("lol", "lol", typeof(string))]
    [TestCase("1.234", 1.234, typeof(decimal))]
    public void Parse_Primitives_ShouldBeExpectedValue(string input, object expected, Type type)
    {
        var result = input.Parse(type);

        result.Should().Be(expected);
        result.Should().BeOfType(type);
    }

    [Test]
    public void Parse_DateTime_ShouldBeExpected()
    {
        var result = "1/1/2024 1:23:45 AM".Parse<DateTime>();

        result.Should().BeAfter(new DateTime(2024, 1, 1));
    }

    [Test]
    public void Parse_LogixEnum_ShouldBeExpected()
    {
        var result = "ReadWrite".Parse<ExternalAccess>();

        result.Should().NotBeNull();
        result.Should().Be(ExternalAccess.ReadWrite);
    }

    [Test]
    public void Parse_Radix_ShouldBeExpected()
    {
        var result = "Decimal".Parse<Radix>();

        result.Should().NotBeNull();
        result.Should().Be(Radix.Decimal);
    }

    [Test]
    public void Parse_RadixTypeFromDerivedEnumValue_ToEnsurePrivateClassesAreAlsoParsable()
    {
        var type = Radix.Octal.GetType();
        var result = "Octal".Parse(type);

        result.Should().NotBeNull();
        result.Should().Be(Radix.Octal);
    }
    
    [Test]
    public void Parse_RadixTypeFromDerivedEnumValue_ToEnsureThatThisAlsoParsedToOtherRadixTypes()
    {
        var type = Radix.Decimal.GetType();
        var result = "Octal".Parse(type);

        result.Should().NotBeNull();
        result.Should().Be(Radix.Octal);
    }

    [Test]
    public void Parse_RadixByValue_ShouldBeExpected()
    {
        var result = "ASCII".Parse<Radix>();

        result.Should().NotBeNull();
        result.Should().Be(Radix.Ascii);
    }

    [Test]
    public void Parse_TriggerOperationWhichHasIntValueType_ShouldBeExpected()
    {
        var result = "1".Parse<TriggerOperation>();

        result.Should().NotBeNull();
        result.Should().Be(TriggerOperation.TriggerLevel);
    }

    [Test]
    public void Parse_TriggerOperationWhichHasIntValueTypeByName_ShouldBeExpected()
    {
        var result = "PositiveSlope".Parse<TriggerOperation>();

        result.Should().NotBeNull();
        result.Should().Be(TriggerOperation.PositiveSlope);
    }

    [Test]
    public void Parse_OperatorWhichHasSymbolValues_ShouldBeExpected()
    {
        var result = "=".Parse<Operator>();

        result.Should().NotBeNull();
        result.Should().Be(Operator.Equal);
    }

    [Test]
    public void ParseByType_LogixEnum_ShouldBeExpected()
    {
        var result = "RLL".Parse(typeof(RoutineType));

        result.Should().NotBeNull();
        result.Should().Be(RoutineType.RLL);
    }

    [Test]
    public void TryParse_LogixEnum_ShouldBeExpected()
    {
        var result = "Periodic".TryParse<TaskType>();

        result.Should().NotBeNull();
        result.Should().Be(TaskType.Periodic);
    }

    [Test]
    public void TryParse_InvalidEnum_ShouldBeNull()
    {
        var result = "Fake".TryParse<Use>();

        result.Should().BeNull();
    }

    [Test]
    public void TryParseByType_LogixEnumInvalidValue_ShouldBeNull()
    {
        var result = "Invalid".TryParse(typeof(TagType));

        result.Should().BeNull();
    }

    [Test]
    public void Parse_AtomicType_ShouldBeExpected()
    {
        var result = "123".Parse<DINT>();

        result.Should().NotBeNull();
        result.Should().Be(new DINT(123));
    }

    [Test]
    public void Parse_GenericAtomicTypeBooleanValue_ShouldBeExpected()
    {
        var result = "true".Parse<AtomicType>();

        result.Should().NotBeNull();
    }
}