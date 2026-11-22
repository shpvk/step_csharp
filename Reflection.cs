using System;
using System.Collections.Generic;
using System.Reflection;

// ================= INTERFACES =================

public interface ITransaction
{
    Guid Id { get; }
    DateTime CreatedAt { get; }
    decimal Amount { get; }

    void Execute();
}

public interface IAccount
{
    string AccountNumber { get; }
    decimal Balance { get; }

    void Deposit(decimal amount);
    void Withdraw(decimal amount);
}

public interface IAuditable
{
    void WriteAuditLog(string message);
}

// ================= ENUMS =================

public enum AccountStatus
{
    Active,
    Blocked,
    Closed
}

public enum TransactionType
{
    Deposit,
    Withdrawal,
    Transfer
}

public enum Currency
{
    UAH,
    USD,
    EUR
}

// ================= ABSTRACT CLASSES =================

public abstract class BankAccount : IAccount, IAuditable
{
    public Guid Id { get; private set; }

    public string AccountNumber { get; protected set; }

    public decimal Balance { get; protected set; }

    public AccountStatus Status { get; private set; }

    public Currency Currency { get; set; }

    protected string OwnerName { get; set; }

    protected BankAccount(string ownerName, Currency currency)
    {
        Id = Guid.NewGuid();
        OwnerName = ownerName;
        Currency = currency;
        AccountNumber = Guid.NewGuid().ToString()[..10];
        Balance = 0;
        Status = AccountStatus.Active;
    }

    public abstract void CalculateMonthlyFee();

    public virtual void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive");

        Balance += amount;
    }

    public virtual void Withdraw(decimal amount)
    {
        if (amount <= 0 || amount > Balance)
            throw new InvalidOperationException("Invalid withdrawal");

        Balance -= amount;
    }

    public void WriteAuditLog(string message)
    {
        Console.WriteLine($"[AUDIT] {message}");
    }

    private void BlockAccount()
    {
        Status = AccountStatus.Blocked;
    }
}

public abstract class Transaction : ITransaction
{
    public Guid Id { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public decimal Amount { get; protected set; }

    public string Description { get; set; }

    protected Transaction(decimal amount, string description)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        Amount = amount;
        Description = description;
    }

    public abstract void Execute();

    protected void PrintTransactionInfo()
    {
        Console.WriteLine($"{Description}: {Amount}");
    }
}

// ================= REGULAR CLASSES =================

public class SavingsAccount : BankAccount
{
    public decimal InterestRate { get; set; }

    public int WithdrawalLimit { get; private set; }

    public SavingsAccount(
        string ownerName,
        Currency currency,
        decimal interestRate)
        : base(ownerName, currency)
    {
        InterestRate = interestRate;
        WithdrawalLimit = 5;
    }

    public override void CalculateMonthlyFee()
    {
        Console.WriteLine("Savings account has no monthly fee");
    }

    public void ApplyInterest()
    {
        Balance += Balance * InterestRate / 100;
    }
}

public class CreditAccount : BankAccount
{
    public decimal CreditLimit { get; private set; }

    public decimal InterestRate { get; set; }

    public CreditAccount(
        string ownerName,
        Currency currency,
        decimal creditLimit)
        : base(ownerName, currency)
    {
        CreditLimit = creditLimit;
    }

    public override void CalculateMonthlyFee()
    {
        Console.WriteLine("Credit account monthly fee calculated");
    }

    public override void Withdraw(decimal amount)
    {
        if (amount <= 0 || Balance - amount < -CreditLimit)
            throw new InvalidOperationException("Credit limit exceeded");

        Balance -= amount;
    }

    public void IncreaseCreditLimit(decimal amount)
    {
        CreditLimit += amount;
    }
}

public class DepositTransaction : Transaction
{
    private IAccount _account;

    public DepositTransaction(IAccount account, decimal amount)
        : base(amount, "Deposit")
    {
        _account = account;
    }

    public override void Execute()
    {
        _account.Deposit(Amount);
        PrintTransactionInfo();
    }
}

public class WithdrawalTransaction : Transaction
{
    private IAccount _account;

    public WithdrawalTransaction(IAccount account, decimal amount)
        : base(amount, "Withdrawal")
    {
        _account = account;
    }

    public override void Execute()
    {
        _account.Withdraw(Amount);
        PrintTransactionInfo();
    }
}

public class BankCustomer
{
    public Guid Id { get; private set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; private set; }

    public DateTime RegistrationDate { get; }

    public List<BankAccount> Accounts { get; private set; }

    public BankCustomer(string firstName, string lastName, string email)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        RegistrationDate = DateTime.Now;
        Accounts = new List<BankAccount>();
    }

    public void AddAccount(BankAccount account)
    {
        Accounts.Add(account);
    }

    private string GetFullName()
    {
        return $"{FirstName} {LastName}";
    }
}

// ================= BANK =================

public class Bank
{
    public string Name { get; set; }

    public List<BankCustomer> Customers { get; private set; }

    public Bank(string name)
    {
        Name = name;
        Customers = new List<BankCustomer>();
    }

    public void AddCustomer(BankCustomer customer)
    {
        Customers.Add(customer);
    }

    public int GetCustomerCount()
    {
        return Customers.Count;
    }

    private void PrintBankStatistics()
    {
        Console.WriteLine($"Total customers: {Customers.Count}");
    }
}

// ================= PROGRAM =================

public class Program
{
    static BindingFlags allMembers = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;

    static string GetTypeName(Type type)
    {
        if (!type.IsGenericType)
        {
            return type.Name;
        }
        string name = type.Name.Substring(0, type.Name.IndexOf('`')) + "<";
        Type[] arguments = type.GetGenericArguments();
        for (int i = 0; i < arguments.Length; i++)
        {
            if (i > 0)
            {
                name += ", ";
            }
            name += GetTypeName(arguments[i]);
        }
        return name + ">";
    }

    static string GetAccess(bool isPublic, bool isFamily, bool isPrivate)
    {
        if (isPublic)
        {
            return "public";
        }
        if (isFamily)
        {
            return "protected";
        }
        if (isPrivate)
        {
            return "private";
        }
        return "internal";
    }

    static string GetMethodAccess(MethodInfo method)
    {
        if (method == null)
        {
            return "none";
        }
        return GetAccess(method.IsPublic, method.IsFamily, method.IsPrivate);
    }

    static string GetParameters(MethodBase method)
    {
        string result = "";
        ParameterInfo[] parameters = method.GetParameters();
        for (int i = 0; i < parameters.Length; i++)
        {
            if (i > 0)
            {
                result += ", ";
            }
            result += GetTypeName(parameters[i].ParameterType) + " " + parameters[i].Name;
        }
        return result;
    }

    static string GetKind(Type type)
    {
        if (type.IsEnum)
        {
            return "enum";
        }
        if (type.IsInterface)
        {
            return "interface";
        }
        if (type.IsAbstract)
        {
            return "abstract class";
        }
        return "class";
    }

    static void ShowTypeInfo(Type type)
    {
        Console.WriteLine();
        Console.WriteLine("========== " + type.Name + " ==========");
        Console.WriteLine("Full name: " + type.FullName);
        Console.WriteLine("Kind: " + GetKind(type));
        Console.WriteLine("Public: " + type.IsPublic);
        Console.WriteLine("Sealed: " + type.IsSealed);
        Console.WriteLine("Base type: " + type.BaseType.Name);

        if (type.IsEnum)
        {
            Console.WriteLine("Underlying type: " + Enum.GetUnderlyingType(type).Name);
            Console.WriteLine("Values:");
            foreach (object value in Enum.GetValues(type))
            {
                Console.WriteLine("  " + value + " = " + (int)value);
            }
            return;
        }

        Console.WriteLine("Interfaces:");
        foreach (Type item in type.GetInterfaces())
        {
            Console.WriteLine("  " + item.Name);
        }

        Console.WriteLine("Constructors:");
        foreach (ConstructorInfo constructor in type.GetConstructors(allMembers))
        {
            string access = GetAccess(constructor.IsPublic, constructor.IsFamily, constructor.IsPrivate);
            Console.WriteLine("  " + access + " " + type.Name + "(" + GetParameters(constructor) + ")");
        }

        Console.WriteLine("Fields:");
        foreach (FieldInfo field in type.GetFields(allMembers))
        {
            if (field.Name.StartsWith("<"))
            {
                continue;
            }
            string access = GetAccess(field.IsPublic, field.IsFamily, field.IsPrivate);
            Console.WriteLine("  " + access + " " + GetTypeName(field.FieldType) + " " + field.Name);
        }

        Console.WriteLine("Properties:");
        foreach (PropertyInfo property in type.GetProperties(allMembers))
        {
            string getAccess = GetMethodAccess(property.GetGetMethod(true));
            string setAccess = GetMethodAccess(property.GetSetMethod(true));
            Console.WriteLine("  " + GetTypeName(property.PropertyType) + " " + property.Name + " { get: " + getAccess + ", set: " + setAccess + " }");
        }

        Console.WriteLine("Methods:");
        foreach (MethodInfo method in type.GetMethods(allMembers))
        {
            if (method.IsSpecialName)
            {
                continue;
            }
            string modifiers = GetMethodAccess(method);
            if (method.IsStatic)
            {
                modifiers += " static";
            }
            if (method.IsAbstract)
            {
                modifiers += " abstract";
            }
            else if (method.GetBaseDefinition() != method)
            {
                modifiers += " override";
            }
            else if (method.IsVirtual && !method.IsFinal)
            {
                modifiers += " virtual";
            }
            Console.WriteLine("  " + modifiers + " " + GetTypeName(method.ReturnType) + " " + method.Name + "(" + GetParameters(method) + ")");
        }
    }

    public static void Main()
    {
        Bank bank = new Bank("Reflection Bank");

        BankCustomer customer = new BankCustomer(
            "Ivan",
            "Petrenko",
            "ivan@gmail.com"
        );

        SavingsAccount savings = new SavingsAccount(
            "Ivan Petrenko",
            Currency.UAH,
            5.5m
        );

        CreditAccount credit = new CreditAccount(
            "Ivan Petrenko",
            Currency.USD,
            10000m
        );

        customer.AddAccount(savings);
        customer.AddAccount(credit);

        bank.AddCustomer(customer);

        savings.Deposit(5000);
        savings.ApplyInterest();

        DepositTransaction transaction =
            new DepositTransaction(savings, 1000);

        transaction.Execute();

        Console.WriteLine($"Balance: {savings.Balance}");
        Console.WriteLine($"Bank: {bank.Name}");

        ShowTypeInfo(typeof(BankCustomer));
        ShowTypeInfo(typeof(DepositTransaction));
        ShowTypeInfo(typeof(Transaction));
        ShowTypeInfo(typeof(TransactionType));
    }
}
