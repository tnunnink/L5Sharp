using FluentAssertions;
using L5Sharp.Common;
using NUnit.Framework;

namespace L5Sharp.Internal.Tests.Common
{
    [TestFixture]
    public class LogixNamesTests
    {
        [Test]
        public void NamesShouldNotBeNull()
        {
            LogixNames.RsLogix5000Content.Should().NotBeNull();
            LogixNames.Controller.Should().NotBeNull();
            LogixNames.DataTypes.Should().NotBeNull();
            LogixNames.DataType.Should().NotBeNull();
            LogixNames.Members.Should().NotBeNull();
            LogixNames.Member.Should().NotBeNull();
            LogixNames.Modules.Should().NotBeNull();
            LogixNames.Module.Should().NotBeNull();
            LogixNames.AddOnInstructionDefinitions.Should().NotBeNull();
            LogixNames.AddOnInstructionDefinition.Should().NotBeNull();
            LogixNames.Parameters.Should().NotBeNull();
            LogixNames.Parameter.Should().NotBeNull();
            LogixNames.Tags.Should().NotBeNull();
            LogixNames.Tag.Should().NotBeNull();
            LogixNames.Programs.Should().NotBeNull();
            LogixNames.Program.Should().NotBeNull();
            LogixNames.Routines.Should().NotBeNull();
            LogixNames.Routine.Should().NotBeNull();
            LogixNames.RllContent.Should().NotBeNull();
            LogixNames.StContent.Should().NotBeNull();
            LogixNames.Rungs.Should().NotBeNull();
            LogixNames.Rung.Should().NotBeNull();
            LogixNames.Tasks.Should().NotBeNull();
            LogixNames.Data.Should().NotBeNull();
            LogixNames.Value.Should().NotBeNull();
            LogixNames.DataValue.Should().NotBeNull();
            LogixNames.Array.Should().NotBeNull();
            LogixNames.Index.Should().NotBeNull();
            LogixNames.Element.Should().NotBeNull();
            LogixNames.Structure.Should().NotBeNull();
            LogixNames.ArrayMember.Should().NotBeNull();
            LogixNames.DataValueMember.Should().NotBeNull();
            LogixNames.StructureMember.Should().NotBeNull();
        }
    }
}