namespace L5Sharp.Extensions
{
    internal static class InternalExtensions
    {
        /// <summary>
        /// Safely copies a string to a new instance of a string with the same value.
        /// </summary>
        /// <remarks>
        /// This simply adds a null check to allow the method to return null if the string is in fact null.
        /// </remarks>
        /// <param name="str">The string to copy.</param>
        /// <returns>A new instance of the string with the same value.</returns>
        public static string SafeCopy(this string str)
        {
            return str == null ? null : string.Copy(str);
        }
    }
}