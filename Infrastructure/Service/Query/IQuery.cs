using Domain.Model;
using Infrastructure.Response;

namespace Infrastructure.Service.Query;

public interface IQuery
{
    Task<ApiResponse<List<Customer>>> GetCustomerByName();
    Task<ApiResponse<List<Order>>> GetOrderByCustomer(int id);
    Task<ApiResponse<List<Table>>> GetUnreservedЕables();
    Task<ApiResponse<decimal>> GetSum(int id);
}