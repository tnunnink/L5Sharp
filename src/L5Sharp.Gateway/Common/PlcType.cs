namespace L5Sharp.Gateway.Common;

/// <summary>
/// Represents the type of programmable logic controller (PLC) being used.
/// </summary>
/// <remarks>
/// These are the only PLC types supported by the underlying libplctag library that make sense for this project
/// since we rely on the underlying L5X (XML) representation.
/// </remarks>
public enum PlcType
{
    /// <summary>
    /// Represents the Rockwell Automation ControlLogix series PLC.
    /// </summary>
    ControlLogix,

    /// <summary>
    /// Represents the Rockwell Automation MicroLogix series PLC.
    /// </summary>
    MicroLogix
}