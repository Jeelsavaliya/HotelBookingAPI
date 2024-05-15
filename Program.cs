using AutoMapper;
using HotelBookingAPI.Data;
using HotelBookingAPI.Extensions;
using HotelBookingAPI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using HotelBookingAPI.Models;
using HotelBookingAPI.Service.IService;
using HotelBookingAPI.Service;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Example API",
                    Version = "v1",
                    Description = "An example of an ASP.NET Core Web API",
                    Contact = new OpenApiContact
                    {
                        Name = "Example Contact",
                        Email = "example@example.com",
                        Url = new Uri("https://example.com/contact"),
                    },
                });

            });

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("myConnectionStrings")));

//AutoMapper
#region Mapper
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#endregion

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));

builder.Services.AddTransient<IMailService, MailService>();

builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
/*builder.Services.AddEndpointsApiExplorer();*/

//JWT Authentication
#region JWT Authentication
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
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
                    Id =  JwtBearerDefaults.AuthenticationScheme
                }
            }, new string[]{ }
        }
    });
});

builder.AddAppAuthetication();

#endregion

/*builder.Services.AddAuthorization();*/

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(option =>
    {
        option.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
