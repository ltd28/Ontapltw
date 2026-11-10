using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeTienDat_241230697.Models;

[Table("HangHoa")]
public class HangHoa
{
    [Key]
    public int MaHang { get; set; }

    public int MaLoai { get; set; }

    [Required, StringLength(50)]
    public string TenHang { get; set; } = string.Empty;

    public decimal? Gia { get; set; }

    [StringLength(100)]
    public string? Anh { get; set; }

    public LoaiHang? LoaiHang { get; set; }
}
