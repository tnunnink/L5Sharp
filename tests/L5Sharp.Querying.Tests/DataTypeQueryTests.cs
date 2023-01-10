using System;
using System.Linq;
using FluentAssertions;
using L5Sharp.Enums;
using L5SharpTests;
using NUnit.Framework;

namespace L5Sharp.Querying.Tests
{
    [TestFixture]
    public class DataTypeQueryTests
    {
        [Test]
        public void SomeQuery_EmptyContext_ShouldBeEmptyResult()
        {
            var context = LogixContent.Load(Known.Empty);

            var results = context.DataTypes(q => q.DependingOn("Type"));

            results.Should().BeEmpty();
        }
        
        [Test]
        public void DependingOn_NullName_ShouldThrowArgumentNullException()
        {
            var context = LogixContent.Load(Known.Test);

            FluentActions.Invoking(() => context.DataTypes(q => q.DependingOn(null!))).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void DependingOn_NonExistingName_ShouldBeEmpty()
        {
            var context = LogixContent.Load(Known.Test);

            var results = context.DataTypes(q => q.DependingOn("Fake"));

            results.Should().BeEmpty();
        }

        [Test]
        public void DependingOn_ValidName_ShouldNotBeEmpty()
        {
            var context = LogixContent.Load(Known.Test);

            var results = context.DataTypes(q => q.DependingOn("SimpleType"));

            results.Should().NotBeEmpty();
        }

        [Test]
        public void OfFamily_NullFamily_ShouldThrowArgumentNullException()
        {
            var context = LogixContent.Load(Known.Test);

            FluentActions.Invoking(() => context.DataTypes(q => q.OfFamily(null!))).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void OfFamily_NoFamily_ShouldThrowArgumentNullException()
        {
            var context = LogixContent.Load(Known.Test);

            var results = context.DataTypes(q => q.OfFamily(DataTypeFamily.None));

            results.Should().NotBeEmpty();
        }

        [Test]
        public void OfFamily_StringFamily_ShouldNotBeEmpty()
        {
            var context = LogixContent.Load(Known.Test);

            var results = context.DataTypes(q => q.OfFamily(DataTypeFamily.String));

            results.Should().NotBeEmpty();
        }

        [Test]
        public void UsedBy_NullType_ShouldThrowArgumentNullException()
        {
            var context = LogixContent.Load(Known.Test);

            FluentActions.Invoking(() => context.DataTypes(q => q.UsedBy(null!))).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void UsedBy_NonExistingType_ShouldBeEmpty()
        {
            var context = LogixContent.Load(Known.Test);

            var results = context.DataTypes(q => q.UsedBy("FakeType"));

            results.Should().BeEmpty();
        }

        [Test]
        public void UsedBy_ValidType_ShouldNotBeEmpty()
        {
            var context = LogixContent.Load(Known.Test);

            var results = context.DataTypes(q => q.UsedBy("ComplexType")).ToList();

            results.Should().NotBeEmpty();
            results.Should().Contain(d => d.Name == "SimpleType");
        }
    }
}