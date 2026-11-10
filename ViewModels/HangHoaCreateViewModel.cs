using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace LeTienDat_241230697.ViewModels;

public class HangHoaCreateViewModel
{
    [Required(ErrorMessage = "Vui lòng chọn loại hàng.")]
    [Display(Name = "Mã loại")]
    public int? MaLoai { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên hàng.")]
    [StringLength(50, ErrorMessage = "Tên hàng tối đa 50 ký tự.")]
    [Display(Name = "Tên hàng")]
    public string TenHang { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập giá.")]
    [Range(typeof(decimal), "100", "5000", ErrorMessage = "Giá phải nằm trong khoảng 100 đến 5000.")]
    [Display(Name = "Giá")]
    public decimal? Gia { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn ảnh.")]
    [AllowedImageExtension]
    [Display(Name = "Ảnh")]
    public IFormFile? Anh { get; set; }
}


