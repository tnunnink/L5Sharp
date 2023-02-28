namespace L5Sharp.Types
{
    /// <summary>
    /// A static helper class for <see cref="ILogixType"/> objects.
    /// </summary>
    public static class LogixType
    {
        /// <summary>
        /// Returns the singleton null <see cref="ILogixType"/> object.
        /// </summary>
        public static ILogixType Null => NullType.Instance;
    }
}