namespace L5Sharp.Repositories
{
    internal class TagRepository : Repository<ITag<IDataType>>, ITagRepository
    {
        public TagRepository(LogixContext context) : base(context)
        {
        }
    }
}