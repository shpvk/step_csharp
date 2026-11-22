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

            var allFirms = firms.Select(firm => firm);
            Print("All firms:", allFirms);

            var foodFirms = firms.Where(firm => firm.Name.Contains("Food"));
            Print("Name contains Food:", foodFirms);

            var marketingFirms = firms.Where(firm => firm.Profile == "Marketing");
            Print("Marketing:", marketingFirms);

            var marketingOrItFirms = firms.Where(firm => firm.Profile == "Marketing" || firm.Profile == "IT");
            Print("Marketing or IT:", marketingOrItFirms);

            var bigFirms = firms.Where(firm => firm.EmployeesCount > 100);
            Print("More than 100 employees:", bigFirms);

            var middleFirms = firms.Where(firm => firm.EmployeesCount >= 100 && firm.EmployeesCount <= 300);
            Print("From 100 to 300 employees:", middleFirms);

            var londonFirms = firms.Where(firm => firm.Address.Contains("London"));
            Print("In London:", londonFirms);

            var whiteDirectorFirms = firms.Where(firm => firm.Director.EndsWith(" White"));
            Print("Director surname White:", whiteDirectorFirms);

            var oldFirms = firms.Where(firm => firm.FoundedDate < DateTime.Today.AddYears(-2));
            Print("Founded more than 2 years ago:", oldFirms);

            var days123Firms = firms.Where(firm => (DateTime.Today - firm.FoundedDate).Days == 123);
            Print("Founded 123 days ago:", days123Firms);

            var blackWhiteFirms = firms.Where(firm => firm.Director.EndsWith(" Black") && firm.Name.Contains("White"));
            Print("Director surname Black and name contains White:", blackWhiteFirms);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
