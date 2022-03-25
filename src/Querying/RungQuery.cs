using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.L5X;
using L5Sharp.Serialization.Components;

namespace L5Sharp.Querying
{
    internal class RungQuery : LogixQuery<Rung>, IRungQuery
    {
        public RungQuery(IEnumerable<XElement> elements) : base(elements, new RungSerializer())
        {
        }

        public IRungQuery InRange(int first, int last)
        {
            var results = Elements.Where(e =>
            {
                var number = int.Parse(e.Attribute(L5XAttribute.Number.ToString())?.Value!);
                return number >= first && number <= last;
            });

            return new RungQuery(results);
        }

        public IRungQuery IncludeAddOns()
        {
            throw new NotImplementedException();
        }

        public IRungQuery InRoutine(ComponentName routineName)
        {
            var results = Elements.Where(e =>
                e.Ancestors(L5XElement.Routine.ToString()).FirstOrDefault()?.ComponentName() == (string)routineName);

            return new RungQuery(results);
        }

        public IRungQuery WithTag(TagName tagName)
        {
            var results = Elements.Where(e =>
            {
                var text = e.Attribute(L5XElement.Text.ToString())?.Value;
                return text is not null && text.Contains(tagName);
            });

            return new RungQuery(results);
        }

        public IRungQuery WithTag(TagName tagName, TagQueryOptions options)
        {
            //todo tag query options
            var results = Elements.Where(e =>
            {
                var text = e.Attribute(L5XElement.Text.ToString())?.Value;
                return text is not null && text.Contains(tagName);
            });

            return new RungQuery(results);
        }

        public IRungQuery WithDataType(string typeName)
        {
            // we need to find data types of the current tags...
            //1. Get tag names from current collection
            //2. Find tags in document with specified root name
            //3. Need some way to get member names
            //4. Join results on tag name
            throw new NotImplementedException();
        }

        public IRungQuery WithComment(string comment)
        {
            var results = Elements.Where(e =>
            {
                var value = e.Element(L5XElement.Comment.ToString())?.Value;
                return value is not null && string.Equals(value, comment);
            });

            return new RungQuery(results);
        }

        public IRungQuery WithInstruction(string name)
        {
            var results = Elements.Where(e =>
            {
                var text = e.Element(L5XElement.Text.ToString())?.Value;
                return text is not null && string.Equals(text, name);
            });

            return new RungQuery(results);
        }

        public IRungQuery WithInstruction(Instruction instruction)
        {
            throw new NotImplementedException();
        }
    }
}