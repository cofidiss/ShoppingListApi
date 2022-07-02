using ShoppingListApi.Dependencies;

namespace ShoppingListApi.MiddleWares
{
    public class MiddleWare2
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        private readonly IOperationSingleton _singletonOperation;

        public MiddleWare2(RequestDelegate next, ILogger<MyMiddleware> logger,
            IOperationSingleton singletonOperation)
        {
            _logger = logger;
            _singletonOperation = singletonOperation;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context,
            IOperationTransient transientOperation, IOperationScoped scopedOperation, IOperationSingleton OperationSingleton)
        {
            _logger.LogInformation("Transient: " + transientOperation.OperationId);
            _logger.LogInformation("Scoped: " + scopedOperation.OperationId);
            _logger.LogInformation("Singleton: " + OperationSingleton.OperationId);
            _logger.LogInformation("Singleton: " + _singletonOperation.OperationId);
            await context.Response.WriteAsync("MiddleWare2");
            await _next(context);
        }
    }
}
