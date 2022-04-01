using System;
using System.IO;

namespace L5Sharp.Querying.Tests.Content
{
    public static class Known
    {
        public static readonly string Test = Path.Combine(Environment.CurrentDirectory, @"Content\Test.xml");
        
        public static readonly string RungExample1 = Path.Combine(Environment.CurrentDirectory, @"RungExamples\RungExample1.xml");
    }
}