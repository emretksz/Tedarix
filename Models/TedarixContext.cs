using Tedarix.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Tedarix.Models
{
    public class TedarixContext:DbContext
    {
        public DbSet<Age>Ages { get; set; }
        public DbSet<Atolye> Atolyes { get; set; }
        public DbSet<Cins> Cinss { get; set; }
        public DbSet<Foy>Foys { get; set; }
        public DbSet<FoyAndCins> FoyAndCinsS { get; set; }
        public DbSet<FoyAndKesim> FoyAndKesims { get; set; }
        public DbSet<Kesim> Kesims { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FoyAndColor> FoyAndColors { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
