using KuyumcuWebApi.dto;
using KuyumcuWebApi.exception;
using KuyumcuWebApi.Models;
using KuyumcuWebApi.Repository;

namespace KuyumcuWebApi.Service;


public class ProductImageService
{

    private readonly IProductRepository productRepository;
    private readonly IProductImageRepository productImageRepository;
    private readonly FileService fileService;
    public ProductImageService(IProductRepository productRepository, IProductImageRepository productImageRepository, FileService fileService)
    {
        this.productRepository = productRepository;
        this.productImageRepository = productImageRepository;
        this.fileService = fileService;
    }

    public async Task<ProductImage> AddProductPhoto(AddProductPhotoDto productPhotoDto)
    {
        var productControl = await productRepository.getByIdAsync(productPhotoDto.ProductId);
        if (productControl is null)
        {
            throw new NotFoundException("Böyle bir ürün bulunamadı");
        }
        var photoUrl = await fileService.SavePhotoAsync(productPhotoDto.Photo);
        var response = await productImageRepository.addAsync(new ProductImage()
        {
            ProductId = productPhotoDto.ProductId,
            Path = photoUrl,
        });

        return response;
    }

    public async Task deletePhoto(int photoId)
    {
        var selectedProductImage = await productImageRepository.getByIdAsync(photoId);
        if (selectedProductImage is null)
        {
            throw new NotFoundException("fotoğraf bulunamadı");
        }

        productImageRepository.Delete(selectedProductImage);
        await productImageRepository.saveChangesAsync();
        await fileService.DeletePhotoAsync(selectedProductImage.Path);

    }
}