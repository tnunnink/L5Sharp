using System.Collections.Generic;
using L5Sharp.Common;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics;

// ReSharper disable InconsistentNaming RSLogix naming

namespace L5Sharp.Types.Predefined
{
    /// <summary>
    /// A predefined or built in data type used with message instructions. 
    /// </summary>
    public sealed class MESSAGE : StructureType
    {
        /// <summary>
        /// Creates a new <see cref="MESSAGE"/> data type instance.
        /// </summary>
        public MESSAGE() : base(nameof(MESSAGE))
        {
        }

        /// <inheritdoc />
        public override DataTypeClass Class => DataTypeClass.Predefined;

        /// <summary>
        /// Gets the <see cref="Flags"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public INT Flags { get; set; } = new(Radix.Hex);

        /// <summary>
        /// Gets the <see cref="EW"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public BOOL EW { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ER"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public BOOL DN { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="DN"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public BOOL ER { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ST"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public BOOL ST { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="EN"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public BOOL EN { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="TO"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public BOOL TO { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="EN_CC"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public BOOL EN_CC { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ERR"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public INT ERR { get; set; } = new(Radix.Hex);

        /// <summary>
        /// Gets the <see cref="EXERR"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public DINT EXERR { get; set; } = new(Radix.Hex);

        /// <summary>
        /// Gets the <see cref="ERR_SRC"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public SINT ERR_SRC { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="DN_LEN"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public INT DN_LEN { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="REQ_LEN"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public INT REQ_LEN { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="DestinationLink"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public INT DestinationLink { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="DestinationNode"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public INT DestinationNode { get; set; } = new(Radix.Octal);

        /// <summary>
        /// Gets the <see cref="SourceLink"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public INT SourceLink { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="MessageClass"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public INT MessageClass { get; set; } = new(Radix.Hex);

        /// <summary>
        /// Gets the <see cref="Attribute"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public INT Attribute { get; set; } = new(Radix.Hex);

        /// <summary>
        /// Gets the <see cref="Instance"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public DINT Instance { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="LocalIndex"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public DINT LocalIndex { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="Channel"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public SINT Channel { get; set; } = new(Radix.Ascii);

        /// <summary>
        /// Gets the <see cref="Rack"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public SINT Rack { get; set; } = new(Radix.Octal);

        /// <summary>
        /// Gets the <see cref="Group"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public SINT Group { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="Slot"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public SINT Slot { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="Path"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public STRING Path { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="RemoteIndex"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public DINT RemoteIndex { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="RemoteElement"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public STRING RemoteElement { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="UnconnectedTimeout"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public DINT UnconnectedTimeout { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="ConnectionRate"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public DINT ConnectionRate { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="TimeoutMultiplier"/> member of the <see cref="MESSAGE"/> data type.
        /// </summary>
        public SINT TimeoutMultiplier { get; set; } = new();
    }
}