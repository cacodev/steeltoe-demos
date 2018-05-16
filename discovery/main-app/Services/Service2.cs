using Microsoft.Extensions.Logging;
using Steeltoe.Common.Discovery;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace main_app.Services
{
    public class Service2 : BaseDiscoveryService, IService2
    {
        private const string VALUE_URL = "http://service2/api/values";

        public Service2(IDiscoveryClient client, ILoggerFactory logFactory) : base(client, logFactory.CreateLogger<Service2>())
        {
        }

        public async Task<List<string>> GetValuesFrom2()
        {
            return await Invoke<List<string>>(new HttpRequestMessage(HttpMethod.Get, VALUE_URL));
        }
    }
}
