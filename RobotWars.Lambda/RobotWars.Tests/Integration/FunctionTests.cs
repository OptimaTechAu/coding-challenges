using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.TestUtilities;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using RobotWars.Core.Interfaces;
using RobotWars.Lambda;
using RobotWars.Lambda.Models;
using Xunit;


namespace RobotWars.Tests.Unit
{
    public class FunctionTests
    {
        private readonly Mock<ICommandProcessor> _mockCommandProcessor;
        private readonly Function _function;

        public FunctionTests()
        {
            _mockCommandProcessor = new Mock<ICommandProcessor>();

            var services = new ServiceCollection();
            services.AddSingleton(_mockCommandProcessor.Object);
            var serviceProvider = services.BuildServiceProvider();

            _function = new Function(serviceProvider);
        }

        [Fact]
        public async Task FunctionHandler_WithValidInput_ReturnsSuccessResponse()
        {            
            var input = "5 5\n1 2 N\nLMLMLMLMM\n3 3 E\nMMRMMRMRRM";
            var expectedOutput = "1 3 N\n5 1 E";

            _mockCommandProcessor
                .Setup(p => p.ProcessCommands(input))
                .Returns(expectedOutput);

            var request = new APIGatewayProxyRequest { Body = input };
            var context = new TestLambdaContext();
            
            var response = await _function.FunctionHandler(request, context);
            
            Assert.Equal(200, response.StatusCode);

            var responseModel = JsonConvert.DeserializeObject<Response>(response.Body);
            Assert.True(responseModel.Success);
            Assert.Equal(expectedOutput, responseModel.Result);
        }

        [Fact]
        public async Task FunctionHandler_WithEmptyBody_ReturnsBadRequest()
        {          
            var request = new APIGatewayProxyRequest { Body = "" };
            var context = new TestLambdaContext();
         
            var response = await _function.FunctionHandler(request, context);
            
            Assert.Equal(400, response.StatusCode);

            var responseModel = JsonConvert.DeserializeObject<Response>(response.Body);
            Assert.False(responseModel.Success);
            Assert.Equal("Request body is required", responseModel.ErrorMessage);
        }

        [Fact]
        public async Task FunctionHandler_WithInvalidInput_ReturnsBadRequest()
        {            
            var invalidInput = "Invalid input";
            _mockCommandProcessor
                .Setup(p => p.ProcessCommands(invalidInput))
                .Throws(new Exception("Processing error"));

            var request = new APIGatewayProxyRequest { Body = invalidInput };
            var context = new TestLambdaContext();
            
            var response = await _function.FunctionHandler(request, context);
            
            Assert.Equal(500, response.StatusCode);

            var responseModel = JsonConvert.DeserializeObject<Response>(response.Body);
            Assert.False(responseModel.Success);
            Assert.Equal("An error occurred processing the request", responseModel.ErrorMessage);
        }
    }
}
