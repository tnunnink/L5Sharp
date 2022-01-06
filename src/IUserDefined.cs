namespace L5Sharp
{
    /// <summary>
    /// Represents a Logix User Defined data type component. 
    /// </summary>
    /// <remarks>
    /// <c>UserDefined</c> data types are those that (as the name implies) are created and configured by the user.
    /// These types are high level components in the L5X, which can be accessed using the <see cref="ILogixContext"/>.
    /// </remarks>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer> 
    public interface IUserDefined : IComplexType
    {
        /// <summary>
        /// Member collection of the <c>UserDefined</c> that defined the complex structure of the type. 
        /// </summary>
        new IComponentCollection<IMember<IDataType>> Members { get; }
    }
}