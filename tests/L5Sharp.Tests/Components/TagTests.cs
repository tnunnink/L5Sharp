using L5Sharp.Components;
using L5Sharp.Types.Atomics;
using NUnit.Framework;

namespace L5Sharp.Tests.Components
{
    [TestFixture]
    public class TagTests
    {
        [Test]
        public void TagTesting()
        {
            var tag = new Tag();

            var members = tag.Members("MyTag.MemberName");

            var value = tag.Member("MyTag.MemberName.SomeOtherName").GetValue<BOOL>();
            
            
        }
    }
}