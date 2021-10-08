using System;
using System.Globalization;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class PredefinedTests
    {
        [Test]
        public void Types_WhenCalled_ShouldNotBeEmpty()
        {
            var predefined = Predefined.Types();

            predefined.Should().NotBeEmpty();
        }

        [Test]
        public void GetAtomic_WhenCalled_ShouldNotBeEmpty()
        {
            var atomic = Predefined.Types().Where(t => t.IsAtomic).ToList();

            atomic.Should().NotBeEmpty();
            atomic.Should().Contain(x => x.Name == "BOOL");
            atomic.Should().Contain(x => x.Name == "SINT");
            atomic.Should().Contain(x => x.Name == "INT");
            atomic.Should().Contain(x => x.Name == "DINT");
            atomic.Should().Contain(x => x.Name == "LINT");
            atomic.Should().Contain(x => x.Name == "REAL");
        }
        
        [Test]
        public void Family_ValidType_ShouldReturnExpected()
        {
            var type = Predefined.Null;

            type.Family.Should().Be(DataTypeFamily.None);
        }
        
        [Test]
        public void Class_ValidType_ShouldReturnExpected()
        {
            var type = Predefined.Null;

            type.Class.Should().Be(DataTypeClass.Predefined);
        }
        
        [Test]
        public void DefaultValue_ValidType_ShouldReturnExpected()
        {
            var type = Predefined.Null;

            type.DefaultValue.Should().Be(null);
        }
        
        [Test]
        public void DefaultRadix_ValidType_ShouldReturnExpected()
        {
            var type = Predefined.Null;

            type.DefaultRadix.Should().Be(Radix.Null);
        }
        
        [Test]
        public void DataFormat_ValidType_ShouldReturnExpected()
        {
            var type = Predefined.Null;

            type.DataFormat.Should().Be(TagDataFormat.Decorated);
        }

        [Test]
        public void ContainsType_TypeThatExistsAsPredefined_ShouldBeTrue()
        {
            Predefined.ContainsType("BOOL").Should().BeTrue();
        }

        [Test]
        public void ContainsType_TypeThatDoesNotExistAsPredefined_ShouldBeFalse()
        {
            Predefined.ContainsType("TEMP").Should().BeFalse();
        }

        [Test]
        public void SupportsRadix_NonAtomicValidRadixForType_ShouldBeTrue()
        {
            var type = Predefined.Null;

            var result = type.SupportsRadix(Radix.Null);

            result.Should().BeTrue();
        }
        
        [Test]
        public void SupportsRadix_NonAtomicInvalidRadixForType_ShouldBeFalse()
        {
            var type = Predefined.Null;

            var result = type.SupportsRadix(Radix.Decimal);

            result.Should().BeFalse();
        }

        [Test]
        public void SupportsRadix_AtomicValidRadixForType_ShouldBeTrue()
        {
            var type = Predefined.Dint;

            var result = type.SupportsRadix(Radix.Decimal);

            result.Should().BeTrue();
        }
        
        [Test]
        public void SupportsRadix_AtomicInvalidRadixForType_ShouldBeFalse()
        {
            var type = Predefined.Dint;

            var result = type.SupportsRadix(Radix.Float);

            result.Should().BeFalse();
        }
        
        [Test]
        public void IsValidValue_ValidType_ShouldBeExpected()
        {
            var type = Predefined.Null;

            var result = type.IsValidValue(null);

            result.Should().BeTrue();
        }
        
        [Test]
        public void ParseValue_NullType_ShouldBeNull()
        {
            var type = Predefined.Null;

            var result = type.ParseValue("true");

            result.Should().Be(null);
        }
        
        [Test]
        public void IsValidValue_Null_ShouldBeNull()
        {
            var type = Predefined.Null;

            var result = type.ParseValue("true");

            result.Should().Be(null);
        }
        
        [Test]
        public void Predefined_WhenCastedToDataType_ShouldThrowInvalidCastException()
        {
            var atomic = (IDataType) Predefined.Bool;

            FluentActions.Invoking(() => (DataType)atomic).Should().Throw<InvalidCastException>();
        }

        [Test]
        public void Parse_ValidValueSint_ShouldReturnExpected()
        {
            var fixture = new Fixture();
            var value = fixture.Create<byte>();
            var type = Predefined.Sint;

            var result = type.ParseValue(value.ToString());

            result.Should().Be(value);
        }

        [Test]
        public void Parse_ValidValueInt_ShouldReturnExpected()
        {
            var fixture = new Fixture();
            var value = fixture.Create<short>();
            var type = Predefined.Sint;

            var result = type.ParseValue(value.ToString());

            result.Should().Be(value);
        }

        [Test]
        public void Parse_ValidValueDint_ShouldReturnExpected()
        {
            var fixture = new Fixture();
            var value = fixture.Create<int>();
            var type = Predefined.Sint;

            var result = type.ParseValue(value.ToString());

            result.Should().Be(value);
        }

        [Test]
        public void Parse_ValidValueLint_ShouldReturnExpected()
        {
            var fixture = new Fixture();
            var value = fixture.Create<long>();
            var type = Predefined.Lint;

            var result = type.ParseValue(value.ToString());

            result.Should().Be(value);
        }

        [Test]
        public void Parse_ValidValueReal_ShouldReturnExpected()
        {
            var fixture = new Fixture();
            var value = fixture.Create<float>();
            var type = Predefined.Real;

            var result = type.ParseValue(value.ToString(CultureInfo.InvariantCulture));

            result.Should().Be(value);
        }


        [Test]
        public void New_Bool_ShouldThrowPredefinedCollisionException()
        {
            FluentActions.Invoking(() => new DataType("BOOL")).Should().Throw<PredefinedCollisionException>();
        }

        [Test]
        public void New_Sint_ShouldNotBeNull()
        {
            var type = Predefined.Sint;

            type.Should().NotBeNull();
            type.Name.Should().Be("SINT");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }

        [Test]
        public void ParseType_Sint_ShouldReturnExpectedType()
        {
            var type = Predefined.ParseType("SINT");

            type.Should().NotBeNull();
            type.Name.Should().Be("SINT");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }

        [Test]
        public void New_Sint_ShouldThrowDataTypeAlreadyExistsException()
        {
            FluentActions.Invoking(() => new DataType("SINT")).Should().Throw<PredefinedCollisionException>();
        }

        [Test]
        public void New_Int_ShouldNotBeNull()
        {
            var type = Predefined.Int;

            type.Should().NotBeNull();
            type.Name.Should().Be("INT");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }

        [Test]
        public void New_Dint_ShouldNotBeNull()
        {
            var type = Predefined.Dint;

            type.Should().NotBeNull();
            type.Name.Should().Be("DINT");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }

        [Test]
        public void New_Real_ShouldNotBeNull()
        {
            var type = Predefined.Real;

            type.Should().NotBeNull();
            type.Name.Should().Be("REAL");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }

        [Test]
        public void New_String_ShouldNotBeNull()
        {
            var type = Predefined.String;

            type.Should().NotBeNull();
            type.Name.Should().Be("STRING");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
            type.Members.Should().HaveCount(2);
        }

        [Test]
        public void New_Timer_ShouldNotBeNull()
        {
            var type = Predefined.Timer;

            type.Should().NotBeNull();
            type.Name.Should().Be("TIMER");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
            type.Members.Should().NotBeEmpty();
        }

        [Test]
        public void New_Alarm_ShouldHaveExpectedProperties()
        {
            var type = Predefined.Alarm;

            type.Should().NotBeNull();

            type.Name.Should().Be("ALARM");
            type.Members.Should().HaveCount(24);
        }

        

        [Test]
        public void DefaultValue_Sint_ShouldBeZero()
        {
            var type = Predefined.Sint;

            var value = type.DefaultValue;

            value.Should().Be(0);
        }

        [Test]
        public void DefaultValue_Int_ShouldBeZero()
        {
            var type = Predefined.Int;

            var value = type.DefaultValue;

            value.Should().Be(0);
        }

        [Test]
        public void DefaultValue_Dint_ShouldBeZero()
        {
            var type = Predefined.Dint;

            var value = type.DefaultValue;

            value.Should().Be(0);
        }

        [Test]
        public void DefaultValue_Lint_ShouldBeZero()
        {
            var type = Predefined.Lint;

            var value = type.DefaultValue;

            value.Should().Be(0);
        }

        [Test]
        public void DefaultValue_Real_ShouldBeZeroFloat()
        {
            var type = Predefined.Real;

            var value = type.DefaultValue;

            value.Should().Be(0f);
        }

        [Test]
        public void DefaultRadix_Real_ShouldBeDecimal()
        {
            var type = Predefined.Real;

            var value = type.DefaultRadix;

            value.Should().Be(Radix.Float);
        }

        [Test]
        public void SupportsRadix_BoolInvalidRadix_ShouldBeFalse()
        {
            var type = Predefined.Bool;

            var result = type.SupportsRadix(Radix.Exponential);

            result.Should().BeFalse();
        }

        [Test]
        public void SupportsRadix_BoolValidRadix_ShouldBeTrue()
        {
            var type = Predefined.Bool;

            var result = type.SupportsRadix(Radix.Binary);

            result.Should().BeTrue();
        }

        [Test]
        public void SupportsRadix_CounterValidRadix_ShouldBeTrue()
        {
            var type = Predefined.Counter;

            var result = type.SupportsRadix(Radix.Null);

            result.Should().BeTrue();
        }
    }
}