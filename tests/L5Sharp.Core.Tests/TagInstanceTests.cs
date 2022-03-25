using FluentAssertions;
using L5Sharp.Creators;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class TagInstanceTests
    {
        [Test]
        public void String_ValidParameters_ShouldHaveValidMembers()
        {
            var tag = Tag.Create<STRING>("Test");

            tag.Should().NotBeNull();
            tag.Member(m => m.LEN).Should().NotBeNull();
            tag.Member(m => m.DATA).Should().NotBeNull();
        }
        
        [Test]
        public void Timer_ValidParameters_ShouldHaveValidMembers()
        {
            var tag = Tag.Create<TIMER>("Test");

            tag.Should().NotBeNull();
            tag.Member(m => m.PRE).Should().NotBeNull();
            tag.Member(m => m.ACC).Should().NotBeNull();
            tag.Member(m => m.TT).Should().NotBeNull();
            tag.Member(m => m.EN).Should().NotBeNull();
            tag.Member(m => m.DN).Should().NotBeNull();
        }
        
        [Test]
        public void Counter_ValidParameters_ShouldHaveValidMembers()
        {
            var tag = Tag.Create<COUNTER>("Test");

            tag.Should().NotBeNull();
            tag.Member(m => m.PRE).Should().NotBeNull();
            tag.Member(m => m.ACC).Should().NotBeNull();
            tag.Member(m => m.CU).Should().NotBeNull();
            tag.Member(m => m.CD).Should().NotBeNull();
            tag.Member(m => m.DN).Should().NotBeNull();
            tag.Member(m => m.OV).Should().NotBeNull();
            tag.Member(m => m.UN).Should().NotBeNull();
        }
        
        [Test]
        public void New_Alarm_ShouldHaveValidMembers()
        {
            var tag = Tag.Create("Test", new ALARM());

            tag.Should().NotBeNull();
            tag.Member(m => m.EnableIn).Should().NotBeNull();
            tag.Member(m => m.In).Should().NotBeNull();
            tag.Member(m => m.HHLimit).Should().NotBeNull();
            tag.Member(m => m.HLimit).Should().NotBeNull();
            tag.Member(m => m.LLimit).Should().NotBeNull();
            tag.Member(m => m.LLLimit).Should().NotBeNull();
            tag.Member(m => m.Deadband).Should().NotBeNull();
            tag.Member(m => m.ROCPosLimit).Should().NotBeNull();
            tag.Member(m => m.ROCNegLimit).Should().NotBeNull();
            tag.Member(m => m.ROCPeriod).Should().NotBeNull();
            tag.Member(m => m.EnableOut).Should().NotBeNull();
            tag.Member(m => m.HHAlarm).Should().NotBeNull();
            tag.Member(m => m.HAlarm).Should().NotBeNull();
            tag.Member(m => m.LAlarm).Should().NotBeNull();
            tag.Member(m => m.LLAlarm).Should().NotBeNull();
            tag.Member(m => m.ROCPosAlarm).Should().NotBeNull();
            tag.Member(m => m.ROCNegAlarm).Should().NotBeNull();
            tag.Member(m => m.ROC).Should().NotBeNull();
            tag.Member(m => m.Status).Should().NotBeNull();
            tag.Member(m => m.InstructFault).Should().NotBeNull();
            tag.Member(m => m.DeadbandInv).Should().NotBeNull();
            tag.Member(m => m.ROCPosLimitInv).Should().NotBeNull();
            tag.Member(m => m.ROCNegLimitInv).Should().NotBeNull();
            tag.Member(m => m.ROCPeriodInv).Should().NotBeNull();
        }
    }
}