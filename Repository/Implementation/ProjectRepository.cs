using SeiveIT.Entities;
using SeiveIT.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeiveIT.Repository.Implementation
{
    internal class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        public ProjectRepository(DatabaseManager dbManager) : base(dbManager)
        {
            
        }
    }
}
