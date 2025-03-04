using SeiveIT.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeiveIT.Repository.Implementation
{
    class RepositoryManager : IRepositoryManager
    {
        readonly Lazy<IProjectRepository> _projectRepo;
        readonly DatabaseManager _dbManager;
        public IProjectRepository Project => _projectRepo.Value;

        public RepositoryManager(DatabaseManager dbManager)
        {
            _dbManager = dbManager;
            _projectRepo = new Lazy<IProjectRepository>(() => new ProjectRepository(_dbManager));
            _dbManager = dbManager;
        }
    }
}
