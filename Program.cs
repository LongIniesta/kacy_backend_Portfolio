using AutoMapper;
using Kacy_Backend.Mapper;
using BussinessObjects;
using BussinessObjects.Repository;
using BussinessObjects.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using BussinessObjects.Entities;
using Services.Services.InterfaceServices;
using Services.Services.ImplementServices;
using Services.Exceptions;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Options;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//JWT + Noti Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
        "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
        "Enter 'Bearer' [space] and then your token in the text input below. \r\n\r\n" +
        "Example: \"Bearer 12345abdcef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            },
            Scheme = "oauth2",
            Name = "Bearer",
            In = ParameterLocation.Header,
        },
        new List<string>()
        }
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});
builder.Services.AddAutoMapper(typeof(Mapping));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddScoped<IAdminServices, AdminService>();
builder.Services.AddScoped<IAuthorizationHandler, CustomAuthorizationHandler>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IPackService, PackService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IAnalysisService, AnalysisService>();

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("_myAllowSpecificOrigins",
        builder =>
        {
            builder
            //.WithOrigins(GetDomain())
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

//Database Connection
builder.Services.AddDbContext<KacyManagerContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});



//JWT
var key = builder.Configuration.GetValue<string>("ApiSetting:Secret");
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

//Policy
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Customer", policy =>
    {
        policy.RequireRole("Customer", "Admin", "Staff");
        policy.AddRequirements(new CustomRequirement());
    });
    options.AddPolicy("Staff", policy =>
    {
        policy.RequireRole("Staff", "Admin");
        policy.AddRequirements(new CustomRequirement());
    });
    options.AddPolicy("Admin", policy =>
    {
        policy.RequireRole("Admin");
        policy.AddRequirements(new CustomRequirement());
    });
    options.AddPolicy("User", policy =>
    {
        policy.RequireRole("Customer", "Staff");
        policy.AddRequirements(new CustomRequirement());
    });
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

else
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Kacy.API");
        options.RoutePrefix = String.Empty;
    });
}
app.UseHttpsRedirection();
app.UseCors("_myAllowSpecificOrigins");
app.UseMiddleware(typeof(GlobalErrorHandlingMiddleware));
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
/*app.Run("https://0.0.0.0:80");
app.Run("https://0.0.0.0:7273");*/

