using Microsoft.Extensions.Caching.Memory;
using Steeltoe.CircuitBreaker.Hystrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace main_app.Services
{
    public class GetService1Values : HystrixCommand<List<string>>
    {
        public IHystrixCommandOptions Options { get; }
        public IService1 _svc1 { get; }

        public GetService1Values(IHystrixCommandOptions options,IService1 service1):base(options)
        {
            Options = options;
            _svc1 = service1;
            IsFallbackUserDefined = true;
        }
        public async Task<List<string>> GetValuesAsync()
        {
            return await ExecuteAsync();
        }

        protected override async Task<List<string>> RunAsync()
        {
            return await FetchFromServiceAsync();
        }

        public async Task<List<string>> FetchFromServiceAsync()
        {
            return await _svc1.GetValuesFrom1();
        }

        protected override async Task<List<string>> RunFallbackAsync()
        {
            return await Task.FromResult(FetchFromFallback());
        }

        private List<string> FetchFromFallback()
        {
            return new List<string>
            {
                "Fallback - value1", "Fallback - value2"
            };
        }

    }
}
