using Amazon.Lambda.Core;

namespace SupermarketCheckout.Tests;

public class TestLambdaContext : ILambdaContext
{
    public string AwsRequestId => "test-request-id";
    public IClientContext ClientContext => null!;
    public string FunctionName => "TestFunction";
    public string FunctionVersion => "1.0";
    public ICognitoIdentity Identity => null!;
    public string InvokedFunctionArn => "arn:aws:lambda:test";
    public ILambdaLogger Logger => new TestLambdaLogger();
    public string LogGroupName => "test-log-group";
    public string LogStreamName => "test-log-stream";
    public int MemoryLimitInMB => 256;
    public TimeSpan RemainingTime => TimeSpan.FromMinutes(5);
}
