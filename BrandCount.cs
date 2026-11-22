class Product
{
    public string Name { get; set; }
    public string Category { get; set; }
    public string Brand { get; set; }
    public decimal Price { get; set; }
}

class Program
{
    static void Main()
    {
        List<Product> products = new List<Product>
        {
            new Product { Name = "iPhone 15", Category = "Smartphones", Brand = "Apple", Price = 35000 },
            new Product { Name = "iPhone 14", Category = "Smartphones", Brand = "Apple", Price = 28000 },
            new Product { Name = "Samsung S24", Category = "Smartphones", Brand = "Samsung", Price = 32000 },
            new Product { Name = "Samsung A55", Category = "Smartphones", Brand = "Samsung", Price = 15000 },

            new Product { Name = "MacBook Air", Category = "Laptops", Brand = "Apple", Price = 45000 },
            new Product { Name = "MacBook Pro", Category = "Laptops", Brand = "Apple", Price = 75000 },
            new Product { Name = "Lenovo ThinkPad", Category = "Laptops", Brand = "Lenovo", Price = 38000 },
            new Product { Name = "Lenovo IdeaPad", Category = "Laptops", Brand = "Lenovo", Price = 25000 },

            new Product { Name = "Samsung TV", Category = "TV", Brand = "Samsung", Price = 29000 },
            new Product { Name = "LG OLED", Category = "TV", Brand = "LG", Price = 55000 },

            new Product { Name = "AirPods", Category = "Accessories", Brand = "Apple", Price = 8000 },
            new Product { Name = "Galaxy Buds", Category = "Accessories", Brand = "Samsung", Price = 5000 },
            new Product { Name = "Logitech Mouse", Category = "Accessories", Brand = "Logitech", Price = 1500 },
            new Product { Name = "Logitech Keyboard", Category = "Accessories", Brand = "Logitech", Price = 2500 }
        };

        var brands = products.GroupBy(product => product.Brand);

        foreach (var brand in brands)
        {
            Console.WriteLine(brand.Key + ": " + brand.Count());
        }
    }
}
