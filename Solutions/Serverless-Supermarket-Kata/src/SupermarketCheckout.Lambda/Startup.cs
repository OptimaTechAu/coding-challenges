using Amazon.DynamoDBv2;
using Amazon.Lambda.Core;
using Microsoft.Extensions.DependencyInjection;
using SupermarketCheckout.Lambda.Interfaces;
using SupermarketCheckout.Lambda.Services;

namespace SupermarketCheckout.Lambda;

public static class Startup
{
    private const string CheckoutTableNameEnvVar = "CHECKOUT_TABLE_NAME";

    public static ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        // DynamoDB client — only register when a table name is configured
        var tableName = Environment.GetEnvironmentVariable(CheckoutTableNameEnvVar);
        if (!string.IsNullOrEmpty(tableName))
        {
            services.AddSingleton<IAmazonDynamoDB, AmazonDynamoDBClient>();
        }

        services.AddSingleton<Func<ICheckoutStorageProvider>>(sp =>
        {
            var dynamoDb = sp.GetService<IAmazonDynamoDB>();
            return () => dynamoDb != null
                ? new DynamoDbCheckoutStorageProvider(dynamoDb, tableName: Environment.GetEnvironmentVariable(CheckoutTableNameEnvVar))
                : new InMemoryCheckoutStorageProvider();
        });

        services.AddSingleton<IPricingRuleProvider, PricingRuleProvider>();

        // Transient checkout handler, if this was causing performance issues, could cache instances per session ID, but for simplicity we create a new one per request
        services.AddTransient<Func<string, ILambdaLogger, ICheckout>>(sp =>
        {
            var storageProviderFactory = sp.GetRequiredService<Func<ICheckoutStorageProvider>>();
            var pricingRuleProvider = sp.GetRequiredService<IPricingRuleProvider>();
            return (sessionId, logger) => new CheckoutHandler(pricingRuleProvider, storageProviderFactory(), logger, sessionId);
        });

        return services.BuildServiceProvider();
    }
}
