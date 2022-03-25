using L5Sharp.Abstractions;
using L5Sharp.Creators;
using L5Sharp.Enums;

// ReSharper disable InconsistentNaming RSLogix naming

namespace L5Sharp.Types
{
    /// <summary>
    /// A predefined or built in data type used with message instructions. 
    /// </summary>
    public sealed class MESSAGE : ComplexType
    {
        /// <summary>
        /// Creates a new <see cref="MESSAGE"/> data type instance.
        /// </summary>
        public MESSAGE() : base(nameof(MESSAGE))
        {
        }

        /// <inheritdoc />
        public override DataTypeClass Class => DataTypeClass.Predefined;

        /// <inheritdoc />
        protected override IDataType New() => new MESSAGE();

        /// <summary>
        /// Gets the <see cref="Flags"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public IMember<INT> Flags = Member.Create<INT>(nameof(Flags), Radix.Hex);

        /// <summary>
        /// Gets the <see cref="EW"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public IMember<BOOL> EW = Member.Create<BOOL>(nameof(EW));

        /// <summary>
        /// Gets the <see cref="ER"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public IMember<BOOL> ER = Member.Create<BOOL>(nameof(ER));

        /// <summary>
        /// Gets the <see cref="DN"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public IMember<BOOL> DN = Member.Create<BOOL>(nameof(DN));

        /// <summary>
        /// Gets the <see cref="ST"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public IMember<BOOL> ST = Member.Create<BOOL>(nameof(ST));

        /// <summary>
        /// Gets the <see cref="EN"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public IMember<BOOL> EN = Member.Create<BOOL>(nameof(EN));

        /// <summary>
        /// Gets the <see cref="TO"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public IMember<BOOL> TO = Member.Create<BOOL>(nameof(TO));

        /// <summary>
        /// Gets the <see cref="EN_CC"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public IMember<BOOL> EN_CC = Member.Create<BOOL>(nameof(EN_CC));

        /// <summary>
        /// Gets the <see cref="ERR"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public IMember<INT> ERR = Member.Create<INT>(nameof(ERR), radix: Radix.Hex);

        /// <summary>
        /// Gets the <see cref="EXERR"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public IMember<DINT> EXERR = Member.Create<DINT>(nameof(EXERR), radix: Radix.Hex);

        /// <summary>
        /// Gets the <see cref="ERR_SRC"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public IMember<SINT> ERR_SRC = Member.Create<SINT>(nameof(ERR_SRC));

        /// <summary>
        /// Gets the <see cref="DN_LEN"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public IMember<INT> DN_LEN = Member.Create<INT>(nameof(DN_LEN));

        /// <summary>
        /// Gets the <see cref="REQ_LEN"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public IMember<INT> REQ_LEN = Member.Create<INT>(nameof(REQ_LEN));

        /// <summary>
        /// Gets the <see cref="DestinationLink"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public IMember<INT> DestinationLink = Member.Create<INT>(nameof(DestinationLink));

        /// <summary>
        /// Gets the <see cref="DestinationNode"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public IMember<INT> DestinationNode = Member.Create<INT>(nameof(DestinationNode), radix: Radix.Octal);

        /// <summary>
        /// Gets the <see cref="SourceLink"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public IMember<INT> SourceLink = Member.Create<INT>(nameof(SourceLink));

        /// <summary>
        /// Gets the <see cref="MessageClass"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public IMember<INT> MessageClass = Member.Create<INT>(nameof(MessageClass), radix: Radix.Hex);

        /// <summary>
        /// Gets the <see cref="Attribute"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public IMember<INT> Attribute = Member.Create<INT>(nameof(Attribute), radix: Radix.Hex);

        /// <summary>
        /// Gets the <see cref="Instance"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public IMember<DINT> Instance = Member.Create<DINT>(nameof(Instance));

        /// <summary>
        /// Gets the <see cref="LocalIndex"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public IMember<DINT> LocalIndex = Member.Create<DINT>(nameof(LocalIndex));

        /// <summary>
        /// Gets the <see cref="Channel"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public IMember<SINT> Channel = Member.Create<SINT>(nameof(Channel), radix: Radix.Ascii);

        /// <summary>
        /// Gets the <see cref="Rack"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public IMember<SINT> Rack = Member.Create<SINT>(nameof(Rack), radix: Radix.Octal);

        /// <summary>
        /// Gets the <see cref="Group"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public IMember<SINT> Group = Member.Create<SINT>(nameof(Group));

        /// <summary>
        /// Gets the <see cref="Slot"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public IMember<SINT> Slot = Member.Create<SINT>(nameof(Slot));

        /// <summary>
        /// Gets the <see cref="Path"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public IMember<STRING> Path = Member.Create<STRING>(nameof(Path));

        /// <summary>
        /// Gets the <see cref="RemoteIndex"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public IMember<DINT> RemoteIndex = Member.Create<DINT>(nameof(RemoteIndex));

        /// <summary>
        /// Gets the <see cref="RemoteElement"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public IMember<STRING> RemoteElement = Member.Create<STRING>(nameof(RemoteElement));

        /// <summary>
        /// Gets the <see cref="UnconnectedTimeout"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public IMember<DINT> UnconnectedTimeout = Member.Create<DINT>(nameof(UnconnectedTimeout));

        /// <summary>
        /// Gets the <see cref="ConnectionRate"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public IMember<DINT> ConnectionRate = Member.Create<DINT>(nameof(ConnectionRate));

        /// <summary>
        /// Gets the <see cref="TimeoutMultiplier"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public IMember<SINT> TimeoutMultiplier = Member.Create<SINT>(nameof(TimeoutMultiplier));
    }
}