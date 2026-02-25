using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;
using Microsoft.Extensions.DependencyInjection;
using SupermarketCheckout.Lambda.Interfaces;
using SupermarketCheckout.Lambda.Models;
using SupermarketCheckout.Lambda.Services;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace SupermarketCheckout.Lambda;

public class Function
{
    private readonly Func<string, ILambdaLogger, ICheckout> _checkoutFactory;
    private ICheckout? _checkoutHandler;

    /// <summary>
    /// Default constructor used by AWS Lambda.
    /// </summary>
    public Function()
    {
        var serviceProvider = Startup.ConfigureServices();
        _checkoutFactory = serviceProvider.GetRequiredService<Func<string, ILambdaLogger, ICheckout>>();
    }

    /// <summary>
    /// Constructor for unit testing with an explicit checkout dependency.
    /// </summary>
    /// <param name="checkoutFactory">The factory function to create a checkout handler.</param>
    public Function(Func<string, ILambdaLogger, ICheckout> checkoutFactory)
    {
        _checkoutFactory = checkoutFactory;
    }

    /// <summary>
    /// Lambda function handler that processes checkout requests.
    /// </summary>
    /// <param name="request">The checkout request containing items to scan.</param>
    /// <param name="context">The Lambda execution context.</param>
    /// <returns>A checkout response with the total price and session ID.</returns>
    public async Task<CheckoutResponse> FunctionHandler(CheckoutRequest request, ILambdaContext context)
    {
        context.Logger.LogInformation("Processing checkout request with {itemLength} items (Session: {sessionId})",
            request.Items?.Length ?? 0,
            request.SessionId ?? "new");

        var sessionId = request.SessionId ?? Guid.NewGuid().ToString();
        _checkoutHandler = _checkoutFactory(sessionId, context.Logger);

        try
        {
            foreach (var item in request?.Items ?? Array.Empty<string>())
            {
                _checkoutHandler.Scan(item);
            }

            var totalPrice = _checkoutHandler.GetTotalPrice();
            var itemBreakdown = await _checkoutHandler.GetAllSkusAndCountsAsync();

            context.Logger.LogInformation("Scan complete. Total price: {totalPrice} (Session: {sessionId})", totalPrice, _checkoutHandler.SessionId);

            return new CheckoutResponse
            {
                SessionId = _checkoutHandler.SessionId,
                TotalPrice = totalPrice,
                ItemBreakdown = itemBreakdown
            };
        }
        catch (ArgumentException ex)
        {
            context.Logger.LogError("Invalid item in checkout: {errorMessage}", ex.Message);

            return new CheckoutResponse
            {
                SessionId = sessionId,
                TotalPrice = 0,
                Error = ex.Message
            };
        }
        catch (Exception ex)
        {
            context.Logger.LogError("Error processing checkout: {errorMessage}", ex.Message);

            return new CheckoutResponse
            {
                SessionId = sessionId,
                TotalPrice = 0,
                Error = "An error occurred processing the checkout"
            };
        }
    }
}
