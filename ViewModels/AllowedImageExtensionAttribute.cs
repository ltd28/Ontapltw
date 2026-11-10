using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace LeTienDat_241230697.ViewModels;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public sealed class AllowedImageExtensionAttribute : ValidationAttribute
{
    private static readonly HashSet<string> Extensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".jpg", ".png", ".gif", ".tiff"
    };

    public override bool IsValid(object? value)
    {
        if (value is null) return true;
        return value is IFormFile file && Extensions.Contains(Path.GetExtension(file.FileName));
    }

    public override string FormatErrorMessage(string name) =>
        $"{name} chỉ nhận đuôi .jpg, .png, .gif hoặc .tiff.";
}
