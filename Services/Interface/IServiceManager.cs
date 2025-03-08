using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeiveIT.Services.Interface
{
    public interface IServiceManager
    {
        IProjectService ProjectService { get; }
    }
}
