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
using Microsoft.OpenApi.Models;
using M_API.Infrastructure.Repositories;

dotenv.net.DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API V1", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter the JWT token in the format: Bearer {token}"
    };

    c.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new string[] {}
        }
    };


    c.AddSecurityRequirement(securityRequirement);
});
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPendingRegistrationRepository, PendingRegistrationRepository>();
builder.Services.AddScoped<ICustomerProfileRepository, CustomerProfileRepository>();
builder.Services.AddScoped<IVendorProfileRepository, VendorProfileRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductStripeRepository, ProductStripeRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<CreateCheckoutSessionUseCase>();
builder.Services.AddScoped<ConfirmOrderPaymentUseCase>();
builder.Services.AddScoped<CreateProductUseCase>();
builder.Services.AddScoped<CreateCustomerProfileUseCase>();
builder.Services.AddScoped<ActivateUserUseCase>();
builder.Services.AddScoped<RegisterPendingUserUseCase>();
builder.Services.AddScoped<LoginUseCase>();
builder.Services.AddScoped<CreateVendorProfileUseCase>();
builder.Services.AddScoped<AddItemToCartUseCase>();
builder.Services.AddScoped<CreateOrderFromCartUseCase>();

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

var stripeSettings = new Infrastructure.Payments.Stripe.StripeSettings
{
    PublicKey = Environment.GetEnvironmentVariable("STRIPE_PUBLIC_KEY")!,
    SecretKey = Environment.GetEnvironmentVariable("STRIPE_SECRET_KEY")!,
    WebhookSecret = Environment.GetEnvironmentVariable("WEBHOOK_SECRET")!
};

Stripe.StripeConfiguration.ApiKey = stripeSettings.SecretKey;

builder.Services.AddSingleton(stripeSettings);

builder.Services.AddScoped<IStripeProductService, StripeProductService>();
builder.Services.AddSingleton(new Stripe.ProductService());
builder.Services.AddSingleton(new Stripe.PriceService());

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

app.UseMiddleware<JwtMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
