using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using config_app.Models;
using Microsoft.Extensions.Options;

namespace config_app.Controllers
{
    [Route("api/[controller]")]
    public class ConfigValuesController : Controller
    {
        private readonly ConfigServerValues _configValues;

        public ConfigValuesController(IOptions<ConfigServerValues>  configOptions)
        {
            _configValues = configOptions.Value;
        }

        // GET api/values
        [HttpGet]
        public ConfigServerValues Get()
        {
            return _configValues;
        }
    }
}
