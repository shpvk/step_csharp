class Employee
{
    public string FullName;
    public string Position;
    public string Phone;
    public string Email;
    public decimal Salary;

    public Employee(string fullName, string position, string phone, string email, decimal salary)
    {
        if (fullName == null || position == null || phone == null || email == null)
        {
            throw new Exception("Empty employee data");
        }
        if (salary < 0)
        {
            throw new Exception("Wrong salary");
        }
        FullName = fullName;
        Position = position;
        Phone = phone;
        Email = email;
        Salary = salary;
    }

    public override string ToString()
    {
        return FullName + " | " + Position + " | " + Phone + " | " + Email + " | " + Salary;
    }
}

class Firm
{
    public string Name;
    public DateTime FoundedDate;
    public string Profile;
    public string Director;
    public string Address;
    public Employee[] Employees;

    public Firm(string name, DateTime foundedDate, string profile, string director, string address, Employee[] employees)
    {
        if (name == null || profile == null || director == null || address == null || employees == null)
        {
            throw new Exception("Empty firm data");
        }
        Name = name;
        FoundedDate = foundedDate;
        Profile = profile;
        Director = director;
        Address = address;
        Employees = employees;
    }

    public int EmployeesCount
    {
        get { return Employees.Length; }
    }

    public override string ToString()
    {
        return Name + " | " + FoundedDate.ToShortDateString() + " | " + Profile + " | " + Director + " | " + EmployeesCount + " | " + Address;
    }
}

class Program
{
    static void Print(string title, IEnumerable<Employee> employees)
    {
        Console.WriteLine(title);
        foreach (Employee employee in employees)
        {
            Console.WriteLine("  " + employee);
        }
        Console.WriteLine();
    }

    static void Main()
    {
        try
        {
            Firm[] firms =
            {
                new Firm("Fresh Food", new DateTime(2015, 3, 10), "Marketing", "John White", "London, Oxford St 1", new Employee[]
                {
                    new Employee("Lionel Smith", "Manager", "231234567", "lionel@food.com", 32000),
                    new Employee("Diana Ross", "Designer", "501112233", "diana@food.com", 27000)
                }),
                new Firm("Code Lab", new DateTime(2024, 9, 1), "IT", "Anna Black", "Kyiv, Khreshchatyk 5", new Employee[]
                {
                    new Employee("Dima Petrenko", "Developer", "234445566", "dima@codelab.com", 45000),
                    new Employee("Olga Koval", "Manager", "671234567", "olga@codelab.com", 38000),
                    new Employee("Lionel Brown", "Tester", "239876543", "brown@codelab.com", 25000)
                }),
                new Firm("White Media", new DateTime(2020, 7, 1), "Marketing", "Tom Black", "Paris, Rue Lafayette 3", new Employee[]
                {
                    new Employee("Pierre Dubois", "Manager", "331234567", "dinner@media.com", 30000)
                })
            };

            string firmName = "Code Lab";
            decimal minSalary = 30000;

            var firmEmployees = from firm in firms
                                where firm.Name == firmName
                                from employee in firm.Employees
                                select employee;
            Print("All employees of " + firmName + ":", firmEmployees);

            var richEmployees = from firm in firms
                                where firm.Name == firmName
                                from employee in firm.Employees
                                where employee.Salary > minSalary
                                select employee;
            Print("Employees of " + firmName + " with salary more than " + minSalary + ":", richEmployees);

            var managers = from firm in firms
                           from employee in firm.Employees
                           where employee.Position == "Manager"
                           select employee;
            Print("Managers of all firms:", managers);

            var phone23 = from firm in firms
                          from employee in firm.Employees
                          where employee.Phone.StartsWith("23")
                          select employee;
            Print("Phone starts with 23:", phone23);

            var emailDi = from firm in firms
                          from employee in firm.Employees
                          where employee.Email.StartsWith("di")
                          select employee;
            Print("Email starts with di:", emailDi);

            var lionels = from firm in firms
                          from employee in firm.Employees
                          where employee.FullName.StartsWith("Lionel ")
                          select employee;
            Print("Name Lionel:", lionels);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
