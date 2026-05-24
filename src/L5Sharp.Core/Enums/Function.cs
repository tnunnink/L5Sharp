namespace L5Sharp.Core;

/// <summary>
/// An enumeration of known Logix built-in functions found within the Logix programming languages.
/// </summary>
public class Function : LogixEnum<Function, string>
{
    private Function(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents the absolute value Logix <see cref="Function"/>.
    /// </summary>
    public static readonly Function Abs = new(nameof(Abs), "ABS");

    /// <summary>
    /// Represents the arc cosine Logix <see cref="Function"/>.
    /// </summary>
    public static readonly Function Acos = new(nameof(Acos), "ACOS");

    /// <summary>
    /// Represents the arc sine Logix <see cref="Function"/>.
    /// </summary>
    public static readonly Function Asin = new(nameof(Asin), "ASIN");

    /// <summary>
    /// Represents the arc tangent Logix <see cref="Function"/>.
    /// </summary>
    public static readonly Function Atan = new(nameof(Atan), "ATAN");

    /// <summary>
    /// Represents the cosine Logix <see cref="Function"/>.
    /// </summary>
    public static readonly Function Cos = new(nameof(Cos), "COS");

    /// <summary>
    /// Represents the radians to degrees Logix <see cref="Function"/>.
    /// </summary>
    public static readonly Function Deg = new(nameof(Deg), "DEG");

    /// <summary>
    /// Represents the natural log Logix <see cref="Function"/>.
    /// </summary>
    public static readonly Function Ln = new(nameof(Ln), "LN");

    /// <summary>
    /// Represents the log base 10 Logix <see cref="Function"/>.
    /// </summary>
    public static readonly Function Log = new(nameof(Log), "LOG");

    /// <summary>
    /// Represents the degrees to radians Logix <see cref="Function"/>.
    /// </summary>
    public static readonly Function Rad = new(nameof(Rad), "RAD");

    /// <summary>
    /// Represents the sine Logix <see cref="Function"/>.
    /// </summary>
    public static readonly Function Sin = new(nameof(Sin), "SIN");

    /// <summary>
    /// Represents the square root Logix <see cref="Function"/>.
    /// </summary>
    public static readonly Function Sqrt = new(nameof(Sqrt), "SQRT");

    /// <summary>
    /// Represents the tangent Logix <see cref="Function"/>.
    /// </summary>
    public static readonly Function Tan = new(nameof(Tan), "TAN");

    /// <summary>
    /// Represents the truncate Logix <see cref="Function"/>.
    /// </summary>
    public static readonly Function Trunc = new(nameof(Trunc), "TRUNC");
}