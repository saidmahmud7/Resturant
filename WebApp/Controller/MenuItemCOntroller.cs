using Domain.Model;
using Infrastructure.Response;
using Infrastructure.Service.MenuItemService;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controller;

[ApiController]
[Route("api/[controller]")]
public class MenuItemController(IMenuItemService service) :ControllerBase
{
    [HttpGet("GetMenuItem")]
    public async Task<ApiResponse<List<MenuItem>>> GetAll() => await service.GetAll();

    [HttpGet("GetById/{id}")]
    public async Task<ApiResponse<MenuItem>> GetCustomerByID(int id) => await service.GetById(id);

    [HttpPost("Create")]
    public async Task<ApiResponse<bool>> AddCustomer(MenuItem menuItem) => await service.AddMenuItem(menuItem);

    [HttpPut("Update")]
    public async Task<ApiResponse<bool>> Update(MenuItem menuItem) => await service.UpdateMenuItem(menuItem);

    [HttpDelete]
    public async Task<ApiResponse<bool>> Delete(int id) => await service.DeleteMenuItem(id);

}