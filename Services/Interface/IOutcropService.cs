using SeiveIT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeiveIT.Services.Interface
{
    public interface IOutcropService
    {
        Task<Outcrop> AddOutcrop(Outcrop o);
        Task<List<Outcrop>> GetAllOutcrops(long projectid, int page, int limit);
    }
}
