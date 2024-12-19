using Domain.Model;
using Infrastructure.Response;
using Infrastructure.Service.CustomerService;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controller;




[ApiController]
[Route("api/[controller]")]
public class CustomerController(ICustomerService service) :ControllerBase
{
    [HttpGet("GetCustomers")]
    public async Task<ApiResponse<List<Customer>>> GetAll() => await service.GetAll();

    [HttpGet("GetById/{id}")]
    public async Task<ApiResponse<Customer>> GetCustomerByID(int id) => await service.GetById(id);

    [HttpPost("CreateCustomer")]
    public async Task<ApiResponse<bool>> AddCustomer(Customer customer) => await service.AddCustomer(customer);

    [HttpPut("Update")]
    public async Task<ApiResponse<bool>> Update(Customer customer) => await service.UpdateCustomer(customer);

    [HttpDelete]
    public async Task<ApiResponse<bool>> Delete(int id) => await service.DeleteCustomer(id);

}