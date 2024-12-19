using Domain.Model;
using Infrastructure.Response;

namespace Infrastructure.Service.TableService;

public interface ITableService
{
    Task<ApiResponse<List<Table>>> GetAll();
    Task<ApiResponse<Table>> GetById(int id);
    Task<ApiResponse<bool>> AddTable(Table table);
    Task<ApiResponse<bool>> UpdateTable(Table table);
    Task<ApiResponse<bool>> DeleteTable(int id);
}