using System;
using System.IO;

namespace L5Sharp.Context.Tests
{
    public static class Known
    {
        public static readonly string L5X = Path.Combine(Environment.CurrentDirectory, @"TestFiles\Test.xml");
        
        public static readonly string Template = Path.Combine(Environment.CurrentDirectory, @"TestFiles\Template.xml");
    }
}