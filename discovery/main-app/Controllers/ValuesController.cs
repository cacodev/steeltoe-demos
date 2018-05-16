using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using main_app.Services;
using Microsoft.AspNetCore.Mvc;

namespace main_app.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IService1 _svc1;
        private readonly IService2 _svc2;
        public ValuesController(IService1 service1, IService2 service2)
        {
            _svc1 = service1;
            _svc2 = service2;
        }


        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var result1 = await _svc1.GetValuesFrom1();
            var result2 = await _svc2.GetValuesFrom2();
            return result1.Union(result2);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<List<string>> Get(int id)
        {
            if(id == 1)
            {
                return await _svc1.GetValuesFrom1();
            }
            else if (id == 2)
            {
                return await _svc2.GetValuesFrom2();
            }

            return new List<string>();
        }
    }
}
