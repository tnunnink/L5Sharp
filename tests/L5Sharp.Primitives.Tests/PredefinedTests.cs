using System;
using System.Globalization;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Exceptions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Primitives.Tests
{
    [TestFixture]
    public class PredefinedTests
    {
        [Test]
        public void List_WhenCalled_ShouldNotBeEmpty()
        {
            var predefined = Predefined.List;

            predefined.Should().NotBeEmpty();
        }
        
        [Test]
        public void GetAtomic_WhenCalled_ShouldNotBeEmpty()
        {
            var atomic = Predefined.List.Where(t => t.IsAtomic);

            atomic.Should().NotBeEmpty();
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
        public void Parse_ValidValueBool_ShouldReturnExpected()
        {
            var fixture = new Fixture();
            var value = fixture.Create<bool>();
            var type = Predefined.Bool;

            var result = type.ParseValue(value.ToString());

            result.Should().Be(value);
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
        public void Bool_ShouldNotBeNull()
        {
            var type = DataType.Bool;

            type.Should().NotBeNull();
            type.Name.Should().Be("BOOL");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }
        
        /*[Test]
        public void New_Bool_ShouldNotBeNull()
        {
            var type = new Bool();

            type.Should().NotBeNull();
            type.Name.Should().Be("BOOL");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }*/

        [Test]
        public void BoolAtomicShouldNotBeCastToDataType()
        {
            var atomic = (IDataType) Predefined.Sint;

            FluentActions.Invoking(() => (DataType)atomic).Should().Throw<InvalidCastException>();
        }

        [Test]
        public void FromName_Bool_ShouldReturnExpectedType()
        {
            var type = Predefined.FromName("BOOL");

            type.Should().NotBeNull();
            type.Name.Should().Be("BOOL");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }
        
        [Test]
        public void FromName_BoolLower_ShouldReturnExpectedType()
        {
            var type = Predefined.FromName("bool", true);

            type.Should().NotBeNull();
            type.Name.Should().Be("BOOL");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }

        [Test]
        public void New_Bool_ShouldThrowDataTypeAlreadyExistsException()
        {
            FluentActions.Invoking(() => new DataType("BOOL")).Should().Throw<PredefinedCollisionException>();
        }
        
        [Test]
        public void New_Sint_ShouldNotBeNull()
        {
            var type = DataType.Sint;

            type.Should().NotBeNull();
            type.Name.Should().Be("SINT");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }
        
        [Test]
        public void FromName_Sint_ShouldReturnExpectedType()
        {
            var type = Predefined.FromName("SINT");

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
            var type = DataType.Int;

            type.Should().NotBeNull();
            type.Name.Should().Be("INT");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }
        
        [Test]
        public void New_Dint_ShouldNotBeNull()
        {
            var type = DataType.Dint;

            type.Should().NotBeNull();
            type.Name.Should().Be("DINT");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }
        
        [Test]
        public void New_Real_ShouldNotBeNull()
        {
            var type = DataType.Real;

            type.Should().NotBeNull();
            type.Name.Should().Be("REAL");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }

        [Test]
        public void New_String_ShouldNotBeNull()
        {
            var type = DataType.String;

            type.Should().NotBeNull();
            type.Name.Should().Be("STRING");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
            type.Members.Should().HaveCount(2);
        }
        
        [Test]
        public void New_Timer_ShouldNotBeNull()
        {
            var type = DataType.Timer;

            type.Should().NotBeNull();
            type.Name.Should().Be("TIMER");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }
    }
}