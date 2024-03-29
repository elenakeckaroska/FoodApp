using FoodApp.Models.Identity;
using FoodApp.Repository.Interface;
using FoodApp.Service.Interface;
using FoodApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FoodApp.Repository;
using FoodApp.Service.Implementation;
using FoodApp.Repository.Implementation;
using FoodApp.Models.Models;
using Stripe;
using FoodApp.Service.Events;

namespace FoodApp.Web
{
    public class Startup
    {
        private EmailSettings emailSettings;
        public Startup(IConfiguration configuration)
        {
            emailSettings = new EmailSettings();
            Configuration = configuration;
            Configuration.GetSection("EmailSettings").Bind(emailSettings);

        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));


            services.AddScoped(typeof(IRecipeRepository), typeof(RecipeRepository));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(IIngredientRepository), typeof(IngredientRepository));
            services.AddScoped(typeof(IFavoriteRecipeUsersRepository), typeof(FavoriteRecipeUsersRepository));
            services.AddScoped(typeof(ICookingClassesRepository), typeof(CookingClassesRepository));
            services.AddScoped(typeof(ICookingClassesUserRepository), typeof(CookingClassesUserRepository));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
            

            services.AddScoped<EmailSettings>(es => emailSettings);
            services.AddScoped<IEmailService, EmailService>(email => new EmailService(emailSettings));
            services.AddScoped<IBackgroundEmailSender, BackgroundEmailSender>();

            services.AddScoped<OrderCompletionNotifier>();

            services.AddTransient<IRecipeServive, RecipeService>();
            services.AddTransient<ICookingClassesService, CookingClassesService>();
            services.AddTransient<IShoppingCartService, ShoppingCartService>();
            services.AddTransient<IRemoteAuthenticationService, RemoteAuthenticationService>();

            services.AddTransient<IOrderService, OrderService>();

            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));
            //services.AddDefaultIdentity<EBiletsUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddDefaultIdentity<FoodAppUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //Relations

            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            StripeConfiguration.SetApiKey(Configuration.GetSection("Stripe")["SecretKey"]);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Recipe}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
