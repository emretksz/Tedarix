using Microsoft.AspNetCore.Authentication.Cookies;


public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
   Host.CreateDefaultBuilder(args)
       .ConfigureWebHostDefaults(webBuilder =>
       {
           webBuilder.UseStartup<Startup>();
       });
}

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {

        services.AddControllersWithViews();
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
          .AddCookie(opt => {
              opt.Cookie.Name = "TedarixDemo";
              opt.ExpireTimeSpan = TimeSpan.FromSeconds(99999);
              opt.Cookie.IsEssential = true;
              opt.ReturnUrlParameter = "/Account/Login";
              opt.LogoutPath = "/Account/Login";
              opt.LoginPath = "/Account/Login";
              opt.AccessDeniedPath = "/Account/Login";
          });
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromSeconds(99999);
            options.Cookie.HttpOnly = true;
            options.Cookie.SameSite = SameSiteMode.Strict;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        });
     
    }

 

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSession();



        app.UseEndpoints(endpoints =>
        {
     
            endpoints.MapControllerRoute(
                name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}");

            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");




            //endpoints.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=Index}/{id?}"
            //);
        });

      

    }

}