using Blog.Data;
using Blog.Data.Infrastructure.Entities;
using Blog.WebUI.Infrastructure.Cache;
using Blog.WebUI.Infrastructure.Rules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.WebUI.Site
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IConfigurationRoot ConfigurationRoot { get; set; }
        public Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment { get; set; }

        public Startup(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;

            ConfigurationRoot = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            if(Environment.IsDevelopment())
            {
                services.AddControllersWithViews().AddRazorRuntimeCompilation();
            }

            //CONFÝG    
            services.Configure<DatabaseSettings>(Configuration.GetSection("DatabaseSettings"));
            services.AddOptions();

            //CACHE
            services.AddMemoryCache();
            services.AddTransient<ICache, Blog.Infrastructure.Caching.Memory.Cache>();
            services.AddTransient<CacheHelper>();


            services.AddTransient<ContentData>();
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
            });
            object value = services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddTransient<CategoryData>();
            services.AddTransient<ContentData>();
            services.AddTransient<ContentCategoryData>();
            services.AddTransient<ContentTagData>();
            services.AddTransient<TagData>();
            services.AddTransient<MediaData>();
            services.AddTransient<AuthorData>();
            services.AddTransient<SettingData>();
            services.AddTransient<CommentData>();
            services.AddTransient<RolePageData>();
            services.AddTransient<RoleData>();

            services.AddMvc(x =>
            {
                x.EnableEndpointRouting = false;
            });

            services.Configure<RouteOptions>(routeOptions => routeOptions.AppendTrailingSlash = true);
        }
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseStatusCodePages();
            }

            app.UseDeveloperExceptionPage();

            RedirectToHttpsWwwNonWwwRule rule = new RedirectToHttpsWwwNonWwwRule
            {
                status_code = 301,
                redirect_to_https = true,
                redirect_to_www = false,
                redirect_to_non_www = true,
                append_slash = true
            };
            RewriteOptions options = new RewriteOptions();
            options.Rules.Add(rule);
            app.UseRewriter(options);

            app.UseRouting();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "category", template: "kategori/{slug}", defaults: new { controller = "category", action = "Index", page = 1 });
                routes.MapRoute(name: "categoryWithPage", template: "kategori/{slug}/sayfa/{page}", defaults: new { controller = "category", action = "Index", page = 1 });
                routes.MapRoute(name: "content", template: "{slug}", defaults: new { controller = "content", action = "Index"});

                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}