using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// The base class for all logix type classes, which represent the value or data structure of a logix tag component.
/// </summary>
/// <remarks>
/// <para>
/// <see cref="LogixData"/> is a special type of <see cref="LogixElement"/> which models the tag data structure found
/// in L5X files. This class contains a <see cref="Name"/> and <see cref="Members"/> which comprise the hierarchy of
/// data for a given type. This class has built in conversion for implicitly converting .NET primitives and collections
/// (arrays and dictionaries) to the corresponding <see cref="LogixData"/> derived class.
/// </para>
/// <para>
/// The serialization implemented in the library will always attempt to instantiate the concrete type of a
/// given <see cref="LogixData"/>. For example, a TIMER L5X data structure is always deserialized as the concrete
/// <c>TIMER</c> class, so that the user can cast the type and manipulate the structure statically and compile time.
/// This applies for <see cref="AtomicData"/>, <see cref="ArrayData"/>, and all derived instance of <see cref="StructureData"/>.
/// </para>
/// <para>
/// Is you wish to create in memory complex data structures, use the <see cref="ComplexData"/> class which exposes
/// methods for adding, removing, replacing, and inserting <see cref="Core.Member"/> objects for the data structure.
/// </para>
/// </remarks>
/// <seealso cref="AtomicData"/>
/// <seealso cref="StructureData"/>
/// <seealso cref="ArrayData"/>
/// <seealso cref="StringData"/>
/// <seealso cref="NullData"/>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public abstract class LogixData : LogixElement
{
    /// <inheritdoc />
    protected LogixData(string name) : base(name)
    {
    }

    /// <inheritdoc />
    protected LogixData(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The type name of the logix data.
    /// </summary>
    /// <value>A <see cref="string"/> name identifying the data type of the data object.</value>
    public virtual string Name => Element.DataType() ?? throw Element.L5XError(L5XName.DataType);

    /// <summary>
    /// The collection of <see cref="Core.Member"/> objects that make up the structure of the data.
    /// </summary>
    /// <value>A <see cref="IEnumerable{T}"/> containing <see cref="Core.Member"/> objects.</value>
    /// <remarks>
    /// Complex data structures such as <see cref="StructureData"/> and <see cref="ArrayData{TLogixType}"/> will return
    /// members. <see cref="AtomicData"/> will not return the bit members since they are not present in the underlying
    /// XML and having them would exponentially increase the number of members a given tags has.
    /// </remarks>
    public virtual IEnumerable<Member> Members => [];

    /// <summary>
    /// Gets a <see cref="Core.Member"/> with the specified name if it exists for the <see cref="LogixData"/>;
    /// Otherwise, returns <c>null</c>.
    /// </summary>
    /// <param name="name">The name of the member to get.</param>
    /// <returns>A <see cref="Core.Member"/> with the specified name if found; Otherwise, <c>null</c>.</returns>
    /// <remarks>This performs a case-insensitive comparison for the member name.</remarks>
    public Member? Member(string name) =>
        Members.SingleOrDefault(m => string.Equals(m.Name, name, StringComparison.OrdinalIgnoreCase));

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj is not LogixData type) return false;
        return Name == type.Name;
    }

    /// <inheritdoc />
    public override int GetHashCode() => Name.GetHashCode();

    /// <inheritdoc />
    public override string ToString() => Name;

    /// <summary>
    /// Returns the singleton null <see cref="LogixData"/> object.
    /// </summary>
    public static LogixData Null => NullData.Instance;

    /// <summary>
    /// Creates a concrete instance of a <see cref="LogixData"/> using the specified data type name.
    /// </summary>
    /// <param name="dataType">The name of the data type to create.</param>
    /// <returns>A new <see cref="LogixData"/> representing the provided data type name.</returns>
    /// <remarks>
    /// <para>
    /// Internally we are again using reflection and expression builders and caching them for efficiency.
    /// This method will create concrete instances for types defined in this library which have a default parameterless
    /// constructor in order to generate the type, and it's structure/members for a given type name.
    ///</para>
    /// <para>
    /// If the type name is not
    /// statically defined (i.e. not an AtomicData or known/predeinfed StructureData object), then this method will
    /// return a default <see cref="ComplexData"/> instance with the provided name and no members, as it is impossible to
    /// know what the structure of the type is.
    /// </para>
    /// </remarks>
    public static LogixData Create(string dataType)
    {
        return Facotries.Value.TryGetValue(dataType, out var factory) ? factory() : new ComplexData(dataType);
    }

    /// <summary>
    /// Determines whether the <see cref="LogixData"/> values are equal.
    /// </summary>
    /// <param name="left">An logix type to compare.</param>
    /// <param name="right">An logix type to compare.</param>
    /// <returns><c>true</c> if the objects are equal, otherwise, <c>false</c>.</returns>
    public static bool operator ==(LogixData left, LogixData right) => Equals(left, right);

    /// <summary>
    /// Determines whether the <see cref="LogixData"/> values are not equal.
    /// </summary>
    /// <param name="left">An logix type to compare.</param>
    /// <param name="right">An logix type to compare.</param>
    /// <returns><c>true</c> if the objects are not equal, otherwise, <c>false</c>.</returns>
    public static bool operator !=(LogixData left, LogixData right) => !Equals(left, right);

    /// <summary>
    /// Compares two objects and determines if a is greater than b.
    /// </summary>
    /// <param name="a">An logix type to compare.</param>
    /// <param name="b">An logix type to compare.</param>
    /// <returns><c>true</c> if <c>a</c> is greater than <c>b</c>, otherwise, <c>false</c>.</returns>
    public static bool operator >(LogixData a, LogixData b)
    {
        if (a is not IComparable comparable)
            throw new ArgumentException($"Type {a.GetType()} does not implement {typeof(IComparable)}.");

        return comparable.CompareTo(b) > 0;
    }

    /// <summary>
    /// Compares two objects and determines if a is less than b.
    /// </summary>
    /// <param name="a">An logix type to compare.</param>
    /// <param name="b">An logix type to compare.</param>
    /// <returns><c>true</c> if <c>a</c> is less than <c>b</c>, otherwise, <c>false</c>.</returns>
    public static bool operator <(LogixData a, LogixData b)
    {
        if (a is not IComparable comparable)
            throw new ArgumentException($"Type {a.GetType()} does not implement {typeof(IComparable)}.");

        return comparable.CompareTo(b) < 0;
    }

    /// <summary>
    /// Compares two objects and determines if a is greater or equal to than b.
    /// </summary>
    /// <param name="a">An logix type to compare.</param>
    /// <param name="b">An logix type to compare.</param>
    /// <returns><c>true</c> if <c>a</c> is greater than or equal to <c>b</c>, otherwise, <c>false</c>.</returns>
    public static bool operator >=(LogixData a, LogixData b)
    {
        if (a is not IComparable comparable)
            throw new ArgumentException($"Type {a.GetType()} does not implement {typeof(IComparable)}.");

        return comparable.CompareTo(b) >= 0;
    }

    /// <summary>
    /// Compares two objects and determines if a is less than or equal to b.
    /// </summary>
    /// <param name="a">An logix type to compare.</param>
    /// <param name="b">An logix type to compare.</param>
    /// <returns><c>true</c> if <c>a</c> is less than or equal to <c>b</c>, otherwise, <c>false</c>.</returns>
    public static bool operator <=(LogixData a, LogixData b)
    {
        if (a is not IComparable comparable)
            throw new ArgumentException($"Type {a.GetType()} does not implement {typeof(IComparable)}.");

        return comparable.CompareTo(b) <= 0;
    }

    /// <summary>
    /// Converts the provided <see cref="bool"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(bool value) => new BOOL(value);

    /// <summary>
    /// Converts the provided <see cref="sbyte"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(sbyte value) => new SINT(value);

    /// <summary>
    /// Converts the provided <see cref="short"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(short value) => new INT(value);

    /// <summary>
    /// Converts the provided <see cref="int"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(int value) => new DINT(value);

    /// <summary>
    /// Converts the provided <see cref="long"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(long value) => new LINT(value);

    /// <summary>
    /// Converts the provided <see cref="float"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(float value) => new REAL(value);

    /// <summary>
    /// Converts the provided <see cref="double"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(double value) => new LREAL(value);

    /// <summary>
    /// Converts the provided <see cref="byte"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(byte value) => new USINT(value);

    /// <summary>
    /// Converts the provided <see cref="ushort"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(ushort value) => new UINT(value);

    /// <summary>
    /// Converts the provided <see cref="uint"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(uint value) => new UDINT(value);

    /// <summary>
    /// Converts the provided <see cref="ulong"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(ulong value) => new ULINT(value);

    /// <summary>
    /// Converts the provided <see cref="string"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(string value) => new STRING(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(LogixData[] value) => new ArrayData<LogixData>(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(LogixData[,] value) => new ArrayData<LogixData>(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(LogixData[,,] value) => new ArrayData<LogixData>(value);

    /// <summary>
    /// Converts the provided <see cref="Dictionary{TKey,TValue}"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(Dictionary<string, LogixData> value) =>
        new ComplexData(nameof(ComplexData), value.Select(m => new Member(m.Key, m.Value)));

    /// <summary>
    /// The global cache for all <see cref="LogixData"/> object factory delegate functions.
    /// </summary>
    private static readonly Lazy<Dictionary<string, Func<LogixData>>> Facotries = new(DataFactories,
        LazyThreadSafetyMode.ExecutionAndPublication);

    /// <summary>
    /// Provides factory methods for creating instances of LogixData-derived classes.
    /// </summary>
    /// <returns>A dictionary containing the LogixData types as keys and factory functions as values.</returns>
    private static Dictionary<string, Func<LogixData>> DataFactories()
    {
        var factories = new List<KeyValuePair<string, Func<LogixData>>>();

        //Scan and add types from this assembly first.
        var sharp = typeof(LogixData).Assembly;
        factories.AddRange(Scan(sharp));

        //Scan other loaded assemblies for use defined types.
        var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a != sharp);
        foreach (var assembly in assemblies)
            factories.AddRange(Scan(assembly));

        return factories.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }

    /// <summary>
    /// Scans the provided assemble for types safisfying the LogixData factory condition and creates a collection of
    /// key value pairs where the key is a name of the type and the value is a function that instantiates the type.
    /// </summary>
    /// <param name="assembly">The assembly to scan.</param>
    /// <returns>A collection of key value pairs for all logix data factories in the scanned assembly.</returns>
    private static IEnumerable<KeyValuePair<string, Func<LogixData>>> Scan(Assembly assembly)
    {
        var factories = new List<KeyValuePair<string, Func<LogixData>>>();

        var logixTypes = assembly.GetTypes().Where(IsLogixDataType).ToList();

        foreach (var logixType in logixTypes)
        {
            var factory = Instantiator(logixType);
            var names = logixType.L5XTypes().ToList();
            var functions = names.Select(n => new KeyValuePair<string, Func<LogixData>>(n, factory));
            factories.AddRange(functions);
        }

        return factories;
    }

    /// <summary>
    /// Determines whether the given type is a LogixData derivative that we can instantiate a default instance of
    /// given a type name.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns>Returns true if the type is a LogixDataType; otherwise, false.</returns>
    private static bool IsLogixDataType(Type type)
    {
        HashSet<Type> exlucde =
        [
            typeof(ComplexData),
            typeof(StringData),
            typeof(ArrayData<>),
            typeof(NullData)
        ];

        return typeof(LogixData).IsAssignableFrom(type) &&
               !exlucde.Contains(type) &&
               type is { IsAbstract: false, IsPublic: true } &&
               type.GetConstructor(Type.EmptyTypes) is not null;
    }

    /// <summary>
    /// Creates aand compiles a function expresstion for a given logix data type in order to create new instances efficiently. 
    /// </summary>
    /// <param name="type">The type of LogixData to instantiate.</param>
    /// <returns>A Func delegate that can be invoked to create a new instance of the specified type.</returns>
    private static Func<LogixData> Instantiator(Type type)
    {
        var constructor = type.GetConstructor(Type.EmptyTypes)!;
        var expression = Expression.New(constructor);
        return Expression.Lambda<Func<LogixData>>(expression).Compile();
    }
}