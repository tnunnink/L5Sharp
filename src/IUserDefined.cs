namespace L5Sharp
{
    /// <summary>
    /// A <c>DataType</c> that represents a <c>UserDefined</c> Logix Component. 
    /// </summary>
    /// <remarks>
    /// User defined data types are those that (as the name implies) are created and configured by the user or developer.
    /// These types are high level components in the L5X under the element <DataTypes/>.
    /// </remarks>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer> 
    public interface IUserDefined : IDataType
    {
        /// <summary>
        /// Member collection of the <c>UserDefined</c> that defined the complex structure of the type. 
        /// </summary>
        IMemberCollection<IMember<IDataType>> Members { get; }
    }
}