using System.Net;
using Dapper;
using Domain.Model;
using Infrastructure.DataContext;
using Infrastructure.Response;

namespace Infrastructure.Service.Query;

public class Query(IContext context) : IQuery
{
    public async Task<ApiResponse<List<Customer>>> GetCustomerByName()
    {
        using var connection = context.Connection();
        var sql = @"select * from customers where Name = @Name";
        var res = await connection.QueryAsync<Customer>(sql);
        return new ApiResponse<List<Customer>>(res.ToList());
    }

    public async Task<ApiResponse<List<Order>>> GetOrderByCustomer(int id)
    {
        using var connection = context.Connection();
        var sql = @"select * from orders where customerid=@CustomerId";
        var res = await connection.QueryAsync<Order>(sql, new { CustomerId = id });
        return new ApiResponse<List<Order>>(res.ToList());
    }

    public async Task<ApiResponse<List<Table>>> GetUnreservedЕables()
    {
        using var connection = context.Connection();
        var sql = "select * from tables where IsOccupied =  false ";
        var res = await connection.QueryAsync<Table>(sql);
        return new ApiResponse<List<Table>>(res.ToList());
    }

    public async Task<ApiResponse<decimal>> GetSum(int id)
    {
        using var connection = context.Connection();
        var sql = @"select sum(price) as total from MenuItems
                     join OrderItems on OrderItems.menuitemid = MenuItems.menuitemid
                     join orders ON orders.orderid = OrderItems.orderid
                     join customers on customers.customerid = orders.customerid
                     where customers.customerid = @Id";
        var res = await connection.ExecuteScalarAsync<decimal>(sql,new {Id = id});
        return new ApiResponse<decimal>(res);
    }
}