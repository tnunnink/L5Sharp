using System;
using L5Sharp.Core;

namespace L5Sharp.Utilities
{
    internal static class Check
    {
        public static void TagNameNotNullOrEmpty(TagName tagName)
        {
            if (tagName is null)
                throw new ArgumentNullException(nameof(tagName));
            
            if (tagName.IsEmpty)
                throw new ArgumentException(nameof(tagName));
        }
    }
}