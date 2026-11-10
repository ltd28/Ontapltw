using LeTienDat_241230697.Data;
using LeTienDat_241230697.Models;
using LeTienDat_241230697.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LeTienDat_241230697.Controllers;

public class HomeController(QLHangHoaContext db, IWebHostEnvironment environment) : Controller
{
    private const decimal GiaToiThieu = 100m;
    private readonly QLHangHoaContext _db = db;
    private readonly IWebHostEnvironment _environment = environment;

    public async Task<IActionResult> Index()
    {
        var hangHoa = await _db.HangHoas.AsNoTracking()
            .Where(x => x.Gia >= GiaToiThieu)
            .OrderBy(x => x.MaHang)
            .ToListAsync();
        await LoadCategories();
        return View(hangHoa);
    }

    [HttpGet]
    public async Task<IActionResult> FilterByLoai(int? maLoai)
    {
        var query = _db.HangHoas.AsNoTracking().Where(x => x.Gia >= GiaToiThieu);
        if (maLoai.HasValue && maLoai.Value > 0)
            query = query.Where(x => x.MaLoai == maLoai.Value);
        var products = await query.OrderBy(x => x.MaHang).ToListAsync();
        return PartialView("_ProductList", products);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await LoadCategories();
        return View(new HangHoaCreateViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(HangHoaCreateViewModel model)
    {
        await LoadCategories();
        if (model.Anh is not null)
        {
            var extension = Path.GetExtension(model.Anh.FileName).ToLowerInvariant();
            var allowed = new[] { ".jpg", ".png", ".gif", ".tiff" };
            if (!allowed.Contains(extension))
                ModelState.AddModelError(nameof(model.Anh), "Ảnh chỉ nhận đuôi .jpg, .png, .gif hoặc .tiff.");
            if (model.Anh.Length == 0)
                ModelState.AddModelError(nameof(model.Anh), "File ảnh đang trống.");
        }
        if (!ModelState.IsValid)
            return View(model);

        var extensionSafe = Path.GetExtension(model.Anh!.FileName).ToLowerInvariant();
        var fileName = $"{Guid.NewGuid():N}{extensionSafe}";
        var imagesPath = Path.Combine(_environment.WebRootPath, "images");
        Directory.CreateDirectory(imagesPath);
        await using (var stream = System.IO.File.Create(Path.Combine(imagesPath, fileName)))
            await model.Anh.CopyToAsync(stream);

        var entity = new HangHoa
        {
            MaLoai = model.MaLoai!.Value,
            TenHang = model.TenHang.Trim(),
            Gia = model.Gia,
            Anh = fileName
        };
        _db.HangHoas.Add(entity);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Đã thêm hàng hóa mới.";
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadCategories()
    {
        var categories = await _db.LoaiHangs.AsNoTracking().OrderBy(x => x.MaLoai).ToListAsync();
        ViewBag.LoaiHangs = categories;
        ViewBag.LoaiHangSelect = new SelectList(categories, "MaLoai", "TenLoai");
    }
}


