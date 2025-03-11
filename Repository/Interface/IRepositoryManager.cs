

namespace SeiveIT.Repository.Interface
{
    public interface IRepositoryManager
    {
        IProjectRepository Project { get; }
        IOutcropRepository Outcrop { get; }
    }
}
