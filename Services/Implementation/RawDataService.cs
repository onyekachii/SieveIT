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
    public class RawDataService : IRawDataService
    {
        readonly IRepositoryManager _repositoryManager;

        public RawDataService(IRepositoryManager repoManger)
        {
            _repositoryManager = repoManger;
        }

        public async Task<List<SeiveData>> Get(long projectId, long outcropId)
            => await _repositoryManager.RawData.FindByCondition(r => r.ProjectId == projectId && r.OutcropId == outcropId).ToListAsync();

        public async Task UpsertSeiveData(List<SeiveData> data) => await _repositoryManager.RawData.UpsertAll(data);
        
    }
}
