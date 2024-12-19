using Domain.Model;
using Infrastructure.Response;

namespace Infrastructure.Service.CustomerService;

public interface ICustomerService
{
    Task<ApiResponse<List<Customer>>> GetAll();
    Task<ApiResponse<Customer>> GetById(int id);
    Task<ApiResponse<bool>> AddCustomer(Customer customer);
    Task<ApiResponse<bool>> UpdateCustomer(Customer customer);
    Task<ApiResponse<bool>> DeleteCustomer(int id);
}