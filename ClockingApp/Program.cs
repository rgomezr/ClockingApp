var builder = WebApplication.CreateBuilder(args);

var mongoDBConnection = builder.Configuration["mongoDBConnection"];

builder.Services.Configure<ClockingApp.Settings.MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));

builder.Services.AddSingleton<ClockingApp.Settings.IMongoDBSettings>(service =>
        service.GetRequiredService<Microsoft.Extensions.Options.IOptions<ClockingApp.Settings.MongoDBSettings>>().Value);

builder.Services.Configure<ClockingApp.Settings.UserSettings>(builder.Configuration.GetSection("UserSettings"));

builder.Services.AddSingleton<ClockingApp.Settings.IUserSettings>(service =>
        service.GetRequiredService<Microsoft.Extensions.Options.IOptions<ClockingApp.Settings.UserSettings>>().Value);

builder.Services.AddSingleton<MongoDB.Driver.IMongoClient>(instance =>
{
    return new MongoDB.Driver.MongoClient(mongoDBConnection);
});

builder.Services.AddSingleton<ClockingApp.CustomServices.ClockingService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

