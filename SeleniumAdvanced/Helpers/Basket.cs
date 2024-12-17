namespace SeleniumAdvanced.Helpers;

public class Basket(string name, int quantity, decimal price)
{
    public string Name { get; } = name;
    public int Quantity { get; private set; } = quantity;
    public decimal Price { get; } = price;

    public void IncreaseQuantity(int quantity)
    {
        Quantity += quantity;
    }
}