using Infrastructure.DataContext;
using Infrastructure.Service.CustomerService;
using Infrastructure.Service.MenuItemService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICustomerService,CustomerService>();
builder.Services.AddScoped<IMenuItemService,MenuItemService>();
builder.Services.AddSingleton<IContext,DapperContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();


