using System.Collections.Generic;
using System.Linq;
using System.Net;
using L5Sharp.Components;

namespace L5Sharp.Extensions;

/// <summary>
/// A collection of built in extensions for the <see cref="Module"/> component.
/// </summary>
public static class ModuleExtensions
{
    /// <summary>
    /// Gets the slot number of the current module if one exists. If the module does not have an slot, returns null.
    /// </summary>
    /// <value>An <see cref="byte"/> representing the slot number of the module.</value>
    /// <remarks>
    /// This is a helper that just looks for an upstream <see cref="Port"/> with a valid slot byte number.
    /// </remarks>
    public static byte? Slot(this Module module) =>
        module.Ports.FirstOrDefault(p => p.Upstream && p.Address.IsSlot)?.Address.ToSlot();

    /// <summary>
    /// Gets the IP address of the current module if one exists. If the module does not have an IP, returns null.
    /// </summary>
    /// <value>An <see cref="IPAddress"/> representing the IP of the module.</value>
    /// <remarks>
    /// This is a helper property that just looks for <see cref="Port"/> for an Ethernet port with a
    /// valid IP address.
    /// </remarks>
    public static IPAddress? IP(this Module module) =>
        module.Ports.FirstOrDefault(p => p is { Type: "Ethernet", Address.IsIPv4: true })?.Address.ToIPAddress();

    /// <summary>
    /// Returns a collection of all non-null <see cref="Tag"/> objects for the current Module, including all
    /// input, output, and config tags.
    /// </summary>
    /// <value>An <see cref="IEnumerable{T}"/> containing the base tags for the Module.</value>
    public static IEnumerable<Tag> Tags(this Module module)
    {
        var tags = new List<Tag>();

        if (module.Config is not null)
            tags.Add(module.Config);

        foreach (var connection in module.Connections)
        {
            if (connection.Input is not null)
                tags.Add(connection.Input);

            if (connection.Output is not null)
                tags.Add(connection.Output);
        }

        return tags;
    }
}