using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using UserMng.Core.Common;
using UserMng.Core.Common.Email;
using UserMng.Core.Contracts;
using UserMng.Core.Services;
using UserMng.Data.Context;
using UserMng.Web.Common.MessageBox;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDatabaseContext>(op =>
{
    op.UseSqlServer(builder.Configuration.GetConnectionString("AppConnection"));
});


builder.Services.AddScoped<ImsgBox, msgBox>();
builder.Services.AddScoped<IuserService, UserServices>();
builder.Services.AddScoped<IViewRenderService,ViewRenderService>();
builder.Services.AddScoped<IEmailSender,EmailSender>();
builder.Services.AddScoped<IPanelService, PanelService>();

#region Authentication

builder.Services.AddAuthentication(op =>
{

    op.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    op.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    op.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

}).AddCookie(op =>
{
    op.LoginPath = "/Home/Index";
    op.LogoutPath = "/Logout";
    op.ExpireTimeSpan = TimeSpan.FromDays(29);
});


#endregion


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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();
