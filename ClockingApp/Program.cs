﻿var builder = WebApplication.CreateBuilder(args);

var mongoDBConnection = builder.Configuration["mongoDBConnection"];

builder.Services.AddSingleton<MongoDB.Driver.IMongoClient>(instance =>
{
    return new MongoDB.Driver.MongoClient(mongoDBConnection);
});

builder.Services.AddScoped(typeof(ClockingApp.Repository.IMongoRepositoryBase<>), typeof(ClockingApp.Repository.MongoRepositoryBase<>));

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

