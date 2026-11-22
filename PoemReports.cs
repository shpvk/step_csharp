class Poem
{
    public string Title;
    public string Author;
    public int Year;
    public string Text;
    public string Theme;

    public Poem(string title, string author, int year, string text, string theme)
    {
        if (title == null || title == "")
        {
            throw new Exception("Title is empty");
        }
        if (author == null || author == "")
        {
            throw new Exception("Author is empty");
        }
        if (year < 0 || year > DateTime.Now.Year)
        {
            throw new Exception("Wrong year");
        }
        if (text == null || text == "")
        {
            throw new Exception("Text is empty");
        }
        if (theme == null || theme == "")
        {
            throw new Exception("Theme is empty");
        }
        Title = title;
        Author = author;
        Year = year;
        Text = text;
        Theme = theme;
    }

    public override string ToString()
    {
        return Title + " | " + Author + " | " + Year + " | " + Theme + " | " + Text;
    }
}

class Program
{
    static Poem[] poems = new Poem[0];
    static string fileName = "poems.txt";

    static void Main()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("1 - Add poem");
            Console.WriteLine("2 - Remove poem");
            Console.WriteLine("3 - Edit poem");
            Console.WriteLine("4 - Show all poems");
            Console.WriteLine("5 - Save to file");
            Console.WriteLine("6 - Load from file");
            Console.WriteLine("7 - Report by title");
            Console.WriteLine("8 - Report by author");
            Console.WriteLine("9 - Report by theme");
            Console.WriteLine("0 - Exit");
            Console.Write("Choice: ");
            try
            {
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 0:
                        return;
                    case 1:
                        AddPoem();
                        break;
                    case 2:
                        RemovePoem();
                        break;
                    case 3:
                        EditPoem();
                        break;
                    case 4:
                        ShowPoems();
                        break;
                    case 5:
                        SaveToFile();
                        break;
                    case 6:
                        LoadFromFile();
                        break;
                    case 7:
                        ReportByTitle();
                        break;
                    case 8:
                        ReportByAuthor();
                        break;
                    case 9:
                        ReportByTheme();
                        break;
                    default:
                        throw new Exception("Wrong choice");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }

    static Poem ReadPoem()
    {
        Console.Write("Title: ");
        string title = Console.ReadLine();
        Console.Write("Author: ");
        string author = Console.ReadLine();
        Console.Write("Year: ");
        int year = int.Parse(Console.ReadLine());
        Console.Write("Theme: ");
        string theme = Console.ReadLine();
        Console.Write("Text: ");
        string text = Console.ReadLine();
        return new Poem(title, author, year, text, theme);
    }

    static Poem[] AddToArray(Poem[] array, Poem poem)
    {
        Poem[] result = new Poem[array.Length + 1];
        for (int i = 0; i < array.Length; i++)
        {
            result[i] = array[i];
        }
        result[array.Length] = poem;
        return result;
    }

    static void AddPoem()
    {
        poems = AddToArray(poems, ReadPoem());
        Console.WriteLine("Poem added");
    }

    static int ChoosePoem()
    {
        if (poems.Length == 0)
        {
            throw new Exception("No poems");
        }
        ShowPoems();
        Console.Write("Poem number: ");
        int number = int.Parse(Console.ReadLine());
        if (number < 1 || number > poems.Length)
        {
            throw new Exception("Wrong number");
        }
        return number - 1;
    }

    static void RemovePoem()
    {
        int index = ChoosePoem();
        Poem[] result = new Poem[poems.Length - 1];
        int j = 0;
        for (int i = 0; i < poems.Length; i++)
        {
            if (i != index)
            {
                result[j] = poems[i];
                j++;
            }
        }
        poems = result;
        Console.WriteLine("Poem removed");
    }

    static void EditPoem()
    {
        int index = ChoosePoem();
        Console.WriteLine("Enter new data");
        poems[index] = ReadPoem();
        Console.WriteLine("Poem changed");
    }

    static void ShowPoems()
    {
        if (poems.Length == 0)
        {
            Console.WriteLine("No poems");
        }
        for (int i = 0; i < poems.Length; i++)
        {
            Console.WriteLine((i + 1) + ". " + poems[i]);
        }
    }

    static void SaveToFile()
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (Poem poem in poems)
            {
                writer.WriteLine(poem.Title);
                writer.WriteLine(poem.Author);
                writer.WriteLine(poem.Year);
                writer.WriteLine(poem.Theme);
                writer.WriteLine(poem.Text);
            }
        }
        Console.WriteLine("Saved to " + fileName);
    }

    static void LoadFromFile()
    {
        if (!File.Exists(fileName))
        {
            throw new Exception("File not found");
        }
        Poem[] loaded = new Poem[0];
        using (StreamReader reader = new StreamReader(fileName))
        {
            string title;
            while ((title = reader.ReadLine()) != null)
            {
                string author = reader.ReadLine();
                int year = int.Parse(reader.ReadLine());
                string theme = reader.ReadLine();
                string text = reader.ReadLine();
                loaded = AddToArray(loaded, new Poem(title, author, year, text, theme));
            }
        }
        poems = loaded;
        Console.WriteLine("Loaded " + poems.Length + " poems");
    }

    static void OutputReport(string header, Poem[] found)
    {
        Console.WriteLine("1 - Show on screen");
        Console.WriteLine("2 - Save to file");
        Console.Write("Choice: ");
        int choice = int.Parse(Console.ReadLine());
        if (choice == 1)
        {
            Console.WriteLine(header);
            foreach (Poem poem in found)
            {
                Console.WriteLine(poem);
            }
            Console.WriteLine("Found: " + found.Length);
        }
        else if (choice == 2)
        {
            Console.Write("Report file name: ");
            string reportFile = Console.ReadLine();
            using (StreamWriter writer = new StreamWriter(reportFile))
            {
                writer.WriteLine(header);
                foreach (Poem poem in found)
                {
                    writer.WriteLine(poem);
                }
                writer.WriteLine("Found: " + found.Length);
            }
            Console.WriteLine("Report saved to " + reportFile);
        }
        else
        {
            throw new Exception("Wrong choice");
        }
    }

    static void ReportByTitle()
    {
        Console.Write("Title: ");
        string title = Console.ReadLine();
        Poem[] found = new Poem[0];
        foreach (Poem poem in poems)
        {
            if (poem.Title == title)
            {
                found = AddToArray(found, poem);
            }
        }
        OutputReport("Report by title: " + title, found);
    }

    static void ReportByAuthor()
    {
        Console.Write("Author: ");
        string author = Console.ReadLine();
        Poem[] found = new Poem[0];
        foreach (Poem poem in poems)
        {
            if (poem.Author == author)
            {
                found = AddToArray(found, poem);
            }
        }
        OutputReport("Report by author: " + author, found);
    }

    static void ReportByTheme()
    {
        Console.Write("Theme: ");
        string theme = Console.ReadLine();
        Poem[] found = new Poem[0];
        foreach (Poem poem in poems)
        {
            if (poem.Theme == theme)
            {
                found = AddToArray(found, poem);
            }
        }
        OutputReport("Report by theme: " + theme, found);
    }
}
