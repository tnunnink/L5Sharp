using System.Xml.Linq;
using FluentAssertions;

namespace L5Sharp.Tests.Core.Components;

[TestFixture]
public class ControllerTests
{
    [Test]
    public void New_Default_ShouldNotBeNull()
    {
        var controller = new Controller();

        controller.Should().NotBeNull();
    }

    [Test]
    public void New_Default_ShouldHaveExpectedValues()
    {
        var controller = new Controller();

        controller.Name.Should().BeEmpty();
        controller.Description.Should().BeNull();
        controller.Revision.Should().Be(new Revision());
        controller.ProcessorType.Should().BeNull();
        controller.TimeSlice.Should().BeNull();
        controller.ShareUnusedTimeSlice.Should().BeNull();
        controller.PowerLossProgram.Should().BeNull();
        controller.MajorFaultProgram.Should().BeNull();
        controller.CommPath.Should().BeNull();
        controller.CommDriver.Should().BeNull();
        controller.ProjectCreationDate.Should().BeWithin(TimeSpan.FromSeconds(1));
        controller.LastModifiedDate.Should().BeWithin(TimeSpan.FromSeconds(1));
        controller.SFCExecutionControl.Should().BeNull();
        controller.SFCRestartPosition.Should().BeNull();
        controller.SFCLastScan.Should().BeNull();
        controller.ProjectSN.Should().BeNull();
        controller.MatchProjectToController.Should().BeNull();
        controller.InhibitAutomaticFirmwareUpdate.Should().BeNull();
        controller.CurrentProjectLanguage.Should().BeNull();
        controller.DefaultProjectLanguage.Should().BeNull();
        controller.ControllerLanguage.Should().BeNull();
        controller.CanUseRPIFromProducer.Should().BeNull();
        controller.PassThroughConfiguration.Should().BeNull();
        controller.DownloadProjectDocumentationAndExtendedProperties.Should().BeNull();
        controller.SafetyInfo.Should().BeNull();
        controller.Security.Should().BeNull();
        controller.RedundancyInfo.Should().BeNull();
        controller.DataTypes.Should().BeEmpty();
        controller.Instructions.Should().BeEmpty();
        controller.Modules.Should().BeEmpty();
        controller.Tags.Should().BeEmpty();
        controller.Programs.Should().BeEmpty();
        controller.Tasks.Should().BeEmpty();
        controller.ParameterConnections.Should().BeEmpty();
        controller.Trends.Should().BeEmpty();
        controller.WatchLists.Should().BeEmpty();
    }

    [Test]
    public void New_Overridden_ShouldHaveExpectedValues()
    {
        var controller = new Controller
        {
            Name = "Test",
            Description = "This is a test",
            Revision = new Revision(33, 12),
            ProcessorType = "TestProcessorType",
            TimeSlice = "TestTimeSlice",
            ShareUnusedTimeSlice = true,
            PowerLossProgram = "TestPowerLossProgram",
            MajorFaultProgram = "TestMajorFaultProgram",
            CommPath = "TestCommPath",
            CommDriver = "TestCommDriver",
            ProjectCreationDate = DateTime.Now,
            LastModifiedDate = DateTime.Now,
            SFCExecutionControl = SFCExecutionControl.CurrentActive,
            SFCRestartPosition = SFCRestartPosition.MostRecent,
            SFCLastScan = SFCLastScan.AutomaticReset,
            ProjectSN = "TestProjectSN",
            MatchProjectToController = true,
            InhibitAutomaticFirmwareUpdate = true,
            CurrentProjectLanguage = "TestCurrentProjectLanguage",
            DefaultProjectLanguage = "TestDefaultProjectLanguage",
            ControllerLanguage = "TestControllerLanguage",
            CanUseRPIFromProducer = true,
            PassThroughConfiguration = PassThroughOption.Disabled,
            DownloadProjectDocumentationAndExtendedProperties = true,
            SafetyInfo = new SafetyInfo()
            {
                SafetyLocked = true,
                SafetySignature = "Test SafetSignature",
                SafetyTagMap = ["TestTag", "TestTag"]
            },
            Security = new Security
            {
                SecurityAuthorityID = "TestSecurityAuthorityID"
            },
            RedundancyInfo = new RedundancyInfo
            {
                Enabled = true,
                DataTablePadPercentage = 50,
                IOMemoryPadPercentage = 50,
                KeepTestEditsOnSwitchOver = true
            }
        };

        controller.Name.Should().Be("Test");
        controller.Description.Should().Be("This is a test");
        controller.Revision.Should().Be(new Revision(33, 12));
        controller.ProcessorType.Should().Be("TestProcessorType");
        controller.TimeSlice.Should().Be("TestTimeSlice");
        controller.ShareUnusedTimeSlice.Should().BeTrue();
        controller.PowerLossProgram.Should().Be("TestPowerLossProgram");
        controller.MajorFaultProgram.Should().Be("TestMajorFaultProgram");
        controller.CommPath.Should().Be("TestCommPath");
        controller.CommDriver.Should().Be("TestCommDriver");
        controller.ProjectCreationDate.Should().BeWithin(TimeSpan.FromSeconds(1));
        controller.LastModifiedDate.Should().BeWithin(TimeSpan.FromSeconds(1));
        controller.SFCExecutionControl.Should().Be(SFCExecutionControl.CurrentActive);
        controller.SFCRestartPosition.Should().Be(SFCRestartPosition.MostRecent);
        controller.SFCLastScan.Should().Be(SFCLastScan.AutomaticReset);
        controller.ProjectSN.Should().Be("TestProjectSN");
        controller.MatchProjectToController.Should().BeTrue();
        controller.InhibitAutomaticFirmwareUpdate.Should().BeTrue();
        controller.CurrentProjectLanguage.Should().Be("TestCurrentProjectLanguage");
        controller.DefaultProjectLanguage.Should().Be("TestDefaultProjectLanguage");
        controller.ControllerLanguage.Should().Be("TestControllerLanguage");
        controller.CanUseRPIFromProducer.Should().BeTrue();
        controller.PassThroughConfiguration.Should().Be(PassThroughOption.Disabled);
        controller.DownloadProjectDocumentationAndExtendedProperties.Should().BeTrue();
        controller.SafetyInfo.Should().NotBeNull();
        controller.SafetyInfo.SafetyLocked.Should().BeTrue();
        controller.SafetyInfo.SafetySignature.Should().Be("Test SafetSignature");
        controller.SafetyInfo.SafetyTagMap.Should().NotBeNull();
        controller.SafetyInfo.SafetyTagMap.Should().Contain("TestTag");
        controller.Security.Should().NotBeNull();
        controller.Security.SecurityAuthorityID.Should().Be("TestSecurityAuthorityID");
        controller.RedundancyInfo.Should().NotBeNull();
        controller.RedundancyInfo.Enabled.Should().BeTrue();
        controller.RedundancyInfo.DataTablePadPercentage.Should().Be(50);
        controller.RedundancyInfo.IOMemoryPadPercentage.Should().Be(50);
        controller.RedundancyInfo.KeepTestEditsOnSwitchOver.Should().BeTrue();
    }

    [Test]
    public void New_Element_ShouldHaveExpectedValues()
    {
        const string xml =
            """
            <Controller Name="Test" Use="Context" MajorRev="33" MinorRev="12" ProcessorType="TestProcessorType" TimeSlice="TestTimeSlice" ShareUnusedTimeSlice="1" PowerLossProgram="TestPowerLossProgram" MajorFaultProgram="TestMajorFaultProgram" CommPath="TestCommPath" CommDriver="TestCommDriver" SFCExecutionControl="CurrentActive" SFCRestartPosition="MostRecent" SFCLastScan="AutomaticReset" ProjectSN="TestProjectSN" MatchProjectToController="Yes" InhibitAutomaticFirmwareUpdate="1" CurrentProjectLanguage="TestCurrentProjectLanguage" DefaultProjectLanguage="TestDefaultProjectLanguage" ControllerLanguage="TestControllerLanguage" CanUseRPIFromProducer="true" PassThroughConfiguration="Disabled" DownloadProjectDocumentationAndExtendedProperties="true">
              <Description>This is a test</Description>
              <RedundancyInfo Enabled="true" DataTablePadPercentage="50" IOMemoryPadPercentage="50" KeepTestEditsOnSwitchOver="true" />
              <Security SecurityAuthorityID="TestSecurityAuthorityID" />
              <SafetyInfo SafetyLocked="true" SafetySignature="Test SafetSignature">
                <SafetyTagMap>TestTag,TestTag</SafetyTagMap>
              </SafetyInfo>
            </Controller>
            """;

        var element = XElement.Parse(xml);

        var controller = new Controller(element);

        controller.Name.Should().Be("Test");
        controller.Description.Should().Be("This is a test");
        controller.Revision.Should().Be(new Revision(33, 12));
        controller.ProcessorType.Should().Be("TestProcessorType");
        controller.TimeSlice.Should().Be("TestTimeSlice");
        controller.ShareUnusedTimeSlice.Should().BeTrue();
        controller.PowerLossProgram.Should().Be("TestPowerLossProgram");
        controller.MajorFaultProgram.Should().Be("TestMajorFaultProgram");
        controller.CommPath.Should().Be("TestCommPath");
        controller.CommDriver.Should().Be("TestCommDriver");
        controller.ProjectCreationDate.Should().BeWithin(TimeSpan.FromSeconds(1));
        controller.LastModifiedDate.Should().BeWithin(TimeSpan.FromSeconds(1));
        controller.SFCExecutionControl.Should().Be(SFCExecutionControl.CurrentActive);
        controller.SFCRestartPosition.Should().Be(SFCRestartPosition.MostRecent);
        controller.SFCLastScan.Should().Be(SFCLastScan.AutomaticReset);
        controller.ProjectSN.Should().Be("TestProjectSN");
        controller.MatchProjectToController.Should().BeTrue();
        controller.InhibitAutomaticFirmwareUpdate.Should().BeTrue();
        controller.CurrentProjectLanguage.Should().Be("TestCurrentProjectLanguage");
        controller.DefaultProjectLanguage.Should().Be("TestDefaultProjectLanguage");
        controller.ControllerLanguage.Should().Be("TestControllerLanguage");
        controller.CanUseRPIFromProducer.Should().BeTrue();
        controller.PassThroughConfiguration.Should().Be(PassThroughOption.Disabled);
        controller.DownloadProjectDocumentationAndExtendedProperties.Should().BeTrue();
        controller.SafetyInfo.Should().NotBeNull();
        controller.SafetyInfo?.SafetyLocked.Should().BeTrue();
        controller.SafetyInfo?.SafetySignature.Should().Be("Test SafetSignature");
        controller.SafetyInfo?.SafetyTagMap.Should().NotBeNull();
        controller.SafetyInfo?.SafetyTagMap.Should().Contain("TestTag");
        controller.Security?.Should().NotBeNull();
        controller.Security?.SecurityAuthorityID.Should().Be("TestSecurityAuthorityID");
        controller.RedundancyInfo.Should().NotBeNull();
        controller.RedundancyInfo?.Enabled.Should().BeTrue();
        controller.RedundancyInfo?.DataTablePadPercentage.Should().Be(50);
        controller.RedundancyInfo?.IOMemoryPadPercentage.Should().Be(50);
        controller.RedundancyInfo?.KeepTestEditsOnSwitchOver.Should().BeTrue();
    }

    [Test]
    public Task Serialize_Overriden_ShouldBeVerified()
    {
        var controller = new Controller
        {
            Name = "Test",
            Description = "This is a test",
            Revision = new Revision(33, 12),
            ProcessorType = "TestProcessorType",
            TimeSlice = "TestTimeSlice",
            ShareUnusedTimeSlice = true,
            PowerLossProgram = "TestPowerLossProgram",
            MajorFaultProgram = "TestMajorFaultProgram",
            CommPath = "TestCommPath",
            CommDriver = "TestCommDriver",
            ProjectCreationDate = DateTime.Now,
            LastModifiedDate = DateTime.Now,
            SFCExecutionControl = SFCExecutionControl.CurrentActive,
            SFCRestartPosition = SFCRestartPosition.MostRecent,
            SFCLastScan = SFCLastScan.AutomaticReset,
            ProjectSN = "TestProjectSN",
            MatchProjectToController = true,
            InhibitAutomaticFirmwareUpdate = true,
            CurrentProjectLanguage = "TestCurrentProjectLanguage",
            DefaultProjectLanguage = "TestDefaultProjectLanguage",
            ControllerLanguage = "TestControllerLanguage",
            CanUseRPIFromProducer = true,
            PassThroughConfiguration = PassThroughOption.Disabled,
            DownloadProjectDocumentationAndExtendedProperties = true,
            SafetyInfo = new SafetyInfo
            {
                SafetyLocked = true,
                SafetySignature = "Test SafetSignature",
                SafetyTagMap = ["TestTag", "TestTag"]
            },
            Security = new Security
            {
                SecurityAuthorityID = "TestSecurityAuthorityID"
            },
            RedundancyInfo = new RedundancyInfo
            {
                Enabled = true,
                DataTablePadPercentage = 50,
                IOMemoryPadPercentage = 50,
                KeepTestEditsOnSwitchOver = true
            }
        };

        var xml = controller.Serialize().ToString();

        return VerifyXml(xml)
            .IgnoreMember("ProjectCreationDate")
            .IgnoreMember("LastModifiedDate");
    }

    [Test]
    public void Import_SimpleDataType_ShouldBeVerified()
    {
        var controller = L5X.Load(Known.Test).Controller;

        controller.Import("The/file/containing/MyDataType", b => b
            .Overwrite<DataType>(d => d.Name == "someType")
            .Overwrite<DataType>(d => d.Name.Contains("Test"))
            .Modify<DataType>(d => d.Name == "Something", d => { d.Description = "This is an update"; })
            .Rename<DataType>("Current", "NewName")
        );
    }
}