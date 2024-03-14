using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleCrm.SqlDbServices;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using SimpleCrm.WebApi.Auth;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.AspNetCore.Builder;
using NSwag.Generation.Processors.Security;
using NSwag;
using NSwag.AspNetCore;
using Microsoft.Net.Http.Headers;
using System.Text.Json.Serialization;
using SimpleCrm.WebApi.Filters;

namespace SimpleCrm.WebApi
{
    public class Startup
    {
        private const string SecretKey = "sdkdhsHOQPdjspQNSHsjsSDQWJqzkpdn";
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

            services.AddOpenApiDocument(options =>
            {
                options.DocumentName = "v1";
                options.Title = "Simple CRM";
                options.Version = "1.0";
                options.DocumentProcessors.Add(new SecurityDefinitionAppender("JWT token",
                    new List<string>(),
                    new OpenApiSecurityScheme
                    {
                        In = OpenApiSecurityApiKeyLocation.Header,
                        Name = "Authorization",
                        Type = OpenApiSecuritySchemeType.ApiKey
                    }
                ));
                options.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT token"));
            });
            //services.AddSwaggerDocument();

            //var secretKey = Configuration["Tokens:SigningSecretKey"];
            //var _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

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
                 configureOptions.IncludeErrorDetails = true;
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


            services.AddControllersWithViews(o =>
            {
                o.Filters.Add(typeof(GlobalExceptionFilter));
            });

            services.AddResponseCaching();

            services.AddControllersWithViews().AddJsonOptions(options =>
            {
                var settings = options.JsonSerializerOptions;

                settings.PropertyNameCaseInsensitive = true;
                settings.Converters.Add(new JsonStringEnumConverter());
            });

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

            app.UseStaticFiles(new StaticFileOptions
                {
                OnPrepareResponse = ctx =>
                {
                    const int durationInSeconds = 60 * 60 * 24; // 1 day
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] = "public, max-age=" + durationInSeconds;
                }
            });
            app.UseSpaStaticFiles();

            //app.UseOpenApi();
            //app.UseSwaggerUi();

            app.UseRouting();
            app.UseResponseCaching();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseOpenApi();
            app.UseSwaggerUi3(settings =>
            {
                var microsoftOptions = Configuration.GetSection(nameof(MicrosoftAuthSettings));
                settings.OAuth2Client = new OAuth2ClientSettings
                {
                    ClientId = microsoftOptions[nameof(MicrosoftAuthSettings.ClientId)],
                    ClientSecret = microsoftOptions[nameof(MicrosoftAuthSettings.ClientSecret)],
                    AppName = "Simple CRM",
                    Realm = "Nexul Academy"
                };
            });

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