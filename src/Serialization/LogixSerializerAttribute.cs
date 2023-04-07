using System;
using System.Linq;

namespace L5Sharp.Serialization;

/// <summary>
/// An custom <see cref="Attribute"/> that allows the user to specify a type implementing
/// <see cref="ILogixSerializer{T}"/> that is to be used for L5X serialization/deserialization.
/// </summary>
public class LogixSerializerAttribute : Attribute
{
    /// <summary>
    /// Initializes a <see cref="LogixSerializerAttribute"/> with the specified serializer type.
    /// </summary>
    /// <param name="serializerType">The <see cref="ILogixSerializer{T}"/> type.</param>
    /// <exception cref="ArgumentNullException"><c>serializerType</c> is null.</exception>
    /// <exception cref="ArgumentException"><c>serializerType</c> does not implement <see cref="ILogixSerializer{T}"/>.</exception>
    /// <exception cref="InvalidOperationException"><c>serializerType</c> does not have a default parameterless constructor.</exception>
    public LogixSerializerAttribute(Type serializerType)
    {
        if (serializerType is null)
            throw new ArgumentNullException(nameof(serializerType));

        if (!serializerType.GetInterfaces()
                .Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ILogixSerializer<>)))
            throw new ArgumentException(
                $"The specified type {serializerType} does not implement {typeof(ILogixSerializer<>)}");

        if (serializerType.GetConstructor(Type.EmptyTypes) is null)
            throw new InvalidOperationException(
                $"The specified serializer {serializerType} does not have a parameterless constructor.");

        SerializerType = serializerType;
    }

    /// <summary>
    /// The <see cref="ILogixSerializer{T}"/> type.
    /// </summary>
    public Type SerializerType { get; }
}