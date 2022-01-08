namespace L5Sharp
{
    /// <summary>
    /// Defines a method for instantiating new instance of a given data type. 
    /// </summary>
    /// <typeparam name="T">The type to instantiate.</typeparam>
    /// <remarks>
    /// This interface is meant to to allow a class to create new copies of itself.
    /// This is not meant to be a function that creates an exact deep or shallow copy, where all of the object members
    /// have equal value to the original instance, but rather a function that generates new instances of the specified
    /// type. The new instance, or any of it's members, should not refer to the original instance
    /// (it should not have referential equality), nor should it necessarily have value equality (although some members
    /// be have equal values). This method should in effect be like calling the default parameterless constructor
    /// of a given class.
    /// </remarks>
    public interface IInstantiable<out T>
    {
        /// <summary>
        /// Creates a new default instance of the specified type.
        /// </summary>
        /// <returns>A new instance of the specified type with default values.</returns>
        T Instantiate();
    }
}