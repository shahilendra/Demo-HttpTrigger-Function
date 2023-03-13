using Demo.HttpTrigger.Model;

namespace Demo.HttpTrigger.Services.Abstraction
{
    public interface IDataService
    {
        Task ProcessData(Product data);
    }
}