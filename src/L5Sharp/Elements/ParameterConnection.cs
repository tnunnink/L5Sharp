using System;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A logix <c>ParameterConnection</c> element defining the connection between two different program tags.
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public class ParameterConnection : LogixElement
{
    /// <summary>
    /// Creates a new <see cref="ParameterConnection"/> with default values.
    /// </summary>
    public ParameterConnection()
    {
        EndPoint1 = string.Empty;
        EndPoint2 = string.Empty;
    }

    /// <summary>
    /// Creates a new <see cref="ParameterConnection"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public ParameterConnection(XElement element) : base(element)
    {
    }
    
    /// <summary>
    /// Specify the first end point of the parameter connection.
    /// </summary>
    /// <value>A <see cref="string"/> containing the end point path. The format for end point connections
    /// is program_name.parameter_name</value>
    public string EndPoint1
    {
        get => GetRequiredValue<string>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// Specify the second end point of the parameter connection.
    /// </summary>
    /// <value>A <see cref="string"/> containing the end point path. The format for end point connections
    /// is program_name.parameter_name</value>
    public string EndPoint2
    {
        get => GetRequiredValue<string>();
        set => SetRequiredValue(value);
    }
}