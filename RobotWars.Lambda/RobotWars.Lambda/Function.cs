using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;
using RobotWars.Core.Exceptions;
using RobotWars.Core.Interfaces;
using RobotWars.Lambda.Extensions;
using RobotWars.Lambda.Models;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace RobotWars.Lambda
{
    public class Function
    {
        private readonly IServiceProvider _serviceProvider;

        public Function()
        {
            var services = new ServiceCollection();
            services.AddRobotWarsServices();
            _serviceProvider = services.BuildServiceProvider();
        }

        
        public Function(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                context.Logger.LogLine("Processing request");

                if (string.IsNullOrEmpty(request.Body))
                {
                    return BuildResponse(400, new Response
                    {
                        Success = false,
                        ErrorMessage = "Request body is required"
                    });
                }

                var commandProcessor = _serviceProvider.GetRequiredService<ICommandProcessor>();
                var result = commandProcessor.ProcessCommands(request.Body);

                return BuildResponse(200, new Response
                {
                    Success = true,
                    Result = result
                });
            }
            catch (RobotWarsException ex)
            {
                context.Logger.LogLine($"Business error: {ex.Message}");
                return BuildResponse(400, new Response
                {
                    Success = false,
                    ErrorMessage = ex.Message
                });
            }
            catch (Exception ex)
            {
                context.Logger.LogLine($"Error: {ex.Message}");
                context.Logger.LogLine($"Stack trace: {ex.StackTrace}");

                return BuildResponse(500, new Response
                {
                    Success = false,
                    ErrorMessage = "An error occurred processing the request"
                });
            }
        }

        private APIGatewayProxyResponse BuildResponse(int statusCode, Response responseModel)
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = statusCode,
                Body = JsonConvert.SerializeObject(responseModel),
                Headers = new Dictionary<string, string>
                {
                    { "Content-Type", "application/json" }
                }
            };
        }
    }
}
