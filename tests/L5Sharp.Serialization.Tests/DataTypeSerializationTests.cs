using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Serialization.Tests
{
    [TestFixture]
    public class DataTypeSerializationTests
    {
        [Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var type = new DataType("Test", members: new[] { Member.Create("Test", new Dint()) });
            
            var element = type.Serialize();

            element.Should().NotBeNull();
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_WhenCalled_ShouldHaveApprovedOutput()
        {
            var type = new DataType("Test", members: new[] { Member.Create("Test", new Dint()) });
            
            var element = type.Serialize();

            Approvals.VerifyXml(element.ToString());
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_WithMembers_ShouldHaveApprovedOutput()
        {
            var type = new DataType("Test", "My Test DataType", new List<IMember<IDataType>>
            {
                Member.Create("Member_Bool", new Bool()),
                Member.Create("Member_Sint", new Sint()),
                Member.Create("Member_Int", new Int()),
                Member.Create("Member_Dint", new Dint()),
                Member.Create("Member_Lint", new Lint()),
                Member.Create("Member_Real", new Real())
            });
            
            var element = type.Serialize();

            Approvals.VerifyXml(element.ToString());
        }
        
    }
}