using SeiveIT.Entities;
using SeiveIT.Repository.Interface;
using SeiveIT.Services.Interface;

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

        public async Task DeleteAll(List<SeiveData> entity)
        {
            await _repositoryManager.RawData.DeleteAllAsync(entity);
        }
    }
}
