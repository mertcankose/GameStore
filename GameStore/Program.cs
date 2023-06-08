using GameStore.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddResponseCaching();
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<UserContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:UserConnection"]);
    opts.EnableSensitiveDataLogging(true);
});


var app = builder.Build();


var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<UserContext>();
SeedData.SeedDatabase(context);

app.UseResponseCaching();

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
