using Application.UseCases;
using Application.Security;
using Domain.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Infrastructure.Services;
using Application.Services;
using M_API.Domain.Repositories;
using M_API.Application.UseCases;

dotenv.net.DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IPendingRegistrationRepository, PendingRegistrationRepository>();
builder.Services.AddScoped<IActivationTokenRepository, ActivationTokenRepository>();

builder.Services.AddScoped<ActivateUserUseCase>();
builder.Services.AddScoped<RegisterPendingUserUseCase>();
builder.Services.AddScoped<CreateUserUseCase>();
builder.Services.AddScoped<CreateProductUseCase>();
builder.Services.AddScoped<LoginUseCase>();

builder.Services.Configure<JwtSettings>(options =>
{
    options.SecretKey = Environment.GetEnvironmentVariable("JWT_KEY")!;
    options.Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER")!;
    options.Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE")!;
    options.ExpirationMinutes = int.Parse(
        Environment.GetEnvironmentVariable("JWT_DURATION_IN_MINUTES")!
    );
});

var smtpSettings = new SmtpSettings
{
    Host = Environment.GetEnvironmentVariable("SMTP_HOST")!,
    Port = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT")!),
    Username = Environment.GetEnvironmentVariable("SMTP_USER")!,
    Password = Environment.GetEnvironmentVariable("SMTP_PASS")!,
    From = Environment.GetEnvironmentVariable("SMTP_FROM")!,
    EnableSsl = true
};

builder.Services.AddSingleton(smtpSettings);
builder.Services.AddScoped<IEmailSender, SmtpEmailSender>();


builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
        ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                Environment.GetEnvironmentVariable("JWT_KEY")!
            )
        )
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
