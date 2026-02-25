using System.Text.Json.Serialization;

namespace SupermarketCheckout.Lambda.Models;

public class CheckoutResponse
{
    public required string SessionId { get; init; }

    public required int TotalPrice { get; init; }

    public IReadOnlyDictionary<string, int>? ItemBreakdown { get; init; }

    public string? Error { get; init; }

    public bool Success => string.IsNullOrEmpty(Error);
}
