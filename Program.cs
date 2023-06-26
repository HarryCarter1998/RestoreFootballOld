using Microsoft.EntityFrameworkCore;
using RestoreFootball.Data;
using RestoreFootball.Data.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<RestoreFootballContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SMARTERASP_CONNECTIONSTRING") ?? throw new InvalidOperationException("Connection string 'AZURE_SQL_CONNECTIONSTRING' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IGameweekService, GameweekService>();

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
