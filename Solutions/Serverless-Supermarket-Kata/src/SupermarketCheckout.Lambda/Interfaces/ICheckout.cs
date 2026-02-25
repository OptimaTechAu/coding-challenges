namespace SupermarketCheckout.Lambda.Interfaces;

/// <summary>
/// Interface for the supermarket checkout system.
/// Allows scanning items and calculating the total price.
/// </summary>
public interface ICheckout
{
    /// <summary>
    /// Gets the unique identifier for this checkout session.
    /// </summary>
    string SessionId { get; }

    /// <summary>
    /// Scans an item and adds it to the current basket.
    /// </summary>
    /// <param name="item">The SKU of the item to scan.</param>
    void Scan(string item);

    /// <summary>
    /// Calculates the total price of all scanned items,
    /// applying any applicable special offers.
    /// </summary>
    /// <returns>The total price.</returns>
    int GetTotalPrice();

    /// <summary>
    /// Gets the breakdown of all scanned items with their quantities.
    /// </summary>
    /// <returns>A dictionary of SKUs with quantities.</returns>
    Task<IReadOnlyDictionary<string, int>> GetAllSkusAndCountsAsync();
}
