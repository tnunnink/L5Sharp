namespace L5Sharp.Repositories
{
    internal class TagRepository : Repository<ITag<IDataType>>
    {
        public TagRepository(LogixContext context) : base(context)
        {
        }
    }
}