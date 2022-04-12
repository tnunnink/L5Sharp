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
            

            var target = context.Modules().Get("Local");

            var modules = context.Modules(q => q.WithParent("SomeParent"));

            var dataTypes = context.DataTypes(q => q.DependingOn("This  "));

            Assert.Pass();
        }
    }
}