using SeiveIT.Entities;
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
        public async Task<SeiveData> UpsertSeiveData(SeiveData data)
        {            
            if(data.Id > 0)
                data.UpdatedOn = DateTime.UtcNow;
            else
                data.CreatedOn = DateTime.UtcNow;
            await _repositoryManager.RawData.Upsert(data);
            return data;
        }
    }
}
