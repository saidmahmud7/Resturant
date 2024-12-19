using System.Net;
using Dapper;
using Domain.Model;
using Infrastructure.DataContext;
using Infrastructure.Response;

namespace Infrastructure.Service.TableService;

public class TableService(DapperContext context) : ITableService
{
    public async Task<ApiResponse<List<Table>>> GetAll()
    {
        using var connection = context.Connection();
        var sql = "select * from Tables";
        var res = await connection.QueryAsync<Table>(sql);
        return new ApiResponse<List<Table>>(res.ToList());
    }

    public async Task<ApiResponse<Table>> GetById(int id)
    {
        using var connection = context.Connection();
        var sql = "select * from Tables where TableId = @Id";
        var res = await connection.QuerySingleOrDefaultAsync<Table>(sql, new { Id = id });
        if (res == null) return new ApiResponse<Table>(HttpStatusCode.NotFound, "Table Not Found");
        return new ApiResponse<Table>(res);
    }

    public async Task<ApiResponse<bool>> AddTable(Table table)
    {
        using var connection = context.Connection();
        var sql = "insert into Tables (TableNumber,IsOccupied) values(@TableNumber,@IsOccupied)";
        var res = await connection.ExecuteAsync(sql, table);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public async Task<ApiResponse<bool>> UpdateTable(Table table)
    {
        using var connection = context.Connection();
        var sql = "update Tables set TableNumber=@TableNumber,IsOccupied=@IsOccupied where TableId = @TableId";
        var res = await context.Connection().ExecuteAsync(sql, table);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public async Task<ApiResponse<bool>> DeleteTable(int id)
    {
        using var connection = context.Connection();
        var sql = "delete from Tables where TableId = @Id";
        var res = await context.Connection().ExecuteAsync(sql, new { Id = id });
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.NotFound, "Table not found");
        return new ApiResponse<bool>(res != 0);
    }
}