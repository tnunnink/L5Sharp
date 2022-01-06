using L5Sharp.Abstractions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;

// ReSharper disable InconsistentNaming RSLogix naming

namespace L5Sharp.Types
{
    public sealed class Message : ComplexType
    {
        public Message() : base(nameof(Message).ToUpper())
        {
        }

        /// <inheritdoc />
        public override DataTypeClass Class => DataTypeClass.Predefined;

        /// <inheritdoc />
        protected override IDataType New() => new Message();

        public IMember<Int> Flags = Member.Create<Int>(nameof(Flags),radix: Radix.Hex);
        public IMember<Bool> EW = Member.Create<Bool>(nameof(EW));
        public IMember<Bool> ER = Member.Create<Bool>(nameof(ER));
        public IMember<Bool> DN = Member.Create<Bool>(nameof(DN));
        public IMember<Bool> ST = Member.Create<Bool>(nameof(ST));
        public IMember<Bool> EN = Member.Create<Bool>(nameof(EN));
        public IMember<Bool> TO = Member.Create<Bool>(nameof(TO));
        public IMember<Bool> EN_CC = Member.Create<Bool>(nameof(EN_CC));
        public IMember<Int> ERR = Member.Create<Int>(nameof(ERR), radix: Radix.Hex);
        public IMember<Dint> EXERR = Member.Create<Dint>(nameof(EXERR), radix: Radix.Hex);
        public IMember<Sint> ERR_SRC = Member.Create<Sint>(nameof(ERR_SRC));
        public IMember<Int> DN_LEN = Member.Create<Int>(nameof(DN_LEN));
        public IMember<Int> REQ_LEN = Member.Create<Int>(nameof(REQ_LEN));
        public IMember<Int> DestinationLink = Member.Create<Int>(nameof(DestinationLink));
        public IMember<Int> DestinationNode = Member.Create<Int>(nameof(DestinationNode), radix: Radix.Octal);
        public IMember<Int> SourceLink = Member.Create<Int>(nameof(SourceLink));
        public IMember<Int> MessageClass = Member.Create<Int>(nameof(MessageClass), radix: Radix.Hex);
        public IMember<Int> Attribute = Member.Create<Int>(nameof(Attribute), radix: Radix.Hex);
        public IMember<Dint> Instance = Member.Create<Dint>(nameof(Instance));
        public IMember<Dint> LocalIndex = Member.Create<Dint>(nameof(LocalIndex));
        public IMember<Sint> Channel = Member.Create<Sint>(nameof(Channel), radix: Radix.Ascii);
        public IMember<Sint> Rack = Member.Create<Sint>(nameof(Rack), radix: Radix.Octal);
        public IMember<Sint> Group = Member.Create<Sint>(nameof(Group));
        public IMember<Sint> Slot = Member.Create<Sint>(nameof(Slot));
        public IMember<String> Path = Member.Create<String>(nameof(Path));
        public IMember<Dint> RemoteIndex = Member.Create<Dint>(nameof(RemoteIndex));
        public IMember<String> RemoteElement = Member.Create<String>(nameof(RemoteElement));
        public IMember<Dint> UnconnectedTimeout = Member.Create<Dint>(nameof(UnconnectedTimeout));
        public IMember<Dint> ConnectionRate = Member.Create<Dint>(nameof(ConnectionRate));
        public IMember<Sint> TimeoutMultiplier = Member.Create<Sint>(nameof(TimeoutMultiplier));
    }
}