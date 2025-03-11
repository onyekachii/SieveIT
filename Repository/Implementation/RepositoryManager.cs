using SeiveIT.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeiveIT.Repository.Implementation
{
    public class RepositoryManager : IRepositoryManager
    {
        readonly Lazy<IProjectRepository> _projectRepo;
        readonly Lazy<IOutcropRepository> _outcropRepo;
        readonly DatabaseManager _dbManager;
        public IProjectRepository Project => _projectRepo.Value;
        public IOutcropRepository Outcrop => _outcropRepo.Value;

        public RepositoryManager(DatabaseManager dbManager)
        {
            _dbManager = dbManager;
            _projectRepo = new Lazy<IProjectRepository>(() => new ProjectRepository(_dbManager));
            _outcropRepo = new Lazy<IOutcropRepository>(() => new OutcropRepository(_dbManager));
        }
    }
}
