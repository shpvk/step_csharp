Employee employee = new Employee();
employee.Name = "John Smith";
employee.Position = "Developer";
employee.Salary = 10000;
employee.Print();

Employee other = new Employee();
other.Name = "Jane Brown";
other.Salary = 12000;

Console.WriteLine("+2000: " + (employee + 2000).Salary);
Console.WriteLine("-1000: " + (employee - 1000).Salary);
Console.WriteLine("== : " + (employee == other));
Console.WriteLine("!= : " + (employee != other));
Console.WriteLine("< : " + (employee < other));
Console.WriteLine("> : " + (employee > other));
Console.WriteLine("Equals: " + employee.Equals(other));
employee++;
Console.WriteLine("++ : " + employee.Salary);
employee--;
Console.WriteLine("-- : " + employee.Salary);
employee.Salary = 300;
employee--;
Console.WriteLine("-- from 300: " + employee.Salary);

class Employee
{
    private decimal salary;

    public string Name { get; set; } = "";
    public DateTime BirthDate { get; set; }
    public string Phone { get; set; } = "";
    public string Email { get; set; } = "";
    public string Position { get; set; } = "";
    public string Responsibilities { get; set; } = "";

    public decimal Salary
    {
        get
        {
            return salary;
        }
        set
        {
            if (value < 0)
            {
                throw new Exception("Salary cannot be negative");
            }
            salary = value;
        }
    }

    public void Input()
    {
        Console.Write("Name: ");
        Name = Console.ReadLine()!;
        Console.Write("Birth date: ");
        BirthDate = DateTime.Parse(Console.ReadLine()!);
        Console.Write("Phone: ");
        Phone = Console.ReadLine()!;
        Console.Write("Email: ");
        Email = Console.ReadLine()!;
        Console.Write("Position: ");
        Position = Console.ReadLine()!;
        Console.Write("Responsibilities: ");
        Responsibilities = Console.ReadLine()!;
        Console.Write("Salary: ");
        Salary = decimal.Parse(Console.ReadLine()!);
    }

    public void Print()
    {
        Console.WriteLine("Name: " + Name);
        Console.WriteLine("Birth date: " + BirthDate.ToShortDateString());
        Console.WriteLine("Phone: " + Phone);
        Console.WriteLine("Email: " + Email);
        Console.WriteLine("Position: " + Position);
        Console.WriteLine("Responsibilities: " + Responsibilities);
        Console.WriteLine("Salary: " + Salary);
    }

    private Employee Copy()
    {
        Employee copy = new Employee();
        copy.Name = Name;
        copy.BirthDate = BirthDate;
        copy.Phone = Phone;
        copy.Email = Email;
        copy.Position = Position;
        copy.Responsibilities = Responsibilities;
        copy.Salary = Salary;
        return copy;
    }

    public static Employee operator +(Employee employee, decimal amount)
    {
        if (amount < 0)
        {
            throw new Exception("Amount cannot be negative");
        }
        Employee result = employee.Copy();
        result.Salary = employee.Salary + amount;
        return result;
    }

    public static Employee operator -(Employee employee, decimal amount)
    {
        if (amount < 0)
        {
            throw new Exception("Amount cannot be negative");
        }
        Employee result = employee.Copy();
        if (amount > employee.Salary)
        {
            result.Salary = 0;
        }
        else
        {
            result.Salary = employee.Salary - amount;
        }
        return result;
    }

    public static Employee operator ++(Employee employee)
    {
        return employee + 500;
    }

    public static Employee operator --(Employee employee)
    {
        return employee - 500;
    }

    public static bool operator ==(Employee? left, Employee? right)
    {
        if (left is null || right is null)
        {
            return left is null && right is null;
        }
        return left.Salary == right.Salary;
    }

    public static bool operator !=(Employee? left, Employee? right)
    {
        return !(left == right);
    }

    public static bool operator <(Employee left, Employee right)
    {
        return left.Salary < right.Salary;
    }

    public static bool operator >(Employee left, Employee right)
    {
        return left.Salary > right.Salary;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != typeof(Employee))
        {
            return false;
        }
        Employee other = (Employee)obj;
        return this == other;
    }

    public override int GetHashCode()
    {
        return Salary.GetHashCode();
    }
}
