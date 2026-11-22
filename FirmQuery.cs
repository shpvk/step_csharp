class Firm
{
    public string Name;
    public DateTime FoundedDate;
    public string Profile;
    public string Director;
    public int EmployeesCount;
    public string Address;

    public Firm(string name, DateTime foundedDate, string profile, string director, int employeesCount, string address)
    {
        if (name == null || profile == null || director == null || address == null)
        {
            throw new Exception("Empty firm data");
        }
        if (employeesCount < 0)
        {
            throw new Exception("Wrong employees count");
        }
        Name = name;
        FoundedDate = foundedDate;
        Profile = profile;
        Director = director;
        EmployeesCount = employeesCount;
        Address = address;
    }

    public override string ToString()
    {
        return Name + " | " + FoundedDate.ToShortDateString() + " | " + Profile + " | " + Director + " | " + EmployeesCount + " | " + Address;
    }
}

class Program
{
    static void Print(string title, IEnumerable<Firm> firms)
    {
        Console.WriteLine(title);
        foreach (Firm firm in firms)
        {
            Console.WriteLine("  " + firm);
        }
        Console.WriteLine();
    }

    static void Main()
    {
        try
        {
            Firm[] firms =
            {
                new Firm("Fresh Food", new DateTime(2015, 3, 10), "Marketing", "John White", 250, "London, Oxford St 1"),
                new Firm("Code Lab", DateTime.Today.AddDays(-123), "IT", "Anna Black", 80, "Kyiv, Khreshchatyk 5"),
                new Firm("White Media", new DateTime(2020, 7, 1), "Marketing", "Tom Black", 120, "Paris, Rue Lafayette 3"),
                new Firm("Food Market", new DateTime(2023, 1, 15), "Trade", "Lisa Brown", 400, "London, King St 7"),
                new Firm("Soft Group", new DateTime(2010, 5, 20), "IT", "Mike White", 150, "Berlin, Hauptstrasse 2"),
                new Firm("Ad Pro", new DateTime(2025, 6, 1), "Marketing", "Kate Green", 40, "London, Queen St 9")
            };

            var allFirms = from firm in firms
                           select firm;
            Print("All firms:", allFirms);

            var foodFirms = from firm in firms
                            where firm.Name.Contains("Food")
                            select firm;
            Print("Name contains Food:", foodFirms);

            var marketingFirms = from firm in firms
                                 where firm.Profile == "Marketing"
                                 select firm;
            Print("Marketing:", marketingFirms);

            var marketingOrItFirms = from firm in firms
                                     where firm.Profile == "Marketing" || firm.Profile == "IT"
                                     select firm;
            Print("Marketing or IT:", marketingOrItFirms);

            var bigFirms = from firm in firms
                           where firm.EmployeesCount > 100
                           select firm;
            Print("More than 100 employees:", bigFirms);

            var middleFirms = from firm in firms
                              where firm.EmployeesCount >= 100 && firm.EmployeesCount <= 300
                              select firm;
            Print("From 100 to 300 employees:", middleFirms);

            var londonFirms = from firm in firms
                              where firm.Address.Contains("London")
                              select firm;
            Print("In London:", londonFirms);

            var whiteDirectorFirms = from firm in firms
                                     where firm.Director.EndsWith(" White")
                                     select firm;
            Print("Director surname White:", whiteDirectorFirms);

            var oldFirms = from firm in firms
                           where firm.FoundedDate < DateTime.Today.AddYears(-2)
                           select firm;
            Print("Founded more than 2 years ago:", oldFirms);

            var days123Firms = from firm in firms
                               where (DateTime.Today - firm.FoundedDate).Days == 123
                               select firm;
            Print("Founded 123 days ago:", days123Firms);

            var blackWhiteFirms = from firm in firms
                                  where firm.Director.EndsWith(" Black") && firm.Name.Contains("White")
                                  select firm;
            Print("Director surname Black and name contains White:", blackWhiteFirms);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
