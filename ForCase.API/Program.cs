using Forcast.Data.Infrastructure.Interfaces;
using Forcast.Data.Infrastructure;
using Forcast.Service.Constracts;
using Forcast.Data.Entities;
using Forcast.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Task01Context>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IMaterialMasterService, MaterialMasterService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("https://localhost:7035") // Specify the exact origins
                          .AllowAnyMethod() // Allows all methods
                          .AllowAnyHeader() // Allows all headers
                          .AllowCredentials()); // Allows credentials such as cookies, authorization headers, etc.
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");
app.UseAuthorization();

app.MapControllers();

app.Run();
