namespace L5Sharp.Repositories
{
    internal class TaskRepository : Repository<ITask>, ITaskRepository
    {
        public TaskRepository(LogixContext context) : base(context)
        {
        }
    }
}