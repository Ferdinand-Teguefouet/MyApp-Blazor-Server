using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MyApp.Models.Opts;
using MyApp.Opts;
using MyApp.Services;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;

namespace MyApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            //services.AddSingleton<IWeatherForecastService, InMemoryWeatherForecastService>();
            //services.AddSingleton<IWeatherForecastService, HttpWeatherForecastService>();
            services.AddHttpClient<IWeatherForecastService, HttpWeatherForecastService>((serviceProvider, client) => {
                //client.BaseAddress = new System.Uri(uriString: "http://localhost:6000");


                //client.BaseAddress = new System.Uri(Configuration[key: "Api:Url"]); // ':' permet de naviguer en json d'un objet parent vers enfant ...

                // la meilleure option ou meilleure façon de faire
                var options = serviceProvider.GetRequiredService<IOptions<ApiOptions>>();
                client.BaseAddress = new System.Uri(options.Value.Url); 
            })
                //.AddPolicyHandler(GetHttpPolicy()) pour le WeatherForecasts --> gestion de la résilience
                .AddPolicyHandler(GetHttpPolicyForPizza()); // ¨Pour les pizzas --> gestion de la résilience

            // Utilisation du pattern AddOptions
            services.AddOptions();
            services.Configure<ApiOptions>(Configuration.GetSection(key: "Api"));
            services.Configure<PizzaApiOptions>(Configuration.GetSection("Api"));


            //Pour la partie pizza -- singleton parce que la gestion de notre pizza est unique. on a pas besoin de plusieurs instances en mémoire.
            //services.AddSingleton<IPizzaManager, InMemoryPizzaManager>();

            services.AddHttpClient<IPizzaManager, HttpPizzaManager>((sp, client) =>{
                var options = sp.GetRequiredService<IOptions<PizzaApiOptions>>();
                client.BaseAddress = new Uri(options.Value.Url);
            });
        }

        //Polly
        private IAsyncPolicy<HttpResponseMessage> GetHttpPolicyForPizza()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(3, i => TimeSpan.FromMilliseconds(300 + (i * 100)));
        }
        private IAsyncPolicy<HttpResponseMessage> GetHttpPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(5, retry => System.TimeSpan.FromMilliseconds(200 + (retry * 150)));
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
