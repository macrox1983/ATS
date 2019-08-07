using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ATS.MessageBus;
using ATS.AggregateSearchService;
using ATS.Common;
using ATS.AggregateDictionaryService;
using ATS.OtherProvider.DI;
using ATS.TuiProvider.DI;

namespace ATS
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
            services.Configure<AppSettings>(Configuration);

            services
                .AddMessageBusClient()
                .AddAggregateSearchService()
                .AddAggregateDictionariesService()
                .AddTuiProvider()
                .AddOtherProvider();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseMvc(routes =>
            {
                AppSettings settings = routes.ServiceProvider.GetService<IOptions<AppSettings>>().Value;
                string prefix = $"api/{settings.ApiVersion}";
                routes.MapRoute(
                    name: "tours",
                    template: $"{prefix}/{{controller=Tours}}/{{action=Search}}");
                routes.MapRoute(
                    name: "dictionaries_DepartureCities",
                    template: $"{prefix}/{{controller=Dictionaries}}/{{action=DepartureCities}}");
                routes.MapRoute(
                    name: "dictionaries_Countries",
                    template: $"{prefix}/{{controller=Dictionaries}}/{{action=Countries}}");
                routes.MapRoute(
                    name: "dictionaries_Cities",
                    template: $"{prefix}/{{controller=Dictionaries}}/{{action=Cities}}");
                routes.MapRoute(
                    name: "dictionaries_Hotels",
                    template: $"{prefix}/{{controller=Dictionaries}}/{{action=Hotels}}");
                routes.MapRoute(
                    name: "dictionaries_Hotel",
                    template: $"{prefix}/{{controller=Dictionaries}}/{{action=Hotel}}/{{id}}");
            });
        }
    }
}
