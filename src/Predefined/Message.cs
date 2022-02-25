using L5Sharp.Abstractions;
using L5Sharp.Atomics;
using L5Sharp.Enums;
using L5Sharp.Factories;

// ReSharper disable InconsistentNaming RSLogix naming

namespace L5Sharp.Predefined
{
    /// <summary>
    /// A predefined or built in data type used with message instructions. 
    /// </summary>
    public sealed class Message : ComplexType
    {
        /// <summary>
        /// Creates a new <see cref="Message"/> data type instance.
        /// </summary>
        public Message() : base(nameof(Message).ToUpper())
        {
        }

        /// <inheritdoc />
        public override DataTypeClass Class => DataTypeClass.Predefined;

        /// <inheritdoc />
        protected override IDataType New() => new Message();

        /// <summary>
        /// Gets the <see cref="Flags"/> member of the <see cref="Message"/> data type.
        /// </summary>
        public IMember<Int> Flags = Member.Create<Int>(nameof(Flags), Radix.Hex);

        /// <summary>
        /// Gets the <see cref="EW"/> member of the <see cref="Message"/> data type.
        /// </summary>
        public IMember<Bool> EW = Member.Create<Bool>(nameof(EW));

        /// <summary>
        /// Gets the <see cref="ER"/> member of the <see cref="Message"/> data type.
        /// </summary>
        public IMember<Bool> ER = Member.Create<Bool>(nameof(ER));

        /// <summary>
        /// Gets the <see cref="DN"/> member of the <see cref="Message"/> data type.
        /// </summary>
        public IMember<Bool> DN = Member.Create<Bool>(nameof(DN));

        /// <summary>
        /// Gets the <see cref="ST"/> member of the <see cref="Message"/> data type.
        /// </summary>
        public IMember<Bool> ST = Member.Create<Bool>(nameof(ST));

        /// <summary>
        /// Gets the <see cref="EN"/> member of the <see cref="Message"/> data type.
        /// </summary>
        public IMember<Bool> EN = Member.Create<Bool>(nameof(EN));

        /// <summary>
        /// Gets the <see cref="TO"/> member of the <see cref="Message"/> data type.
        /// </summary>
        public IMember<Bool> TO = Member.Create<Bool>(nameof(TO));

        /// <summary>
        /// Gets the <see cref="EN_CC"/> member of the <see cref="Message"/> data type.
        /// </summary>
        public IMember<Bool> EN_CC = Member.Create<Bool>(nameof(EN_CC));

        /// <summary>
        /// Gets the <see cref="ERR"/> member of the <see cref="Message"/> data type.
        /// </summary>
        public IMember<Int> ERR = Member.Create<Int>(nameof(ERR), radix: Radix.Hex);

        /// <summary>
        /// Gets the <see cref="EXERR"/> member of the <see cref="Message"/> data type.
        /// </summary>
        public IMember<Dint> EXERR = Member.Create<Dint>(nameof(EXERR), radix: Radix.Hex);

        /// <summary>
        /// Gets the <see cref="ERR_SRC"/> member of the <see cref="Message"/> data type.
        /// </summary>
        public IMember<Sint> ERR_SRC = Member.Create<Sint>(nameof(ERR_SRC));

        /// <summary>
        /// Gets the <see cref="DN_LEN"/> member of the <see cref="Message"/> data type.
        /// </summary>
        public IMember<Int> DN_LEN = Member.Create<Int>(nameof(DN_LEN));

        /// <summary>
        /// Gets the <see cref="REQ_LEN"/> member of the <see cref="Message"/> data type.
        /// </summary>
        public IMember<Int> REQ_LEN = Member.Create<Int>(nameof(REQ_LEN));

        /// <summary>
        /// Gets the <see cref="DestinationLink"/> member of the <see cref="Message"/> data type.
        /// </summary>
        public IMember<Int> DestinationLink = Member.Create<Int>(nameof(DestinationLink));

        /// <summary>
        /// Gets the <see cref="DestinationNode"/> member of the <see cref="Message"/> data type.
        /// </summary>
        public IMember<Int> DestinationNode = Member.Create<Int>(nameof(DestinationNode), radix: Radix.Octal);

        /// <summary>
        /// Gets the <see cref="SourceLink"/> member of the <see cref="Message"/> data type.
        /// </summary>
        public IMember<Int> SourceLink = Member.Create<Int>(nameof(SourceLink));

        /// <summary>
        /// Gets the <see cref="MessageClass"/> member of the <see cref="Message"/> data type.
        /// </summary>
        public IMember<Int> MessageClass = Member.Create<Int>(nameof(MessageClass), radix: Radix.Hex);

        /// <summary>
        /// Gets the <see cref="Attribute"/> member of the <see cref="Message"/> data type.
        /// </summary>
        public IMember<Int> Attribute = Member.Create<Int>(nameof(Attribute), radix: Radix.Hex);

        /// <summary>
        /// Gets the <see cref="Instance"/> member of the <see cref="Message"/> data type.
        /// </summary>
        public IMember<Dint> Instance = Member.Create<Dint>(nameof(Instance));

        /// <summary>
        /// Gets the <see cref="LocalIndex"/> member of the <see cref="Message"/> data type.
        /// </summary>
        public IMember<Dint> LocalIndex = Member.Create<Dint>(nameof(LocalIndex));

        /// <summary>
        /// Gets the <see cref="Channel"/> member of the <see cref="Message"/> data type.
        /// </summary>
        public IMember<Sint> Channel = Member.Create<Sint>(nameof(Channel), radix: Radix.Ascii);

        /// <summary>
        /// Gets the <see cref="Rack"/> member of the <see cref="Message"/> data type.
        /// </summary>
        public IMember<Sint> Rack = Member.Create<Sint>(nameof(Rack), radix: Radix.Octal);

        /// <summary>
        /// Gets the <see cref="Group"/> member of the <see cref="Message"/> data type.
        /// </summary>
        public IMember<Sint> Group = Member.Create<Sint>(nameof(Group));

        /// <summary>
        /// Gets the <see cref="Slot"/> member of the <see cref="Message"/> data type.
        /// </summary>
        public IMember<Sint> Slot = Member.Create<Sint>(nameof(Slot));

        /// <summary>
        /// Gets the <see cref="Path"/> member of the <see cref="Message"/> data type.
        /// </summary>
        public IMember<String> Path = Member.Create<String>(nameof(Path));

        /// <summary>
        /// Gets the <see cref="RemoteIndex"/> member of the <see cref="Message"/> data type.
        /// </summary>
        public IMember<Dint> RemoteIndex = Member.Create<Dint>(nameof(RemoteIndex));

        /// <summary>
        /// Gets the <see cref="RemoteElement"/> member of the <see cref="Message"/> data type.
        /// </summary>
        public IMember<String> RemoteElement = Member.Create<String>(nameof(RemoteElement));

        /// <summary>
        /// Gets the <see cref="UnconnectedTimeout"/> member of the <see cref="Message"/> data type.
        /// </summary>
        public IMember<Dint> UnconnectedTimeout = Member.Create<Dint>(nameof(UnconnectedTimeout));

        /// <summary>
        /// Gets the <see cref="ConnectionRate"/> member of the <see cref="Message"/> data type.
        /// </summary>
        public IMember<Dint> ConnectionRate = Member.Create<Dint>(nameof(ConnectionRate));

        /// <summary>
        /// Gets the <see cref="TimeoutMultiplier"/> member of the <see cref="Message"/> data type.
        /// </summary>
        public IMember<Sint> TimeoutMultiplier = Member.Create<Sint>(nameof(TimeoutMultiplier));
    }
}