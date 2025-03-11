using SeiveIT.Entities;
using SeiveIT.Repository.Implementation;
using SeiveIT.Repository.Interface;
using SeiveIT.Services.Interface;
using SQLite;
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

        public async Task<Project> AddProject(Project project)
        {
            project.CreatedOn = DateTime.UtcNow;
            await _repositoryManager.Project.CreateAsync(project);
            return project;
        }

        public async Task<List<Project>> GetAllProject(int page, int limit)
        {
            return await _repositoryManager.Project.FindAll(page, limit);
        }

    }
}
