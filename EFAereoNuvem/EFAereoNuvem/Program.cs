using EFAereoNuvem;
using EFAereoNuvem.Data;
using EFAereoNuvem.Repository;
using EFAereoNuvem.Repository.Interface;
using EFAereoNuvem.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configure JWT Authentication
var Key = Encoding.ASCII.GetBytes(Configuration.JwtKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Add services to the container.
builder.Services.AddControllersWithViews();

// Database Context
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IFlightRepository, FlightRepository>();
builder.Services.AddScoped<IAirplaneRepository, AirplaneRepository>();
builder.Services.AddScoped<IScaleRepository, ScaleRepository>();

// Services
builder.Services.AddTransient<TokenServices>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<AuthService>();
builder.Services.AddHttpClient<AuthService>();
builder.Services.AddHttpClient<ApiClient>();

// Configure HttpClient para API
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7157/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// A ORDEM É IMPORTANTE: Authentication antes de Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();