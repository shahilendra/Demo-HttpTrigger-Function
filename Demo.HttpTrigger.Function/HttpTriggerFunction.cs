using Demo.HttpTrigger.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using static Demo.HttpTrigger.Comman.Constants;

namespace Demo.HttpTrigger.Function
{
    public class HttpTriggerFunction
    {

        private readonly ILogger<HttpTriggerFunction> _logger;
        public HttpTriggerFunction(ILogger<HttpTriggerFunction> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [FunctionName(HTTP_TRIGGER_FUNCTION_NAME)]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            try
            {
                _logger.LogInformation($"{nameof(HttpTriggerFunction)}.{nameof(Run)} processed a request.");

                var product = new Product
                {
                    Id = Convert.ToInt32(req.Query["id"]),
                    Name = req.Query["name"],
                    Status = req.Query["status"]
                };
                // Or we can get parameters from body
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                Product data = JsonConvert.DeserializeObject<Product>(requestBody);

                if (data == null)
                    return new BadRequestObjectResult("Product details cannot be null. Please pass product deatils in body.");
                else
                {
                    await starter.StartNewAsync(ORCHESTRATION_TRIGGER_FUNCTION_NAME, data);
                    return new OkObjectResult($"Process has been started successfully you will get notification after completing the process!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(HttpTriggerFunction)}.{nameof(Run)} getting error Message: {ex.Message}.", ex);
                return new BadRequestObjectResult(ex);
            }
        }
    }
}
