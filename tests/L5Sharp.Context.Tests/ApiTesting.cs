using FluentAssertions;
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

            var changed = context.IsChanged;
        }
        
        [Test]
        public void RungQuerier()
        {
            var context = L5XContext.Load(Known.Template);

            var rungs = context.Rungs()
                .InRoutine("Test")
                .WithTag("TagToSearch")
                .InRange(1, 3)
                .IncludeAddOns()
                .All();

            rungs.Should().NotBeEmpty();
        }
    }
}