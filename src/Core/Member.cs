using System;
using L5Sharp.Attributes;
using L5Sharp.Enums;
using L5Sharp.Serialization;
using L5Sharp.Types;

namespace L5Sharp.Core
{
    /// <summary>
    /// ..
    /// </summary>
    /// <remarks>
    /// <para>
    /// Members are used to define the structure of an <see cref="StructureType"/>.
    /// Since each member holds a strongly typed reference to it's data type,
    /// the structure forms a hierarchical tree of nested members and types. The member's <see cref="Dimensions"/>,
    /// <see cref="Radix"/>, and <see cref="ExternalAccess"/> properties defined the configuration for a given member.
    /// </para>
    /// </remarks>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer>
    [LogixSerializer(typeof(MemberSerializer))]
    public class Member
    {
        /// <summary>
        /// Creates a new <see cref="Member"/> instance with the provided parameters.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="dataType">The member <see cref="ILogixType"/>.</param>
        /// <param name="radix">the radix format of the member value.</param>
        /// <param name="externalAccess">The external access value of the member.</param>
        /// <param name="description">The description of the member.</param>
        /// <exception cref="ArgumentNullException">name or datatype are null.</exception>
        public Member(string name, ILogixType dataType, Radix? radix = null,
            ExternalAccess? externalAccess = null, string? description = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            DataType = dataType ?? throw new ArgumentNullException(nameof(dataType));
            Dimensions = Dimensions.OfType(DataType);
            Radix = radix ?? Radix.Default(DataType);
            ExternalAccess = externalAccess ?? ExternalAccess.ReadWrite;
            Description = description ?? string.Empty;
        }

        /// <summary>
        /// The name of the <see cref="Member"/> instance.
        /// </summary>
        /// <returns>A <see cref="string"/> representing the member name.</returns>
        public string Name { get; set; }

        /// <summary>
        /// The description of the <see cref="Member"/> instance.
        /// </summary>
        /// <returns>A <see cref="string"/> representing the member description.</returns>
        public string Description { get; set; }

        /// <summary>
        /// The logix type of the <see cref="Member"/> instance.
        /// </summary>
        /// <returns>A <see cref="ILogixType"/> representing the member data type.</returns>
        public ILogixType DataType { get; set; }

        /// <summary>
        /// The dimensions of the <see cref="Member"/> instance.
        /// </summary>
        /// <returns>A <see cref="Core.Dimensions"/> value representing the member array dimensions.</returns>
        public Dimensions Dimensions { get; set; }

        /// <summary>
        /// The radix format of the <see cref="Member"/> instance.
        /// </summary>
        /// <returns>A <see cref="Enums.Radix"/> enum representing the member data format.</returns>
        public Radix Radix { get; set; }

        /// <summary>
        /// The external access of the <see cref="Member"/> instance.
        /// </summary>
        /// <returns>An <see cref="Enums.ExternalAccess"/> enum representing the read/write access of the member.</returns>
        public ExternalAccess ExternalAccess { get; set; }

        /*/// <inheritdoc />
        public XElement Serialize()
        {
            return DataType switch
            {
                AtomicType atomicType => SerializeValueMember(atomicType),
                StringType stringType => SerializeStringMember(stringType),
                StructureType structureType => SerializeStructureMember(structureType),
                ArrayType<AtomicType> atomicTypes => SerializeAtomicArrayMember(atomicTypes),
                ArrayType<StructureType> structureTypes => SerializeStructureArrayMember(structureTypes),
                _ => throw new InvalidOperationException()
            };
        }

        private XElement SerializeValueMember(AtomicType atomicType)
        {
            var element = new XElement(L5XName.DataValueMember);
            element.Add(new XAttribute(L5XName.Name, Name));
            element.Add(new XAttribute(L5XName.DataType, atomicType.Name));

            if (atomicType is not BOOL)
                element.Add(new XAttribute(L5XName.Radix, atomicType.Radix));
            
            element.Add(new XAttribute(L5XName.Value, atomicType.ToString()));

            return element;
        }

        private XElement SerializeStructureMember(StructureType structureType)
        {
            var structureMember = new XElement(L5XName.StructureMember);
            structureMember.Add(new XAttribute(L5XName.Name, Name));
            structureMember.Add(new XAttribute(L5XName.DataType, structureType.Name));

            var members = structureType.Members().Select(m => m.Serialize());
            structureMember.Add(members);

            return structureMember;
        }

        private XElement SerializeAtomicArrayMember(ArrayType<AtomicType> atomicArray)
        {
            var arrayMember = new XElement(L5XName.ArrayMember);
            arrayMember.Add(new XAttribute(L5XName.Name, Name));
            arrayMember.Add(new XAttribute(L5XName.DataType, atomicArray[0].Name));
            arrayMember.Add(new XAttribute(L5XName.Dimensions, atomicArray.Dimensions));
            arrayMember.Add(new XAttribute(L5XName.Radix, atomicArray[0].Radix));

            var elements = atomicArray.Members().Select(m =>
            {
                var e = new XElement(L5XName.Element);
                e.Add(new XAttribute(L5XName.Index, m.Name));
                e.Add(new XAttribute(L5XName.Value, m.DataType.ToString()));
                return e;
            });
            
            arrayMember.Add(elements);

            return arrayMember;
        }

        private XElement SerializeStructureArrayMember(ArrayType<StructureType> structureArray)
        {
            var arrayMember = new XElement(L5XName.ArrayMember);
            arrayMember.Add(new XAttribute(L5XName.Name, Name));
            arrayMember.Add(new XAttribute(L5XName.DataType, structureArray[0].Name));
            arrayMember.Add(new XAttribute(L5XName.Dimensions, structureArray.Dimensions));

            var elements = structureArray.Members().Select(m =>
            {
                var element = new XElement(L5XName.Element);
                element.Add(new XAttribute(L5XName.Index, m.Name));
                
                var structure = new XElement(L5XName.Structure);
                structure.Add(new XAttribute(L5XName.DataType, m.DataType.Name));
                structure.Add(((StructureType)m.DataType).Members().Select(sm => sm.Serialize()));
                element.Add(structure);
                
                element.Add(m.DataType.Serialize());
                return element;
            });
            
            arrayMember.Add(elements);

            return arrayMember;
        }

        private XElement SerializeStringMember(StringType stringType)
        {
            var structureMember = new XElement(L5XName.StructureMember);
            structureMember.Add(new XAttribute(L5XName.Name, Name));
            structureMember.Add(new XAttribute(L5XName.DataType, stringType.Name));

            var len = new XElement(L5XName.DataValueMember);
            len.Add(new XAttribute(L5XName.Name, nameof(stringType.LEN)));
            len.Add(new XAttribute(L5XName.DataType, stringType.LEN.Name));
            len.Add(new XAttribute(L5XName.Radix, stringType.LEN.Radix));
            len.Add(new XAttribute(L5XName.Value, stringType.LEN.ToString()));
            structureMember.Add(len);

            var data = new XElement(L5XName.DataValueMember);
            data.Add(new XAttribute(L5XName.Name, nameof(stringType.DATA)));
            data.Add(new XAttribute(L5XName.DataType, stringType.Name));
            data.Add(new XAttribute(L5XName.Radix, Radix.Ascii.Value));
            data.Add(new XCData(stringType.DATA.AsString()));
            structureMember.Add(data);

            return structureMember;
        }*/
    }
}