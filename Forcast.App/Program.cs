using Forcast.Intergration.ApiClients;
using Forcast.Intergration.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register IHttpClientFactory
builder.Services.AddHttpClient(); // This line is crucial
//builder.Services.AddControllers();

// Register your MaterialMasterApiClient
builder.Services.AddScoped<IMaterialMasterApiClient, MaterialMasterApiClient>();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowSpecificOrigin",
//        builder => builder.WithOrigins("https://localhost:7035") // Specify the exact origins
//                          .AllowAnyMethod() // Allows all methods
//                          .AllowAnyHeader() // Allows all headers
//                          .AllowCredentials()); // Allows credentials such as cookies, authorization headers, etc.
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
//app.UseCors("AllowSpecificOrigin");
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=MaterialMaster}/{action=Index}/{id?}");

app.Run();
