using GYMManagementBLL;
using GYMManagementBLL.Services.AttachmentService;
using GYMManagementBLL.Services.Classes;
using GYMManagementBLL.Services.Interfaces;
using GYMManagementDL.Data.Contexts;
using GYMManagementDL.Data.DataSeeding;
using GYMManagementDL.Enitities;
using GYMManagementDL.Repositories.Classes;
using GYMManagementDL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GYMManagementPL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<GymManagementDbContext>(options =>

            //options.UseSqlServer(builder.Configuration.GetSection("ConnentionString")["DefaultConnection"])

            //options.UseSqlServer(builder.Configuration["ConnentionString:DefaultConnection"])

            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))

            );

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
               options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<GymManagementDbContext>();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
            });

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            builder.Services.AddScoped<IPlanRepository, PlanRepository>();
            builder.Services.AddScoped<ISessionRepository, SessionRepository>();
            builder.Services.AddScoped<IAnalyticsDataService, AnalyticsDataService>();
            builder.Services.AddScoped<IMemberService, MemberService>();
            builder.Services.AddScoped<ITrainerService, TrainerService>();
            builder.Services.AddScoped<IPlanService, PlanService>();
            builder.Services.AddScoped<ISessionService, SessionService>();
            builder.Services.AddScoped<IAttachmentService, AttachmentService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IAccountService, AccountService>();



            builder.Services.AddAutoMapper(x => x.AddProfile(new MappingProfile()));





            var app = builder.Build();

            #region Seeding Data

           using var scope= app.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var dbContext = scope.ServiceProvider.GetRequiredService<GymManagementDbContext>();
           var pendingMigrations= dbContext.Database.GetPendingMigrations();
            if(pendingMigrations?.Any()??false) dbContext.Database.Migrate();
            GymManagementDbContextSeeding.IsSeeding(dbContext);
            IdentityDbContextseeding.IsSeeding(userManager, roleManager);

            #endregion

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();    
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
