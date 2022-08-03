using Serilog;
using HotelListingAPI.VSCode.Data;
using Microsoft.EntityFrameworkCore;
using HotelListingAPI.VSCode.Configuration;
using HotelListingAPI.VSCode.Utils;
using HotelListingAPI.VSCode.Contract;
using HotelListingAPI.VSCode.Repository;
using Microsoft.AspNetCore.Identity;
using HotelListingAPI.VSCode.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// instead of putting the password into HotelListingDbConnectionString,
// I just go with entering the password at every run; later I might see how it works with Azure Key stores.

var connectionString = builder.Configuration.GetConnectionString("HotelListingDbConnectionString");
Console.WriteLine("Please enter the Azure database password below:");
string password = Console.ReadLine();
connectionString = StringHelpers.SafeReplace(connectionString, "PASSWORD", password, true);
System.Console.WriteLine($"the used connection string is {connectionString}");
builder.Services.AddDbContext<HotelListingDbContext>(options => {
    options.UseSqlServer(connectionString);
});

builder.Services.AddIdentityCore<ApiUser>()
    .AddRoles<IdentityRole>()
    //.AddTokenProvider<DataProtectorTokenProvider<ApiUser>>("HotelListingApi")
    .AddEntityFrameworkStores<HotelListingDbContext>();
    //.AddDefaultTokenProviders();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", b => b.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
});

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));
builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ICountriesRepository, CountriesRepository>();
builder.Services.AddScoped<IHotelsRepository, HotelsRepository>();
builder.Services.AddScoped<IAuthManager, AuthManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
} else{
    System.Console.WriteLine("no development mode");
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
