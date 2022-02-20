using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using L5Sharp.Exceptions;
using NUnit.Framework;

// ReSharper disable CollectionNeverQueried.Local

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class ComponentCollectionTests
    {
        private List<MyComponent> _components;

        [SetUp]
        public void Setup()
        {
            _components = new List<MyComponent>
            {
                new("Test1", "This is test component #1"),
                new("Test2", "This is test component #2"),
                new("Test3", "This is test component #3"),
            };
        }

        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            // ReSharper disable once CollectionNeverUpdated.Local
            var collection = new ComponentCollection<MyComponent>();

            collection.Should().NotBeNull();
        }

        [Test]
        public void New_Default_ShouldBeEmpty()
        {
            // ReSharper disable once CollectionNeverUpdated.Local
            var collection = new ComponentCollection<MyComponent>();

            collection.Should().BeEmpty();
        }

        [Test]
        public void New_NullComponents_ShouldBeValidEmptyCollection()
        {
            FluentActions.Invoking(() => new ComponentCollection<MyComponent>(null!)).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void New_Components_ShouldHaveCount()
        {
            var collection = new ComponentCollection<MyComponent>(_components);

            collection.Should().HaveCount(_components.Count);
        }

        [Test]
        public void Count_NonEmpty_ShouldBePositive()
        {
            var collection = new ComponentCollection<MyComponent>(_components);

            collection.Count.Should().BePositive();
        }

        [Test]
        public void Add_Null_ShouldThrowArgumentNullException()
        {
            var collection = new ComponentCollection<MyComponent>(_components);

            FluentActions.Invoking(() => collection.Add(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Add_DuplicateName_ShouldThrowComponentNameCollisionException()
        {
            var collection = new ComponentCollection<MyComponent>(_components);

            FluentActions.Invoking(() => collection.Add(new MyComponent("Test1"))).Should()
                .Throw<ComponentNameCollisionException>();
        }

        [Test]
        public void Add_ValidComponent_ShouldHaveExpectedCount()
        {
            var collection = new ComponentCollection<MyComponent>(_components);

            collection.Add(new MyComponent("Test"));

            collection.Should().HaveCount(_components.Count + 1);
        }

        [Test]
        public void Clear_WhenCalled_ShouldHaveNotComponents()
        {
            var collection = new ComponentCollection<MyComponent>(_components);

            collection.Clear();

            collection.Should().HaveCount(0);
        }

        [Test]
        public void Contains_Null_ShouldThrowArgumentNullException()
        {
            var collection = new ComponentCollection<MyComponent>(_components);

            FluentActions.Invoking(() => collection.Contains(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Contains_NonExisting_ShouldBeFalse()
        {
            var collection = new ComponentCollection<MyComponent>(_components);

            var result = collection.Contains(new MyComponent("Test"));

            result.Should().BeFalse();
        }

        [Test]
        public void Contains_Existing_ShouldBeTrue()
        {
            var collection = new ComponentCollection<MyComponent>(_components);

            var result = collection.Contains("Test1");

            result.Should().BeTrue();
        }

        [Test]
        public void Find_Null_ShouldThrowArgumentNullException()
        {
            var collection = new ComponentCollection<MyComponent>(_components);

            FluentActions.Invoking(() => collection.Find(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Find_NonExisting_ShouldBeNull()
        {
            var collection = new ComponentCollection<MyComponent>(_components);

            var component = collection.Find(c => c.Description == "This is test component");

            component.Should().BeNull();
        }

        [Test]
        public void Find_Existing_ShouldBeExpected()
        {
            var collection = new ComponentCollection<MyComponent>(_components);

            var component = collection.Find(c => c.Description == "This is test component #2");

            component.Should().NotBeNull();
            component?.Name.Should().Be("Test2");
        }

        [Test]
        public void FindAll_Null_ShouldThrowArgumentNullException()
        {
            var collection = new ComponentCollection<MyComponent>(_components);

            FluentActions.Invoking(() => collection.FindAll(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void FindAll_NonExisting_ShouldBeEmpty()
        {
            var collection = new ComponentCollection<MyComponent>(_components);

            var components = collection.FindAll(c => c.Description == "This is test component");

            components.Should().BeEmpty();
        }

        [Test]
        public void FindAll_Existing_ShouldHaveCount()
        {
            var collection = new ComponentCollection<MyComponent>(_components);

            var components = collection.FindAll(c => c.Description.Contains("This is test component")).ToList();

            components.Should().HaveCount(3);
        }

        [Test]
        public void Get_Null_ShouldThrowArgumentNullException()
        {
            var collection = new ComponentCollection<MyComponent>(_components);

            FluentActions.Invoking(() => collection.Get(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Get_NonExisting_ShouldBeNull()
        {
            var collection = new ComponentCollection<MyComponent>(_components);

            var component = collection.Get("Test");

            component.Should().BeNull();
        }

        [Test]
        public void Get_Existing_ShouldBeExpected()
        {
            var collection = new ComponentCollection<MyComponent>(_components);

            var component = collection.Get("Test2");

            component.Should().NotBeNull();
            component?.Name.Should().Be("Test2");
        }

        [Test]
        public void Remove_NullName_ShouldThrowArgumentNullException()
        {
            var collection = new ComponentCollection<MyComponent>(_components);

            FluentActions.Invoking(() => collection.Remove(null!))
                .Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Remove_NonExistingName_ShouldBeFalse()
        {
            var collection = new ComponentCollection<MyComponent>(_components);

            var removed = collection.Remove("Test");

            removed.Should().BeFalse();
        }

        [Test]
        public void Remove_ExistingName_ShouldBeTrue()
        {
            var collection = new ComponentCollection<MyComponent>(_components);

            var removed = collection.Remove("Test2");

            removed.Should().BeTrue();
        }

        [Test]
        public void Update_NullComponent_ShouldThrowArgumentNullException()
        {
            var collection = new ComponentCollection<MyComponent>(_components);

            FluentActions.Invoking(() => collection.Update(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Update_NonExistingComponent_ShouldHaveExpectedCount()
        {
            var collection = new ComponentCollection<MyComponent>(_components);

            collection.Update(new MyComponent("Test"));

            collection.Should().HaveCount(_components.Count + 1);
        }

        [Test]
        public void Update_ExistingComponent_ShouldHaveExpectedCount()
        {
            var collection = new ComponentCollection<MyComponent>(_components);

            collection.Update(new MyComponent("Test1"));

            collection.Should().HaveCount(_components.Count);
        }

        [Test]
        public void Update_ExistingComponent_ShouldHaveUpdatedDescription()
        {
            var collection = new ComponentCollection<MyComponent>(_components);

            collection.Update(new MyComponent("Test3", "this is an updated description"));

            var component = collection.Single(c => c.Name == "Test3");
            component.Description.Should().Be("this is an updated description");
        }

        [Test]
        public void UpdateMany_NullComponents_ShouldThrowArgumentNullException()
        {
            var collection = new ComponentCollection<MyComponent>(_components);

            FluentActions.Invoking(() => collection.UpdateMany(null!)).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void UpdateMany_ValidComponents_ShouldHaveExpectedCount()
        {
            var collection = new ComponentCollection<MyComponent>(_components);

            var components = new List<MyComponent>
            {
                new("Test2", "This one already exists"),
                new("Test4", "This is a new test component"),
                new("Test5", "This is a new test component"),
            };

            collection.UpdateMany(components);

            collection.Should().HaveCount(5);
        }

        [Test]
        public void GetEnumerator_WhenCalled_ShouldNotBeNull()
        {
            var collection = new ComponentCollection<MyComponent>(_components);

            using var enumerator = collection.GetEnumerator();

            enumerator.Should().NotBeNull();
        }

        [Test]
        public void GetEnumerator_AsEnumerable_ShouldNotBeNull()
        {
            var collection = new ComponentCollection<MyComponent>(_components);

            var enumerator = ((IEnumerable)collection).GetEnumerator();

            enumerator.Should().NotBeNull();
        }
    }

    public class MyComponent : ILogixComponent
    {
        public MyComponent(string name, string description = null)
        {
            Name = name;
            Description = description ?? string.Empty;
        }

        public string Name { get; set; }
        public string Description { get; }
    }
}