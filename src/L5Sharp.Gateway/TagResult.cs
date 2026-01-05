namespace L5Sharp.Gateway;

/// <summary>
/// Represents the result of an operation involving a tag in a PLC system.
/// This enum defines various possible outcomes, encompassing both successful and error states,
/// and provides standardized status codes for handling tag-related operations.
/// </summary>
public enum TagResult
{
    /// <summary>
    /// Indicates that the operation is currently in a pending state. This status is typically used
    /// to represent an operation that has been initiated but is not yet complete or resolved.
    /// </summary>
    Pending = 1,

    /// <summary>
    /// Indicates that the operation completed successfully without any errors or issues.
    /// This status is typically used to represent a fully resolved and successful outcome.
    /// </summary>
    Ok = 0,

    /// <summary>
    /// Indicates that the operation was aborted. This status is generally used to signify that the
    /// operation was intentionally terminated prior to completion, often due to an error or user request.
    /// </summary>
    Abort = -1,

    /// <summary>
    /// Indicates that the configuration provided is invalid or not supported. This status typically
    /// represents an error encountered due to incorrect setup or parameters during an operation.
    /// </summary>
    BadConfig = -2,

    /// <summary>
    /// Indicates that the connection to the PLC has failed.
    /// This can mean that the remote PLC was power cycled, for instance.
    /// </summary>
    BadConnection = -3,

    /// <summary>
    /// The data received from the remote PLC was undecipherable or otherwise not able to be processed.
    /// Can also be returned from a remote system that cannot process the data sent to it.
    /// Indicates that the operation encountered data that is invalid, corrupted, or not in the expected format.
    /// This status is typically used to signal issues related to the integrity or suitability of the data being processed.
    /// </summary>
    BadData = -4,

    /// <summary>
    /// Represents a failure related to a device, indicating that the device
    /// has encountered an issue preventing normal operation or communication.
    /// This status is often used to denote hardware or connectivity problems
    /// originating from the device itself.
    /// </summary>
    BadDevice = -5,

    /// <summary>
    /// Usually returned when the library is unable to connect to a remote system.
    /// Represents a status indicating that a gateway-related issue has occurred. This status is used
    /// to signal errors in communication or data transmission through a gateway, often as a result
    /// of temporary or persistent failures in the network or intermediary systems.
    /// </summary>
    BadGateway = -6,

    /// <summary>
    /// Represents an error status indicating that an invalid or inappropriate parameter was provided to a method or function.
    /// This result is typically returned when input values fail validation or do not meet the required criteria.
    /// </summary>
    BadParam = -7,

    /// <summary>
    /// Represents a failure resulting from receiving an unexpected or malformed reply during an operation.
    /// This status typically indicates an error in communication or data exchange between systems.
    /// </summary>
    BadReply = -8,

    /// <summary>
    /// Represents a general error or failure in the operation where the specific nature of the issue
    /// is undefined or does not fall into other predefined error categories.
    /// </summary>
    BadStatus = -9,

    /// <summary>
    /// Indicates that an error occurred while attempting to close a resource or connection. This status
    /// typically represents a failure during the termination or cleanup process of an operation.
    /// </summary>
    CloseErr = -10,

    /// <summary>
    /// Represents an error that occurred during the creation of a resource or entity. This status
    /// is typically used to indicate a failure when attempting to create a new instance of a tag,
    /// object, or related component.
    /// </summary>
    CreateErr = -11,

    /// <summary>
    /// Represents a result indicating a duplicate entry or condition.
    /// This status is used to signal that the operation encountered a conflict due to
    /// an existing duplicate element or value.
    /// </summary>
    Duplicate = -12,

    /// <summary>
    /// Represents an error encountered during the encoding process. This result typically indicates
    /// a failure due to issues in converting data into the desired encoded format, which could arise
    /// from invalid input, unsupported encoding types, or other internal processing problems.
    /// </summary>
    EncodeErr = -13,

    /// <summary>
    /// Indicates that the operation failed due to an error encountered while attempting to destroy
    /// a mutex. This status is typically used to signal issues in tearing down synchronization primitives.
    /// </summary>
    MutexDestroy = -14,

    /// <summary>
    /// Represents an error state indicating a failure in the initialization of a mutex.
    /// This result is typically used to signify that a mutex could not be successfully created
    /// or initialized, often due to system-level resource or configuration issues.
    /// </summary>
    MutexInit = -15,

    /// <summary>
    /// Indicates a failure to acquire a mutex lock. This status typically signifies that a synchronization
    /// mechanism could not get exclusive access to a resource, potentially due to contention or
    /// other locking constraints.
    /// </summary>
    MutexLock = -16,

    /// <summary>
    /// Represents an error encountered when attempting to unlock a mutex. This status indicates that the
    /// operation to release the mutex was unsuccessful, potentially due to an invalid mutex handle,
    /// improper ownership, or a system-level locking error.
    /// </summary>
    MutexUnlock = -17,

    /// <summary>
    /// Represents a failure where the requested operation is explicitly disallowed. This status
    /// typically indicates that the action attempted is restricted due to security policies,
    /// operational constraints, or insufficient permissions.
    /// </summary>
    NotAllowed = -18,

    /// <summary>
    /// Represents a failure result indicating that the requested tag or resource could not be found.
    /// Typically used when an operation attempts to access a non-existent entity.
    /// </summary>
    NotFound = -19,

    /// <summary>
    /// Represents a state where the requested operation or functionality is not implemented.
    /// This status indicates that the feature or method being invoked is recognized
    /// but has not been developed or made available.
    /// </summary>
    NotImplemented = -20,

    /// <summary>
    /// Indicates that no data was available or provided for the operation. This result is typically
    /// used when an expected data source, input, or response yields no usable content.
    /// </summary>
    NoData = -21,

    /// <summary>
    /// Represents a result where no match was found during the operation. This status is typically
    /// used to indicate that a search, lookup, or pattern matching process did not yield any corresponding result.
    /// </summary>
    NoMatch = -22,

    /// <summary>
    /// Indicates an error resulting from insufficient memory. This status is typically used
    /// to signify that a memory allocation failure has occurred during an operation.
    /// </summary>
    NoMem = -23,

    /// <summary>
    /// Indicates that the operation could not complete due to insufficient system resources.
    /// This result is typically returned when the necessary resources, such as memory or processing
    /// capacity, are unavailable to carry out the requested operation.
    /// </summary>
    NoResources = -24,

    /// <summary>
    /// Indicates that a null pointer was encountered where a valid reference was expected. This result
    /// typically represents an operation failure due to an invalid or null reference being used.
    /// </summary>
    NullPtr = -25,

    /// <summary>
    /// Represents an error that occurs when a resource fails to open. This result typically indicates
    /// that the system encountered an issue in accessing or initializing the requested entity during an open operation.
    /// </summary>
    OpenErr = -26,

    /// <summary>
    /// Indicates that an operation attempted to access data on a tag outside the valid boundaries.
    /// This status is typically returned when an index or position exceeds its allowable range.
    /// </summary>
    OutOfBounds = -27,

    /// <summary>
    /// Represents an error that occurs when a read operation fails. This status is used to indicate
    /// that a problem was encountered while attempting to retrieve data during the operation.
    /// </summary>
    ReadErr = -28,

    /// <summary>
    /// Indicates that the operation failed due to a remote system error. This status suggests that
    /// an issue occurred on a remote device or system involved in the operation, preventing it from completing successfully.
    /// </summary>
    RemoteErr = -29,

    /// <summary>
    /// Represents an error that occurs when a thread creation operation fails. This status is typically
    /// used to indicate that the system encountered an issue while attempting to initialize or create
    /// a new thread within the application. An internal library error. If you see this, it is likely that
    /// everything is about to crash.
    /// </summary>
    ThreadCreate = -30,

    /// <summary>
    /// Indicates that a thread join operation has failed. This status is typically used to represent
    /// an issue encountered when attempting to join a previously spawned thread, such as system-level
    /// errors or resource conflicts. Another internal library error. It is very unlikely that you will see this.
    /// </summary>
    ThreadJoin = -31,

    /// <summary>
    /// Indicates that the operation failed to complete within the allocated time. This status is typically used
    /// to represent scenarios where a timeout occurs due to delays in processing, communication, or resource availability.
    /// </summary>
    Timeout = -32,

    /// <summary>
    /// Indicates that an operation or data exceeds the maximum allowable size or capacity.
    /// This result is typically returned when the input or resource being processed
    /// cannot be handled due to its excessive size.
    /// </summary>
    TooLarge = -33,

    /// <summary>
    /// Indicates that the specified tag result failed because the associated data or value
    /// is smaller than the minimum acceptable size or limit. This status is typically encountered
    /// when input validation or size constraints are breached.
    /// </summary>
    TooSmall = -34,

    /// <summary>
    /// Indicates that the operation failed because it involves functionality or behavior
    /// that is not supported. This status is used to signal attempts to use unsupported features
    /// or operations in the current context or environment.
    /// </summary>
    Unsupported = -35,

    /// <summary>
    /// Represents an error related to the Windows Sockets (Winsock) networking API. This status is typically used
    /// to indicate issues arising from network communication, such as initialization failures or connection errors,
    /// specific to the Winsock layer.
    /// </summary>
    Winsock = -36,

    /// <summary>
    /// Represents a failure that occurs during a write operation. This status is typically used to indicate
    /// errors encountered while attempting to write data, such as disk failures, permission issues, or
    /// other circumstances preventing the completion of the writing process.
    /// </summary>
    WriteErr = -37
}