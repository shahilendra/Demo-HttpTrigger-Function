using Demo.HttpTrigger.Model;
using Demo.HttpTrigger.Services.Abstraction;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using static Demo.HttpTrigger.Comman.Constants;

namespace Demo.HttpTrigger.Function
{
    public class ActivityFunction
    {
        private readonly IDataService _dataService;
        private readonly ILogger<ActivityFunction> _logger;

        public ActivityFunction(IDataService dataService, ILogger<ActivityFunction> logger)
        {
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [FunctionName(ACTIVITY_TRIGGER_FUNCTION_NAME)]
        public async Task Run([ActivityTrigger] IDurableActivityContext context, ILogger log)
        {
            try
            {
                _logger.LogInformation($"{nameof(ActivityFunction)}.{nameof(Run)} processing a request.");
                var data = context.GetInput<Product>();
                if (data != null)
                    await _dataService.ProcessData(data);
                _logger.LogInformation($"{nameof(ActivityFunction)}.{nameof(Run)} completed request.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(ActivityFunction)}.{nameof(Run)} getting Error Message: {ex}.", ex);
            }
        }
    }
}
