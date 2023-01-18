using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Tests.Types.Custom;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;
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

            var value = tag.Member("MyTag.MemberName.SomeOtherName")?.GetValue<BOOL>();
        }

        [Test]
        public void New_Tag_ShouldNotBeNull()
        {
            var tag = new Tag
            {
                Name = "MyTagName",
                DataType = "DINT",
                Dimensions = new Dimensions(14, 14, 14),
                TagType = TagType.Alias,
                Radix = Radix.Float,
                Alias = new TagName("SomeOtherTag")
            };


            tag.Comments.Add("SomeTagMember", "This is the comment for the SomeTagMember");

            tag.Constant = false;

            tag.Description = "this is a test of the tag component";

            var value = tag.GetValue<DINT>();

            tag.SetValue(new INT(4000));

            var isSet = tag.TrySetValue(new INT(6000));
            
            var member = tag.Member("ChildMember.SubMember[0].Bit1");


            var nested = new Tag<MyNestedType>();

            var m1 = nested.Member(m => m.Simple.M1);
            
            m1.SetValue(b => b, new BOOL(true));
            
            nested.SetValue(m => m.Flags[5], new BOOL(false));
        }
    }
}