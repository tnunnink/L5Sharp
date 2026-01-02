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
    /// Indicates that the connection to a required resource or service has failed.
    /// This status is used to represent issues such as network disruptions, unavailable endpoints,
    /// or failure to establish communication with the target system.
    /// </summary>
    BadConnection = -3,

    /// <summary>
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
    /// Represents a status indicating that a gateway-related issue has occurred. This status is used
    /// to signal errors in communication or data transmission through a gateway, often as a result
    /// of temporary or persistent failures in the network or intermediary systems.
    /// </summary>
    BadGateway = -6,

    /// <summary>
    ///
    /// </summary>
    BadParam = -7,

    /// <summary>
    ///
    /// </summary>
    BadReply = -8,

    /// <summary>
    ///
    /// </summary>
    BadStatus = -9,

    /// <summary>
    ///
    /// </summary>
    CloseErr = -10,

    /// <summary>
    ///
    /// </summary>
    CreateErr = -11,

    /// <summary>
    ///
    /// </summary>
    Duplicate = -12,

    /// <summary>
    ///
    /// </summary>
    EncodeErr = -13,

    /// <summary>
    ///
    /// </summary>
    MutexDestroy = -14,

    /// <summary>
    ///
    /// </summary>
    MutexInit = -15,

    /// <summary>
    ///
    /// </summary>
    MutexLock = -16,

    /// <summary>
    ///
    /// </summary>
    MutexUnlock = -17,

    /// <summary>
    ///
    /// </summary>
    NotAllowed = -18,

    /// <summary>
    ///
    /// </summary>
    NotFound = -19,

    /// <summary>
    ///
    /// </summary>
    NotImplemented = -20,

    /// <summary>
    ///
    /// </summary>
    NoData = -21,

    /// <summary>
    ///
    /// </summary>
    NoMatch = -22,

    /// <summary>
    ///
    /// </summary>
    NoMem = -23,

    /// <summary>
    ///
    /// </summary>
    NoResources = -24,

    /// <summary>
    ///
    /// </summary>
    NullPtr = -25,

    /// <summary>
    ///
    /// </summary>
    OpenErr = -26,

    /// <summary>
    ///
    /// </summary>
    OutOfBounds = -27,

    /// <summary>
    ///
    /// </summary>
    ReadErr = -28,

    /// <summary>
    ///
    /// </summary>
    RemoteErr = -29,

    /// <summary>
    ///
    /// </summary>
    ThreadCreate = -30,

    /// <summary>
    ///
    /// </summary>
    ThreadJoin = -31,

    /// <summary>
    ///
    /// </summary>
    Timeout = -32,

    /// <summary>
    ///
    /// </summary>
    TooLarge = -33,

    /// <summary>
    ///
    /// </summary>
    TooSmall = -34,

    /// <summary>
    ///
    /// </summary>
    Unsupported = -35,

    /// <summary>
    ///
    /// </summary>
    Winsock = -36,

    /// <summary>
    ///
    /// </summary>
    WriteErr = -37
}