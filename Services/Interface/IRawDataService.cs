using SeiveIT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeiveIT.Services.Interface
{
    public interface IRawDataService
    {
        Task<List<SeiveData>> Get(long projectId, long outcropId);
        Task UpsertSeiveData(List<SeiveData> entity);
        Task DeleteAll(List<SeiveData> entity);
    }
}
