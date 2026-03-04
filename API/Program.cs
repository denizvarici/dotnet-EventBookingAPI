using API.Middleware;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Security;
using Services.Abstract;
using Services.Concrete;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Services.Jwt;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

//DI registry
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IValidationService, ValidationService>();

//db context registry with SqlServer
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});

//Auto mapper registry
builder.Services.AddAutoMapper(cfg => cfg.LicenseKey = SecureDataHolder.AutoMapperLicenceKey, AppDomain.CurrentDomain.GetAssemblies()); ;


//OpenApi Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.Title = "API";
    config.Version = "v1";
    config.AddSecurity("Bearer", new NSwag.OpenApiSecurityScheme
    {
        Type = NSwag.OpenApiSecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Enter JWT token"
    });
    config.OperationProcessors.Add(
        new NSwag.Generation.Processors.Security.AspNetCoreOperationSecurityScopeProcessor("Bearer")
    );
});

//Api controllers activation
builder.Services.AddControllers();

//jwt configuration
builder.Services.AddScoped<JwtService>();
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});
builder.Services.AddAuthorization();


var app = builder.Build();

//middlewares
app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
    //app.MapOpenApi();
    //app.UseSwaggerUi(options =>
    //{
    //    options.DocumentPath = "/openapi/v1.json";
    //});
}


//last configurations
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();




app.Run();
