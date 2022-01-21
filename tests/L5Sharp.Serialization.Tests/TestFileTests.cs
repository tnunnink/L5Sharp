using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Serialization.Tests
{
    [TestFixture]
    public class TestFileTests
    {
        public static readonly string L5X = Path.Combine(Environment.CurrentDirectory, @"TestFiles\Test.xml");
        
        [Test]
        public void KnownTestFileShouldExists()
        {
            FileAssert.Exists(L5X);
        }

        [Test]
        public void Load_ValidFile_ShouldNotBeNull()
        {
            var context = new LogixContext(L5X);

            context.Should().NotBeNull();
        }
    }
}