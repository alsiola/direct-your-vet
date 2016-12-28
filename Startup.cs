using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AutoMapper;
using DYV.Data;
using DYV.Models;
using DYV.Services;
using DYV.Services.Options;
using DYV.Services.Places;
using DYV.Services.Practices;
using DYV.Services.MailSMS;
using DYV.Services.Authentication;
using DYV.Services.Providers;
using System.Text.RegularExpressions;
using DYV.Services.DayListService;
using DYV.Services.User;
using DYV.Services.Admin;
using DYV.Services.ClientRelations;
using Stripe;
using Microsoft.ApplicationInsights;

namespace DYV
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();

                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<QRCodeTokenProvider>("QR Code");

            services.AddAutoMapper();

            services.AddMvc();

            services.AddOptions();

            services.Configure<SparkPostOptions>(Configuration);
            services.Configure<DragonFlySmsOptions>(Configuration);
            services.Configure<GoogleMapsOptions>(Configuration);
            services.Configure<QRCodeTokenProviderOptions>(Configuration);
            services.Configure<StripeOptions>(Configuration);

            // Add application services.

            services.AddScoped<IEmailSender, AuthMessageSender>();
            services.AddScoped<IEmailProvider, SparkPostMailer>();
            services.AddScoped<ISmsProvider, DragonflySmsSender>();
            services.AddScoped<ISmsSender, AuthMessageSender>();
            services.AddScoped<IPlacesService, PlacesService>();
            services.AddScoped<IPracticesService, PracticesService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDayListService, DayListService>();
            services.AddScoped<IClientRelationsService, ClientRelationsService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddSingleton<IDateProvider, DateProvider>();
            services.AddScoped<IUserProvider, UserProvider>();
            services.AddSingleton<IInviteCodeProvider, InviteCodeProvider>();
            services.AddSingleton<ApiKeyManager>();
            services.AddSingleton<TelemetryClient>(new TelemetryClient());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseFacebookAuthentication(new FacebookOptions()
            {
                AppId = Configuration["Authentication:Facebook:AppId"],
                AppSecret = Configuration["Authentication:Facebook:AppSecret"]
            });

            app.UseGoogleAuthentication(new GoogleOptions()
            {
                ClientId = Configuration["Authentication:Google:ClientId"],
                ClientSecret = Configuration["Authentication:Google:ClientSecret"]
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            StripeConfiguration.SetApiKey(Configuration["StripeTestKeySecret"]);
        }        
    }
    
}
