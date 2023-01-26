using System.IdentityModel.Tokens.Jwt;

namespace Identity.MVC.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // add openid connect support
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";

            }).AddCookie("Cookies")
             .AddOpenIdConnect("oidc", options =>
             {
                 options.Authority = "identity url";
                 options.ClientId = "mvc";
                 options.ClientSecret = "secret";
                 options.ResponseType = "code";
                 options.SaveTokens = true;
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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}