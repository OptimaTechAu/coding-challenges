using Amazon.Lambda.Core;
using RobotWarServerless.Services;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace RobotWarServerless
{
    public class Function
    {

        private readonly IRobotCommandProcessor _commandProcessor;

        public Function()
        {
            // First Constructor to initialize with default processor
            _commandProcessor = new RobotCommandProcessor();
        }

        // Second Constructor for testing with mock processor
        public Function(IRobotCommandProcessor commandProcessor)
        {
            _commandProcessor = commandProcessor;
        }

        public List<string> FunctionHandler(string input, ILambdaContext context)
        {
            try
            {
                context.Logger.LogInformation($"Processing, input is: {input}");
                var results = _commandProcessor.Process(input).ToList();
                context.Logger.LogInformation($"Returning results: {string.Join(", ", results)}");
                return results;
            }
            catch (Exception ex)
            {
                context.Logger.LogError($"Error processing Message: {ex.Message}");
                throw;
            }
        }
    }
}
