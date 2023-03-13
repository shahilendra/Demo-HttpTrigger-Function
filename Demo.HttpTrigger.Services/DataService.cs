using Demo.HttpTrigger.Model;
using Demo.HttpTrigger.Services.Abstraction;
using Microsoft.Extensions.Logging;

namespace Demo.HttpTrigger.Services
{
    public class DataService : IDataService
    {
        private readonly ILogger<DataService> _logger;

        public DataService(ILogger<DataService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task ProcessData(Product data)
        {
            try
            {
                _logger.LogInformation($"{nameof(DataService)}.{nameof(ProcessData)} called!");
                _logger.LogInformation($"Product Id {data.Id}, Name: {data.Name}, Status: {data.Status}");
                await Task.FromResult("Process has been Completed");
                _logger.LogInformation($"{nameof(DataService)}.{nameof(ProcessData)} Commpleted!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(DataService)}.{nameof(ProcessData)} getting error Message: {ex.Message}", ex);
                throw;
            }
        }
    }
}