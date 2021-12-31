using System.Linq;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Types;
using L5Sharp.Types.Predefined;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class TagInstanceTests
    {
        [Test]
        public void String_ValidParameters_ShouldHaveValidMembers()
        {
            var tag = Tag.Create<String>("Test");

            tag.Should().NotBeNull();
            tag.GetMember(m => m.LEN).Should().NotBeNull();
            tag.GetMember(m => m.DATA).Should().NotBeNull();
        }
        
        [Test]
        public void Timer_ValidParameters_ShouldHaveValidMembers()
        {
            var tag = Tag.Create<Timer>("Test");

            tag.Should().NotBeNull();
            tag.GetMember(m => m.PRE).Should().NotBeNull();
            tag.GetMember(m => m.ACC).Should().NotBeNull();
            tag.GetMember(m => m.TT).Should().NotBeNull();
            tag.GetMember(m => m.EN).Should().NotBeNull();
            tag.GetMember(m => m.DN).Should().NotBeNull();
        }
        
        [Test]
        public void Counter_ValidParameters_ShouldHaveValidMembers()
        {
            var tag = Tag.Create<Counter>("Test");

            tag.Should().NotBeNull();
            tag.GetMember(m => m.PRE).Should().NotBeNull();
            tag.GetMember(m => m.ACC).Should().NotBeNull();
            tag.GetMember(m => m.CU).Should().NotBeNull();
            tag.GetMember(m => m.CD).Should().NotBeNull();
            tag.GetMember(m => m.DN).Should().NotBeNull();
            tag.GetMember(m => m.OV).Should().NotBeNull();
            tag.GetMember(m => m.UN).Should().NotBeNull();
        }
        
        [Test]
        public void New_Alarm_ShouldHaveValidMembers()
        {
            var tag = Tag.Create("Test", new Alarm());

            tag.Should().NotBeNull();
            tag.GetMember(m => m.EnableIn).Should().NotBeNull();
            tag.GetMember(m => m.In).Should().NotBeNull();
            tag.GetMember(m => m.HHLimit).Should().NotBeNull();
            tag.GetMember(m => m.HLimit).Should().NotBeNull();
            tag.GetMember(m => m.LLimit).Should().NotBeNull();
            tag.GetMember(m => m.LLLimit).Should().NotBeNull();
            tag.GetMember(m => m.Deadband).Should().NotBeNull();
            tag.GetMember(m => m.ROCPosLimit).Should().NotBeNull();
            tag.GetMember(m => m.ROCNegLimit).Should().NotBeNull();
            tag.GetMember(m => m.ROCPeriod).Should().NotBeNull();
            tag.GetMember(m => m.EnableOut).Should().NotBeNull();
            tag.GetMember(m => m.HHAlarm).Should().NotBeNull();
            tag.GetMember(m => m.HAlarm).Should().NotBeNull();
            tag.GetMember(m => m.LAlarm).Should().NotBeNull();
            tag.GetMember(m => m.LLAlarm).Should().NotBeNull();
            tag.GetMember(m => m.ROCPosAlarm).Should().NotBeNull();
            tag.GetMember(m => m.ROCNegAlarm).Should().NotBeNull();
            tag.GetMember(m => m.ROC).Should().NotBeNull();
            tag.GetMember(m => m.Status).Should().NotBeNull();
            tag.GetMember(m => m.InstructFault).Should().NotBeNull();
            tag.GetMember(m => m.DeadbandInv).Should().NotBeNull();
            tag.GetMember(m => m.ROCPosLimitInv).Should().NotBeNull();
            tag.GetMember(m => m.ROCNegLimitInv).Should().NotBeNull();
            tag.GetMember(m => m.ROCPeriodInv).Should().NotBeNull();
        }
    }
}