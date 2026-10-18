Magazine magazine = new Magazine();
magazine.Input();
magazine.Print();

magazine.SetName("Forbes");
Console.WriteLine(magazine.GetName());

class Magazine
{
    private string name = "";
    private int year;
    private string description = "";
    private string phone = "";
    private string email = "";

    public void Input()
    {
        Console.Write("Name: ");
        name = Console.ReadLine()!;
        Console.Write("Year: ");
        year = int.Parse(Console.ReadLine()!);
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
        Console.WriteLine("Year: " + year);
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

    public int GetYear()
    {
        return year;
    }

    public void SetYear(int value)
    {
        year = value;
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
