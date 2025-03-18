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
        readonly Lazy<IOutcropService> _outcropService;
        readonly Lazy<IRawDataService> _rawDataService;
        public IProjectService ProjectService => _projectService.Value;
        public IOutcropService OutcropService => _outcropService.Value;
        public IRawDataService RawDataService => _rawDataService.Value;

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _projectService = new Lazy<IProjectService>(() => new ProjectService(repositoryManager));
            _outcropService = new Lazy<IOutcropService>(() => new OutcropService(repositoryManager));
            _rawDataService = new Lazy<IRawDataService>(() => new RawDataService(repositoryManager));
        }
    }
}
