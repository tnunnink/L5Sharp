using System;

namespace L5Sharp.Exceptions
{
    /// <summary>
    /// An exception that is thrown when the provided L5X document is not a valid document.
    /// </summary>
    public class L5XParseException : Exception
    {
        internal L5XParseException(string message, string fileName, Exception xmlException) : base(
            $"The L5X file {fileName} is not valid. {message}", xmlException)
        {
        }
    }
}