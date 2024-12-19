using System.Net;
using Dapper;
using Domain.Model;
using Infrastructure.DataContext;
using Infrastructure.Response;

namespace Infrastructure.Service.MenuItemService;

public class MenuItemService(IContext context) : IMenuItemService
{
    public async Task<ApiResponse<List<MenuItem>>> GetAll()
    {
        using var connection = context.Connection();
        var sql = "select * from MenuItems";
        var res = await connection.QueryAsync<MenuItem>(sql);
        return new ApiResponse<List<MenuItem>>(res.ToList());
    }

    public async Task<ApiResponse<MenuItem>> GetById(int id)
    {
        using var connection = context.Connection();
        var sql = "select * from MenuItems where MenuItemId = @Id";
        var res = await connection.QuerySingleOrDefaultAsync<MenuItem>(sql, new { Id = id });
        if (res == null) return new ApiResponse<MenuItem>(HttpStatusCode.NotFound, "MenuItem Not Found");
        return new ApiResponse<MenuItem>(res);
    }

    public async Task<ApiResponse<bool>> AddMenuItem(MenuItem menuItem)
    {
        using var connection = context.Connection();
        var sql = "insert into MenuItems (Name,Price,Category) values(@Name,@Price,@Category)";
        var res = await connection.ExecuteAsync(sql, menuItem);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public async Task<ApiResponse<bool>> UpdateMenuItem(MenuItem menuItem)
    {
        using var connection = context.Connection();
        var sql = "update MenuItems set Name=@Name,Price=@Price,Category=@Category where MenuItemId = @MenuItemId";
        var res = await context.Connection().ExecuteAsync(sql, menuItem);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public async Task<ApiResponse<bool>> DeleteMenuItem(int id)
    {
        using var connection = context.Connection();
        var sql = "delete from MenuItems where MenuItemId = @Id";
        var res = await context.Connection().ExecuteAsync(sql, new { Id = id });
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.NotFound, "MenuItem not found");
        return new ApiResponse<bool>(res != 0);
    }
}