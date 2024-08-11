using FluentValidation;
using KuyumcuWebApi.dto;
using KuyumcuWebApi.Models;

public class CreateProductValidator : AbstractValidator<CreateProductRequestDto>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ürün adı boş olamaz.")
            .Length(2, 100).WithMessage("Ürün adı 2 ile 100 karakter arasında olmalıdır.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Açıklama boş olamaz.")
            .Length(10, 1000).WithMessage("Açıklama 10 ile 1000 karakter arasında olmalıdır.");

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Kategori boş olamaz.")
            .Length(2, 50).WithMessage("Kategori 2 ile 50 karakter arasında olmalıdır.");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("Stok miktarı negatif olamaz.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalıdır.");

        RuleFor(x => x.Weight)
            .GreaterThan(0).WithMessage("Ağırlık 0'dan büyük olmalıdır.");

        RuleFor(x => x.Material)
            .NotEmpty().WithMessage("Materyal boş olamaz.")
            .Length(2, 50).WithMessage("Materyal 2 ile 50 karakter arasında olmalıdır.");

        RuleFor(x => x.Photos)
            .Must(photos => photos == null || photos.Length > 0).WithMessage("En az bir fotoğraf yüklenmelidir.");
    }
}
