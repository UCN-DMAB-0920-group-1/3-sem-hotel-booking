using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PrimeStay.DataAccessLayer;
using PrimeStay.DataAccessLayer.DAO;
using PrimeStay.Model;
using PrimeStayApi.Enviroment;
using System.Data;

namespace PrimeStayApi
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
            IDataContext<IDbConnection> dataContext = new SQLDataContext(ENV.ConnectionString);
            services.AddScoped<IDao<Hotel>>(s => DaoFactory.Create<Hotel>(dataContext));
            services.AddScoped<IDao<Room>>(s => DaoFactory.Create<Room>(dataContext));
            services.AddScoped<IDao<Location>>(s => DaoFactory.Create<Location>(dataContext));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PrimeStayApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
