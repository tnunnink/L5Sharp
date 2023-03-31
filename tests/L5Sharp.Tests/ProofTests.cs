using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Extensions;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Tests;

[TestFixture]
public class ProofTests
{
    [Test]
    public void ValidateSomeTagValue()
    {
        var content = LogixContent.Load(Known.Test);

        var tags = content.Tags().Where(t => t.DataType == "DINT" && t.Data is AtomicType);

        foreach (var tag in tags)
        {
            tag.Data.To<DINT>().Should().BeGreaterOrEqualTo(0);
        }
    }

    [Test]
    public void UnusedTags()
    {
        var content = LogixContent.Load(Known.Test);

        var referencedTags = content.Logic().SelectMany(t => t.Tags());

        var unused = content.Tags().Select(t => t.TagName).Where(t => referencedTags.All(r => r != t)).ToList();

        foreach (var tagName in unused)
        {
            Console.WriteLine(tagName);
        }
    }

    [Test]
    public void ModuleTagComments()
    {
        var content = LogixContent.Load(@"C:\Users\tnunnink\Local\Transfer\Site.L5X");

        var config = content.Modules().Find("R0S4").Config;

        var comment = string.Empty;

        config?.Comments.TryGetValue(".CH0CountLimit", out comment);

        comment.Should().NotBeEmpty();

        var member = config?.Member("Ch0CountLimit");

        member?.As<TagMember>().Comment.Should().NotBeEmpty();
    }

    [Test]
    public void TagLookup()
    {
        var content = LogixContent.Load(@"C:\Users\tnunnink\Local\Tests\L5X\Template.L5X");

         var tagLookup = content.Query<Tag>().SelectMany(t => t.MembersAndSelf()).ToLookup(k => k.TagName, t => t);

         var target = tagLookup.FirstOrDefault(t => t.Key == "Spare_DI_Channel_5094.ChData");
         target.Should().NotBeEmpty();

         var result = tagLookup.Contains("Spare_DI_Channel_5094.ChData");
         result.Should().BeTrue();
    }
    
    [Test]
    public void GetLogixDigitalInputAoi()
    {
        var content = LogixContent.Load(@"C:\Users\tnunnink\Local\Tests\L5X\Template.L5X");

        var instruction = content.Instructions().Find("aoi5094IB16");

        var logix = instruction.Logic(
            "aoi5094IB16(PLCModule1AOI,IO_PLC_FLEX:1:I,IO_Faults.PLC_S01.Faulted,di_01SS_901B,Spare_DI_Channel_5094,di_KS44_1128,di_CR44_1125,di_CR01_1193,Spare_DI_Channel_5094,di_CR01_1925,di_CR44_1128,Spare_DI_Channel_5094,di_AC01_1211,Spare_DI_Channel_5094,di_AC24_1211,di_CR01_1210,di_CR44_1130_Inactive,di_CR44_1130_Active,Spare_DI_Channel_5094);");

        logix.Should().NotBeEmpty();
    }
    
    [Test]
    public void GetLogixMmInstruction()
    {
        var content = LogixContent.Load(@"C:\Users\tnunnink\Local\Tests\L5X\Template.L5X");

        var instruction = content.Instructions().Find("aoiMicroMotion5700");

        var logix = instruction.Logic(
            "aoiMicroMotion5700(aoiMM_INJ01,MM_INJ01:I1,eai_13FT_101,0.0,21.5,eai_13DT_101,0.0,1.0,eai_13TT_102,-20.0,120.0,edi_13MM_101_ChFault);");

        logix.Should().NotBeEmpty();
    }
}