using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using WeBScraper_CourseProject_;
using WeBScraper_CourseProject_.Areas.Identity;
using WeBScraper_CourseProject_.Data;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

  
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

builder.Services.AddTransient<ScraperService>().AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddMudServices();
builder.Services.AddAuthentication()
   .AddGoogle(options =>
   {
       options.ClientId = builder.Configuration.GetSection("Authentication")
        .GetValue<string>("ClientId");
       options.ClientSecret = builder.Configuration.GetSection("Authentication")
       .GetValue<string>("ClientSecret");

   })
   .AddFacebook(options =>
   {
       options.ClientId = builder.Configuration.GetSection("AuthenticationFB")
       .GetValue<string>("ClientId");
       options.ClientSecret = builder.Configuration.GetSection("AuthenticationFB")
       .GetValue<string>("ClientSecret");
   })
   .AddTwitter(options =>
   {
       options.ConsumerKey = builder.Configuration.GetSection("AuthenticationTwitter")
       .GetValue<string>("ConsumerKey");
       options.ConsumerSecret = builder.Configuration.GetSection("AuthenticationTwitter")
       .GetValue<string>("ConsumerSecret");
   });
   

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
