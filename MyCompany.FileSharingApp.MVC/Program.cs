using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using MyCompany.FileSharingApp.Business.Abstract;
using MyCompany.FileSharingApp.Business.Concrete;
using MyCompany.FileSharingApp.DataAccess.Abstract;
using MyCompany.FileSharingApp.DataAccess.Concrete.EntityFramework;
using MyCompany.FileSharingApp.MVC.NewFolder.FileTools;
using MyCompany.FileSharingApp.MVC.NewFolder.FolderTools;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddScoped<IAuthService,AuthManager>();
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IFileService, FileManager>();
builder.Services.AddScoped<IFolderService, FolderManager>();
builder.Services.AddScoped<IDisposableLinkService, DisposableLinkManager>();

builder.Services.AddScoped<IFileDal, EfFileDal>();
builder.Services.AddScoped<IFolderDal, EfFolderDal>();
builder.Services.AddScoped<IUserDal, EfUserDal>();
builder.Services.AddScoped<IDosposableLinkDal, EfDisposableLinkDal>();

builder.Services.AddScoped<IFileTool, FileTool>();
builder.Services.AddScoped<IFolderTool, FolderTool>();

builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));

builder.Services.AddDbContext<FileSharingAppContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnecion")));

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/auth/login";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });


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

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

using (var scope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    scope.ServiceProvider.GetRequiredService<FileSharingAppContext>().Database.Migrate();
}

app.Run();
