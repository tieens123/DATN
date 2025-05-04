using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using K21CNT2_2110900086_DATN.Models;
using K21CNT2_2110900086_DATN.Middlewares; // Thêm middleware kiểm tra session
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình dịch vụ DbContext
builder.Services.AddDbContext<K21cnt2NguyenVietTien2110900086DatnContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("dbWebBanGiay")));

// Cấu hình HtmlEncoder với các phạm vi Unicode cho phép
builder.Services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.All }));

// Thêm dịch vụ Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDistributedMemoryCache();

// Thêm Notyf để hiển thị thông báo
builder.Services.AddNotyf(options =>
{
    options.DurationInSeconds = 10;
    options.IsDismissable = true;
    options.Position = NotyfPosition.TopRight;
});

// Cấu hình Controller với Razor Runtime Compilation
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseStaticFiles(new StaticFileOptions
{
    ServeUnknownFileTypes = true,
    DefaultContentType = "text/css"
});

app.UseSession(); // Kích hoạt Session
app.UseSessionMiddleware(); // Kiểm tra Session trước khi vào trang khác
app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();

app.UseNotyf(); // Kích hoạt Toast Notification

// Định tuyến cho Areas (Admin, User, ...)
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// Định tuyến mặc định
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
