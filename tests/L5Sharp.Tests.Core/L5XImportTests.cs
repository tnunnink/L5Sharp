namespace L5Sharp.Tests.Core;

[TestFixture]
public class L5XImportTests
{
    [Test]
    public Task Import_ComplexDataTypeWithNotConfiguration_ShouldBeVerified()
    {
        var content = L5X.Load(Known.Empty);

        content.Import(b => b
            .From(Known.DataTypeExport)
            .DataType()
            .Force()
        );

        return VerifyXml(content.Serialize().ToString());
    }

    [Test]
    public Task Import_ComplexDataTypeDiscardSimpleType_ShouldBeVerified()
    {
        var content = L5X.Load(Known.Empty);

        content.Import(b => b
            .From(Known.DataTypeExport)
            .DataType()
            .Discard<DataType>(d => d.Name == "SimpleType")
        );

        return VerifyXml(content.Serialize().ToString());
    }

    [Test]
    public Task Import_ComplexDataTypeDiscardAndModifyToRemoveSimpleType_ShouldBeVerified()
    {
        var content = L5X.Load(Known.Empty);

        content.Import(b => b
            .From(Known.DataTypeExport)
            .DataType()
            .Discard<DataType>(d => d.Name == "SimpleType")
            .Modify<DataType>(
                d => d.Name == "ComplexType",
                d => d.RemoveMember("SimpleMember")
            )
        );

        return VerifyXml(content.Serialize().ToString());
    }

    [Test]
    public Task Import_ModuleComponent_ShouldBeVerified()
    {
        var content = L5X.Load(Known.Empty);

        content.Import(b => b
            .From(Known.ModuleExport)
            .Module()
        );

        return VerifyXml(content.Serialize().ToString());
    }

    [Test]
    public Task Import_ProgramWithSpecifiedTaskSchedule_ShouldBeVerified()
    {
        var content = L5X.Load(Known.Empty);

        content.Import(b => b
            .From(Known.ProgramExport)
            .Program()
            .Rename("ImportedProgram")
            .ScheduleIn("Standard", t =>
            {
                t.Name = "Standard";
                t.Type = TaskType.Periodic;
            })
        );

        return VerifyXml(content.Serialize().ToString());
    }

    [Test]
    public Task Import_SpecificTypeNameFromSource_ShouldBeVerified()
    {
        var content = L5X.Load(Known.Empty);

        content.Import(b => b
            .From(Known.Test)
            .DataType("ComplexType")
        );

        return VerifyXml(content.Serialize().ToString());
    }
}