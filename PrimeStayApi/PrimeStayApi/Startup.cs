using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PrimeStayApi.DataAccessLayer;
using PrimeStayApi.DataAccessLayer.DAO;
using PrimeStayApi.Enviroment;
using PrimeStayApi.Model;

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
            IDataContext dataContext = new DataContext(ENV.ConnectionString);
            services.AddScoped<IDao<HotelEntity>>(s => DaoFactory.Create<HotelEntity>(dataContext));
            services.AddScoped<IDao<RoomEntity>>(s => DaoFactory.Create<RoomEntity>(dataContext));
            services.AddScoped<IDao<LocationEntity>>(s => DaoFactory.Create<LocationEntity>(dataContext));
            services.AddScoped<IDao<BookingEntity>>(s => DaoFactory.Create<BookingEntity>(dataContext));
            services.AddScoped<IDao<PictureEntity>>(s => DaoFactory.Create<PictureEntity>(dataContext));

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
