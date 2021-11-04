using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Tests
{
    [TestFixture]
    public class LogixTests
    {
        [Test]
        public void Types_WhenCalled_ShouldNotBeEmpty()
        {
            var dataTypes = Logix.DataType.All;

            dataTypes.Should().NotBeEmpty();
        }

        [Test]
        public void Atomics_WhenCalled_ShouldNotBeEmpty()
        {
            var atomic = Logix.DataType.Atomics.ToList();

            atomic.Should().NotBeEmpty();
            atomic.Should().Contain(x => x.Name == "BOOL");
            atomic.Should().Contain(x => x.Name == "SINT");
            atomic.Should().Contain(x => x.Name == "INT");
            atomic.Should().Contain(x => x.Name == "DINT");
            atomic.Should().Contain(x => x.Name == "LINT");
            atomic.Should().Contain(x => x.Name == "REAL");
        }
        
        [Test]
        public void ContainsType_TypeThatExistsAsPredefined_ShouldBeTrue()
        {
            Logix.DataType.Contains("BOOL").Should().BeTrue();
        }

        [Test]
        public void ContainsType_TypeThatDoesNotExistAsPredefined_ShouldBeFalse()
        {
            Logix.DataType.Contains("TEMP").Should().BeFalse();
        }
        
        [Test]
        public void ParseType_RegisteredType_ShouldNotBeNull()
        {
            var type = Logix.DataType.Parse("Bool");
            type.Should().NotBeNull();
            type.Should().Be(Logix.DataType.Bool);
        }

        [Test]
        public void ParseType_StaticField_ShouldNotBeNull()
        {
            var type = Logix.DataType.Parse("bit");
            type.Should().NotBeNull();
            type.Should().Be(Logix.DataType.Bit);
            type.Name.Should().Be("BOOL");
        }

        [Test]
        public void ParseType_AssemblyValidType_ShouldNotBeExpected()
        {
            var type = Logix.DataType.Parse("MyPredefined");
            type.Should().NotBeNull();
            type.Name.Should().Be("MyPredefined");
            type.Family.Should().Be(DataTypeFamily.None);
        }
        
        [Test]
        public void ParseType_AssemblyInvalidType_ShouldNotBeUndefined()
        {
            var type = Logix.DataType.Parse("MyNullNamePredefined");
            type.Should().NotBeNull();
            type.Should().Be(Logix.DataType.Undefined);
        }
        
        [Test]
        public void ParseType_NonExistingType_ShouldNotBeUndefined()
        {
            var type = Logix.DataType.Parse("Invalid");
            type.Should().NotBeNull();
            type.Should().Be(Logix.DataType.Undefined);
        }
    }

    public class MyAtomic : Atomic
    {
        public override bool IsValidValue(object value)
        {
            throw new System.NotImplementedException();
        }

        public override object ParseValue(string value)
        {
            throw new System.NotImplementedException();
        }

        internal MyAtomic(string name, IEnumerable<Member> members = null) : base(name, members)
        {
        }
    }
}