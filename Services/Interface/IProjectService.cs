using SeiveIT.Entities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeiveIT.Services.Interface
{
    public interface IProjectService
    {
        Task AddProject(Project project);
        Task<List<Project>> GetAllProject(int page, int limit);
    }
}
