using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.API.Abstraction.DataAccessLayers;
using TodoApp.API.Data;
using TodoApp.API.DataAccessLayers.Implementation;
using TodoApp.API.Helper;
using TodoApp.API.Helper.Authentication;
using TodoApp.API.Managers;
using TodoApp.API.Managers.Abstraction;
using TodoApp.API.Managers.Implementation;

namespace TodoApp.API
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
            services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection"))); 

            services.AddScoped<IHasher, Hasher>();
            services.AddScoped<IUserValidator, UserValidator>();
            services.AddScoped<IFolderDal, FolderDal>();
            services.AddScoped<IFolderManager, FolderManager>();
            services.AddScoped<ITodoDal, TodoDal>();
            services.AddScoped<ITodoManager, TodoManager>();
            services.AddScoped<IUserDal, UserDal>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddSingleton<IAuthService, AuthService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseMvc();
        }
    }
}