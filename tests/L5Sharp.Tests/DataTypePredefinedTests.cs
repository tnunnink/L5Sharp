using FluentAssertions;
using L5Sharp.Enumerations;
using L5Sharp.Exceptions;
using L5Sharp.Primitives;
using NUnit.Framework;

namespace L5Sharp.Tests
{
    [TestFixture]
    public class DataTypePredefinedTests
    {
        [Test]
        public void GetPredefined_WhenCalled_ShouldNotBeEmpty()
        {
            var predefined = DataType.GetPredefined();

            predefined.Should().NotBeEmpty();
        }
        
        [Test]
        public void GetAtomic_WhenCalled_ShouldNotBeEmpty()
        {
            var atomic = DataType.GetAtomic();

            atomic.Should().NotBeEmpty();
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

        [Test]
        public void FromName_Bool_ShouldReturnExpectedType()
        {
            var type = DataType.FromName("BOOL");

            type.Should().NotBeNull();
            type.Name.Should().Be("BOOL");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }

        [Test]
        public void New_Bool_ShouldThrowDataTypeAlreadyExistsException()
        {
            FluentActions.Invoking(() => new DataType("BOOL")).Should().Throw<DataTypeAlreadyExistsException>();
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
            var type = DataType.FromName("SINT");

            type.Should().NotBeNull();
            type.Name.Should().Be("SINT");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }

        [Test]
        public void New_Sint_ShouldThrowDataTypeAlreadyExistsException()
        {
            FluentActions.Invoking(() => new DataType("SINT")).Should().Throw<DataTypeAlreadyExistsException>();
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
        
        /*[Test]
        public void New_Timer_ShouldNotBeNull()
        {
            var type = DataType.Timer;

            type.Should().NotBeNull();
            type.Name.Should().Be("TIMER");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.None);
        }*/
    }
}