using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PrimeStay.MVC.DataAccessLayer;
using PrimeStay.MVC.DataAccessLayer.DAO;
using PrimeStay.MVC.DataAccessLayer.DTO;
using PrimeStay.MVC.Controllers;
using RestSharp;

namespace PrimeStay.MVC
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
            IDataContext<IRestClient> dataContext = new RestDataContext();
            services.AddScoped<IDao<RoomDto>>(s => DaoFactory.Create<RoomDto>(dataContext));
            services.AddScoped<IDao<HotelDto>>(s => DaoFactory.Create<HotelDto>(dataContext));
            services.AddScoped<IDao<LocationDto>>(s => DaoFactory.Create<LocationDto>(dataContext));
            services.AddScoped<IDao<BookingDto>>(s => DaoFactory.Create<BookingDto>(dataContext));

            services.AddControllersWithViews();
            services.AddSession(options =>
            {
                options.IdleTimeout = System.TimeSpan.FromMinutes(15);
            });
            services.AddMemoryCache();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Hotel}/{action=Index}/{id?}");
            });
        }
    }
}
