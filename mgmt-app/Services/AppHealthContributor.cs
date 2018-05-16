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
            // Make the object to return
            var hp = new Health();

            try
            {
                // Attempt to check the service in question
                var didStuff = _aweseomeService.DoStuff(); //did i do stuff?
                
                // If we made it here, then the service returned something
                // Set health check status to up and details of how we know it is up
                hp.Status = HealthStatus.UP;
                hp.Details.Add("zachsAwesomeService", new
                {
                    DoStuff = $"returned {didStuff}"
                });
                
            }
            catch (Exception e)
            {
                // If an exception was thrown, then the service is unavailable
                // Set health check status to DOWN and provide details on how we know its down
                hp.Status = HealthStatus.DOWN;
                hp.Description = e.Message;
                hp.Details = new Dictionary<string, object>
                {
                    { "error", e}
                };
            }
            
            // return the health object
            return hp;
        }

        public string Id { get; } = "Healer";
    }
}