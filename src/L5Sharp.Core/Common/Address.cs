using System;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace L5Sharp.Core;

/// <summary>
/// Provides a wrapper around the string port address value to indicate what type of address the value is.
/// </summary>
public class Address
{
    private static readonly Regex HostNamePattern = new("^[A-Za-z][A-Za-z0-9.-]{1,63}$");
    private readonly string _value;

    /// <summary>
    /// Creates a new <see cref="Address"/> with the provided string value.
    /// </summary>
    /// <param name="address"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public Address(string address)
    {
        if (string.IsNullOrEmpty(address))
            throw new ArgumentException("Can not create address from null or empty value.");

        _value = address;
    }

    /// <summary>
    /// Indicates whether the current <see cref="Address"/> is a slot number address.
    /// </summary>
    /// <returns>true if the address value is a valid byte address; otherwise, false.</returns>
    public bool IsSlot => byte.TryParse(_value, out _);

    /// <summary>
    /// Indicates whether the current <see cref="Address"/> represents a valid IPv4 address.
    /// </summary>
    /// <returns>true if the address value is a valid IPv4 address; otherwise, false.</returns>
    public bool IsIP => IsIPv4(_value);

    /// <summary>
    /// Indicates whether the current <see cref="Address"/> represents a network address, such as an IPv4 or hostname address.
    /// </summary>
    /// <returns>true if the address value is either a valid IPv4 or matches a host name pattern; otherwise, false.</returns>
    public bool IsNetwork => IsIPv4(_value) || IsHostName(_value);

    /// <summary>
    /// Creates a new <see cref="Address"/> instance with the provided IP address or a default IP address if none is specified.
    /// </summary>
    /// <param name="ip">The optional IP address string to initialize the <see cref="Address"/>. Defaults to "192.168.0.1" if not provided.</param>
    /// <returns>A new instance of <see cref="Address"/> initialized with the specified or default IP address.</returns>
    public static Address NewIP(string? ip = null) => ip is not null ? new Address(ip) : new Address("192.168.0.1");

    /// <summary>
    /// /
    /// </summary>
    /// <param name="slot"></param>
    /// <returns></returns>
    public static Address NewSlot(byte slot = 0) => new(slot.ToString());

    /// <summary>
    /// Parses a string into a <see cref="Address"/> value.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>The <see cref="Address"/> representing the parsed value.</returns>
    public static Address Parse(string value) => new(value);

    /// <summary>
    /// Attempts to parse the specified string value into an <see cref="Address"/> object.
    /// </summary>
    /// <param name="value">The string value to parse into an <see cref="Address"/>.</param>
    /// <param name="address">
    /// When this method returns, contains the parsed <see cref="Address"/> object if the conversion succeeded,
    /// or null if the conversion failed.
    /// </param>
    /// <returns>
    /// <c>true</c> if the string value was successfully parsed into an <see cref="Address"/>; otherwise, <c>false</c>.
    /// </returns>
    public static bool TryParse(string? value, out Address address)
    {
        if (value is null || value.IsEmpty())
        {
            address = null!;
            return false;
        }
        
        address = new Address(value);
        return true;
    }

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
    /// Determines if the provided string represents a valid IPv4 address.
    /// </summary>
    private static bool IsIPv4(string value)
    {
        return value.Contains('.') 
               && IPAddress.TryParse(value, out var ip) 
               && ip.AddressFamily == AddressFamily.InterNetwork;
    }

    /// <summary>
    /// Determines if the provided string value is a valid host name.
    /// </summary>
    private static bool IsHostName(string value)
    {
        return HostNamePattern.IsMatch(value);
    }

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

    /// <summary>
    /// Converts the current <see cref="IPAddress"/> to a <see cref="Address"/> value.
    /// </summary>
    /// <param name="address">The value to convert.</param>
    /// <returns>A <see cref="Address"/> representing the address value.</returns>
    public static implicit operator Address(IPAddress address) => new(address.ToString());
}