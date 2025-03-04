using SeiveIT.Entities;
using SeiveIT.Repository.Implementation;
using SeiveIT.Repository.Interface;
using SeiveIT.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeiveIT.Services.Implementation
{
    sealed class ProjectService : IProjectService
    {
        readonly IRepositoryManager _repositoryManager;
        public ProjectService(IRepositoryManager repoManger)
        {
            _repositoryManager = repoManger;
        }

        public async Task AddProject(Project project)
        {
            await _repositoryManager.Project.CreateAsync(project);
        }
    }
}
