using Entities.Const;
using Entities.DataEntities;
using Entities.Dto.LuaChonUngVien;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public static int UngVienId = 1;
        public static List<UngVienDto> UngViens = new();

        public static List<UngVienTieuChiDto> UngVienTieuChis = new();
        public static List<UngVienTieuChiDto> MaTranChuanHoa = new();
        public static List<UngVienTieuChiDto> GiaTriTheoTrongSo = new();
        public static List<double> ASao = new();
        public static List<double> ATru = new();
        public static List<double> SSao = new();
        public static List<double> Stru = new();
        public static List<double> CSao = new();

        public DbSet<TieuChi> TieuChis { get; set; }
        public DbSet<DanhGia> DanhGias { get; set; }
        public DbSet<TieuChiDanhGia> TieuChiDanhGias { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.\\sqlexpress;Initial Catalog=BTL.HTTM;User ID=sa;Password=123456;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TieuChi>(entity =>
            {
                entity.Property(e => e.Type)
                    .HasDefaultValue(LoaiTieuChi.GiaTri);
            });
            modelBuilder.Entity<DanhGia>();
            modelBuilder.Entity<TieuChiDanhGia>();
        }
    }
}
