using L5Sharp.Abstractions;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    public class TestTagMemberBase<TDataType> : TagMemberBase<TDataType> where TDataType : IDataType
    {
        public TestTagMemberBase(string name, TDataType dataType, Dimensions dimensions, Radix radix,
            ExternalAccess externalAccess, string description, IComponent parent) : base(name, dataType, dimensions,
            radix, externalAccess, description, parent)
        {
        }
    }

    [TestFixture]
    public class TagMemberBaseTests
    {
        [Test]
        public void METHOD()
        {
        }
    }
}