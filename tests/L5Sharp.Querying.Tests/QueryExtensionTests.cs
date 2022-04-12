using System.Linq;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.L5X;
using L5SharpTests;
using NUnit.Framework;

namespace L5Sharp.Querying.Tests
{
    [TestFixture]
    public class QueryExtensionTests
    {
        [Test]
        public void OnlyAtomicMembers_WhenCalled_AllMembersShouldBeAtomics()
        {
            var context = L5XContext.Load(Known.Test);
            
            var results = context.DataTypes(q => q.OnlyAtomicMembers()).ToList();

            results.Should().NotBeEmpty();

            foreach (var result in results)
            {
                result.Members.All(m => DataType.Atomics.Contains(m.DataType.Name)).Should().BeTrue();
            }
        }
    }

    public static class QueryExtensions
    {
        // This is an example of how you would extend the DataTypeQuery with a custom query.
        // Imagine you want to query data types to get all types where they only have atomic data type members
        public static DataTypeQuery OnlyAtomicMembers(this DataTypeQuery query)
        {
            // 1. Perform any validation on input parameters. We have none here...
            
            // 2. Perform the query over the set of XElements objects. All query objects are an enumerable of XElement,
            // so you can simply query them using LINQ to XML. This requires you know LINQ to XML and the XML structure
            // of the L5X.
            var results = query.Where(e =>
                e.Descendants("Member").All(m => DataType.Atomics.Contains(m.Attribute("DataType")?.Value)));

            // 4. Return a new object instance with the filtered results.
            // This makes the query a fluent api that can be added into other DataTypeQuery calls.
            // Do not return the same query instance as it will just be returning the original un-filtered elements.
            return new DataTypeQuery(results);
        }
    }
}