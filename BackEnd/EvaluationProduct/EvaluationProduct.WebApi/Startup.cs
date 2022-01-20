using System.Diagnostics.CodeAnalysis;
using EvaluationProduct.Context;
using EvaluationProduct.Context.Repositories;
using EvaluationProduct.Context.UnitOfWork;
using EvaluationProduct.Data;
using EvaluationProduct.Domain.Interfaces;
using EvaluationProduct.Lib;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EvaluationProduct.WebApi
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        string CorsPolicy = "CorsPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: CorsPolicy,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost", "http://localhost:4200", "http://localhost:3000", "http://localhost:5000").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                    });
            });

            services.AddDbContext<ApplicationContext>(opt => opt.UseInMemoryDatabase(databaseName: "EFInMemory"));
            services.AddScoped<ApplicationContext>();

            services.AddControllers();
            services.AddSwaggerGen();
            
            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            #endregion
            
            services.AddTransient<IProductProvider, ProductProvider>();
            services.AddTransient<IProductLoader, ProductLoader>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger(options =>
                {
                    options.SerializeAsV2 = true;
                });
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                });
            }

            app.UseRouting();

            app.UseCors(CorsPolicy);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireCors(CorsPolicy);
            });
        }
    }
}
