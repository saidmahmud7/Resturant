using System.Net;
using Dapper;
using Domain.Model;
using Infrastructure.DataContext;
using Infrastructure.Response;

namespace Infrastructure.Service.CustomerService;

public class CustomerService(IContext context) : ICustomerService
{
    public async Task<ApiResponse<List<Customer>>> GetAll()
    {
        using var connection = context.Connection();
        var sql = "select * from customers";
        var res = await connection.QueryAsync<Customer>(sql);
        return new ApiResponse<List<Customer>>(res.ToList());
    }

    public async Task<ApiResponse<Customer>> GetById(int id)
    {
        using var connection = context.Connection();
        var sql = "select * from customers where CustomerId = @Id";
        var res = await connection.QuerySingleOrDefaultAsync<Customer>(sql, new { Id = id });
        if (res == null) return new ApiResponse<Customer>(HttpStatusCode.NotFound, "Customer Not Found");
        return new ApiResponse<Customer>(res);
    }

    public async Task<ApiResponse<bool>> AddCustomer(Customer customer)
    {
        using var connection = context.Connection();
        var sql = "insert into customers (Name,PhoneNumber) values(@Name,@PhoneNumber)";
        var res = await connection.ExecuteAsync(sql, customer);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public async Task<ApiResponse<bool>> UpdateCustomer(Customer customer)
    {
        using var connection = context.Connection();
        var sql = "update customers set Name=@Name,PhoneNumber=@PhoneNumber where CustomerId = @CustomerId";
        var res = await context.Connection().ExecuteAsync(sql, customer);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public async Task<ApiResponse<bool>> DeleteCustomer(int id)
    {
        using var connection = context.Connection();
        var sql = "delete from customers where CustomerId = @Id";
        var res = await context.Connection().ExecuteAsync(sql, new { Id = id });
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.NotFound, "Customer not found");
        return new ApiResponse<bool>(res != 0);
    }
}