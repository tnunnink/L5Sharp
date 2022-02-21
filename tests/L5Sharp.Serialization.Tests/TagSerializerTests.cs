using L5Sharp.Serialization.Components;
using NUnit.Framework;

namespace L5Sharp.Serialization.Tests
{
    [TestFixture]
    public class TagSerializerTests
    {
        private TagSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new TagSerializer();
        }
    }
}