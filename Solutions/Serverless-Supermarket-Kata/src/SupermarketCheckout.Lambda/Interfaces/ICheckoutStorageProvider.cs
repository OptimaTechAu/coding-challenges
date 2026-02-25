namespace SupermarketCheckout.Lambda.Interfaces;

/// <summary>
/// Interface for checkout data storage.
/// Abstracts the storage mechanism for scanned items during a checkout session.
/// </summary>
public interface ICheckoutStorageProvider
{
    /// <summary>
    /// Adds an item to the checkout session or increments its quantity if already present.
    /// </summary>
    /// <param name="sessionId">The unique identifier for the checkout session.</param>
    /// <param name="sku">The Stock Keeping Unit identifier.</param>
    /// <param name="quantity">The quantity to add (default: 1).</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task AddSkuAsync(string sessionId, string sku, int quantity = 1);

    /// <summary>
    /// Gets the quantity of a specific SKU in the checkout session.
    /// </summary>
    /// <param name="sessionId">The unique identifier for the checkout session.</param>
    /// <param name="sku">The Stock Keeping Unit identifier.</param>
    /// <returns>The quantity of the SKU, or 0 if not present.</returns>
    Task<int> GetSkuQuantityAsync(string sessionId, string sku);

    /// <summary>
    /// Gets all SKUs in the checkout session with their quantities.
    /// </summary>
    /// <param name="sessionId">The unique identifier for the checkout session.</param>
    /// <returns>A dictionary of SKUs to quantities.</returns>
    Task<IReadOnlyDictionary<string, int>> GetAllSkusAsync(string sessionId);

    /// <summary>
    /// Clears all SKUs from the checkout session.
    /// </summary>
    /// <param name="sessionId">The unique identifier for the checkout session.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task ClearAsync(string sessionId);
}
