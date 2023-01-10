using System;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class LadderLogicTests
    {
        private List<Rung> _rungs;

        [SetUp]
        public void Setup()
        {
            _rungs = new List<Rung>
            {
                new(new NeutralText("NOP();"), "Test Rung #1"),
                new(new NeutralText("XIC(Bit1)OTU(Bit2);"), "Test Rung #2"),
                new(new NeutralText("MOV(Something, SomethingElse);"), "Test Rung #3")
            };
        }

        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            // ReSharper disable once CollectionNeverUpdated.Local
            var content = new RLL();

            content.Should().NotBeNull();
        }

        [Test]
        public void New_CollectionOverload_ShouldHaveExpectedCount()
        {
            var content = new RLL(_rungs);

            content.Should().HaveCount(3);
        }

        [Test]
        public void New_CollectionOverload_NumbersShouldMatchIndex()
        {
            var content = new RLL(_rungs);

            content[0].Number.Should().Be(0);
            content[1].Number.Should().Be(1);
            content[2].Number.Should().Be(2);
        }

        [Test]
        public void HasContent_WithRungs_ShouldBeTrue()
        {
            var content = new RLL(_rungs);

            content.HasContent.Should().BeTrue();
        }

        [Test]
        public void HasContent_Empty_ShouldBeFalse()
        {
            var content = new RLL();

            content.HasContent.Should().BeFalse();
        }

        [Test]
        public void Count_WithRungs_ShouldBeExpected()
        {
            var content = new RLL(_rungs);

            content.Count.Should().Be(3);
        }

        [Test]
        public void Count_Empty_ShouldBeZero()
        {
            // ReSharper disable once CollectionNeverUpdated.Local
            var content = new RLL();

            content.Count.Should().Be(0);
        }

        [Test]
        public void Clear_WhenCalled_ShouldHaveNotCount()
        {
            var content = new RLL(_rungs);

            content.Clear();

            content.Should().HaveCount(0);
        }

        [Test]
        public void ContainsText_Null_ShouldBeFalse()
        {
            var content = new RLL(_rungs);

            var result = content.ContainsText(null!);

            result.Should().BeFalse();
        }

        [Test]
        public void ContainsText_NonExisting_ShouldBeFalse()
        {
            var content = new RLL(_rungs);

            var result = content.ContainsText("TON(Timer, 1000);");

            result.Should().BeFalse();
        }

        [Test]
        public void ContainsText_Existing_ShouldBeTrue()
        {
            var content = new RLL(_rungs);

            var result = content.ContainsText("NOP();");

            result.Should().BeTrue();
        }

        [Test]
        public void IndexerGet_InValidIndex_ShouldNotBeNull()
        {
            var content = new RLL(_rungs);

            FluentActions.Invoking(() => content[4]).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void IndexerGet_ValidIndex_ShouldNotBeNull()
        {
            var content = new RLL(_rungs);

            var rung = content[1];

            rung.Should().NotBeNull();
        }

        [Test]
        public void IndexerSet_ValidIndex_ShouldUpdateExpected()
        {
            var content = new RLL(_rungs);

            content[1] = new Rung("TON(Timer, 1000);", "This is an updated rung");

            content[1].Number.Should().Be(1);
            content[1].Comment.Should().Be("This is an updated rung");
            content[1].Text.Should().Be(new NeutralText("TON(Timer, 1000);"));
        }

        [Test]
        public void Find_Null_ShouldThrowArgumentNullException()
        {
            var content = new RLL(_rungs);

            FluentActions.Invoking(() => content.Find(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Find_IsMatch_ShouldReturnExpectedInstance()
        {
            var content = new RLL(_rungs);

            var rung = content.Find(r => r.Comment == "Test Rung #2");

            rung.Should().NotBeNull();
            rung?.Number.Should().Be(1);
            rung?.Comment.Should().Be("Test Rung #2");
        }

        [Test]
        public void Find_NoMatch_ShouldReturnNull()
        {
            var content = new RLL(_rungs);

            var rung = content.Find(r => r.Text == "TON(Timer, 1000);");

            rung.Should().BeNull();
        }

        [Test]
        public void FindAll_Null_ShouldThrowArgumentNullException()
        {
            var content = new RLL(_rungs);

            FluentActions.Invoking(() => content.FindAll(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void FindAll_IsMatch_ShouldHaveExpectedCount()
        {
            var content = new RLL(_rungs);

            var rungs = content.FindAll(r => r.Type == RungType.Normal);

            rungs.Should().HaveCount(3);
        }

        [Test]
        public void FindAll_NoMatch_ShouldReturnNull()
        {
            var content = new RLL(_rungs);

            var rungs = content.FindAll(r => r.Text == "TON(Timer, 1000);");

            rungs.Should().BeEmpty();
        }

        [Test]
        public void Add_NullRung_ShouldThrowArgumentNullException()
        {
            // ReSharper disable once CollectionNeverQueried.Local
            var content = new RLL(_rungs);

            FluentActions.Invoking(() => content.Add(((Rung)null)!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Add_ValidRung_ShouldHaveExpectedCount()
        {
            var content = new RLL(_rungs);

            content.Add(new Rung("NOP();"));

            content.Should().HaveCount(4);
        }

        [Test]
        public void Add_ValidRung_ShouldHaveExpectedNumber()
        {
            var content = new RLL(_rungs);

            content.Add(new Rung("NOP();"));

            var result = content[3];
            result.Should().NotBeNull();
            result.Number.Should().Be(3);
            result.Type.Should().Be(RungType.Normal);
            result.Comment.Should().BeEmpty();
            result.Text.Should().Be(new NeutralText("NOP();"));
        }

        [Test]
        public void Add_NullText_ShouldThrowArgumentNullException()
        {
            // ReSharper disable once CollectionNeverQueried.Local
            var content = new RLL(_rungs);

            FluentActions.Invoking(() => content.Add(((NeutralText)null)!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Add_EmptyText_ShouldHaveExpectedCount()
        {
            // ReSharper disable once CollectionNeverQueried.Local
            var content = new RLL(_rungs);

            content.Add(string.Empty);

            content.Should().HaveCount(4);
        }

        [Test]
        public void Add_ValidText_ShouldHaveExpectedCount()
        {
            var content = new RLL(_rungs);

            content.Add("NOP();");

            content.Should().HaveCount(4);
        }

        [Test]
        public void Add_ValidText_ShouldHaveExpectedNumber()
        {
            var content = new RLL(_rungs);

            content.Add("NOP();");

            var result = content[3];
            result.Should().NotBeNull();
            result.Number.Should().Be(3);
            result.Type.Should().Be(RungType.Normal);
            result.Comment.Should().BeEmpty();
            result.Text.Should().Be(new NeutralText("NOP();"));
        }

        [Test]
        public void AddRange_ValidCollection_ShouldHaveSpecifiedCount()
        {
            var content = new RLL(_rungs);

            content.AddRange(new List<Rung>()
            {
                new(NeutralText.Empty),
                new(NeutralText.Empty),
                new(NeutralText.Empty)
            });

            content.Should().HaveCount(6);
        }

        [Test]
        public void AddRange_Null_ShouldThrowArgumentNullException()
        {
            var content = new RLL(_rungs);

            FluentActions.Invoking(() => content.AddRange(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Remove_InvalidNumber_ShouldThrowArgumentOutOfRangeException()
        {
            var content = new RLL(_rungs);

            FluentActions.Invoking(() => content.Remove(10)).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Remove_ValidNumber_ShouldHaveExpectedCount()
        {
            var content = new RLL(_rungs);

            content.Remove(1);

            content.Should().HaveCount(2);
        }

        [Test]
        public void Insert_RungOverloadInvalidNumber_ShouldThrowArgumentOutOfRangeException()
        {
            var content = new RLL(_rungs);

            FluentActions.Invoking(() => content.Insert(4, new Rung("NOP();"))).Should()
                .Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Insert_RungOverloadNullRung_ShouldThrowArgumentNullException()
        {
            var content = new RLL(_rungs);

            FluentActions.Invoking(() => content.Insert(1, ((Rung)null)!))
                .Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Insert_RungOverloadValid_IndexShouldHaveExpectedValue()
        {
            var content = new RLL(_rungs);

            content.Insert(1, new Rung("NOP();"));

            var rung = content[1];
            rung.Number.Should().Be(1);
            rung.Comment.Should().BeEmpty();
            rung.Text.Should().Be(new NeutralText("NOP();"));
        }

        [Test]
        public void Insert_TextOverloadInvalidNumber_ShouldThrowArgumentOutOfRangeException()
        {
            var content = new RLL(_rungs);

            FluentActions.Invoking(() => content.Insert(4, "NOP();")).Should()
                .Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Insert_TextOverloadNullRung_ShouldThrowArgumentNullException()
        {
            var content = new RLL(_rungs);

            FluentActions.Invoking(() => content.Insert(1, ((NeutralText)null)!))
                .Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Insert_TextOverloadValid_IndexShouldHaveExpectedValue()
        {
            var content = new RLL(_rungs);

            content.Insert(1, "NOP();");

            var rung = content[1];
            rung.Number.Should().Be(1);
            rung.Comment.Should().BeEmpty();
            rung.Text.Should().Be(new NeutralText("NOP();"));
        }

        [Test]
        public void Update_InvalidNumber_ShouldThrownArgumentOutOfRangeException()
        {
            var content = new RLL(_rungs);

            FluentActions.Invoking(() => content.Update(4, "NOP();")).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Update_Null_ShouldThrowArgumentOutOfRangeException()
        {
            var content = new RLL(_rungs);

            FluentActions.Invoking(() => content.Update(4, null!)).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Update_ValidNumberAndTest_IndexShouldBeExpected()
        {
            var content = new RLL(_rungs);

            content.Update(1, "NOP();");

            var rung = content[1];
            rung.Number.Should().Be(1);
            rung.Comment.Should().Be("Test Rung #2");
            rung.Text.Should().Be(new NeutralText("NOP();"));
        }

        [Test]
        public void Comment_Null_IndexShouldBeEmpty()
        {
            var content = new RLL(_rungs);

            content.Comment(1, null!);

            var rung = content[1];
            rung.Comment.Should().Be(string.Empty);
        }

        [Test]
        public void Comment_InvalidIndex_ShouldThrowArgumentOutOfRangeException()
        {
            var content = new RLL(_rungs);

            FluentActions.Invoking(() => content.Comment(4, "This is a test")).Should()
                .Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Comment_ValidNumber_IndexShouldHaveExpectedComment()
        {
            var content = new RLL(_rungs);

            content.Comment(1, "This is a test");

            var rung = content[1];
            rung.Comment.Should().Be("This is a test");
        }

        [Test]
        public void GetEnumerator_AsEnumerable_ShouldNotBeNull()
        {
            var content = (IEnumerable)new RLL(_rungs);

            var enumerator = content.GetEnumerator();

            enumerator.Should().NotBeNull();
        }
    }
}