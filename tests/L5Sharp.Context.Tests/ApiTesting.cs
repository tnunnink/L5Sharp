
using L5Sharp.L5X;
using NUnit.Framework;

namespace L5Sharp.Context.Tests
{
    [TestFixture]
    public class ApiTesting
    {
        [Test]
        public void ContextApiTesting()
        {
            var context = L5XContext.Load(Known.Template);

            var modules = context.Modules().Select();
            
            Assert.Pass();
        }
    }
}