using Library.Model;
using Library.Repository;
using Library.Repository.RepositoryImpl;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication
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
            // Add session
            services.AddSession(option =>
            {
                option.IdleTimeout = TimeSpan.FromMinutes(30);
            });
            // set index page
            services.AddMvc().AddRazorPagesOptions(options => options.Conventions.AddPageRoute("/HomePages/Home", ""));
            services.AddTransient<InventoryManagementContext>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ISuplierRepository, SuplierRepository>();
            services.AddTransient<IConsignmentRepository, ConsignmentRepository>();
            services.AddTransient<IConsignmentDetailRepository, ConsignmentDetailRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IInvoiceOutputRepository, InvoiceOutputRepository>();
            services.AddTransient<IInvoiceOutputDetailRepository, InvoiceOutputDetailRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddAutoMapper(typeof(Program));
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
