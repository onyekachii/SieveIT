using SeiveIT.Repository.Interface;
using SeiveIT.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeiveIT.Services.Implementation
{
    sealed class ServiceManager : IServiceManager
    {
        readonly Lazy<IProjectService> _projectService;
        public IProjectService ProjectService => _projectService.Value;

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _projectService = new Lazy<IProjectService>(() => new ProjectService(repositoryManager));
        }
    }
}
