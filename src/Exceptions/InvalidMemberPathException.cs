using L5Sharp.Core;

namespace L5Sharp.Exceptions
{
    /// <summary>
    /// An exception that is thrown when a specified <see cref="TagName"/> path does not exist for a given
    /// <see cref="ITagMember{TDataType}"/>.  
    /// </summary>
    public class InvalidMemberPathException : LogixException
    {
        /// <summary>
        /// Creates a new <see cref="InvalidMemberPathException"/> with the provided tag name and data type name.
        /// </summary>
        /// <param name="tagName">The tag name that represents the path that does not exists.</param>
        /// <param name="dataType">The name of the data type for which the tag name is invalid.</param>
        public InvalidMemberPathException(TagName tagName, string dataType) : base(
            $"The tag name '{tagName}' is not a valid member path for type '{dataType}'.")
        {
            TagName = tagName;
            DataType = dataType;
        }

        /// <summary>
        /// The tag name that is invalid or does not exists for the given data type.
        /// </summary>
        public TagName TagName { get; }

        /// <summary>
        /// The name of the data type for which the specified tag name is not a valid member path.
        /// </summary>
        public string DataType { get; }
    }
}