using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace L5Sharp.Core;

/// <summary>
/// Provides a wrapper around the string port address value to indicate what type of address the value is.
/// </summary>
public class Address : ILogixParsable<Address>
{
    private readonly string _value;
    private static readonly Regex HostNamePattern = new("^[A-Za-z][A-Za-z0-9.-]{1,63}$");

    /// <summary>
    /// Creates a new <see cref="Address"/> with the provided string value.
    /// </summary>
    /// <param name="address"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public Address(string address)
    {
        _value = address ?? throw new ArgumentNullException(nameof(address));
    }

    /// <summary>
    /// Indicates whether the current <see cref="Address"/> is a slot number address.
    /// </summary>
    /// <returns>true if the address value is a valid byte address; otherwise, false.</returns>
    public bool IsSlot => byte.TryParse(_value, out _);

    /// <summary>
    /// Indicates whether the current <see cref="Address"/> is a IPv4 address.
    /// </summary>
    /// <returns>true if the address value is a valid IPv4 address; otherwise, false.</returns>
    public bool IsIPv4 => _value.Count(c => c == '.') == 3 && IPAddress.TryParse(_value, out _);

    /// <summary>
    /// Indicates whether the current <see cref="Address"/> is a host name address.
    /// </summary>
    /// <returns>true if the address value is a valid host name address; otherwise, false.</returns>
    /// <remarks>
    /// A host name must start with a letter, contain only alphanumeric characters or special characters '.' and '-',
    /// and have a maximum length of 64 characters.
    /// </remarks>
    public bool IsHostName => HostNamePattern.IsMatch(_value);

    /// <summary>
    /// Indicates that the address value is an empty string.
    /// </summary>
    public bool IsEmpty => _value.IsEmpty();

    /// <summary>
    /// Represents no address value or an empty string.
    /// </summary>
    public static Address None => new(string.Empty);

    /// <summary>
    /// Creates a new <see cref="Address"/> with the optional IP address value.
    /// </summary>
    /// <returns>A <see cref="Address"/> with the default IP value.</returns>
    public static Address IP(string? ip = null) => ip is not null ? new Address(ip) : new Address("192.168.0.1");

    /// <summary>
    /// Creates a new <see cref="Address"/> with the optional slot number.
    /// </summary>
    /// <param name="slot">The slot number to create. If not provided will default to 0.</param>
    /// <returns>A <see cref="Address"/> representing a slot number within a chassis.</returns>
    public static Address Slot(byte slot = 0) => new(slot.ToString());

    /// <summary>
    /// Parses a string into a <see cref="Address"/> value.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>The <see cref="Address"/> representing the parsed value.</returns>
    public static Address Parse(string value) => new(value);

    /// <summary>
    /// Tries to parse a string into a <see cref="Address"/> value.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>An <see cref="Address"/> representing the parsed value if successful; Otherwise, <see cref="None"/>.</returns>
    public static Address TryParse(string? value) => value is null || value.IsEmpty() ? None : new Address(value);

    /// <summary>
    /// Converts the address value to an IPAddress object.
    /// </summary>
    /// <returns>An <see cref="IPAddress"/> object representing the address.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the address value is not a valid IP address.</exception>
    public IPAddress ToIPAddress()
    {
        if (!IPAddress.TryParse(_value, out var ip))
            throw new InvalidOperationException($"The current address '{_value}' is not a valid IP address");

        return ip;
    }

    /// <summary>
    /// Converts the current address to a slot number.
    /// </summary>
    /// <returns>The slot number of the address.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the address is not a valid slot number.</exception>
    public byte ToSlot()
    {
        if (!byte.TryParse(_value, out var slot))
            throw new InvalidOperationException($"The current address '{_value}' is not a valid slot number");

        return slot;
    }

    /// <inheritdoc />
    public override string ToString() => _value;

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            Address other => string.Equals(_value, other._value, StringComparison.OrdinalIgnoreCase),
            string other => string.Equals(_value, other, StringComparison.OrdinalIgnoreCase),
            byte other => string.Equals(_value, other.ToString(), StringComparison.OrdinalIgnoreCase),
            IPAddress other => string.Equals(_value, other.ToString(), StringComparison.OrdinalIgnoreCase),
            _ => false
        };
    }

    /// <inheritdoc />
    public override int GetHashCode() => _value.GetHashCode();

    /// <summary>
    /// Determines if the provided objects are equal.
    /// </summary>
    /// <param name="left">An object to compare.</param>
    /// <param name="right">An object to compare.</param>
    /// <returns>true if the provided objects are equal; otherwise, false.</returns>
    public static bool operator ==(Address? left, Address? right) => Equals(left, right);

    /// <summary>
    /// Determines if the provided objects are not equal.
    /// </summary>
    /// <param name="left">An object to compare.</param>
    /// <param name="right">An object to compare.</param>
    /// <returns>true if the provided objects are not equal; otherwise, false.</returns>
    public static bool operator !=(Address? left, Address? right) => !Equals(left, right);

    /// <summary>
    /// Converts the current <see cref="string"/> to a <see cref="Address"/> value.
    /// </summary>
    /// <param name="address">The value to convert.</param>
    /// <returns>A <see cref="Address"/> representing the address value.</returns>
    public static implicit operator Address(string address) => new(address);

    /// <summary>
    /// Converts the current <see cref="Address"/> to a <see cref="string"/> value.
    /// </summary>
    /// <param name="address">The value to convert.</param>
    /// <returns>A <see cref="string"/> representing the address value.</returns>
    public static implicit operator string(Address address) => address.ToString();

    /// <summary>
    /// Converts the current <see cref="string"/> to a <see cref="Address"/> value.
    /// </summary>
    /// <param name="address">The value to convert.</param>
    /// <returns>A <see cref="Address"/> representing the address value.</returns>
    public static implicit operator Address(byte address) => new(address.ToString());

    /// <summary>
    /// Converts the current <see cref="Address"/> to a <see cref="string"/> value.
    /// </summary>
    /// <param name="address">The value to convert.</param>
    /// <returns>A <see cref="string"/> representing the address value.</returns>
    public static implicit operator byte(Address address) => byte.Parse(address._value);
}