namespace L5Sharp.Repositories
{
    internal class ProgramRepository : Repository<IProgram>
    {
        public ProgramRepository(L5XContext context) : base(context)
        {
        }
    }
}