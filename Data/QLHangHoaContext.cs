using LeTienDat_241230697.Models;
using Microsoft.EntityFrameworkCore;

namespace LeTienDat_241230697.Data;

public class QLHangHoaContext(DbContextOptions<QLHangHoaContext> options) : DbContext(options)
{
    public DbSet<LoaiHang> LoaiHangs => Set<LoaiHang>();
    public DbSet<HangHoa> HangHoas => Set<HangHoa>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LoaiHang>(entity =>
        {
            entity.ToTable("LoaiHang");
            entity.HasKey(x => x.MaLoai);
            entity.Property(x => x.TenLoai).HasMaxLength(50).IsRequired();
        });
        modelBuilder.Entity<HangHoa>(entity =>
        {
            entity.ToTable("HangHoa");
            entity.HasKey(x => x.MaHang);
            entity.Property(x => x.TenHang).HasMaxLength(50).IsRequired();
            entity.Property(x => x.Anh).HasMaxLength(100);
            entity.Property(x => x.Gia).HasColumnType("numeric(18,0)");
            entity.HasOne(x => x.LoaiHang).WithMany(x => x.HangHoas)
                .HasForeignKey(x => x.MaLoai);
        });
    }
}
