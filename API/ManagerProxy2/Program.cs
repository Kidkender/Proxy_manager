using ManagerProxy2.Models.Context;
using ManagerProxy2.Services.RepoBaseUnit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("db")), ServiceLifetime.Scoped);
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddCors(p => p.AddPolicy("corsapp", builders =>
{
    builders.WithOrigins(builder.Configuration.GetConnectionString("allowOrigin").Split(";")).AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
    option.SaveToken = true;
    option.RequireHttpsMetadata = false;
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidAudience = "UI",
        ValidIssuer = "API",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("dIwIV4lWGYkPlk0hcUuu9rYihYa74NHNxm0xruJmAaT.MrgySyq"))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors("corsapp");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
                   Path.Combine(Directory.GetCurrentDirectory(), "Files")),
    RequestPath = "/Files"
});

app.UseCookiePolicy();

app.MapControllers();

app.Run();
