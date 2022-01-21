using System;
using System.IO;
using NUnit.Framework;

namespace L5Sharp.Serialization.Tests
{
    [TestFixture]
    public class ModuleSerializerTests
    {
        private static readonly string L5X = Path.Combine(Environment.CurrentDirectory, @"TestFiles\Test.xml");
        private ModuleSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            var context = new LogixContext(L5X);
            _serializer = new ModuleSerializer(context);
        }
    }
}