using L5Sharp.Extensions;

namespace L5Sharp.Components;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TLogixType"></typeparam>
public sealed class Tag<TLogixType> : Tag where TLogixType : LogixType
{
    /// <summary>
    /// The value of the <c>TagMember</c> data.
    /// </summary>
    /// <value>A <see cref="LogixType"/> representing the value of the <c>Tag</c>.</value>
    public new TLogixType Value
    {
        get => base.Value.To<TLogixType>();
        set => base.Value = value;
    }
}