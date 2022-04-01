using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.L5X;

namespace L5Sharp.Querying
{
    /*public interface ILogixQueryTest
    {
        void AddFilter(Func<IEnumerable<XElement>, IEnumerable<XElement>> filter);
    }

    internal class LogixQueryTest : ILogixQueryTest
    {
        private readonly L5XDocument _document;
        private readonly IEnumerable<XElement> _source;

        public LogixQueryTest(L5XDocument document, IEnumerable<XElement> source)
        {
            _document = document;
            _source = source;
        }

        public void AddFilter(Func<IEnumerable<XElement>, IEnumerable<XElement>> filter)
        {
            throw new System.NotImplementedException();
        }

        public ILogixQueryTest Take(int number)
        {
            AddFilter(e => e.Take(number));
            return this;
        }
    }

    internal class LogixRepo
    {
        public IEnumerable<string> Query(Action<ILogixQueryTest> query)
        {
            
        }
    }
    
    
    
    /// <summary>
    /// 
    /// </summary>
    public static class QueryExtensions
    {
        public static ILogixQueryTest Custom(this ILogixQueryTest query, string arg)
        {
            query.AddFilter(c => c.Descendants().Where(e => e.Value == arg));
            return query;
        }
    }*/
}