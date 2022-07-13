using ICS.EmployeesProject.Configuration;
using ICS.EmployeesProject.DAL.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterRepository();

builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection(ApplicationConfiguration.ConnectionStrings));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
