using KuyumcuWebApi.dto;
using KuyumcuWebApi.exception;
using KuyumcuWebApi.Models;
using KuyumcuWebApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace KuyumcuWebApi.Service;

public class ProductService
{

    public IProductRepository productRepository;
    public IProductImageRepository productImageRepository;
    public FileService fileService;
    public IConfiguration configuration;
    public ProductService(IConfiguration configuration, IProductRepository productRepository, IProductImageRepository productImageRepository, FileService fileService)
    {
        this.productRepository = productRepository;
        this.productImageRepository = productImageRepository;
        this.fileService = fileService;
        this.configuration = configuration;
    }

    public async Task<Product> createProduct(CreateProductRequestDto createProductRequestDto)
    {
        var ProductElement = await productRepository.addAsync(new Product()
        {
            Material = createProductRequestDto.Material,
            Category = createProductRequestDto.Category,
            Description = createProductRequestDto.Description,
            Name = createProductRequestDto.Name,
            Price = createProductRequestDto.Price,
            Stock = createProductRequestDto.Stock,
            Weight = createProductRequestDto.Weight,
            Karat = createProductRequestDto.Karat,
        });
        await productRepository.saveChangesAsync();
        var baseUrl = configuration.GetValue<string>("AppSettings:BaseUrl");

        var productImages = new List<ProductImage>();
        foreach (var photo in createProductRequestDto.Photos)
        {
            if (photo != null)
            {
                var imageUrl = await fileService.SavePhotoAsync(photo);
                productImages.Add(new ProductImage
                {
                    Path = imageUrl,
                    ProductId = ProductElement.Id
                });
            }
        }
        // Ürün resimlerini veritabanına kaydet
        if (productImages.Count > 0)
        {
            await productImageRepository.AddRangeAsync(productImages);
            await productImageRepository.saveChangesAsync();
        }
        ProductElement.productImages = productImages.Select(item =>
        {
            return new ProductImage()
            {
                Id = item.Id,
                Path = $"{baseUrl}{item.Path}",
                ProductId = item.ProductId,
            };
        }).ToList();
        return ProductElement;
    }


    public async Task<PagedResultDto<Product>> getAllProductAsync(PagedRequestDto pagedRequestDto)
    {

        var products = await productRepository.getAllAsync(pagedRequestDto.PageIndex, pagedRequestDto.PageSize, item => item.IsDeleted == false, u => u.Include(x => x.productImages)
        .OrderByDescending(item => item.UpdatedAt));
        var baseUrl = configuration.GetValue<string>("AppSettings:BaseUrl");
        var liste = products.Items.Select(item => new Product()
        {
            Id = item.Id,
            Stock = item.Stock,
            Price = item.Price,
            Weight = item.Weight,
            Category = item.Category,
            Description = item.Description,
            Material = item.Material,
            Name = item.Name,
            Karat = item.Karat,
            CreatedAt = item.CreatedAt,
            UpdatedAt = item.UpdatedAt,
            IsDeleted = item.IsDeleted,
            productImages = item.productImages.Select(p => new ProductImage()
            {
                Path = $"{baseUrl}{p.Path}",
                Id = p.Id,
                ProductId = p.ProductId,
            }).ToList()
        }).ToList();
        products.Items = liste;
        return products;
    }


    public async Task<Product> getByProduct(int productId)
    {
        var productElement = await productRepository.getByIdAsync(productId, el => el.Where(item => item.IsDeleted == false).Include(item => item.productImages));
        if (productElement is null)
        {
            throw new NotFoundException("Ürün bulunamadı");
        }
        var baseUrl = configuration.GetValue<string>("AppSettings:BaseUrl");
        var Product = new Product()
        {
            Id = productElement.Id,
            Category = productElement.Category,
            Description = productElement.Description,
            Material = productElement.Material,
            Name = productElement.Name,
            Price = productElement.Price,
            Karat = productElement.Karat,
            productImages = productElement.productImages.Select(item => new ProductImage()
            {
                Path = $"{baseUrl}{item.Path}",
                Id = item.Id,
                ProductId = item.ProductId,
            }).ToList()
        };

        return Product;
    }



    public async Task<Product> updateProduct(UpdateProductRequestDto updateProductRequestDto)
    {
        var product = await productRepository.getByIdAsync(updateProductRequestDto.Id, item => item.Where(el => el.IsDeleted == false));
        if (product is null)
        {
            throw new NotFoundException("Ürün bulunamadı");
        }
        if (product.IsDeleted)
        {
            throw new ConflictException("Bu ürün silinmiş");
        }

        product.Id = updateProductRequestDto.Id;
        product.Name = updateProductRequestDto.Name;
        product.Description = updateProductRequestDto.Description;
        product.Category = updateProductRequestDto.Category;
        product.Stock = updateProductRequestDto.Stock;
        product.Price = updateProductRequestDto.Price;
        product.Weight = updateProductRequestDto.Weight;
        product.Material = updateProductRequestDto.Material;
        product.Karat = updateProductRequestDto.Karat;
        var updateElement = await productRepository.updateAsync(product);
        await productRepository.saveChangesAsync();

        return updateElement;
    }


    public async Task deleteProduct(int id)
    {
        var element = await productRepository.getByIdAsync(id, null);

        if (element is null)
        {
            throw new NotFoundException("Ürün bulunamadı");
        }

        element.IsDeleted = true;
        await productRepository.saveChangesAsync();
    }


}