﻿using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Core
{
    /// <summary>
    /// A component of the <see cref="AddOnInstruction"/> that makes up the structure of the instruction type.
    /// </summary>
    /// <remarks><see cref="Parameter"/> inherits from <see cref="DataTypeMember"/> as it is in effect a data type
    /// member that defines the structure or an AOI.</remarks>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer>
    public sealed class Parameter : DataTypeMember
    {
        /// <summary>
        /// A type indicating whether the current parameter is a base tag, or alias for another tag instance.
        /// </summary>
        /// <value>A <see cref="Enums.TagType"/> option representing the type of parameter.
        /// Default is <see cref="Enums.TagType.Base"/>.</value>
        public TagType TagType { get; set; } = TagType.Base;
        
        /// <summary>
        /// The usage option indicating the scope in which the parameter is visible or usable from.
        /// </summary>
        /// <value>A <see cref="Enums.TagUsage"/> option representing the parameter scope.
        /// Default for AOI is <see cref="Enums.TagUsage.Input"/>. Only valid options for AOI are Input, Output,
        /// and InOut.</value>
        public TagUsage Usage { get; set; } = TagUsage.Input;
        
        /// <summary>
        /// Indicates whether the parameter is required form the instruction code clock.
        /// </summary>
        /// <value><c>true</c> if the parameter is required; otherwise, false. Default is <c>false</c>.</value>
        public bool Required { get; set; }
        
        /// <summary>
        /// Indicates whether the parameter is visible from the instruction code block.
        /// </summary>
        /// <value><c>true</c> if the parameter is visible; otherwise, false. Default is <c>false</c>.</value>
        public bool Visible { get; set; }
        
        /// <summary>
        /// The tag name of the tag that is the alias of the current parameter.
        /// </summary>
        /// <value>A <see cref="Core.TagName"/> string representing the full tag name of the alias tag.</value>
        public TagName AliasFor { get; set; } = TagName.Empty;
        
        /// <summary>
        /// A default value of the parameter when instantiated.
        /// </summary>
        /// <value>An <see cref="ILogixType"/> representing the default value/data. Default is <c>null</c>.</value>
        public AtomicType? Default { get; set; }
        
        /// <summary>
        /// Indicates whether the parameter is a constant.
        /// </summary>
        /// <value><c>true</c> if the parameter is constant; otherwise, <c>false</c>.</value>
        /// <remarks>Only value type tags have the ability to be set as a constant. Default is <c>false</c>.</remarks>
        public bool Constant { get; set; }
    }
}