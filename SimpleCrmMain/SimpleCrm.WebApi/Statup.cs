using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleCrm.SqlDbServices;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using SimpleCrm.WebApi.Auth;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

namespace SimpleCrm.WebApi
{
    public class Startup
    {
        private const string SecretKey = "sdkdhsHOQPdjspQNSHsjsSDQWJqzkpdnf";
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionStr = Configuration.GetConnectionString("SimpleCrmConnection");
            var googleOptions = Configuration.GetSection(nameof(GoogleAuthSettings));
            var microsoftOptions = Configuration.GetSection(nameof(MicrosoftAuthSettings));

            services.Configure<GoogleAuthSettings>(options =>
            {
                options.ClientId = googleOptions[nameof(GoogleAuthSettings.ClientId)];
                options.ClientSecret = googleOptions[nameof(GoogleAuthSettings.ClientSecret)];
            });

            services.Configure<MicrosoftAuthSettings>(options =>
            {
                options.ClientId = microsoftOptions[nameof(MicrosoftAuthSettings.ClientId)];
                options.ClientSecret = microsoftOptions[nameof(MicrosoftAuthSettings.ClientSecret)];
            });

            services.AddDbContext<SimpleCrmDbContext>(options =>
                options.UseMySql(connectionStr, ServerVersion.AutoDetect(connectionStr)));
            services.AddDbContext<CrmIdentityDbContext>(options =>
                options.UseMySql(connectionStr, ServerVersion.AutoDetect(connectionStr)));

            var secretKey = Configuration["Tokens:SigningSecretKey"];
            _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            var jwtOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationPrms = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtOptions[nameof(JwtIssuerOptions.Issuer)],
                ValidateAudience = true,
                ValidAudience = jwtOptions[nameof(JwtIssuerOptions.Audience)],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,
                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(configureOptions =>
             {
                 configureOptions.ClaimsIssuer = jwtOptions[nameof(JwtIssuerOptions.Issuer)];
                 configureOptions.TokenValidationParameters = tokenValidationPrms;
                 configureOptions.SaveToken = true;
             });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim(
                    Constants.JwtClaimIdentifiers.Rol,
                    Constants.JwtClaims.ApiAccess
                    ));
            });

            var identityBuilder = services.AddIdentityCore<CrmUser>(o =>
            {

            });
            identityBuilder = new IdentityBuilder(
                identityBuilder.UserType,
                typeof(IdentityRole),
                identityBuilder.Services);
            identityBuilder.AddEntityFrameworkStores<CrmIdentityDbContext>();
            identityBuilder.AddRoleValidator<RoleValidator<IdentityRole>>();
            identityBuilder.AddRoleManager<RoleManager<IdentityRole>>();
            identityBuilder.AddSignInManager<SignInManager<CrmUser>>();
            identityBuilder.AddDefaultTokenProviders();


            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddSpaStaticFiles(config =>
            {
                config.RootPath = Configuration["SpaRoot"];
            });

            services.AddSingleton<IJwtFactory, JwtFactory>();

            services.AddScoped<ICustomerData, SqlCustomerData>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseWhen(
                context => !context.Request.Path.StartsWithSegments("/api"),
                appBuilder => appBuilder.UseSpa(spa =>
                {
                    spa.Options.SourcePath = "../simple-crm-cli";
                    if (env.IsDevelopment())
                    {
                        spa.Options.SourcePath = "../simple-crm-cli";
                        spa.Options.StartupTimeout = new TimeSpan(0, 0, 300);
                        spa.UseAngularCliServer(npmScript: "start");
                    }
                }));
        }
    }
}