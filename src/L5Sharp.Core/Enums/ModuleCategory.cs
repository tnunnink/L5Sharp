namespace L5Sharp.Core;

/// <summary>
/// Represents an enumeration of <see cref="ModuleCategory"/> for a given <see cref="Module"/>.
/// </summary>
public sealed class ModuleCategory : LogixEnum<ModuleCategory, string>
{
    private ModuleCategory(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents the <c>1746_Analog_IO</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory Analog =
        new(nameof(Analog), "Analog");

    /// <summary>
    /// Represents the <c>1746_Discrete_IO</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory Analog1746IO =
        new(nameof(Analog1746IO), "1746 Analog I/O");

    /// <summary>
    /// Represents the <c>1746_Specialty_IO</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory CipMotionSafetyTrackSection =
        new(nameof(CipMotionSafetyTrackSection), "CIP Motion Safety Track Section");

    /// <summary>
    /// Represents the <c>20_Comm_ER</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory ColorRegistrationMarkSensor45Crm =
        new(nameof(ColorRegistrationMarkSensor45Crm), "Color Registration Mark Sensor - 45CRM");

    /// <summary>
    /// Represents the <c>Analog</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory Comm20Er =
        new(nameof(Comm20Er), "20 - Comm-ER");

    /// <summary>
    /// Represents the <c>CIP_Motion_Safety_Track_Section</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory Communication =
        new(nameof(Communication), "Communication");

    /// <summary>
    /// Represents the <c>Color_Registration_Mark_Sensor_45CRM</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory Communications =
        new(nameof(Communications), "Communications");

    /// <summary>
    /// Represents the <c>Communication</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory CommunicationsAdapter =
        new(nameof(CommunicationsAdapter), "Communications Adapter");

    /// <summary>
    /// Represents the <c>Communications</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory Controller =
        new(nameof(Controller), "Controller");

    /// <summary>
    /// Represents the <c>Communications_Adapter</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory Digital =
        new(nameof(Digital), "Digital");

    /// <summary>
    /// Represents the <c>Controller</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory Discrete =
        new(nameof(Discrete), "Discrete");

    /// <summary>
    /// Represents the <c>Digital</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory Discrete1746IO =
        new(nameof(Discrete1746IO), "1746 Discrete I/O");

    /// <summary>
    /// Represents the <c>Discrete</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory DpiToEtherNetIP =
        new(nameof(DpiToEtherNetIP), "DPI to EtherNet/IP");

    /// <summary>
    /// Represents the <c>DPI_to_EtherNetIP</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory Drive =
        new(nameof(Drive), "Drive");

    /// <summary>
    /// Represents the <c>Drive</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory DsiToEtherNetIP =
        new(nameof(DsiToEtherNetIP), "DSI to EtherNet/IP");

    /// <summary>
    /// Represents the <c>DSI_to_EtherNetIP</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory Encoder =
        new(nameof(Encoder), "Encoder");

    /// <summary>
    /// Represents the <c>Encoder</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory EnergyManagementProducts =
        new(nameof(EnergyManagementProducts), "EnergyManagementProducts");

    /// <summary>
    /// Represents the <c>EnergyManagementProducts</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory GeneralPurposeDiscreteIO =
        new(nameof(GeneralPurposeDiscreteIO), "General Purpose Discrete I/O");

    /// <summary>
    /// Represents the <c>General_Purpose_Discrete_IO</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory HartDevice =
        new(nameof(HartDevice), "HART Device (Rockwell Automation)");

    /// <summary>
    /// Represents the <c>HART_Device</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory HMI =
        new(nameof(HMI), "HMI");

    /// <summary>
    /// Represents the <c>HMI</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory HumanMachineInterface =
        new(nameof(HumanMachineInterface), "Human-Machine Interface");

    /// <summary>
    /// Represents the <c>Human_Machine_Interface</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory InductiveProximitySensor871C =
        new(nameof(InductiveProximitySensor871C), "Inductive Proximity Sensor - 871C");

    /// <summary>
    /// Represents the <c>Inductive_Proximity_Sensor_871C</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory InductiveProximitySensor871Fm =
        new(nameof(InductiveProximitySensor871Fm), "Inductive Proximity Sensor - 871FM");

    /// <summary>
    /// Represents the <c>Inductive_Proximity_Sensor_871FM</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory InductiveProximitySensor871Tm =
        new(nameof(InductiveProximitySensor871Tm), "Inductive Proximity Sensor - 871TM");

    /// <summary>
    /// Represents the <c>Inductive_Proximity_Sensor_871TM</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory Input =
        new(nameof(Input), "Input");

    /// <summary>
    /// Represents the <c>Input</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory ManagedEthernetSwitch =
        new(nameof(ManagedEthernetSwitch), "Managed Ethernet Switch");

    /// <summary>
    /// Represents the <c>Managed_Ethernet_Switch</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory MaterialHandling =
        new(nameof(MaterialHandling), "MaterialHandling");

    /// <summary>
    /// Represents the <c>MaterialHandling</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory MdiToEtherNetIP =
        new(nameof(MdiToEtherNetIP), "MDI to EtherNet/IP");

    /// <summary>
    /// Represents the <c>MDI_to_EtherNetIP</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory MeasurementSensor45Lms =
        new(nameof(MeasurementSensor45Lms), "Measurement Sensor - 45LMS");

    /// <summary>
    /// Represents the <c>Measurement_Sensor_45LMS</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory Miscellaneous =
        new(nameof(Miscellaneous), "Miscellaneous");

    /// <summary>
    /// Represents the <c>Miscellaneous</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory Motion =
        new(nameof(Motion), "Motion");

    /// <summary>
    /// Represents the <c>Motion</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    // ReSharper disable once InconsistentNaming we can't rename because MotorOverload is another category (WTF Rockwell)
    public static readonly ModuleCategory Motor_Overload =
        new(nameof(Motor_Overload), "Motor Overload");

    /// <summary>
    /// Represents the <c>Motor_Overload</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory MotorOverload =
        new(nameof(MotorOverload), "MotorOverload");

    /// <summary>
    /// Represents the <c>MotorOverload</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory MotorStarter =
        new(nameof(MotorStarter), "MotorStarter");

    /// <summary>
    /// Represents the <c>MotorStarter</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory Other =
        new(nameof(Other), "Other");

    /// <summary>
    /// Represents the <c>Other</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory Output =
        new(nameof(Output), "Output");

    /// <summary>
    /// Represents the <c>Output</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory PhotoelectricSensor42Af =
        new(nameof(PhotoelectricSensor42Af), "Photoelectric Sensor - 42AF");

    /// <summary>
    /// Represents the <c>Photoelectric_Sensor_42AF</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory PhotoelectricSensor42Ef =
        new(nameof(PhotoelectricSensor42Ef), "Photoelectric Sensor - 42EF");

    /// <summary>
    /// Represents the <c>Photoelectric_Sensor_42EF</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory PhotoelectricSensor42Jt =
        new(nameof(PhotoelectricSensor42Jt), "Photoelectric Sensor - 42JT");

    /// <summary>
    /// Represents the <c>Photoelectric_Sensor_42JT</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory PowerFlex750 =
        new(nameof(PowerFlex750), "PowerFlex 750-Series via Embedded EtherNet/IP");

    /// <summary>
    /// Represents the <c>PowerFlex_750</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory PowerMonitor5000 =
        new(nameof(PowerMonitor5000), "PowerMonitor 5000");

    /// <summary>
    /// Represents the <c>PowerMonitor_5000</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory PressureSensor836P =
        new(nameof(PressureSensor836P), "Pressure Sensor - 836P");

    /// <summary>
    /// Represents the <c>Pressure_Sensor_836P</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory PLC =
        new(nameof(PLC), "Programmable Logic Controller");

    /// <summary>
    /// Represents the <c>Programmable_Logic_Controller</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory Robot =
        new(nameof(Robot), "Robot");

    /// <summary>
    /// Represents the <c>Robot</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory RockwellAutomationMiscellaneous =
        new(nameof(RockwellAutomationMiscellaneous), "Rockwell Automation Miscellaneous");

    /// <summary>
    /// Represents the <c>Rockwell_Automation_Miscellaneous</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory Safety =
        new(nameof(Safety), "Safety");

    /// <summary>
    /// Represents the <c>Safety</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory ScanPortToEtherNetIP =
        new(nameof(ScanPortToEtherNetIP), "SCANport to EtherNet/IP");

    /// <summary>
    /// Represents the <c>SCANPort_To_EtherNetIP</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory Specialty =
        new(nameof(Specialty), "Specialty");

    /// <summary>
    /// Represents the <c>Specialty</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory Specialty1746IO =
        new(nameof(Specialty1746IO), "1746 Specialty I/O");

    /// <summary>
    /// Represents the <c>Temperature_Sensor_837T</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory TemperatureSensor837T =
        new(nameof(TemperatureSensor837T), "Temperature Sensor - 837T");

    /// <summary>
    /// Represents the <c>Uninterruptible_Power_Supply</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory UPS =
        new(nameof(UPS), "Uninterruptible Power Supply");

    /// <summary>
    /// Represents the <c>Universal</c> <see cref="ModuleCategory"/> value.
    /// </summary>
    public static readonly ModuleCategory Universal =
        new(nameof(Universal), "Universal");
}