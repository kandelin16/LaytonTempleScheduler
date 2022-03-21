using LaytonTempleScheduler.DataAccess;
using LaytonTempleScheduler.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaytonTempleScheduler
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<SchedulerContext>(options =>
            {
                options.UseSqlite(Configuration["ConnectionStrings:SchedulerDB"]);
            });
            services.AddScoped<ISchedulerRepository, SchedulerRepository>();
            Settings.ConnectionString = Configuration["ConnectionStrings:SchedulerDB"];
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
           

            using (var context = new SchedulerContext())
            {
                context.TimeSlots.RemoveRange(context.TimeSlots);
                DateTime today = DateTime.Today.Date;
                DateTime target = DateTime.Today.AddDays(90).Date;
                int[] hours = new int[] { 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

                while (today.Date != target.Date)
                {
                    foreach (var hour in hours)
                    {
                        TimeSlot temp = new TimeSlot();
                        temp.Available = true;
                        temp.Start = today.AddHours(hour);
                        context.Add(temp);
                    }
                    today = today.AddDays(1).AddHours(-20);
                    context.SaveChanges();
                }
            }

        }
    }
}
