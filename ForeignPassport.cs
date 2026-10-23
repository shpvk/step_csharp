internal class Program
{
    static void Main()
    {
        try
        {
            ForeignPassport defaultPassport = new ForeignPassport();
            defaultPassport.DisplayInfo();

            ForeignPassport passport = new ForeignPassport("BN123456", "Shevchenko Taras Hryhorovych", new DateTime(1995, 3, 9), new DateTime(2022, 5, 1), new DateTime(2032, 5, 1), "Migration Service 8017", "UKR");
            passport.DisplayInfo();

            Console.WriteLine("Passports created: " + ForeignPassport.CreatedCount);
            Console.WriteLine();

            TryCreate("BN123456", "", new DateTime(1995, 3, 9), new DateTime(2022, 5, 1), new DateTime(2032, 5, 1), "UKR");
            TryCreate("BN123456", null!, new DateTime(1995, 3, 9), new DateTime(2022, 5, 1), new DateTime(2032, 5, 1), "UKR");
            TryCreate("BN234567", "Kovalenko Olena", new DateTime(1995, 3, 9), new DateTime(2027, 5, 1), new DateTime(2032, 5, 1), "UKR");
            TryCreate("BN123456", "Kovalenko Olena", new DateTime(2023, 3, 9), new DateTime(2027, 5, 1), new DateTime(2032, 5, 1), "UKR");
            TryCreate("BN123456", "Kovalenko Olena", new DateTime(1995, 3, 9), new DateTime(2027, 5, 1), new DateTime(2021, 5, 1), "UKR");
            TryCreate("BN123456", "Kovalenko Olena", new DateTime(1995, 3, 9), new DateTime(2027, 5, 1), new DateTime(2035, 5, 1), "UKR");
            TryCreate("BN123456", "Kovalenko Olena", new DateTime(1995, 3, 9), new DateTime(2027, 5, 1), new DateTime(2032, 5, 1), "XYZ");

            Console.WriteLine();
            Console.WriteLine("Passports created: " + ForeignPassport.CreatedCount);
        }
        catch (Exception e)
        {
            Console.WriteLine("Unexpected error: " + e.Message);
        }
    }

    static void TryCreate(string number, string fullName, DateTime birthDate, DateTime issueDate, DateTime expiryDate, string countryCode)
    {
        try
        {
            ForeignPassport passport = new ForeignPassport(number, fullName, birthDate, issueDate, expiryDate, "Migration Service 8017", countryCode);
            passport.DisplayInfo();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.GetType().Name + ": " + e.Message);
        }
    }
}

internal class ForeignPassport
{
    public const int MaxValidityYears = 10;
    public const int NumberLength = 8;

    private static readonly string[] allowedCountryCodes;

    public static int CreatedCount { get; private set; }

    public readonly Guid Id = Guid.NewGuid();

    public string Number { get; }
    public string FullName { get; }
    public DateTime BirthDate { get; }
    public DateTime IssueDate { get; }
    public DateTime ExpiryDate { get; }
    public string Authority { get; }
    public string CountryCode { get; }

    static ForeignPassport()
    {
        allowedCountryCodes = new string[] { "UKR", "POL", "DEU", "FRA", "USA", "GBR" };
        foreach (string code in allowedCountryCodes)
        {
            if (code == null || code.Length != 3 || !IsUpperLetters(code, 0, 3))
            {
                throw new Exception("Invalid country code in the list: " + code);
            }
        }
        CreatedCount = 0;
    }

    public ForeignPassport()
        : this("AA000000", "Unknown", new DateTime(2000, 1, 1), DateTime.Today, DateTime.Today.AddYears(MaxValidityYears), "Unknown", "UKR")
    {
    }

    public ForeignPassport(string number, string fullName, DateTime birthDate, DateTime issueDate, DateTime expiryDate, string authority, string countryCode)
    {
        if (number == null)
        {
            throw new ArgumentNullException(nameof(number), "Passport number is required.");
        }
        if (number.Length != NumberLength || !IsUpperLetters(number, 0, 2) || !IsDigits(number, 2, NumberLength))
        {
            throw new ArgumentException("Passport number must be 2 uppercase letters and 6 digits, e.g. FX123456.");
        }
        if (fullName == null)
        {
            throw new ArgumentNullException(nameof(fullName), "Full name is required.");
        }
        if (!IsValidName(fullName))
        {
            throw new ArgumentException("Full name cannot be empty and must contain only letters, spaces and hyphens.");
        }
        if (birthDate > issueDate)
        {
            throw new ArgumentException("Birth date cannot be later than issue date.");
        }
        if (expiryDate <= issueDate)
        {
            throw new ArgumentException("Expiry date must be later than issue date.");
        }
        if (expiryDate > issueDate.AddYears(MaxValidityYears))
        {
            throw new ArgumentOutOfRangeException(nameof(expiryDate), "Validity cannot exceed " + MaxValidityYears + " years.");
        }
        if (authority == null)
        {
            throw new ArgumentNullException(nameof(authority), "Issuing authority is required.");
        }
        if (authority.Length == 0)
        {
            throw new ArgumentException("Issuing authority cannot be empty.");
        }
        if (!IsAllowedCountry(countryCode))
        {
            throw new ArgumentException("Country code is not allowed: " + countryCode);
        }

        Number = number;
        FullName = fullName;
        BirthDate = birthDate;
        IssueDate = issueDate;
        ExpiryDate = expiryDate;
        Authority = authority;
        CountryCode = countryCode;
        CreatedCount++;
    }

    public void DisplayInfo()
    {
        Console.WriteLine("Passport " + Number + " (" + CountryCode + "), id " + Id);
        Console.WriteLine("  Owner: " + FullName + ", born " + BirthDate.ToShortDateString());
        Console.WriteLine("  Issued " + IssueDate.ToShortDateString() + " by " + Authority + ", valid until " + ExpiryDate.ToShortDateString());
    }

    private static bool IsUpperLetters(string text, int from, int to)
    {
        for (int i = from; i < to; i++)
        {
            if (text[i] < 'A' || text[i] > 'Z')
            {
                return false;
            }
        }
        return true;
    }

    private static bool IsDigits(string text, int from, int to)
    {
        for (int i = from; i < to; i++)
        {
            if (text[i] < '0' || text[i] > '9')
            {
                return false;
            }
        }
        return true;
    }

    private static bool IsValidName(string name)
    {
        int letters = 0;
        foreach (char symbol in name)
        {
            if (char.IsLetter(symbol))
            {
                letters++;
            }
            else if (symbol != ' ' && symbol != '-')
            {
                return false;
            }
        }
        return letters > 0;
    }

    private static bool IsAllowedCountry(string code)
    {
        foreach (string allowed in allowedCountryCodes)
        {
            if (allowed == code)
            {
                return true;
            }
        }
        return false;
    }
}

