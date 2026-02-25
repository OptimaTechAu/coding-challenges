using System.Text.Json.Serialization;

namespace SupermarketCheckout.Lambda.Models;

public class CheckoutRequest
{
    public string? SessionId { get; init; }

    public string[] Items { get; init; } = Array.Empty<string>();
}
