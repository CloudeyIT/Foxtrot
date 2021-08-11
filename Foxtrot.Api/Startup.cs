using System;
using System.Threading.Tasks;
using Foxtrot.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PuppeteerSharp;

namespace Foxtrot.Api
{
    public class Startup
    {
        public Startup (IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services)
        {
            services.AddControllers();
            services.AddLogging();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Foxtrot", Version = "v1"});
            });

            services.AddSingleton(_ =>
                {
                    var revision = new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultChromiumRevision).Result; // Chrome 92.0.4515.131 (2021-08-02)
                    var logger = _.GetRequiredService<ILogger<Startup>>();
                    if (revision.Downloaded)
                    {
                        logger.LogInformation($"Downloaded Chrome revision {revision.Revision}");
                    }
                    else
                    {
                        logger.LogError($"Failed to download Chrome revision {revision.Revision}");
                    }
                    
                    return Puppeteer.LaunchAsync(new LaunchOptions { Headless = true }).Result;
                }
            );
            services.AddScoped<PdfService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Foxtrot v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}