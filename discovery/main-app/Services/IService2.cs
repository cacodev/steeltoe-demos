using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace main_app.Services
{
    public interface IService2
    {
        Task<List<string>> GetValuesFrom2();
    }
}
