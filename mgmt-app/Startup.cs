﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Steeltoe.Management.Endpoint.Health;
using Steeltoe.Management.Endpoint.HeapDump;
using Steeltoe.Management.Endpoint.Info;
using Steeltoe.Management.Endpoint.Info.Contributor;
using Steeltoe.Management.Endpoint.Loggers;
using Steeltoe.Management.Endpoint.ThreadDump;
using Steeltoe.Management.Endpoint.Trace;
using mgmt_app.Services;

namespace mgmt_app
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add zach's awesome service that really really shouldn't fail, promise
            services.AddSingleton<IZachsAwesomeServiceThatNeverFails, ZachsAwesomeServiceThatNeverFails>();
            
            // Add custom health contributor
            services.AddScoped<IHealthContributor, AppHealthContributor>();
            
            // Add info contributor to show gitinfo
            services.AddSingleton<IInfoContributor, GitInfoContributor>();

            // Add managment endpoint services
            services.AddHealthActuator(Configuration);
            services.AddInfoActuator(Configuration);
            services.AddTraceActuator(Configuration);
            services.AddLoggersActuator(Configuration);
            services.AddHeapDumpActuator(Configuration);
            services.AddThreadDumpActuator(Configuration);

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            // use the management endpoint actuators
            app.UseHealthActuator();
            app.UseInfoActuator();
            app.UseTraceActuator();
            app.UseLoggersActuator();
            app.UseHeapDumpActuator();
            app.UseThreadDumpActuator();

            app.UseMvc();
        }
    }
}
