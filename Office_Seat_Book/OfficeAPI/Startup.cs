using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Office_Seat_Book_BLL.Services;
using Office_Seat_Book_DLL;
using Office_Seat_Book_DLL.Repost;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Email;
using System.Net;

namespace OfficeAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

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
            services.AddTransient<HelpService, HelpService>();
            services.AddTransient<IHelpRepost, HelpRepost>();

            services.AddTransient<SecretKeyService, SecretKeyService>();
            services.AddTransient<ISecretKeyRepost, SecretKeyRepost>();

            services.AddTransient<HelpService, HelpService>();
            services.AddTransient<IHelpRepost, HelpRepost>();

            services.AddTransient<SecretKeyService, SecretKeyService>();
            services.AddTransient<ISecretKeyRepost, SecretKeyRepost>();

            var Logger = new LoggerConfiguration()
          .MinimumLevel.Information()
          .MinimumLevel.Override("Google", LogEventLevel.Warning)
          .Enrich.FromLogContext()
          .WriteTo.File("LogTesting.log", LogEventLevel.Information, fileSizeLimitBytes: 10_000_000, rollOnFileSizeLimit: true, shared: true)
           .WriteTo.Email(new EmailConnectionInfo
           {
               FromEmail = "harshjeet35@gmail.com",
               //FromEmail = "kiran.mh@valtech.com",
               ToEmail = "atulyaaj6@gmail.com",
               MailServer = "smtp.gmail.com",
               //MailServer = "192.168.141.52",
               NetworkCredentials = new NetworkCredential
               {
                   UserName = "harshjeet35@gmail.com",
                   Password = "Harsh@123"
               },
               EnableSsl = true,
               /* Port = 29,*/
               Port = 993,
               EmailSubject = "Error in app"
           }, restrictedToMinimumLevel: LogEventLevel.Error, batchPostingLimit: 1)
            .CreateLogger();



            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog(Logger);
            });



            //  var Logger = new LoggerConfiguration()
            //.MinimumLevel.Information()
            //.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            //.Enrich.FromLogContext()
            //.WriteTo.File("LogTesting.log", LogEventLevel.Information, fileSizeLimitBytes: 10_000_000, rollOnFileSizeLimit: true, shared: true)
            //.WriteTo.Email(new EmailConnectionInfo
            //{
            //    FromEmail = "mailapp@valtech.co.in",
            //    //FromEmail = "kiran.mh@valtech.com",
            //    ToEmail = "chandan.m@valtech.com",
            //    MailServer = "192.168.130.134",
            //    //MailServer = "192.168.141.52",
            //    //NetworkCredentials = new NetworkCredential
            //    //{
            //    //    UserName = "Pankaj.Chandrakar@gmail.com",
            //    //    Password = "P@nk@j25"
            //    //},
            //    // EnableSsl = true,
            //    /* Port = 29,*/
            //    Port = 29,
            //    EmailSubject = "Error in app"
            //}, restrictedToMinimumLevel: LogEventLevel.Error, batchPostingLimit: 1)
            // .CreateLogger();

            //  services.AddLogging(loggingBuilder =>
            //  {
            //      loggingBuilder.ClearProviders();
            //      loggingBuilder.AddSerilog(Logger);
            //  });



            services.AddControllers();
            services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Office API",
                    Description = "Office Seat Booking API",
                });
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetService<Office_DB_Context>();
                    dbContext.Database.Migrate();
                }

            }
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Office API"));
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
