using System.Linq.Expressions;
using KuyumcuWebApi.Context;
using KuyumcuWebApi.dto;
using KuyumcuWebApi.exception;
using KuyumcuWebApi.Models;
using KuyumcuWebApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace KuyumcuWebApi.Service;

public class OrderService
{

    private readonly IOrderRepository orderRepository;
    private readonly IUserRepository userRepository;
    private readonly IProductRepository productRepository;
    private readonly AppDbContext appDbContext;
    private readonly IConfiguration configuration;
    public OrderService(AppDbContext appDbContext, IConfiguration configuration, IOrderRepository orderRepository, IUserRepository userRepository, IProductRepository productRepository)
    {
        this.orderRepository = orderRepository;
        this.userRepository = userRepository;
        this.productRepository = productRepository;
        this.configuration = configuration;
        this.appDbContext = appDbContext;
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
    private async Task<Order> OrderCheckControl(int orderId)
    {
        var element = await orderRepository.getByIdAsync(orderId, null);
        if (element is null)
        {
            throw new NotFoundException("Bu id'ye ait bir sipariş bulunamadı");
        }
        return element;
    }

    public async Task<Order> CreateOrder(CreateOrderRequestDto createOrderRequestDto)
    {
        await UserCheckControl(createOrderRequestDto.UserId);
        var selectedProduct = await ProductCheckControl(createOrderRequestDto.ProductId);

        if (selectedProduct.Stock < createOrderRequestDto.Quantity)
        {
            throw new ConflictException("Bu üründen yeteri kadar stok yok");
        }

        var result = await this.orderRepository.addAsync(new Order()
        {
            Description = createOrderRequestDto.Description,
            OrderStatusId = 1,
            ProductId = createOrderRequestDto.ProductId,
            Quantity = createOrderRequestDto.Quantity,
            UserId = createOrderRequestDto.UserId,
        });
        selectedProduct.Stock = selectedProduct.Stock - createOrderRequestDto.Quantity;
        await orderRepository.saveChangesAsync();
        result.product = null;

        return result;
    }

    private async Task orderStatusControl(int statusId)
    {
        if (statusId != 0)
        {
            var element = await this.appDbContext.orderStatus.FirstOrDefaultAsync(item => item.Id == statusId);

            if (element is null)
            {
                throw new BadRequestException("Böyle bir Order Status id bulunamadı");
            }
        }
    }
    private async void EditImagePath(List<Order> orderList)
    {
        var baseUrl = configuration.GetValue<string>("AppSettings:BaseUrl");
        foreach (var order in orderList)
        {
            order.User.Password = null;
            order.product.Orders = null;
            order.product.productImages = order.product.productImages.Select(item =>
            {
                if (!item.Path.StartsWith(baseUrl))
                {
                    item.Path = $"{baseUrl}{item.Path}";
                }
                return item;

            }).ToList();
        }
    }


    public async Task<PagedResultDto<Order>> GetAllOrderService(int UserId, GetAllOrderRequestDto getAllOrderRequestDto)
    {
        var user = await userRepository.getByIdAsync(UserId, null);

        await orderStatusControl(getAllOrderRequestDto.StatusId);
        Expression<Func<Order, bool>> funcs = null;
        if (user.RoleId == 1)
        {
            if (getAllOrderRequestDto.StatusId != 0)
            {
                funcs = x => x.OrderStatusId == getAllOrderRequestDto.StatusId;
            }
            var resultList = await orderRepository.getAllAsync(getAllOrderRequestDto.PageIndex, getAllOrderRequestDto.PageSize, funcs, x =>
            x.Include(el => el.product).ThenInclude(el => el.productImages)
            .Include(x => x.User)
            .Include(x => x.orderStatus));
            EditImagePath(resultList.Items);
            return resultList;
        }
        else
        {
            if (getAllOrderRequestDto.StatusId != 0)
            {
                funcs = x => x.OrderStatusId == getAllOrderRequestDto.StatusId && x.UserId == UserId;
            }
            else
            {
                funcs = x => x.UserId == UserId;
            }
            var resultList = await orderRepository.getAllAsync(getAllOrderRequestDto.PageIndex, getAllOrderRequestDto.PageSize, funcs, x =>
            x.Include(el => el.product).ThenInclude(el => el.productImages)
            .Include(x => x.User)
            .Include(x => x.orderStatus));
            EditImagePath(resultList.Items);
            return resultList;
        }

    }


    public async Task<Order> GetByOrder(int orderId)
    {
        var selectedOrder = await this.orderRepository.getByIdAsync(orderId,
        x => x.Include(item => item.product).ThenInclude(el => el.productImages).Include(x => x.User).Include(x => x.orderStatus));

        if (selectedOrder is null)
        {
            throw new NotFoundException("Böyle bir sipariş bulunamadı");
        }

        var baseUrl = configuration.GetValue<string>("AppSettings:BaseUrl");
        selectedOrder.product.productImages = selectedOrder.product.productImages.Select(item =>
        {
            if (!item.Path.StartsWith(baseUrl))
            {
                item.Path = $"{baseUrl}{item.Path}";
            }
            return item;
        }).ToList();

        return selectedOrder;
    }


    public async Task<Order> UpdateOrder(UpdateOrderDto updateOrderDto)
    {
        var order = await this.OrderCheckControl(updateOrderDto.OrderId);
        var product = await this.productRepository.getByIdAsync(order.ProductId, null);
        var oldStock = product.Stock + order.Quantity;

        if (oldStock < updateOrderDto.Quantity)
        {
            throw new ConflictException("Bu üründen yeteri kadar stok yok");
        }
        product.Stock = product.Stock + order.Quantity;
        product.Stock = product.Stock - updateOrderDto.Quantity;
        order.Quantity = updateOrderDto.Quantity;
        order.Description = updateOrderDto.Description;
        await this.orderRepository.saveChangesAsync();

        return order;
    }


    public async Task<Order> OrderUpdateStatus(OrderUpdateStatusDto request)
    {
        var result = await this.orderRepository.getByIdAsync(request.OrderId, null);
        if (result is null)
        {
            throw new NotFoundException("Sipariş bulunamadı");
        }

        result.OrderStatusId = request.OrderStatusId;
        await this.orderRepository.saveChangesAsync();

        

        return result;
    }
}