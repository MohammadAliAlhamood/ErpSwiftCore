using Azure;
using ErpSwiftCore.API.Extensions;
using ErpSwiftCore.Application.Behaviors;
using ErpSwiftCore.Application.Features.Auth.AuthenticationLocService.Handlers;
using ErpSwiftCore.Domain.Entities.EntityAuth;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IAuthsService;
using ErpSwiftCore.Infrastructure.Validation;
using ErpSwiftCore.Notifications.Events;
using ErpSwiftCore.Persistence.Context;
using ErpSwiftCore.Persistence.Repositories;
using ErpSwiftCore.Persistence.Services.AuthsService;
using ErpSwiftCore.TenantManagement.Behaviors;
using ErpSwiftCore.TenantManagement.Interfaces;
using ErpSwiftCore.TenantManagement.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Quartz;
using System.Reflection;
var builder = WebApplication.CreateBuilder(args);
IConfiguration config = builder.Configuration;
IServiceCollection services = builder.Services;

// -----------------------------------------------------------------------------
// 1. Logging
// -----------------------------------------------------------------------------
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// -----------------------------------------------------------------------------
// 2. Configuration of Options
// -----------------------------------------------------------------------------
services.Configure<JwtOptions>(config.GetSection("ApiSettings:JwtOptions"));
// -----------------------------------------------------------------------------
// 3. Database Context
// -----------------------------------------------------------------------------
services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(config.GetConnectionString("DefaultConnection"))
);

// -----------------------------------------------------------------------------
// 4. Identity
// -----------------------------------------------------------------------------
services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();

// -----------------------------------------------------------------------------
// 5. Authentication & JWT
// -----------------------------------------------------------------------------
builder.AddAppAuthetication();         // يقرأ من "BearerApiSettings"
services.AddAuthentication();


// -----------------------------------------------------------------------------
// 6. Authorization
// -----------------------------------------------------------------------------
services.AddAuthorization();

 
// -----------------------------------------------------------------------------
// 8. Http Context / Multi-Tenancy / User Context
// -----------------------------------------------------------------------------
services.AddHttpContextAccessor();
services.AddScoped<ITenantContext, TenantContext>();
services.AddScoped<ITenantProvider, TenantProvider>();
services.AddScoped<IUserContext, UserContext>();
services.AddScoped<IUserProvider, UserProvider>();
services.AddScoped<IDbInitializer, DbInitializer>();

// -----------------------------------------------------------------------------
// 9. AutoMapper
// -----------------------------------------------------------------------------
var mappingAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic && a.GetName().Name.StartsWith("ErpSwiftCore")).ToArray();
services.AddAutoMapper(mappingAssemblies);

// -----------------------------------------------------------------------------
// 10. MediatR & Pipeline Behaviors
// -----------------------------------------------------------------------------
services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(
        Assembly.GetExecutingAssembly(),
        typeof(RegisterCommandHandler).Assembly
    );
});
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TenantContextBehavior<,>));
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UserContextBehavior<,>));

// -----------------------------------------------------------------------------
// 11. FluentValidation
// -----------------------------------------------------------------------------
services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


services.AddHttpClient();


services.AddEndpointsApiExplorer();
services.AddSwaggerGen(opt =>
{
    // تعريف الـ Bearer في هيدر الـ Swagger UI
    opt.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter Bearer {token}",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            Array.Empty<string>()
        }
    });

    // دعم JsonPatchDocument
    opt.MapType<JsonPatchDocument>(() =>
        new OpenApiSchema
        {
            Type = "array",
            Items = new OpenApiSchema
            {
                Type = "object",
                Properties = new Dictionary<string, OpenApiSchema>
                {
                    ["op"] = new OpenApiSchema { Type = "string", Enum = new List<IOpenApiAny> { new OpenApiString("add"), new OpenApiString("remove"), new OpenApiString("replace") } },
                    ["path"] = new OpenApiSchema { Type = "string" },
                    ["value"] = new OpenApiSchema { Type = "object" }
                },
                Required = new HashSet<string> { "op", "path" }
            }
        }
    );
});


services.AddCors(opt =>
{
    opt.AddPolicy("AllowAll", p => p
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
    );
});

// -----------------------------------------------------------------------------
// 15. Unit of Work & Repositories
// -----------------------------------------------------------------------------
services.AddScoped<IUnitOfWork, UnitOfWork>();
services.AddScoped<IMultiTenantUnitOfWork, MultiTenantUnitOfWork>();
// بعد تسجيل الـ UnitOfWork
services.AddScoped(typeof(IIncludeValidator<>), typeof(IncludeValidator<>));


services.AddAuthServices()
         .AddCrmServices()
        .AddInvoiceServices()
        .AddProductServices()
        .AddCompanyServices()
        .AddFinancialServices()
        .AddInventoryServices()
        .AddNotificationInfrastructure();


 

// 3. تسجيل الـ Delegate الخاص بالإرسال
services.AddScoped<NotificationTransportDelegate>(sp =>
{
    // هذا المثال يستخدم Console فقط، غيّره لاحقًا إلى EmailSender, SmsSender...
    return async (notification) =>
    {
        var logger = sp.GetRequiredService<ILogger<Program>>();
        logger.LogInformation($"[Transport] Sending notification to {notification.RecipientId}: {notification.Title}");
        return true; // نفترض أن الإرسال تم بنجاح
    };
});
// -----------------------------------------------------------------------------
// 17. MVC Controllers
// -----------------------------------------------------------------------------
services.AddControllers();

// -----------------------------------------------------------------------------
// Build & Configure Middleware Pipeline
// -----------------------------------------------------------------------------
var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    try
    {
        if (db.Database.GetPendingMigrations().Any())
        {
            logger.LogInformation("Applying migrations...");
            db.Database.Migrate();
            logger.LogInformation("Migrations applied.");
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Migration error.");
    }

    var initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    initializer.Initialize();
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Company API v1");
        c.RoutePrefix = "swagger";
    });
}

// Security & Static Files
app.UseHttpsRedirection();
app.UseStaticFiles();

// CORS, AuthN & AuthZ
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

// Routing
app.MapControllers();

// Run
app.Run();
