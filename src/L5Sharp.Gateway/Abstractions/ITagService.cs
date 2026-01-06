using System;
using System.Text;

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
    /// <param name="tag">The identifier of the tag to abort the operation on.</param>
    /// <returns>
    /// A status code indicating the result of the abort operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int Abort(int tag);

    /// <summary>
    /// Checks the version of the service or library against the specified major, minor, and patch values.
    /// This method can be used to validate compatibility with expected version numbers.
    /// </summary>
    /// <param name="major">The major version number to check against.</param>
    /// <param name="minor">The minor version number to check against.</param>
    /// <param name="patch">The patch version number to check against.</param>
    /// <returns>
    /// An integer indicating the result of the version check. Returns 0 if the versions match
    /// or a specific error code if there is a mismatch.
    /// </returns>
    int CheckVersion(int major, int minor, int patch);

    /// <summary>
    /// Creates a new tag in the gateway system with the specified path and timeout configuration.
    /// This allows the user to define a tag that can later be interacted with for read, write, or other operations.
    /// </summary>
    /// <param name="path">The string path that uniquely identifies the tag to be created within the gateway system.</param>
    /// <param name="timeout">The maximum time, in milliseconds, to wait for the tag creation to complete before returning a timeout error.</param>
    /// <returns>
    /// A status code indicating the result of the create operation. Returns 0 for success,
    /// or a specific error code representing the failure reason.
    /// </returns>
    int Create(string path, int timeout);

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
    /// Decodes the specified error code into a human-readable string representation.
    /// This provides descriptive information about the error for ease of debugging
    /// or logging.
    /// </summary>
    /// <param name="error">The error code to decode.</param>
    /// <returns>
    /// A string representing the description of the provided error code. If the error
    /// code is not recognized, a generic description may be returned.
    /// </returns>
    string Decode(int error);

    /// <summary>
    /// Reads the data of the specified tag from the gateway system within the provided timeout period.
    /// This function retrieves the most recent value associated with the tag, waiting up to the specified timeout
    /// for the operation to complete successfully or fail.
    /// </summary>
    /// <param name="tag">The identifier of the tag to read the data from.</param>
    /// <param name="timeout">The maximum amount of time, in milliseconds, to wait for the operation to complete.</param>
    /// <returns>
    /// A status code indicating the result of the read operation. A return value of 0 indicates success,
    /// while non-zero values indicate failure or timeout.
    /// </returns>
    int Read(int tag, int timeout);

    /// <summary>
    /// Writes data to the specified tag within the given timeout period.
    /// This operation attempts to transmit the current value of the tag to the connected device.
    /// </summary>
    /// <param name="tag">The identifier of the tag to write data to.</param>
    /// <param name="timeout">The maximum time, in milliseconds, to wait for the write operation to complete.</param>
    /// <returns>
    /// A status code indicating the result of the write operation. Returns 0 for success,
    /// or a specific error code if the operation fails.
    /// </returns>
    int Write(int tag, int timeout);

    /// <summary>
    /// Retrieves the current status of the specified tag. This method queries the tag's state
    /// in the underlying system to determine the result of any ongoing or completed operations.
    /// </summary>
    /// <param name="tag">The identifier of the tag whose status is being requested.</param>
    /// <returns>
    /// A status code indicating the current state of the tag. Returns 0 for success or
    /// a specific error code representing the tag's state or an error in querying it.
    /// </returns>
    int Status(int tag);

    /// <summary>
    /// Registers a callback function for the specified tag. This allows the system to
    /// invoke user-defined logic or handlers when events associated with the tag occur.
    /// </summary>
    /// <param name="tag">The identifier of the tag for which the callback will be registered.</param>
    /// <param name="callback">The callback function to be invoked when events occur for the tag.
    /// Parameters are the tag id (int), event id (int), and status code (int).
    /// </param>
    /// <returns>
    /// A status code indicating the result of the registration process. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int RegisterCallback(int tag, Action<int, int, int> callback);

    /// <summary>
    /// Unregisters a previously registered callback function for the specified tag.
    /// This removes any user-defined event handlers associated with the tag.
    /// </summary>
    /// <param name="tag">The identifier of the tag for which the callback will be unregistered.</param>
    /// <returns>
    /// A status code indicating the result of the unregistration process. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int UnregisterCallback(int tag);

    /// <summary>
    /// Registers a logger to capture and record diagnostic information from the service.
    /// This enables logging of operations, errors, and debug information for troubleshooting purposes.
    /// </summary>
    /// <param name="logger">The callback function to be invoked for logging diagnostic information. 
    /// Parameters are the tag id (int), debug level (int), and log message (string).
    /// </param>
    /// <returns>
    /// A status code indicating the result of the logger registration. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int RegisterLogger(Action<int, int, string> logger);

    /// <summary>
    /// Unregisters the logger associated with the specified tag, disabling diagnostic logging
    /// for that tag's operations.
    /// </summary>
    /// <param name="tag">The identifier of the tag for which the logger will be unregistered.</param>
    /// <returns>
    /// A status code indicating the result of the logger unregistration. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int UnregisterLogger(int tag);

    /// <summary>
    /// Acquires a synchronization lock on the specified tag to prevent concurrent multithreaded access.
    /// This ensures thread-safe operations when reading from or writing to the tag's data.
    /// The lock must be released by calling <see cref="Unlock"/> when the operation is complete.
    /// </summary>
    /// <param name="tag">The identifier of the tag to lock.</param>
    /// <returns>
    /// A status code indicating the result of the lock operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int Lock(int tag);

    /// <summary>
    /// Releases the synchronization lock on the specified tag, allowing other threads to access it.
    /// This should be called after completing operations that required exclusive access to the tag's data.
    /// </summary>
    /// <param name="tag">The identifier of the tag to unlock.</param>
    /// <returns>
    /// A status code indicating the result of the unlock operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int Unlock(int tag);

    /// <summary>
    /// Destroys the specified tag, releasing any associated resources and invalidating its identifier.
    /// This operation makes the tag unavailable for further operations.
    /// </summary>
    /// <param name="tag">The identifier of the tag to be destroyed.</param>
    /// <returns>
    /// A status code indicating the result of the destroy operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int Destroy(int tag);

    /// <summary>
    /// Performs a shutdown operation for the service, ensuring that all associated
    /// resources and operations are properly terminated.
    /// This is typically used for cleanup and releasing resources during application termination.
    /// </summary>
    /// <returns>
    /// A status code indicating the result of the shutdown operation. Returns 0 for success,
    /// or a specific error code if the shutdown fails.
    /// </returns>
    int Shutdown();

    /// <summary>
    /// Configures the debug level for the service. This determines the verbosity
    /// of debug information that will be output by the system.
    /// </summary>
    /// <param name="debugLevel">The desired debug level. Higher values may produce more detailed debug information.</param>
    void SetDebugLevel(int debugLevel);

    /// <summary>
    /// Retrieves the value of a specified attribute associated with a tag.
    /// If the attribute does not exist, a default value is returned.
    /// </summary>
    /// <param name="tag">The identifier of the tag to query.</param>
    /// <param name="attributeName">The name of the attribute to retrieve.</param>
    /// <param name="defaultValue">The default value to return if the attribute is not found.</param>
    /// <returns>
    /// The value of the requested attribute if found, or the specified default value if the attribute does not exist.
    /// </returns>
    int GetAttribute(int tag, string attributeName, int defaultValue);

    /// <summary>
    /// Sets an attribute for the specified tag with a new value.
    /// This operation updates the designated attribute of a tag to the provided value.
    /// </summary>
    /// <param name="tag">The identifier of the tag for which the attribute is being updated.</param>
    /// <param name="attributeName">The name of the attribute to be set.</param>
    /// <param name="newValue">The new value to assign to the specified attribute.</param>
    /// <returns>
    /// A status code indicating the result of the operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int SetAttribute(int tag, string attributeName, int newValue);

    /// <summary>
    /// Retrieves a byte array attribute from the specified tag and copies it into the provided buffer.
    /// This method is used to read binary or raw data attributes associated with a tag.
    /// </summary>
    /// <param name="tag">The identifier of the tag to query.</param>
    /// <param name="attributeName">The name of the byte array attribute to retrieve.</param>
    /// <param name="buffer">The buffer to copy the attribute data into.</param>
    /// <param name="length">The maximum number of bytes to copy into the buffer.</param>
    /// <returns>
    /// A status code indicating the result of the operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int GetByteArrayAttribute(int tag, string attributeName, byte[] buffer, int length);

    /// <summary>
    /// Retrieves the size of the data buffer associated with the specified tag.
    /// This indicates the total number of bytes allocated for the tag's data.
    /// </summary>
    /// <param name="tag">The identifier of the tag whose size is being queried.</param>
    /// <returns>
    /// The size of the tag's data buffer in bytes, or a negative error code on failure.
    /// </returns>
    int GetSize(int tag);

    /// <summary>
    /// Sets the size of the data buffer for the specified tag.
    /// This operation resizes the buffer to accommodate the specified number of bytes.
    /// </summary>
    /// <param name="tag">The identifier of the tag whose size is being set.</param>
    /// <param name="size">The new size of the tag's data buffer in bytes.</param>
    /// <returns>
    /// A status code indicating the result of the operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int SetSize(int tag, int size);

    /// <summary>
    /// Retrieves the value of a specific bit from the tag's data at the specified bit offset.
    /// </summary>
    /// <param name="tag">The identifier of the tag to read from.</param>
    /// <param name="offSetBit">The bit offset within the tag's data buffer.</param>
    /// <returns>
    /// The value of the bit (0 or 1), or a negative error code on failure.
    /// </returns>
    int GetBit(int tag, int offSetBit);

    /// <summary>
    /// Retrieves a signed byte value from the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="tag">The identifier of the tag to read from.</param>
    /// <param name="offSet">The byte offset within the tag's data buffer.</param>
    /// <returns>
    /// The signed byte value at the specified offset.
    /// </returns>
    sbyte GetSByte(int tag, int offSet);

    /// <summary>
    /// Retrieves a 16-bit signed integer value from the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="tag">The identifier of the tag to read from.</param>
    /// <param name="offSet">The byte offset within the tag's data buffer.</param>
    /// <returns>
    /// The 16-bit signed integer value at the specified offset.
    /// </returns>
    short GetShort(int tag, int offSet);

    /// <summary>
    /// Retrieves a 32-bit signed integer value from the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="tag">The identifier of the tag to read from.</param>
    /// <param name="offSet">The byte offset within the tag's data buffer.</param>
    /// <returns>
    /// The 32-bit signed integer value at the specified offset.
    /// </returns>
    int GetInt(int tag, int offSet);

    /// <summary>
    /// Retrieves a 64-bit signed integer value from the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="tag">The identifier of the tag to read from.</param>
    /// <param name="offSet">The byte offset within the tag's data buffer.</param>
    /// <returns>
    /// The 64-bit signed integer value at the specified offset.
    /// </returns>
    long GetLong(int tag, int offSet);

    /// <summary>
    /// Retrieves a single-precision floating-point value from the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="tag">The identifier of the tag to read from.</param>
    /// <param name="offSet">The byte offset within the tag's data buffer.</param>
    /// <returns>
    /// The single-precision floating-point value at the specified offset.
    /// </returns>
    float GetFloat(int tag, int offSet);

    /// <summary>
    /// Retrieves a double-precision floating-point value from the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="tag">The identifier of the tag to read from.</param>
    /// <param name="offSet">The byte offset within the tag's data buffer.</param>
    /// <returns>
    /// The double-precision floating-point value at the specified offset.
    /// </returns>
    double GetDouble(int tag, int offSet);

    /// <summary>
    /// Retrieves an unsigned byte value from the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="tag">The identifier of the tag to read from.</param>
    /// <param name="offSet">The byte offset within the tag's data buffer.</param>
    /// <returns>
    /// The unsigned byte value at the specified offset.
    /// </returns>
    byte GetByte(int tag, int offSet);

    /// <summary>
    /// Retrieves a 16-bit unsigned integer value from the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="tag">The identifier of the tag to read from.</param>
    /// <param name="offSet">The byte offset within the tag's data buffer.</param>
    /// <returns>
    /// The 16-bit unsigned integer value at the specified offset.
    /// </returns>
    ushort GetUShort(int tag, int offSet);

    /// <summary>
    /// Retrieves a 32-bit unsigned integer value from the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="tag">The identifier of the tag to read from.</param>
    /// <param name="offSet">The byte offset within the tag's data buffer.</param>
    /// <returns>
    /// The 32-bit unsigned integer value at the specified offset.
    /// </returns>
    uint GetUInt(int tag, int offSet);

    /// <summary>
    /// Retrieves a 64-bit unsigned integer value from the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="tag">The identifier of the tag to read from.</param>
    /// <param name="offSet">The byte offset within the tag's data buffer.</param>
    /// <returns>
    /// The 64-bit unsigned integer value at the specified offset.
    /// </returns>
    ulong GetULong(int tag, int offSet);

    /// <summary>
    /// Retrieves raw bytes from the tag's data buffer starting at the specified offset
    /// and copies them into the provided buffer.
    /// </summary>
    /// <param name="tag">The identifier of the tag to read from.</param>
    /// <param name="start">The starting byte offset within the tag's data buffer.</param>
    /// <param name="buffer">The buffer to copy the raw bytes into.</param>
    /// <param name="length">The number of bytes to copy.</param>
    /// <returns>
    /// A status code indicating the result of the operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int GetRawBytes(int tag, int start, byte[] buffer, int length);

    /// <summary>
    /// Retrieves the current length of a string stored in the tag's data at the specified offset.
    /// This represents the number of characters currently used in the string.
    /// </summary>
    /// <param name="tag">The identifier of the tag to query.</param>
    /// <param name="offset">The byte offset within the tag's data buffer where the string is located.</param>
    /// <returns>
    /// The length of the string in characters, or a negative error code on failure.
    /// </returns>
    int GetStringLength(int tag, int offset);

    /// <summary>
    /// Retrieves a string from the tag's data at the specified offset and copies it into the provided buffer.
    /// </summary>
    /// <param name="tag">The identifier of the tag to read from.</param>
    /// <param name="offset">The byte offset within the tag's data buffer where the string is located.</param>
    /// <param name="buffer">The StringBuilder buffer to copy the string into.</param>
    /// <param name="length">The maximum number of characters to copy.</param>
    /// <returns>
    /// A status code indicating the result of the operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int GetString(int tag, int offset, StringBuilder buffer, int length);

    /// <summary>
    /// Retrieves the total length of a string stored in the tag's data at the specified offset.
    /// This includes both the string length prefix and the actual string data.
    /// </summary>
    /// <param name="tag">The identifier of the tag to query.</param>
    /// <param name="offset">The byte offset within the tag's data buffer where the string is located.</param>
    /// <returns>
    /// The total length in bytes, or a negative error code on failure.
    /// </returns>
    int GetStringTotalLength(int tag, int offset);

    /// <summary>
    /// Retrieves the maximum capacity of a string stored in the tag's data at the specified offset.
    /// This represents the maximum number of characters the string can hold.
    /// </summary>
    /// <param name="tag">The identifier of the tag to query.</param>
    /// <param name="offset">The byte offset within the tag's data buffer where the string is located.</param>
    /// <returns>
    /// The string capacity in characters, or a negative error code on failure.
    /// </returns>
    int GetStringCapacity(int tag, int offset);

    /// <summary>
    /// Sets the value of a specific bit in the tag's data at the specified bit offset.
    /// </summary>
    /// <param name="tag">The identifier of the tag to modify.</param>
    /// <param name="offSetBit">The bit offset within the tag's data buffer.</param>
    /// <param name="val">The value to set (0 or 1).</param>
    /// <returns>
    /// A status code indicating the result of the operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int SetBit(int tag, int offSetBit, int val);

    /// <summary>
    /// Sets a signed byte value in the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="tag">The identifier of the tag to modify.</param>
    /// <param name="offSet">The byte offset within the tag's data buffer.</param>
    /// <param name="val">The signed byte value to set.</param>
    /// <returns>
    /// A status code indicating the result of the operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int SetSByte(int tag, int offSet, sbyte val);

    /// <summary>
    /// Sets a 16-bit signed integer value in the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="tag">The identifier of the tag to modify.</param>
    /// <param name="offSet">The byte offset within the tag's data buffer.</param>
    /// <param name="val">The 16-bit signed integer value to set.</param>
    /// <returns>
    /// A status code indicating the result of the operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int SetShort(int tag, int offSet, short val);

    /// <summary>
    /// Sets a 32-bit signed integer value in the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="tag">The identifier of the tag to modify.</param>
    /// <param name="offSet">The byte offset within the tag's data buffer.</param>
    /// <param name="val">The 32-bit signed integer value to set.</param>
    /// <returns>
    /// A status code indicating the result of the operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int SetInt(int tag, int offSet, int val);

    /// <summary>
    /// Sets a 64-bit signed integer value in the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="tag">The identifier of the tag to modify.</param>
    /// <param name="offSet">The byte offset within the tag's data buffer.</param>
    /// <param name="val">The 64-bit signed integer value to set.</param>
    /// <returns>
    /// A status code indicating the result of the operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int SetLong(int tag, int offSet, long val);

    /// <summary>
    /// Sets a single-precision floating-point value in the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="tag">The identifier of the tag to modify.</param>
    /// <param name="offSet">The byte offset within the tag's data buffer.</param>
    /// <param name="val">The single-precision floating-point value to set.</param>
    /// <returns>
    /// A status code indicating the result of the operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int SetFloat(int tag, int offSet, float val);

    /// <summary>
    /// Sets a double-precision floating-point value in the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="tag">The identifier of the tag to modify.</param>
    /// <param name="offSet">The byte offset within the tag's data buffer.</param>
    /// <param name="val">The double-precision floating-point value to set.</param>
    /// <returns>
    /// A status code indicating the result of the operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int SetDouble(int tag, int offSet, double val);

    /// <summary>
    /// Sets an unsigned byte value in the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="tag">The identifier of the tag to modify.</param>
    /// <param name="offSet">The byte offset within the tag's data buffer.</param>
    /// <param name="val">The unsigned byte value to set.</param>
    /// <returns>
    /// A status code indicating the result of the operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int SetByte(int tag, int offSet, byte val);

    /// <summary>
    /// Sets a 16-bit unsigned integer value in the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="tag">The identifier of the tag to modify.</param>
    /// <param name="offSet">The byte offset within the tag's data buffer.</param>
    /// <param name="val">The 16-bit unsigned integer value to set.</param>
    /// <returns>
    /// A status code indicating the result of the operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int SetUShort(int tag, int offSet, ushort val);

    /// <summary>
    /// Sets a 32-bit unsigned integer value in the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="tag">The identifier of the tag to modify.</param>
    /// <param name="offSet">The byte offset within the tag's data buffer.</param>
    /// <param name="val">The 32-bit unsigned integer value to set.</param>
    /// <returns>
    /// A status code indicating the result of the operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int SetUInt(int tag, int offSet, uint val);

    /// <summary>
    /// Sets a 64-bit unsigned integer value in the tag's data at the specified byte offset.
    /// </summary>
    /// <param name="tag">The identifier of the tag to modify.</param>
    /// <param name="offSet">The byte offset within the tag's data buffer.</param>
    /// <param name="val">The 64-bit unsigned integer value to set.</param>
    /// <returns>
    /// A status code indicating the result of the operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int SetULong(int tag, int offSet, ulong val);

    /// <summary>
    /// Writes raw bytes from the provided buffer into the tag's data buffer starting at the specified offset.
    /// </summary>
    /// <param name="tag">The identifier of the tag to modify.</param>
    /// <param name="offset">The starting byte offset within the tag's data buffer.</param>
    /// <param name="buffer">The buffer containing the raw bytes to write.</param>
    /// <param name="length">The number of bytes to write from the buffer.</param>
    /// <returns>
    /// A status code indicating the result of the operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int SetRawBytes(int tag, int offset, byte[] buffer, int length);

    /// <summary>
    /// Sets a string value in the tag's data at the specified offset.
    /// The string is written according to the format expected by the underlying system.
    /// </summary>
    /// <param name="tag">The identifier of the tag to modify.</param>
    /// <param name="offset">The byte offset within the tag's data buffer where the string will be written.</param>
    /// <param name="value">The string value to set.</param>
    /// <returns>
    /// A status code indicating the result of the operation. Returns 0 for success,
    /// or a specific error code on failure.
    /// </returns>
    int SetString(int tag, int offset, string value);
}