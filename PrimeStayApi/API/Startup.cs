using API.Services;
using DataAccessLayer;
using Environment;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models;
using System;
using System.Text;
using Version = Database.Version;

namespace API
{
    public class Startup
    {
        private string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var env = Configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT");

            IDataContext dataContext = env switch
            {
                var connectionString when env.Equals("Development") => new SqlDataContext(ENV.ConnectionStringDev),
                var connectionString when env.Equals("Release") => new SqlDataContext(ENV.ConnectionString),
                _ => null
            };

            services.AddScoped(s => DaoFactory.Create<HotelEntity>(dataContext));
            services.AddScoped(s => DaoFactory.Create<RoomTypeEntity>(dataContext));
            services.AddScoped(s => DaoFactory.Create<LocationEntity>(dataContext));
            services.AddScoped(s => DaoFactory.Create<BookingEntity>(dataContext));
            services.AddScoped(s => DaoFactory.Create<PictureEntity>(dataContext));
            services.AddScoped(s => DaoFactory.Create<RoomEntity>(dataContext));
            services.AddScoped(s => DaoFactory.Create<UserEntity>(dataContext));
            services.AddScoped(s => DaoFactory.Create<CustomerEntity>(dataContext));
            services.AddScoped(s => DaoFactory.Create<PriceEntity>(dataContext));

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

            services.AddSwaggerGen(setup =>
            {
                // Include 'SecurityScheme' to use JWT Authentication
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token ín field below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
            }); // JWT Token

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
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "PrimeStayApi v1"));

                Version.Drop(ENV.ConnectionStringDev);
                Version.Upgrade(ENV.ConnectionStringDev);
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