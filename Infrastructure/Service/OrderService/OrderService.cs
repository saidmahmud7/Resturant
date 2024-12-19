using System.Net;
using Dapper;
using Domain.Model;
using Infrastructure.DataContext;
using Infrastructure.Response;

namespace Infrastructure.Service.OrderService;

public class OrderService(IContext context) : IOrderService
{
    public async Task<ApiResponse<List<Order>>> GetAll()
    {
        using var connection = context.Connection();
        var sql = "select * from Orders";
        var res = await connection.QueryAsync<Order>(sql);
        return new ApiResponse<List<Order>>(res.ToList());
    }

    public async Task<ApiResponse<Order>> GetById(int id)
    {
        using var connection = context.Connection();
        var sql = "select * from Orders where OrderId = @Id";
        var res = await connection.QuerySingleOrDefaultAsync<Order>(sql, new { Id = id });
        if (res == null) return new ApiResponse<Order>(HttpStatusCode.NotFound, "Order Not Found");
        return new ApiResponse<Order>(res);
    }

    public async Task<ApiResponse<bool>> AddOrder(Order order)
    {
        using var connection = context.Connection();
        var sql = "insert into Orders (CustomerId,TableId,Status) values(@CustomerId,@TableId,@Status)";
        var res = await connection.ExecuteAsync(sql, order);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public async Task<ApiResponse<bool>> UpdateOrder(Order order)
    {
        using var connection = context.Connection();
        var sql = "update Orders set CustomerId=@CustomerId,TableId=@TableId,Status=@Status where OrderId = @OrderId";
        var res = await context.Connection().ExecuteAsync(sql, order);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public async Task<ApiResponse<bool>> DeleteOrder(int id)
    {
        using var connection = context.Connection();
        var sql = "delete from Orders where OrderId = @Id";
        var res = await context.Connection().ExecuteAsync(sql, new { Id = id });
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.NotFound, "Order not found");
        return new ApiResponse<bool>(res != 0);
    }
}