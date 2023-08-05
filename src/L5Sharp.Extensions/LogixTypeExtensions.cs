using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using L5Sharp.Common;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Extensions;

/// <summary>
/// A class containing extensions for the <see cref="LogixType"/> object.
/// </summary>
public static class LogixTypeExtensions
{
    /// <summary>
    /// Converts the current one dimensional array of logix type objects to a <see cref="ArrayType{TLogixType}"/>
    /// of the concrete logix type. 
    /// </summary>
    /// <param name="array">The array to convert.</param>
    /// <typeparam name="TLogixType">The logix type of the elements of the array.</typeparam>
    /// <returns>An <see cref="ArrayType{TLogixType}"/> representing the current array containing all the elements
    /// of the array.</returns>
    /// <exception cref="ArgumentNullException"><c>array</c> is null.</exception>
    /// <remarks>
    /// This extension uses reflection and compiled lambda functions to create the concrete generic array type
    /// from the current logix type array, and is used by logix type for implicitly converting arrays to a logix type.
    /// </remarks>
    public static ArrayType<TLogixType> ToArrayType<TLogixType>(this IEnumerable<TLogixType> array)
        where TLogixType : LogixType
    {
        if (array is null) throw new ArgumentNullException(nameof(array));
        var arrayType = typeof(ArrayType<>).MakeGenericType(typeof(TLogixType));
        var parameterType = typeof(TLogixType[]);
        var constructor = arrayType.GetConstructor(new[] { parameterType })!;
        var parameter = Expression.Parameter(parameterType, "array");
        var creator = Expression.New(constructor, parameter);
        var lambda = Expression.Lambda<Func<TLogixType[], ArrayType<TLogixType>>>(creator, parameter);
        var func = lambda.Compile();
        return func.Invoke(array.ToArray());
    }
    
    /// <summary>
    /// Traverses the type/member hierarchy of the <see cref="StructureType"/> data and builds a collection of
    /// <see cref="DataType"/> objects based on all the user defined types in the tree.
    /// </summary>
    /// <param name="type">The structure type for which to generate a list of user defined type objects.</param>
    /// <returns>A <see cref="IEnumerable{T}"/> containing a <see cref="DataType"/> for each user type in the
    /// structure's type/member hierarchy</returns>
    public static IEnumerable<DataType> ToUDT(this StructureType type)
    {
        var results = new List<DataType>();

        if (type.Class == DataTypeClass.User)
        {
            var userType = new DataType
            {
                Name = type.Name,
                Class = type.Class,
                Family = type.Family,
                Members = new LogixContainer<DataTypeMember>(type.Members.Select(m => new DataTypeMember
                {
                    Name = m.Name,
                    DataType = m.DataType.Name,
                    Dimension = m.DataType is ArrayType array ? array.Dimensions : Dimensions.Empty,
                    Radix = Radix.Default(m.DataType),
                    ExternalAccess = ExternalAccess.ReadWrite
                }))
            };

            results.Add(userType);
        }

        foreach (var member in type.Members)
            if (member.DataType is ComplexType structureType)
                results.AddRange(structureType.ToUDT());

        return results;
    }
}