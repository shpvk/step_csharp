Magazine magazine = new Magazine();
magazine.Input();
magazine.Print();

magazine.Name = "Forbes";
Console.WriteLine(magazine.Name);

Magazine other = new Magazine();
other.Name = "Time";
other.Employees = 10;

Magazine bigger = magazine + 5;
Magazine smaller = magazine - 2;
Console.WriteLine("+5: " + bigger.Employees);
Console.WriteLine("-2: " + smaller.Employees);
Console.WriteLine("== : " + (magazine == other));
Console.WriteLine("!= : " + (magazine != other));
Console.WriteLine("< : " + (magazine < other));
Console.WriteLine("> : " + (magazine > other));
Console.WriteLine("Equals: " + magazine.Equals(other));

class Magazine
{
    private int employees;

    public string Name { get; set; } = "";
    public int Year { get; set; }
    public string Description { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Email { get; set; } = "";

    public int Employees
    {
        get
        {
            return employees;
        }
        set
        {
            if (value < 0)
            {
                throw new Exception("Employees cannot be negative");
            }
            employees = value;
        }
    }

    public void Input()
    {
        Console.Write("Name: ");
        Name = Console.ReadLine()!;
        Console.Write("Year: ");
        Year = int.Parse(Console.ReadLine()!);
        Console.Write("Description: ");
        Description = Console.ReadLine()!;
        Console.Write("Phone: ");
        Phone = Console.ReadLine()!;
        Console.Write("Email: ");
        Email = Console.ReadLine()!;
        Console.Write("Employees: ");
        Employees = int.Parse(Console.ReadLine()!);
    }

    public void Print()
    {
        Console.WriteLine("Name: " + Name);
        Console.WriteLine("Year: " + Year);
        Console.WriteLine("Description: " + Description);
        Console.WriteLine("Phone: " + Phone);
        Console.WriteLine("Email: " + Email);
        Console.WriteLine("Employees: " + Employees);
    }

    private Magazine Copy()
    {
        Magazine copy = new Magazine();
        copy.Name = Name;
        copy.Year = Year;
        copy.Description = Description;
        copy.Phone = Phone;
        copy.Email = Email;
        copy.Employees = Employees;
        return copy;
    }

    public static Magazine operator +(Magazine magazine, int count)
    {
        Magazine result = magazine.Copy();
        result.Employees = magazine.Employees + count;
        return result;
    }

    public static Magazine operator -(Magazine magazine, int count)
    {
        Magazine result = magazine.Copy();
        result.Employees = magazine.Employees - count;
        return result;
    }

    public static bool operator ==(Magazine left, Magazine right)
    {
        return left.Employees == right.Employees;
    }

    public static bool operator !=(Magazine left, Magazine right)
    {
        return !(left == right);
    }

    public static bool operator <(Magazine left, Magazine right)
    {
        return left.Employees < right.Employees;
    }

    public static bool operator >(Magazine left, Magazine right)
    {
        return left.Employees > right.Employees;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Magazine other)
        {
            return this == other;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return Employees;
    }
}
