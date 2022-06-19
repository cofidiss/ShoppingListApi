using ShoppingListApi.Models;
using Microsoft.EntityFrameworkCore;
using ShoppingListApi.Dependencies;
using ShoppingListApi.MiddleWares;
namespace ShoppingListApi
{

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ShoppingListContext>(opt =>
             opt.UseInMemoryDatabase("ShoppingList"));
            builder.Services.AddTransient<IOperationTransient, Operation>();
            builder.Services.AddScoped<IOperationScoped, Operation>();
            builder.Services.AddSingleton<IOperationSingleton, Operation>();
            builder.Services.AddScoped<IMyDependency1, MyDependency1>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

            }

            app.UseExceptionHandler("/error");
            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseMyMiddleware();

            app.MapControllers();

            app.Run();
        }
    }
}