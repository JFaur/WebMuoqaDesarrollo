using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MuoqaIdentidades;

namespace MuoqaBD
{
    public class MuoqaBDConf : IdentityDbContext<IdentityUser>
    {
        public MuoqaBDConf(DbContextOptions<MuoqaBDConf> options) : base(options)
        {
        }

        public DbSet<Account> RegisteredUsers { get; set; }
        public DbSet<ServicesPrices> ServicesPrices { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Account>(entity =>{
                entity.HasKey(u => u.Id);
                });
            modelBuilder.Entity<ServicesPrices>(entity => { 
                entity.HasKey(u => u.ServiceId);
            });
        }
    }
}
