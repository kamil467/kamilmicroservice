using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pnk.Web.Models.Configuration;
using Pnk.Web.Models.Mapper;
using Pnk.Web.Services.Implementations;
using Pnk.Web.Services.IServices;

namespace Pnk.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            builder.Services.AddControllersWithViews()
                .AddJsonOptions(o =>
                {
                   
                });
                //.AddNewtonsoftJson(o =>
                //{
                //    o.SerializerSettings.SerializationBinder = new Newtonsoft.Json.Serialization.DefaultSerializationBinder();
                //    o.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();


                //});
            builder.Services.AddOptions<ServiceURLConfiguration>()
               .Bind(builder.Configuration.GetSection(ServiceURLConfiguration.ServiceURLSectionName));

            builder.Services.AddHttpClient<IProductService, Service>();
            builder.Services.AddScoped<IBaseService, BaseService>();
            builder.Services.AddScoped<IProductService, Service>();
            
           

            var mapper = MappingConfiguration.GetMappingConfiguration().CreateMapper();
            builder.Services.AddSingleton(mapper);

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
        }
    }
}