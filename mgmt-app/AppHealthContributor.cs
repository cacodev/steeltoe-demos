using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Steeltoe.Management.Endpoint.Health;

namespace mgmt_app
{
    public class AppHealthContributor:IHealthContributor
    {
        private readonly IZachsAwesomeServiceThatNeverFails _aweseomeService;

        public AppHealthContributor(IZachsAwesomeServiceThatNeverFails awesomeService)
        {
            _aweseomeService = awesomeService;
        }

        public Health Health()
        {
            var hp = new Health();

            try
            {
                var didStuff = _aweseomeService.DoStuff(); //did i do stuff?
                hp.Status = HealthStatus.UP;
                hp.Details.Add("zachsAwesomeService", new
                {
                    Status = "Up",
                    DoStuff = $"returned {didStuff}"
                });
                
            }
            catch (Exception e)
            {
                hp.Status = HealthStatus.DOWN;
                hp.Description = e.Message;
                hp.Details = new Dictionary<string, object>
                {
                    { "error", e}
                };
            }
            
            return hp;
        }

        public string Id { get; } = "Healer";
    }
}