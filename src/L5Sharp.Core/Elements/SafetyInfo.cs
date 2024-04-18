using System;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A sub element of the <see cref="Controller"/> component that contains properties or configuration
/// related to the controller safety.
/// </summary>
public class SafetyInfo : LogixElement
{
    /// <summary>
    /// Creates a new <see cref="SafetyInfo"/> with default values.
    /// </summary>
    public SafetyInfo() : base(L5XName.SafetyInfo)
    {
        SafetyTagMap = new SafetyTagMap();
    }

    /// <summary>
    /// Creates a new <see cref="SafetyInfo"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public SafetyInfo(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Specifies the safety signature control as defined in the controller properties
    /// </summary>
    /// <value>A <see cref="string"/> representing the safety signature.</value>
    /// <remarks>This value is exported only; it is ignored on import.</remarks>
    public string? SafetySignature
    {
        get => GetValue<string>();
        set => SetValue(value);
    }
    
    /// <summary>
    /// Displays whether the safety controller is locked or not.
    /// </summary>
    /// <value><c>true</c> if the setting is enabled; Otherwise, <c>false</c>.</value>
    /// <remarks>This value is exported only; it is ignored on import.</remarks>
    public bool SafetyLocked
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specifies the lock password in the controller. This value is encrypted on export.
    /// </summary>
    /// <value>A <see cref="string"/> representing the encrypted password.</value>
    public string? SafetyLockPassword
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specifies the unlock password in the controller. This value is encrypted on export.
    /// </summary>
    /// <value>A <see cref="string"/> representing the encrypted password.</value>
    public string? SafetyUnlockPassword
    {
        get => GetValue<string>();
        set => SetValue(value);
    }
    
    /// <summary>
    /// Specify whether to configure safety I/O when replacing safety I/O.
    /// </summary>
    /// <value><c>true</c> if the setting is enabled; Otherwise, <c>false</c>.</value>
    public bool ConfigureSafetyIOAlways
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }
    
    /// <summary>
    /// Indicates whether you can modify the safety signature when in Run mode.
    /// </summary>
    /// <value><c>true</c> if the setting is enabled; Otherwise, <c>false</c>.</value>
    public bool SignatureRunModeProtect
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// The <see cref="SafetyTagMap"/> configuration for the object.
    /// </summary>
    /// <value>A <see cref="SafetyTagMap"/> which is a collection of TagNames configured for the element.</value>
    public SafetyTagMap? SafetyTagMap
    {
        get => GetComplex<SafetyTagMap>();
        set => SetComplex(value);
    }
}