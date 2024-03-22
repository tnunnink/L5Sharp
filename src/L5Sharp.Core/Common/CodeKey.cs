using System;

namespace L5Sharp.Core;

public readonly struct CodeKey : IEquatable<CodeKey>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="container"></param>
    /// <param name="routine"></param>
    /// <param name="number"></param>
    public CodeKey(string? container, string? routine, uint number)
    {
        Container = container ?? string.Empty;
        Routine = routine ?? string.Empty;
        Number = number;
    }

    /// <summary>
    /// The containing program or instruction for the line of code within the project.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> representing the name of the containing program or instruction if attached.
    /// If not attached to an L5X file, this will be an empty string.
    /// </value>
    public string Container { get; }

    /// <summary>
    /// The name of the routine containing the line of code within the project. 
    /// </summary>
    /// <value>
    /// A <see cref="string"/> representing the name of the containing routine if attached.
    /// If not attached to an L5X file, this will be an empty string.
    /// </value>
    public string Routine { get; }

    /// <summary>
    /// The number representing the rung, line, or sheet within the routine where the code exists.
    /// </summary>
    public uint Number { get; }


    /// <inheritdoc />
    public bool Equals(CodeKey other) =>
        Container.IsEquivalent(other.Container) &&
        Routine.IsEquivalent(other.Routine) &&
        Number == other.Number;

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is CodeKey other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() =>
        StringComparer.OrdinalIgnoreCase.GetHashCode(Container) ^
        StringComparer.OrdinalIgnoreCase.GetHashCode(Routine) ^
        Number.GetHashCode();

    /// <summary>
    /// Determines if two objects are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if the objects have the same type and name property; Otherwise, <c>false</c>.</returns>
    public static bool operator ==(CodeKey left, CodeKey right) => Equals(left, right);

    /// <summary>
    /// Determines if two objects are not equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if the objects have the same type and name property; Otherwise, <c>false</c>.</returns>
    public static bool operator !=(CodeKey left, CodeKey right) => !Equals(left, right);

    /// <inheritdoc />
    public override string ToString() => $"{Container}/{Routine}/{Number}";
}