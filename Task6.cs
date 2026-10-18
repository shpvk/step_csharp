Shop shop = new Shop();
shop.Input();
shop.Print();

shop.SetName("Rozetka");
Console.WriteLine(shop.GetName());

class Shop
{
    private string name = "";
    private string address = "";
    private string description = "";
    private string phone = "";
    private string email = "";

    public void Input()
    {
        Console.Write("Name: ");
        name = Console.ReadLine()!;
        Console.Write("Address: ");
        address = Console.ReadLine()!;
        Console.Write("Description: ");
        description = Console.ReadLine()!;
        Console.Write("Phone: ");
        phone = Console.ReadLine()!;
        Console.Write("Email: ");
        email = Console.ReadLine()!;
    }

    public void Print()
    {
        Console.WriteLine("Name: " + name);
        Console.WriteLine("Address: " + address);
        Console.WriteLine("Description: " + description);
        Console.WriteLine("Phone: " + phone);
        Console.WriteLine("Email: " + email);
    }

    public string GetName()
    {
        return name;
    }

    public void SetName(string value)
    {
        name = value;
    }

    public string GetAddress()
    {
        return address;
    }

    public void SetAddress(string value)
    {
        address = value;
    }

    public string GetDescription()
    {
        return description;
    }

    public void SetDescription(string value)
    {
        description = value;
    }

    public string GetPhone()
    {
        return phone;
    }

    public void SetPhone(string value)
    {
        phone = value;
    }

    public string GetEmail()
    {
        return email;
    }

    public void SetEmail(string value)
    {
        email = value;
    }
}
