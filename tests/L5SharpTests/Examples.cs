using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5SharpTests
{
    [TestFixture]
    public class Examples
    {
        [Test]
        public void Test()
        {
            var tag = Tag.Build<Sint>("NewTag")
                .WithDimensions(10)
                .WithRadix(Radix.Hex)
                .WithAccess(ExternalAccess.ReadOnly)
                .WithDescription("This is a test tag")
                .Create();
        }
    }
}