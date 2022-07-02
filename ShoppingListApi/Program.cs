using ShoppingListApi.Models;
using Microsoft.EntityFrameworkCore;
using ShoppingListApi.Dependencies;
using ShoppingListApi.MiddleWares;
using System.Text;

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
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();

            //}

            app.Map("/map1", HandleBranch);
            //app.MapWhen("/map", HandleBranch);
            static void HandleBranch(IApplicationBuilder app)
            {
                app.Use(async (context,next) =>
                {
                    var branchVer = context.Request.Query["branch"];

                    await context.Response.WriteAsync("marp1 oncesi");
                    await next();
                    await context.Response.WriteAsync("marp1 sonrasi");
                });
            }

            app.UseMiddleware<MiddleWare1>();
            app.UseMiddleware<MiddleWare2>();
            //app.Use(async (context,next) =>            
            //{
            //   await context.Response.WriteAsync("delegate1 önce1");

            //    await context.Response.WriteAsync("delegate1 önce2");
            //    //context.Response.StatusCode = 200;
            //    await next.Invoke();
            //    await context.Response.WriteAsync("delegate1 sonra");
            //    var b = context.Response.HasStarted;
            //});
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("delegate2 önce");
            //    var a = context.Response.HasStarted;

            //   // await next.Invoke();
            //    await context.Response.WriteAsync("delegate2 sonra");
            //    var b = context.Response.HasStarted;
            //});

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("delegate2 önce");
                var a = context.Response.HasStarted;
         
                await context.Response.WriteAsync("delegate2 sonra");
                var b = context.Response.HasStarted;
            });
            app.Run();  
           // app.UseExceptionHandler("/error");
           // app.UseHttpsRedirection();

           // app.UseAuthorization();
           // app.UseMyMiddleware();

           // app.MapControllers();

           //// app.Run();
        }
    }  
}