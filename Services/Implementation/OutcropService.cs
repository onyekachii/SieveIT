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
    sealed class OutcropService : IOutcropService
    {
        readonly IRepositoryManager _repositoryManager;

        public OutcropService(IRepositoryManager repoManger)
        {
            _repositoryManager = repoManger;
        }
        public async Task<Outcrop> AddOutcrop(Outcrop o)
        {
            o.CreatedOn = DateTime.UtcNow;
            await _repositoryManager.Outcrop.CreateAsync(o);
            return o;
        }

        public async Task<List<Outcrop>> GetAllOutcrops(long projectId, int page, int limit)
        {
            return await _repositoryManager.Outcrop.FindByCondition(o => o.ProjectId == projectId).Skip(page * limit).Take(limit).ToListAsync();
        }
    }
}
