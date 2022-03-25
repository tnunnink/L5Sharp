using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Context.Tests
{
    [TestFixture]
    public class SearchTesting
    {
        [Test]
        public void GetUniqueTagDescriptionWordsAndCounts()
        {
            var context = L5XContext.Load(Known.L5X);
            
            var allTagComments = context.Tags().All().SelectMany(t => t.Comments.Select(c => c.Value)).ToList();

            var words = allTagComments.SelectMany(GetWords).GroupBy(v => v);

            foreach (var word in words)
            {
                word.Key.Should().NotBeEmpty();
                word.Count().Should().BePositive();
            }
        }
        
        public static IEnumerable<string> GetWords(string input)
        {
            var matches = Regex.Matches(input, @"\b[\w']*\b");

            var words = matches.Where(m => !string.IsNullOrEmpty(m.Value)).Select(m => TrimSuffix(m.Value));

            return words;
        }
        
        private static string TrimSuffix(string word)
        {
            var apostropheLocation = word.IndexOf('\'');
            
            if (apostropheLocation != -1)
                word = word[..apostropheLocation];

            return word;
        }
    }
}