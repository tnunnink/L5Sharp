﻿using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Extensions.Tests
{
    [TestFixture]
    public class ElementExtensionsTests
    {
        private readonly string _fileName = Path.Combine(Environment.CurrentDirectory, @"TestFiles\Test.xml");

        [Test]
        public void TestFileExists()
        {
            FileAssert.Exists(_fileName);
        }

        [Test]
        public void Contains_DataTypeKnownType_ShouldNotBeNull()
        {
        }

        [Test]
        public void GetValue_WhenCalled_ShouldReturnExpectedValue()
        {
            var element = XElement.Load(_fileName);
            var target = element.Descendants("DataType").FirstOrDefault();

            var value = target.GetAttribute<IDataType>(t => t.Class);

            value.Should().NotBeNull();
        }

        [Test]
        public void ToAttribute_ValidComponent_ShouldHaveExpectedNameAndValue()
        {
            var component = new DataType("Test", members: new[] { Member.Create("Test", new Dint()) });

            var attribute = component.ToAttribute(c => c.Name);

            attribute.Should().NotBeNull();
            attribute.Name.ToString().Should().Be("Name");
            attribute.Should().HaveValue("Test");
        }
    }
}