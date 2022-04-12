using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using L5Sharp.L5X;
using L5SharpTests;
using NUnit.Framework;

namespace L5Sharp.Querying.Tests
{
    [TestFixture]
    public class RungQueryTests
    {
        private static readonly string RungExample1 = 
            Path.Combine(Environment.CurrentDirectory, @"RungExamples\RungExample1.xml");
        
        [Test]
        public void InProgram_WhenCalled_ShouldNotBeEmpty()
        {
            var context = L5XContext.Load(Known.Test);

            var rungs = context.Rungs(q => q.InProgram("MainProgram")).ToList();

            rungs.Should().NotBeEmpty();
        }

        /*[Test]
        public void Flatten_WhenCalled_ShouldNotBeACompleteDisaster()
        {
            var context = L5XContext.Load(Known.Test);

            var rungs = context.Rungs().InProgram("MainProgram").Flatten().Select().ToList();

            rungs.Should().NotBeEmpty();
        }
        
        [Test]
        public void Flatten_RungExample1_PleaseGod()
        {
            var context = L5XContext.Load(RungExample1);

            var rungs = context.Rungs().Flatten().Select().ToList();

            rungs.Should().NotBeEmpty();
        }*/
    }
}