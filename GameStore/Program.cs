using GameStore.Extensions;
using GameStore.Middlewares;
using GameStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddResponseCaching();
builder.Services.AddControllersWithViews(); // to enable support for views

builder.Services.AddDbContext<UserContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:UserConnection"]);
    opts.EnableSensitiveDataLogging(true);
});

builder.Services.AddDbContext<ProductContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:ProductConnection"]);
    opts.EnableSensitiveDataLogging(true);
});

builder.Services.AddDbContext<CartContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:CartConnection"]);
    opts.EnableSensitiveDataLogging(true);
});

builder.Services.AddControllers();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(1800);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

app.MapControllers();

/*
const string BASEURL = "api/users";

app.MapGet($"{BASEURL}/{{id}}", async (HttpContext httpContext, UserContext userContext) =>
{
    string? id = httpContext.Request.RouteValues["id"] as string;
    if (id != null)
    {
        User? user = userContext.Users.Find(long.Parse(id));
        if(user == null)
        {
            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
        } else
        {
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize<User>(user));
        }
    }
});

app.MapGet(BASEURL, async (HttpContext httpContext, UserContext userContext) =>
{
    httpContext.Response.ContentType = "application/json";
    await httpContext.Response.WriteAsync(JsonSerializer.Serialize<IEnumerable<User>>(userContext.Users));
});

app.MapPost(BASEURL, async (HttpContext httpContext, UserContext userContext) =>
{
    User? user = await JsonSerializer.DeserializeAsync<User>(httpContext.Request.Body);

    if(user!=null)
    {
        await userContext.AddAsync(user);
        await userContext.SaveChangesAsync();
        httpContext.Response.StatusCode = StatusCodes.Status200OK;
    }
});


*/

var userContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<UserContext>();
SeedData.SeedDatabaseUser(userContext);

var productContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<ProductContext>();
SeedData.SeedDatabaseProduct(productContext);

var cartContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<CartContext>();
SeedData.SeedDatabaseCart(cartContext);


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

app.UseSession();

//app.UseTest();
app.UseMiddleware<TestMiddleware>();
app.UseAuthorization();

app.MapDefaultControllerRoute();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
