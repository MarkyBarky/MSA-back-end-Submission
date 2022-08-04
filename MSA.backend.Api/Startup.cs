using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MSA.backend.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MSA.backend.Api
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
            services.AddHttpClient("pokemon", configureClient: client =>
            {
                client.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
            });
            services.AddControllers();
            services.AddDbContext<IWebAPIDBContext, WebAPIDBContext>(options => options.UseSqlite(Configuration.GetConnectionString("ConnectionString")));
            
            services.AddScoped<iDbRepo, DbRepo>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MSA.backend.Api", Version = "v1" });
            });
            services.AddSwaggerDocument(options =>
            {
                options.DocumentName = "MSA.backend.Api";
                options.Version = "V1";

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
                
            //    //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MSA.backend.Api v1"));
            //}

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
