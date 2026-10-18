Website site = new Website();
site.Input();
site.Print();

site.SetName("Google");
Console.WriteLine(site.GetName());

class Website
{
    private string name = "";
    private string path = "";
    private string description = "";
    private string ipAddress = "";

    public void Input()
    {
        Console.Write("Name: ");
        name = Console.ReadLine()!;
        Console.Write("Path: ");
        path = Console.ReadLine()!;
        Console.Write("Description: ");
        description = Console.ReadLine()!;
        Console.Write("IP address: ");
        ipAddress = Console.ReadLine()!;
    }

    public void Print()
    {
        Console.WriteLine("Name: " + name);
        Console.WriteLine("Path: " + path);
        Console.WriteLine("Description: " + description);
        Console.WriteLine("IP address: " + ipAddress);
    }

    public string GetName()
    {
        return name;
    }

    public void SetName(string value)
    {
        name = value;
    }

    public string GetPath()
    {
        return path;
    }

    public void SetPath(string value)
    {
        path = value;
    }

    public string GetDescription()
    {
        return description;
    }

    public void SetDescription(string value)
    {
        description = value;
    }

    public string GetIpAddress()
    {
        return ipAddress;
    }

    public void SetIpAddress(string value)
    {
        ipAddress = value;
    }
}
