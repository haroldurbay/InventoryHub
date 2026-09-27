using ClientApp.Models;

namespace ClientApp.Services;

public sealed class ProductCache
{
    private Product[]? _products;

    public bool TryGet(out Product[] products)
    {
        products = _products ?? Array.Empty<Product>();
        return _products is not null;
    }

    public void Set(Product[] products)
    {
        _products = products;
    }
}
