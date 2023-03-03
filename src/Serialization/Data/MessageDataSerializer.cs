using System.Xml.Linq;
using L5Sharp.Extensions;
using L5Sharp.Types.Predefined;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization.Data;

/// <summary>
/// A logix serializer that performs serialization of <see cref="MESSAGE"/> objects.
/// </summary>
public class MessageDataSerializer : ILogixSerializer<MESSAGE>
{
    /// <inheritdoc />
    public XElement Serialize(MESSAGE obj)
    {
        Check.NotNull(obj);

        throw new System.NotImplementedException();
    }

    /// <inheritdoc />
    public MESSAGE Deserialize(XElement element)
    {
        throw new System.NotImplementedException();
    }
}