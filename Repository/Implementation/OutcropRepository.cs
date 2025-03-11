using SeiveIT.Entities;
using SeiveIT.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeiveIT.Repository.Implementation
{
    class OutcropRepository : RepositoryBase<Outcrop>, IOutcropRepository
    {
        public OutcropRepository(DatabaseManager dbManager) : base(dbManager)
        {
        }
    }
}
