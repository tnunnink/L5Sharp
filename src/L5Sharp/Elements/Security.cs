using System;
using System.Xml.Linq;
using L5Sharp.Components;

namespace L5Sharp.Elements;

/// <summary>
/// A sub element of the <see cref="Controller"/> component that contains properties or configuration
/// related to the controller security.
/// </summary>
public class Security : LogixElement
{
    /// <summary>
    /// Creates a new <see cref="Security"/> with default values.
    /// </summary>
    public Security()
    {
    }

    /// <summary>
    /// Creates a new <see cref="Security"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public Security(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Specify whether the RSI Security Server is enabled for the controller. Type 0 if the controller is unsecured;
    /// type a 10-digit, non-zero value if the controller is secured.
    /// </summary>
    public int Code
    {
        get => GetValue<int>();
        set => SetValue(value);
    }

    /// <summary>
    /// ID of the FactoryTalk® Diagnostics to which your controller is bound.
    /// </summary>
    public string? SecurityAuthorityID
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// Network path to the FactoryTalk® Diagnostics to which your controller is bound
    /// </summary>
    public string? SecurityAuthorityURI
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// Name of the set of permissions, configured in FactoryTalk® Security, to apply to this object.
    /// </summary>
    public string? PermissionSet
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// Mask that specifies the controller events that you wish to track.
    /// </summary>
    public string? ChangesToDetect
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// Mask defining the slots through which the Trusted® communication is permitted to the controller.
    /// </summary>
    public string? TrustedSlots
    {
        get => GetValue<string>();
        set => SetValue(value);
    }
}