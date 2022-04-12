using System;
using System.IO;

namespace L5SharpTests
{
    public static class Known
    {
        public static readonly string Test = Path.Combine(Environment.CurrentDirectory, @"L5XFiles\Test.xml");
        public static readonly string Empty = Path.Combine(Environment.CurrentDirectory, @"L5XFiles\Empty.xml");
    }
}