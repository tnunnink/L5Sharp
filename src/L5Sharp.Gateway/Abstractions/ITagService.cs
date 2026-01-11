using System;

namespace L5Sharp.Gateway.Abstractions;

/// <summary>
/// Defines a service for managing and interacting with tags in a gateway system.
/// This mimics the API of the underlying libplctag.NativeImport. We are wrapping this API so that we can
/// test library (or external) code without having an actual PLC to connect 
/// </summary>
public interface ITagService
{
    /// <summary>
    /// Aborts any ongoing operation on the specified tag. This halts the current
    /// action being performed on the tag, such as a read or write, and leaves the tag
    /// in an undefined state.
    /// </summary>
    /// <param name="handle">The identifier of the tag to abort the operation on.</param>
    /// <returns>
    /// A status code indicating the result of the abort operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int Abort(int handle);

    /// <summary>
    /// Creates a new tag with the specified configuration, allows optional user data to be associated,
    /// and specifies the timeout for the creation process.
    /// </summary>
    /// <param name="path">The configuration string describing the path and attributes of the tag.</param>
    /// <param name="callback">A callback function that is invoked when asynchronous events occur for the tag.
    /// Parameters are the tag id (int), event id (int), and status code (int), and user data memory pointer (IntPtr)
    /// </param>
    /// <param name="userData">A pointer to user-defined data that is associated with the tag during its creation.</param>
    /// <param name="timeout">The time, in milliseconds, to wait for the tag creation to complete before timing out.</param>
    /// <returns>
    /// A tag identifier that can be used to perform further operations on the created tag.
    /// </returns>
    int Create(string path, Action<int, int, int, IntPtr> callback, IntPtr userData, int timeout);

    /// <summary>
    /// Reads the data of the specified tag from the gateway system within the provided timeout period.
    /// This function retrieves the most recent value associated with the tag, waiting up to the specified timeout
    /// for the operation to complete successfully or fail.
    /// </summary>
    /// <param name="handle">The identifier of the tag to read the data from.</param>
    /// <param name="timeout">The maximum amount of time, in milliseconds, to wait for the operation to complete.</param>
    /// <returns>
    /// A status code indicating the result of the read operation. A return value of 0 indicates success,
    /// while non-zero values indicate failure or timeout.
    /// </returns>
    int Read(int handle, int timeout);

    /// <summary>
    /// Writes data to the specified tag within the given timeout period.
    /// This operation attempts to transmit the current value of the tag to the connected device.
    /// </summary>
    /// <param name="handle">The identifier of the tag to write data to.</param>
    /// <param name="timeout">The maximum time, in milliseconds, to wait for the write operation to complete.</param>
    /// <returns>
    /// A status code indicating the result of the write operation. Returns 0 for success,
    /// or a specific error code if the operation fails.
    /// </returns>
    int Write(int handle, int timeout);

    /// <summary>
    /// Retrieves the current status of the specified tag. This method queries the tag's state
    /// in the underlying system to determine the result of any ongoing or completed operations.
    /// </summary>
    /// <param name="handle">The identifier of the tag whose status is being requested.</param>
    /// <returns>
    /// A status code indicating the current state of the tag. Returns 0 for success or
    /// a specific error code representing the tag's state or an error in querying it.
    /// </returns>
    int Status(int handle);

    /// <summary>
    /// Destroys the specified tag, releasing any associated resources and invalidating its identifier.
    /// This operation makes the tag unavailable for further operations.
    /// </summary>
    /// <param name="handle">The identifier of the tag to be destroyed.</param>
    /// <returns>
    /// A status code indicating the result of the destroy operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int Destroy(int handle);

    /// <summary>
    /// Sets an attribute for the specified tag with a new value.
    /// This operation updates the designated attribute of a tag to the provided value.
    /// </summary>
    /// <param name="handle">The identifier of the tag for which the attribute is being updated.</param>
    /// <param name="attributeName">The name of the attribute to be set.</param>
    /// <param name="newValue">The new value to assign to the specified attribute.</param>
    /// <returns>
    /// A status code indicating the result of the operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int SetAttribute(int handle, string attributeName, int newValue);

    /// <summary>
    /// Retrieves a signed byte value from the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="handle">The identifier of the tag to read from.</param>
    /// <param name="offset">The byte offset within the tag's data buffer.</param>
    /// <returns>
    /// The signed byte value at the specified offset.
    /// </returns>
    sbyte GetSByte(int handle, int offset);

    /// <summary>
    /// Retrieves a 16-bit signed integer value from the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="handle">The identifier of the tag to read from.</param>
    /// <param name="offset">The byte offset within the tag's data buffer.</param>
    /// <returns>
    /// The 16-bit signed integer value at the specified offset.
    /// </returns>
    short GetShort(int handle, int offset);

    /// <summary>
    /// Retrieves a 32-bit signed integer value from the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="handle">The identifier of the tag to read from.</param>
    /// <param name="offset">The byte offset within the tag's data buffer.</param>
    /// <returns>
    /// The 32-bit signed integer value at the specified offset.
    /// </returns>
    int GetInt(int handle, int offset);

    /// <summary>
    /// Retrieves a 64-bit signed integer value from the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="handle">The identifier of the tag to read from.</param>
    /// <param name="offset">The byte offset within the tag's data buffer.</param>
    /// <returns>
    /// The 64-bit signed integer value at the specified offset.
    /// </returns>
    long GetLong(int handle, int offset);

    /// <summary>
    /// Retrieves a single-precision floating-point value from the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="handle">The identifier of the tag to read from.</param>
    /// <param name="offset">The byte offset within the tag's data buffer.</param>
    /// <returns>
    /// The single-precision floating-point value at the specified offset.
    /// </returns>
    float GetFloat(int handle, int offset);

    /// <summary>
    /// Retrieves a double-precision floating-point value from the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="handle">The identifier of the tag to read from.</param>
    /// <param name="offset">The byte offset within the tag's data buffer.</param>
    /// <returns>
    /// The double-precision floating-point value at the specified offset.
    /// </returns>
    double GetDouble(int handle, int offset);

    /// <summary>
    /// Retrieves an unsigned byte value from the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="handle">The identifier of the tag to read from.</param>
    /// <param name="offset">The byte offset within the tag's data buffer.</param>
    /// <returns>
    /// The unsigned byte value at the specified offset.
    /// </returns>
    byte GetByte(int handle, int offset);

    /// <summary>
    /// Retrieves a 16-bit unsigned integer value from the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="handle">The identifier of the tag to read from.</param>
    /// <param name="offset">The byte offset within the tag's data buffer.</param>
    /// <returns>
    /// The 16-bit unsigned integer value at the specified offset.
    /// </returns>
    ushort GetUShort(int handle, int offset);

    /// <summary>
    /// Retrieves a 32-bit unsigned integer value from the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="handle">The identifier of the tag to read from.</param>
    /// <param name="offset">The byte offset within the tag's data buffer.</param>
    /// <returns>
    /// The 32-bit unsigned integer value at the specified offset.
    /// </returns>
    uint GetUInt(int handle, int offset);

    /// <summary>
    /// Retrieves a 64-bit unsigned integer value from the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="handle">The identifier of the tag to read from.</param>
    /// <param name="offset">The byte offset within the tag's data buffer.</param>
    /// <returns>
    /// The 64-bit unsigned integer value at the specified offset.
    /// </returns>
    ulong GetULong(int handle, int offset);

    /// <summary>
    /// Retrieves a string from the tag's data at the specified offset and copies it into the provided buffer.
    /// </summary>
    /// <param name="handle">The identifier of the tag to read from.</param>
    /// <param name="offset">The byte offset within the tag's data buffer where the string is located.</param>
    /// <returns>
    /// A status code indicating the result of the operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    string GetString(int handle, int offset);

    /// <summary>
    /// Sets a signed byte value in the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="handle">The identifier of the tag to modify.</param>
    /// <param name="offset">The byte offset within the tag's data buffer.</param>
    /// <param name="value">The signed byte value to set.</param>
    /// <returns>
    /// A status code indicating the result of the operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int SetSByte(int handle, int offset, sbyte value);

    /// <summary>
    /// Sets a 16-bit signed integer value in the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="handle">The identifier of the tag to modify.</param>
    /// <param name="offset">The byte offset within the tag's data buffer.</param>
    /// <param name="value">The 16-bit signed integer value to set.</param>
    /// <returns>
    /// A status code indicating the result of the operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int SetShort(int handle, int offset, short value);

    /// <summary>
    /// Sets a 32-bit signed integer value in the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="handle">The identifier of the tag to modify.</param>
    /// <param name="offset">The byte offset within the tag's data buffer.</param>
    /// <param name="value">The 32-bit signed integer value to set.</param>
    /// <returns>
    /// A status code indicating the result of the operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int SetInt(int handle, int offset, int value);

    /// <summary>
    /// Sets a 64-bit signed integer value in the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="handle">The identifier of the tag to modify.</param>
    /// <param name="offset">The byte offset within the tag's data buffer.</param>
    /// <param name="value">The 64-bit signed integer value to set.</param>
    /// <returns>
    /// A status code indicating the result of the operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int SetLong(int handle, int offset, long value);

    /// <summary>
    /// Sets a single-precision floating-point value in the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="handle">The identifier of the tag to modify.</param>
    /// <param name="offset">The byte offset within the tag's data buffer.</param>
    /// <param name="value">The single-precision floating-point value to set.</param>
    /// <returns>
    /// A status code indicating the result of the operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int SetFloat(int handle, int offset, float value);

    /// <summary>
    /// Sets a double-precision floating-point value in the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="handle">The identifier of the tag to modify.</param>
    /// <param name="offset">The byte offset within the tag's data buffer.</param>
    /// <param name="value">The double-precision floating-point value to set.</param>
    /// <returns>
    /// A status code indicating the result of the operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int SetDouble(int handle, int offset, double value);

    /// <summary>
    /// Sets an unsigned byte value in the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="handle">The identifier of the tag to modify.</param>
    /// <param name="offset">The byte offset within the tag's data buffer.</param>
    /// <param name="value">The unsigned byte value to set.</param>
    /// <returns>
    /// A status code indicating the result of the operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int SetByte(int handle, int offset, byte value);

    /// <summary>
    /// Sets a 16-bit unsigned integer value in the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="handle">The identifier of the tag to modify.</param>
    /// <param name="offset">The byte offset within the tag's data buffer.</param>
    /// <param name="value">The 16-bit unsigned integer value to set.</param>
    /// <returns>
    /// A status code indicating the result of the operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int SetUShort(int handle, int offset, ushort value);

    /// <summary>
    /// Sets a 32-bit unsigned integer value in the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="handle">The identifier of the tag to modify.</param>
    /// <param name="offset">The byte offset within the tag's data buffer.</param>
    /// <param name="value">The 32-bit unsigned integer value to set.</param>
    /// <returns>
    /// A status code indicating the result of the operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int SetUInt(int handle, int offset, uint value);

    /// <summary>
    /// Sets a 64-bit unsigned integer value in the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="handle">The identifier of the tag to modify.</param>
    /// <param name="offset">The byte offset within the tag's data buffer.</param>
    /// <param name="value">The 64-bit unsigned integer value to set.</param>
    /// <returns>
    /// A status code indicating the result of the operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int SetULong(int handle, int offset, ulong value);

    /// <summary>
    /// Sets a string value in the tag's data at the specified offset.
    /// The string is written according to the format expected by the underlying system.
    /// </summary>
    /// <param name="handle">The identifier of the tag to modify.</param>
    /// <param name="offset">The byte offset within the tag's data buffer where the string will be written.</param>
    /// <param name="value">The string value to set.</param>
    /// <returns>
    /// A status code indicating the result of the operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int SetString(int handle, int offset, string value);
}