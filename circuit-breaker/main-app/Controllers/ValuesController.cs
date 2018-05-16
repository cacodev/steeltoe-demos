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
        private readonly GetService1Values _svc1;
        public ValuesController(GetService1Values service1)
        {
            _svc1 = service1;
        }


        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var result1 = await _svc1.GetValuesAsync();
            return result1;
        }
    }
}
