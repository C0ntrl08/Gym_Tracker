using Microsoft.Win32;

namespace GymTrackerApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Configure MySQL connection (using Pomelo MySQL provider)
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
            )
            );

            // Register HttpClient (custom - used by Razor Pages for API calls).
            builder.Services.AddHttpClient("DevClient").ConfigurePrimaryHttpMessageHandler(() => {
                var handler = new HttpClientHandler();
                // This bypasses all SSL validation – do not use in production!
                handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                return handler;
            });

            // Register HttpClient (used by Razor Pages for API calls).
            // builder.Services.AddHttpClient();

            // Add Razor Pages for the Admin UI.
            builder.Services.AddRazorPages();

            // Injecting JWTService
            builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // JWT Authentication configuration
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
                    };
                });

            // Add CORS policy
            builder.Services.AddCors(options =>
                options.AddPolicy("AllowAll", policy =>
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader()));

            builder.Services.AddAuthorization();
            // Register session services for saving the JWT token from admin login.
            builder.Services.AddSession();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            // Enable serving static files (if needed for CSS/JS).
            app.UseStaticFiles();

            app.UseRouting();

            // Addition to the Admin-UI Web
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("AllowAll");
            // Map API controllers.
            app.MapControllers();
            // Map Razor Pages endpoints.
            app.MapRazorPages();

            app.Run();
        }
    }
}
