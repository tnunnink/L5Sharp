using L5Sharp.Components;

namespace L5Sharp.Enums;

/// <summary>
/// An enumeration of all <see cref="PassThroughOption"/> for a Logix <see cref="Controller"/>. 
/// </summary>
public class PassThroughOption : LogixEnum<PassThroughOption, string>
{
    private PassThroughOption(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents a <b>Disabled</b> <see cref="PassThroughOption"/>.
    /// </summary>
    public static readonly PassThroughOption Disabled = new(nameof(Disabled), nameof(Disabled));

    /// <summary>
    /// Represents a <b>Enabled</b> <see cref="PassThroughOption"/>.
    /// </summary>
    public static readonly PassThroughOption Enabled = new(nameof(Enabled), nameof(Enabled));

    /// <summary>
    /// Represents a <b>EnabledWithAppend</b> <see cref="PassThroughOption"/>.
    /// </summary>
    public static readonly PassThroughOption EnabledWithAppend =
        new(nameof(EnabledWithAppend), nameof(EnabledWithAppend));
}