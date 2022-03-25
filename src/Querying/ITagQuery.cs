namespace L5Sharp.Querying
{
    public interface ITagQuery : IComponentQuery<ITag<IDataType>>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDataType"></typeparam>
        /// <returns></returns>
        public ITagQuery WithType<TDataType>() where TDataType : IDataType;
    }
}