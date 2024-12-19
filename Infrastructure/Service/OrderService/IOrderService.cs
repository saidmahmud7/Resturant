using Domain.Model;
using Infrastructure.Response;

namespace Infrastructure.Service.OrderService;

public interface IOrderService
{
    Task<ApiResponse<List<Order>>> GetAll();
    Task<ApiResponse<Order>> GetById(int id);
    Task<ApiResponse<bool>> AddOrder(Order order);
    Task<ApiResponse<bool>> UpdateOrder(Order order);
    Task<ApiResponse<bool>> DeleteOrder(int id);
}