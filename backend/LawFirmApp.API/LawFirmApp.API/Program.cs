using Microsoft.EntityFrameworkCore;
using LawFirmApp.Data;
using LawFirmApp.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LawFirmApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Load dbsettings.json
builder.Configuration.AddJsonFile("dbsettings.json", optional: false, reloadOnChange: true);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IUserService, UserService>(); // Register UserService

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAttorneyRepository, AttorneyRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", builder =>
    {
        builder.WithOrigins("http://localhost:4200") // Angular app URL
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add middleware
app.UseCors("AllowAngularApp");

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseAuthentication();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
