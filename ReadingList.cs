ReadingList list = new ReadingList();
list = list + "1984";
list = list + "Dune";
list = list + "The Hobbit";
list.Print();

Console.WriteLine("Has Dune: " + list.Has("Dune"));
Console.WriteLine("First book: " + list[0]);

list[0] = "Animal Farm";
list = list - "Dune";
list.Print();

Console.WriteLine("Has Dune: " + list.Has("Dune"));
Console.WriteLine("Count: " + list.Length);

try
{
    list = list - "Missing";
}
catch (Exception error)
{
    Console.WriteLine(error.Message);
}

class ReadingList
{
    private string[] books = new string[0];

    public int Length
    {
        get
        {
            return books.Length;
        }
    }

    public string this[int index]
    {
        get
        {
            CheckIndex(index);
            return books[index];
        }
        set
        {
            CheckIndex(index);
            if (value == null)
            {
                throw new Exception("Book title is empty");
            }
            books[index] = value;
        }
    }

    private void CheckIndex(int index)
    {
        if (index < 0 || index >= books.Length)
        {
            throw new Exception("Index out of range");
        }
    }

    public int IndexOf(string title)
    {
        for (int i = 0; i < books.Length; i++)
        {
            if (books[i] == title)
            {
                return i;
            }
        }
        return -1;
    }

    public bool Has(string title)
    {
        return IndexOf(title) != -1;
    }

    public void Add(string title)
    {
        if (title == null)
        {
            throw new Exception("Book title is empty");
        }
        if (Has(title))
        {
            throw new Exception("Book is already in the list");
        }
        string[] newBooks = new string[books.Length + 1];
        for (int i = 0; i < books.Length; i++)
        {
            newBooks[i] = books[i];
        }
        newBooks[books.Length] = title;
        books = newBooks;
    }

    public void Remove(string title)
    {
        int index = IndexOf(title);
        if (index == -1)
        {
            throw new Exception("Book not found");
        }
        string[] newBooks = new string[books.Length - 1];
        int position = 0;
        for (int i = 0; i < books.Length; i++)
        {
            if (i != index)
            {
                newBooks[position] = books[i];
                position++;
            }
        }
        books = newBooks;
    }

    public void Print()
    {
        Console.WriteLine("Reading list:");
        for (int i = 0; i < books.Length; i++)
        {
            Console.WriteLine((i + 1) + ". " + books[i]);
        }
    }

    public static ReadingList operator +(ReadingList list, string title)
    {
        list.Add(title);
        return list;
    }

    public static ReadingList operator -(ReadingList list, string title)
    {
        list.Remove(title);
        return list;
    }
}
