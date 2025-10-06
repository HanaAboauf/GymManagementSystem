using GYMManagementDL.Enitities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMManagementDL.Data.Contexts
{
    internal class GymManagementDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-TN70H9CL;Database=GymManagementDB;Trusted_Connection= true;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GymManagementDbContext).Assembly);
        }


        public DbSet<Member> Members { get; set; } = null!;
        public DbSet<Trainer> Trainers { get; set; } = null!;
        public DbSet<Session> Sessions { get; set; } = null!;
        public DbSet<MemberSession> MemberSessions { get; set; } = null!;
        public DbSet<MemberShip> MemberShips { get; set; } = null!;
        public DbSet<Plan> Plans { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<HealthRecord> HealthRecords { get; set; } = null!;





    }
}
