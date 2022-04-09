
using L5Sharp.Core;
using L5Sharp.Types;

//This is just an idea. Not sure it would add any value. Perhaps you could use this for creating rungs...
namespace L5Sharp.Instructions
{
    /// <summary>
    /// An bit <see cref="Instruction"/> representing the XIC Logix instruction.
    /// "The XIC instruction examines the data bit to see if it is set."
    /// </summary>
    /// <footer>
    /// See <a href="">
    /// `Logix 5000 Controllers General Instructions`</a> for more information.
    /// </footer> 
    public class XIC : Instruction
    {
        /// <summary>
        /// Creates a new <see cref="XIC"/> instruction with the provided tag name.
        /// </summary>
        /// <param name="dataBit">The data bit tag argument.</param>
        public XIC(TagName dataBit) : base(nameof(XIC), dataBit)
        {
        }
        
        /// <summary>
        /// Creates a new <see cref="XIC"/> instruction with the provided tag name.
        /// </summary>
        /// <param name="dataBit">The data bit tag argument.</param>
        public XIC(ITagMember<BOOL> dataBit) : base(nameof(XIC), dataBit.TagName)
        {
        }
    }
}