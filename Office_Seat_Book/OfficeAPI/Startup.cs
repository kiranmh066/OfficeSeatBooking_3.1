using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Office_Seat_Book_BLL.Services;
using Office_Seat_Book_DLL;
using Office_Seat_Book_DLL.Repost;
using Office_Seat_Book_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeAPI
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
            string connectionStr = Configuration.GetConnectionString("sqlConnection");
            services.AddDbContext<Office_DB_Context>(options => options.UseSqlServer(connectionStr));

            services.AddTransient<FloorService, FloorService>();
            services.AddTransient<IFloorRepost, FloorRepost>();
            services.AddTransient<SeatService, SeatService>();
            services.AddTransient<ISeatRepost, SeatRepost>();
            services.AddTransient<BookingService, BookingService>();
            services.AddTransient<IBookingRepost, BookingRepost>();
            services.AddTransient<ParkingService, ParkingService>();
            services.AddTransient<IParkingRepost, ParkingRepost>();
            services.AddTransient<EmployeeService, EmployeeService>();
            services.AddTransient<IEmployeeRepost, EmployeeRepost>();
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "office API",
                    Description = "Office secure web System API",
                });
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Office API"));

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
