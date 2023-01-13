﻿using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using L5Sharp.Extensions;

namespace L5Sharp.Core
{
    /// <summary>
    /// Provides a wrapper around the string port address value to indicate what type of address the value is.
    /// </summary>
    public class Address : IEquatable<Address>
    {
        private readonly string _address;

        /// <summary>
        /// Creates a new <see cref="Address"/> with the provided string value.
        /// </summary>
        /// <param name="address"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Address(string address)
        {
            _address = address ?? throw new ArgumentNullException(nameof(address));
        }
        
        /// <summary>
        /// Indicates whether the current <see cref="Address"/> is a slot number address.
        /// </summary>
        /// <returns>true if the address value is a valid byte address; otherwise, false.</returns>
        public bool IsSlot => byte.TryParse(_address, out _);

        /// <summary>
        /// Indicates whether the current <see cref="Address"/> is a IPv4 address.
        /// </summary>
        /// <returns>true if the address value is a valid IPv4 address; otherwise, false.</returns>
        public bool IsIPv4 => _address.Count(c => c == '.') == 3 && IPAddress.TryParse(_address, out _);

        /// <summary>
        /// Indicates whether the current <see cref="Address"/> is a host name address.
        /// </summary>
        /// <returns>true if the address value is a valid host name address; otherwise, false.</returns>
        /// <remarks>
        /// A host name must start with a letter, contain only alpha-numeric characters or special characters '.' and '-',
        /// and have a maximum length of 64 characters.
        /// </remarks>
        public bool IsHostName => Regex.IsMatch(_address, @"^[A-Za-z][A-Za-z0-9\.-]{1,63}$", RegexOptions.Compiled);

        /// <summary>
        /// 
        /// </summary>
        public bool HasAddress => !_address.IsEmpty();

        /// <summary>
        /// Represents no address value, or an empty string.
        /// </summary>
        public static Address None => new(string.Empty);

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
        /// Creates a new <see cref="Address"/> instance from the provided <see cref="IPAddress"/> object.
        /// </summary>
        /// <param name="ipAddress">The <see cref="IPAddress"/> value that represents the port address.</param>
        /// <returns>A new <see cref="Address"/> value from the provided IP.</returns>
        /// <exception cref="ArgumentNullException">ipAddress is null.</exception>
        public static Address FromIP(IPAddress ipAddress)
        {
            if (ipAddress is null)
                throw new ArgumentNullException(nameof(ipAddress));

            return new Address(ipAddress.ToString());
        }
        
        /// <summary>
        /// Creates a new <see cref="Address"/> instance from the provided byte slot number value.
        /// </summary>
        /// <param name="slot">the byte number value that represents the port address.</param>
        /// <returns>A new <see cref="Address"/> value from the provided slot number.</returns>
        public static Address FromSlot(byte slot) => new(slot.ToString());
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public IPAddress ToIPAddress() => IsIPv4
            ? IPAddress.Parse(_address)
            : throw new InvalidOperationException($"The current address '{_address}' is not a valid IP address");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public byte ToSlot() => IsSlot
                ? byte.Parse(_address)
                : throw new InvalidOperationException($"The current address '{_address}' is not a valid slot number");

        /// <inheritdoc />
        public override string ToString() => _address;

        /// <inheritdoc />
        public bool Equals(Address? other)
        {
            if (ReferenceEquals(null, other)) return false;
            return ReferenceEquals(this, other) ||
                   string.Equals(_address, other._address, StringComparison.OrdinalIgnoreCase);
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Address)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode() => _address.GetHashCode();

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
    }
}