using cltxmomo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace cltxmomo.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<RechargePhoneNumber> RechargePhoneNumber { get; set; }
        public DbSet<Log> Log { get; set; }
        public DbSet<PromotionalCode> PromotionalCode { get; set; }
        public DbSet<WebConfig> WebConfig { get; set; }
        public DbSet<HistoryWin> HistoryWin { get; set; }
        public DbSet<TopDay> TopDay { get; set; }
        public DbSet<HistoryWin2> HistoryWin2 { get; set; }
    }
}
