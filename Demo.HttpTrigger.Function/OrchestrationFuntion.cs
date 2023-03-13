using Demo.HttpTrigger.Model;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using static Demo.HttpTrigger.Comman.Constants;

namespace Demo.HttpTrigger.Function
{
    public class OrchestrationFuntion
    {
        private readonly ILogger<OrchestrationFuntion> _logger;
        public OrchestrationFuntion(ILogger<OrchestrationFuntion> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        [FunctionName(ORCHESTRATION_TRIGGER_FUNCTION_NAME)]
        public async Task RunOrchestrator(
           [OrchestrationTrigger] IDurableOrchestrationContext context, ILogger log)
        {
            try
            {
                _logger.LogInformation($"{nameof(OrchestrationFuntion)}.{nameof(RunOrchestrator)} processed a request.");
                await context.CallActivityAsync(ACTIVITY_TRIGGER_FUNCTION_NAME, context.GetInput<Product>());
                _logger.LogInformation($"{nameof(OrchestrationFuntion)}.{nameof(RunOrchestrator)} processed a completed.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(OrchestrationFuntion)}.{nameof(RunOrchestrator)} getting error Message: {ex.Message}.", ex);
            }
        }
    }
}