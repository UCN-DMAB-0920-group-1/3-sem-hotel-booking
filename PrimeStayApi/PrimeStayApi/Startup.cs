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
            services.AddScoped<IDao<HotelDal>>(s => DaoFactory.Create<HotelDal>(dataContext));
            services.AddScoped<IDao<RoomDal>>(s => DaoFactory.Create<RoomDal>(dataContext));
            services.AddScoped<IDao<LocationDal>>(s => DaoFactory.Create<LocationDal>(dataContext));

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
