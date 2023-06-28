using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebBanHang.Models;

namespace WebBanHang.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
           (@"Server=(localdb)\MSSQLLocalDB;Database=aspnet-WebBanHang-53bc9b9d-9d6a-45d4-8429-2a2761773502;Trusted_Connection=True;Encrypt=False"));
        }
        public DbSet<WebBanHang.Models.SanPham> SanPham { get; set; } = default!;
        public DbSet<WebBanHang.Models.GioHang> GioHang { get; set; } = default!;
        public DbSet<WebBanHang.Models.GioHangDangXuLy> GioHangDangXuLy { get; set; } = default!;
    }
}