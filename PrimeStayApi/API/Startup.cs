using API.Services;
using DataAccessLayer;
using Enviroment;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.Text;

namespace API
{
    public class Startup
    {
        private string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IDataContext dataContext = new SqlDataContext(ENV.ConnectionString);
            services.AddScoped(s => DaoFactory.Create<HotelEntity>(dataContext));
            services.AddScoped(s => DaoFactory.Create<RoomTypeEntity>(dataContext));
            services.AddScoped(s => DaoFactory.Create<LocationEntity>(dataContext));
            services.AddScoped(s => DaoFactory.Create<BookingEntity>(dataContext));
            services.AddScoped(s => DaoFactory.Create<PictureEntity>(dataContext));
            services.AddScoped(s => DaoFactory.Create<RoomEntity>(dataContext));
            services.AddScoped(s => DaoFactory.Create<UserEntity>(dataContext));
            services.AddScoped(s => DaoFactory.Create<CustomerEntity>(dataContext));

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyMethod();
                        builder.AllowAnyHeader();
                    });
            });
            services.AddControllers();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSettings:SecretKey"]))
                    };
                });

            services.AddTransient<IAccountService, AccountService>();
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
            app.UseCors(MyAllowSpecificOrigins);


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
