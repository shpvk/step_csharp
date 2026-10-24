Shop shop = new Shop();
shop.Input();
shop.Print();

shop.Name = "Rozetka";
Console.WriteLine(shop.Name);

Shop other = new Shop();
other.Name = "Epicentr";
other.Area = 100;

Shop bigger = shop + 50;
Shop smaller = shop - 20;
Console.WriteLine("+50: " + bigger.Area);
Console.WriteLine("-20: " + smaller.Area);
Console.WriteLine("== : " + (shop == other));
Console.WriteLine("!= : " + (shop != other));
Console.WriteLine("< : " + (shop < other));
Console.WriteLine("> : " + (shop > other));
Console.WriteLine("Equals: " + shop.Equals(other));

class Shop
{
    private double area;

    public string Name { get; set; } = "";
    public string Address { get; set; } = "";
    public string Description { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Email { get; set; } = "";

    public double Area
    {
        get
        {
            return area;
        }
        set
        {
            if (value < 0)
            {
                throw new Exception("Area cannot be negative");
            }
            area = value;
        }
    }

    public void Input()
    {
        Console.Write("Name: ");
        Name = Console.ReadLine()!;
        Console.Write("Address: ");
        Address = Console.ReadLine()!;
        Console.Write("Description: ");
        Description = Console.ReadLine()!;
        Console.Write("Phone: ");
        Phone = Console.ReadLine()!;
        Console.Write("Email: ");
        Email = Console.ReadLine()!;
        Console.Write("Area: ");
        Area = double.Parse(Console.ReadLine()!);
    }

    public void Print()
    {
        Console.WriteLine("Name: " + Name);
        Console.WriteLine("Address: " + Address);
        Console.WriteLine("Description: " + Description);
        Console.WriteLine("Phone: " + Phone);
        Console.WriteLine("Email: " + Email);
        Console.WriteLine("Area: " + Area);
    }

    private Shop Copy()
    {
        Shop copy = new Shop();
        copy.Name = Name;
        copy.Address = Address;
        copy.Description = Description;
        copy.Phone = Phone;
        copy.Email = Email;
        copy.Area = Area;
        return copy;
    }

    public static Shop operator +(Shop shop, double size)
    {
        Shop result = shop.Copy();
        result.Area = shop.Area + size;
        return result;
    }

    public static Shop operator -(Shop shop, double size)
    {
        Shop result = shop.Copy();
        result.Area = shop.Area - size;
        return result;
    }

    public static bool operator ==(Shop left, Shop right)
    {
        return left.Area == right.Area;
    }

    public static bool operator !=(Shop left, Shop right)
    {
        return !(left == right);
    }

    public static bool operator <(Shop left, Shop right)
    {
        return left.Area < right.Area;
    }

    public static bool operator >(Shop left, Shop right)
    {
        return left.Area > right.Area;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Shop other)
        {
            return this == other;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return Area.GetHashCode();
    }
}
