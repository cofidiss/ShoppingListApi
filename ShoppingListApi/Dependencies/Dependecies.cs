using System.Diagnostics;
namespace ShoppingListApi.Dependencies
{
    public class Operation : IOperationTransient, IOperationScoped, IOperationSingleton
    {
        public Operation()
        {
            OperationId = Guid.NewGuid().ToString()[^4..];
        }

        public string OperationId { get; }
    }

    public interface IOperation
    {
        string OperationId { get; }
    }

    public interface IOperationTransient : IOperation { }
    public interface IOperationScoped : IOperation { }
    public interface IOperationSingleton : IOperation { }



    public interface IMyDependency1
    {

        void WriteMessage(string message);

    }

    public  class MyDependency1 : IMyDependency1
    {

        string depen1;
        public MyDependency1()
        {
            depen1 = "1";
        }
        public void WriteMessage(string message)
        {
            Debug.WriteLine(message);

        }
     
    }
}
