namespace L5Sharp.Core;

/// <summary>
/// Represents a builder interface for constructing base or alias tag types of a specified data type or tag name.
/// </summary>
public interface ITagBaseTypeBuilder
{
    /// <summary>
    /// Configures the tag as an atomic tag of the specified type.
    /// </summary>
    /// <typeparam name="TAtomic">
    /// The type of atomic data for the tag. Must inherit from <see cref="AtomicData"/> and have a parameterless constructor.
    /// </typeparam>
    /// <returns>An instance of the <see cref="ITagBaseAtomicBuilder{TAtomic}"/> for further configuration of the atomic tag.</returns>
    ITagBaseAtomicBuilder<TAtomic> AsAtomic<TAtomic>() where TAtomic : AtomicData, new();

    /// <summary>
    /// Configures the tag as a structure tag of the specified data type.
    /// </summary>
    /// <param name="dataType">The name of the structure data type for the tag. If this is a statically defined type, it will
    /// be instantiated with predefined members. Otherwise, we just generate an empty <see cref="ComplexData"/> object
    /// that would require further configuration if needed.</param>
    /// <returns>
    /// An instance of the <see cref="ITagBaseStructureBuilder"/> for further configuration of the structure tag.
    /// </returns>
    ITagBaseStructureBuilder AsStructure(string dataType);

    /// <summary>
    /// Configures the tag as an array of the specified atomic data type with the given dimensions.
    /// </summary>
    /// <typeparam name="TAtomic">
    /// The type of atomic data for the array. Must inherit from <see cref="AtomicData"/>.
    /// </typeparam>
    /// <param name="dimensions">
    /// The dimensions of the array to configure.
    /// </param>
    /// <returns>
    /// An instance of the <see cref="ITagBaseAtomicArrayBuilder{TAtomic}"/> for further configuration of the array tag.
    /// </returns>
    ITagBaseAtomicArrayBuilder<TAtomic> AsArray<TAtomic>(Dimensions dimensions) where TAtomic : AtomicData, new();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dataType"></param>
    /// <param name="dimensions"></param>
    /// <returns></returns>
    ITagBaseStructureArrayBuilder AsArray(string dataType, Dimensions dimensions);

    /// <summary>
    /// Configures the tag as an alias for the specified tag name.
    /// </summary>
    /// <param name="alias">The <see cref="TagName"/> that the tag should alias.</param>
    /// <returns>The current instance of the <see cref="ITagBuilder"/> for further configuration.</returns>
    ITagAliasBuilder AsAlias(TagName alias);
}