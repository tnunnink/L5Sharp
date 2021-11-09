using L5Sharp.Core;
using L5Sharp.Enums;

// ReSharper disable InconsistentNaming RSLogix naming

namespace L5Sharp.Types
{
    public class Message : Predefined
    {
        public Message() : base(nameof(Message).ToUpper())
        {
            RegisterMemberProperties();
        }
        
        public override IDataType Instantiate()
        {
            return new Message();
        }

        public IMember<Int> Flags = Member.OfType<Int>(nameof(Flags), Radix.Hex);
        public IMember<Bool> EW = Member.OfType<Bool>(nameof(EW));
        public IMember<Bool> ER = Member.OfType<Bool>(nameof(ER));
        public IMember<Bool> DN = Member.OfType<Bool>(nameof(DN));
        public IMember<Bool> ST = Member.OfType<Bool>(nameof(ST));
        public IMember<Bool> EN = Member.OfType<Bool>(nameof(EN));
        public IMember<Bool> TO = Member.OfType<Bool>(nameof(TO));
        public IMember<Bool> EN_CC = Member.OfType<Bool>(nameof(EN_CC));
        public IMember<Int> ERR = Member.OfType<Int>(nameof(ERR), Radix.Hex);
        public IMember<Dint> EXERR = Member.OfType<Dint>(nameof(EXERR), Radix.Hex);
        public IMember<Sint> ERR_SRC = Member.OfType<Sint>(nameof(ERR_SRC));
        public IMember<Int> DN_LEN = Member.OfType<Int>(nameof(DN_LEN));
        public IMember<Int> REQ_LEN = Member.OfType<Int>(nameof(REQ_LEN));
        public IMember<Int> DestinationLink = Member.OfType<Int>(nameof(DestinationLink));
        public IMember<Int> DestinationNode = Member.OfType<Int>(nameof(DestinationNode), Radix.Octal);
        public IMember<Int> SourceLink = Member.OfType<Int>(nameof(SourceLink));
        public IMember<Int> MessageClass = Member.OfType<Int>(nameof(MessageClass), Radix.Hex);
        public IMember<Int> Attribute = Member.OfType<Int>(nameof(Attribute), Radix.Hex);
        public IMember<Dint> Instance = Member.OfType<Dint>(nameof(Instance));
        public IMember<Dint> LocalIndex = Member.OfType<Dint>(nameof(LocalIndex));
        public IMember<Sint> Channel = Member.OfType<Sint>(nameof(Channel), Radix.Ascii);
        public IMember<Sint> Rack = Member.OfType<Sint>(nameof(Rack), Radix.Octal);
        public IMember<Sint> Group = Member.OfType<Sint>(nameof(Group));
        public IMember<Sint> Slot = Member.OfType<Sint>(nameof(Slot));
        public IMember<String> Path = Member.OfType<String>(nameof(Path));
        public IMember<Dint> RemoteIndex = Member.OfType<Dint>(nameof(RemoteIndex));
        public IMember<String> RemoteElement = Member.OfType<String>(nameof(RemoteElement));
        public IMember<Dint> UnconnectedTimeout = Member.OfType<Dint>(nameof(UnconnectedTimeout));
        public IMember<Dint> ConnectionRate = Member.OfType<Dint>(nameof(ConnectionRate));
        public IMember<Sint> TimeoutMultiplier = Member.OfType<Sint>(nameof(TimeoutMultiplier));
    }
}