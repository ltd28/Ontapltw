using LeTienDat_241230697.Data;
using Microsoft.EntityFrameworkCore;

namespace LeTienDat_241230697;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<QLHangHoaContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("QLHangHoa")));

        var app = builder.Build();
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
        app.Run();
    }
}
