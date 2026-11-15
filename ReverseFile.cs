class Program
{
    static void Main()
    {
        try
        {
            Console.Write("Enter file path: ");
            string path = Console.ReadLine();
            if (path == null || !File.Exists(path))
            {
                throw new Exception("File not found");
            }

            string text = File.ReadAllText(path);
            char[] reversed = new char[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                reversed[i] = text[text.Length - 1 - i];
            }

            string newPath = Path.Combine(Path.GetDirectoryName(path), "reversed_" + Path.GetFileName(path));
            File.WriteAllText(newPath, new string(reversed));
            Console.WriteLine("Reversed file created: " + newPath);
        }
        catch (Exception error)
        {
            Console.WriteLine(error.Message);
        }
    }
}
