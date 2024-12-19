using Domain.Model;
using Infrastructure.Response;

namespace Infrastructure.Service.MenuItemService;

public interface IMenuItemService
{
    Task<ApiResponse<List<MenuItem>>> GetAll();
    Task<ApiResponse<MenuItem>> GetById(int id);
    Task<ApiResponse<bool>> AddMenuItem(MenuItem menuItem);
    Task<ApiResponse<bool>> UpdateMenuItem(MenuItem menuItem);
    Task<ApiResponse<bool>> DeleteMenuItem(int id);
}