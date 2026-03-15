namespace L5Sharp.Tests.Core;

[TestFixture]
public class L5XImportTests
{
    [Test]
    public Task Import_StructureDataTypeWithNotConfiguration_ShouldBeVerified()
    {
        var content = TestContent.Empty;

        content.Import(b => b
            .From(TestFiles.DataTypes.ComplexType)
            .DataType()
            .Force()
        );

        return VerifyXml(content.ToString());
    }

    [Test]
    public Task Import_StructureDataTypeDiscardSimpleType_ShouldBeVerified()
    {
        var content = TestContent.Empty;

        content.Import(b => b
            .From(TestFiles.DataTypes.ComplexType)
            .DataType()
            .Discard<DataType>(d => d.Name == "SimpleType")
        );

        return VerifyXml(content.ToString());
    }

    [Test]
    public Task Import_StructureDataTypeDiscardAndModifyToRemoveSimpleType_ShouldBeVerified()
    {
        var content = TestContent.Empty;

        content.Import(b => b
            .From(TestFiles.DataTypes.ComplexType)
            .DataType()
            .Discard<DataType>(d => d.Name == "SimpleType")
            .Modify<DataType>(
                d => d.Name == "ComplexType",
                d => d.RemoveMember("SimpleMember")
            )
        );

        return VerifyXml(content.ToString());
    }

    [Test]
    public Task Import_ModuleComponent_ShouldBeVerified()
    {
        var content = TestContent.Empty;

        content.Import(b => b
            .From(TestFiles.Modules.TestCard)
            .Module()
        );

        return VerifyXml(content.ToString());
    }

    [Test]
    public Task Import_ProgramWithSpecifiedTaskSchedule_ShouldBeVerified()
    {
        var content = TestContent.Empty;

        content.Import(b => b
            .From(TestFiles.Programs.TestProgram)
            .Program()
            .Rename("ImportedProgram")
            .ScheduleIn("Standard", t =>
            {
                t.Name = "Standard";
                t.Type = TaskType.Periodic;
            })
        );

        return VerifyXml(content.ToString());
    }

    [Test]
    public Task Import_SpecificTypeNameFromSource_ShouldBeVerified()
    {
        var content = TestContent.Empty;

        content.Import(b => b
            .From(TestContent.Test)
            .DataType("ComplexType")
        );

        return VerifyXml(content.ToString());
    }
}