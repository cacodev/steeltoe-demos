using Microsoft.Extensions.Logging;
using Steeltoe.Common.Discovery;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace main_app.Services
{
    public class Service1 : BaseDiscoveryService, IService1
    {
        private const string VALUE_URL = "http://service1/api/values";

        public Service1(IDiscoveryClient client, ILoggerFactory logFactory) : base(client, logFactory.CreateLogger<Service1>())
        {
        }

        public async Task<List<string>> GetValuesFrom1()
        {
            return await Invoke<List<string>>(new HttpRequestMessage(HttpMethod.Get, VALUE_URL));
        }
    }
}
