using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Steeltoe.Management.Endpoint.HeapDump;
using Steeltoe.Management.Endpoint.Info;
using Steeltoe.Management.Endpoint.Info.Contributor;
using Steeltoe.Management.Endpoint.Loggers;
using Steeltoe.Management.Endpoint.ThreadDump;
using Steeltoe.Management.Endpoint.Trace;

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
            services.AddSingleton<IInfoContributor, AppSettingsInfoContributor>();
            services.AddSingleton<IInfoContributor, GitInfoContributor>();

            // Add managment endpoint services
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
            app.UseInfoActuator();
            app.UseTraceActuator();
            app.UseLoggersActuator();
            app.UseHeapDumpActuator();
            app.UseThreadDumpActuator();

            app.UseMvc();
        }
    }
}
