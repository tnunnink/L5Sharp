namespace L5Sharp.Repositories
{
    internal class TagRepository : Repository<ITag<IDataType>>
    {
        public TagRepository(L5XContext context) : base(context)
        {
        }
    }
}