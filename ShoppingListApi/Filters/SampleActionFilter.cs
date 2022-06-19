using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ShoppingListApi.Filters
{
    public class SampleActionFilter : Attribute, IActionFilter, IResourceFilter,IResultFilter,IExceptionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //throw new Exception("hata");
            context.Result = new ContentResult() { Content = "kisa yol" };
            // Do something before the action executes.
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do something after the action executes.
        }

        public void   OnResourceExecuting(ResourceExecutingContext context)
        {
           // throw new Exception("hata");
          //context.Result = new ContentResult() { Content="kisa yol"};
            // Do something after the action executes.
        }
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            //context.ExceptionHandled = true;
            // Do something after the action executes.
        }
        public void OnException(ExceptionContext context)
        {

            // Do something after the action executes.
        }
        public void OnResultExecuting(ResultExecutingContext context)
        {

            // Do something after the action executes.
        }
        public void OnResultExecuted(ResultExecutedContext context)
        {

            // Do something after the action executes.
        }
        
    }

}
