using KuyumcuWebApi.dto;
using KuyumcuWebApi.exception;
using KuyumcuWebApi.Models;
using KuyumcuWebApi.Repository;

namespace KuyumcuWebApi.Service;

public class OrderService
{

    private readonly IOrderRepository orderRepository;
    private readonly IUserRepository userRepository;
    private readonly IProductRepository productRepository;
    public OrderService(IOrderRepository orderRepository, IUserRepository userRepository, IProductRepository productRepository)
    {
        this.orderRepository = orderRepository;
        this.userRepository = userRepository;
        this.productRepository = productRepository;
    }

    private async Task UserCheckControl(int userId)
    {
        var selected = await userRepository.getByIdAsync(userId, null);
        if (selected is null)
        {
            throw new NotFoundException("Bu id'ye ait bir kullanıcı bulunamadı");
        }
    }
    private async Task<Product> ProductCheckControl(int productId)
    {
        var selected = await productRepository.getByIdAsync(productId, null);
        if (selected is null)
        {
            throw new NotFoundException("Bu id'ye ait bir ürün bulunamadı");
        }
        return selected;
    }


    public async Task<Order> CreateOrder(CreateOrderRequestDto createOrderRequestDto)
    {
        await UserCheckControl(createOrderRequestDto.UserId);
        var selectedProduct = await ProductCheckControl(createOrderRequestDto.ProductId);

        if (selectedProduct.Stock < createOrderRequestDto.Quantity)
        {
            throw new ConflictException("Bu üründen yeteri kadar stok yok");
        }

        var result=await this.orderRepository.addAsync(new Order(){
            Description = createOrderRequestDto.Description,
            OrderStatusId = 1,
            ProductId = createOrderRequestDto.ProductId,
            Quantity = createOrderRequestDto.Quantity,
            UserId = createOrderRequestDto.UserId,
        });
        selectedProduct.Stock = selectedProduct.Stock - createOrderRequestDto.Quantity;
        await orderRepository.saveChangesAsync();
        result.product=null;
        
        return result;
    }
}