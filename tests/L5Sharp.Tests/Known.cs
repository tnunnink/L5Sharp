using System;
using System.IO;

namespace L5Sharp.Tests
{
    public static class Known
    {
        public static readonly string Test = Path.Combine(Environment.CurrentDirectory, @"TestFiles\Test.xml");
        public static readonly string Empty = Path.Combine(Environment.CurrentDirectory, @"TestFiles\Empty.xml");
    }
}