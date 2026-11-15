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
            Console.Write("Enter word to find: ");
            string searchWord = Console.ReadLine();
            Console.Write("Enter replacement word: ");
            string replacementWord = Console.ReadLine();
            if (searchWord == null || searchWord.Length == 0 || replacementWord == null)
            {
                throw new Exception("Words cannot be empty");
            }

            string text = File.ReadAllText(path);
            string result = "";
            string word = "";
            int wordCount = 0;
            int replacedCount = 0;
            for (int i = 0; i <= text.Length; i++)
            {
                if (i < text.Length && char.IsLetterOrDigit(text[i]))
                {
                    word += text[i];
                    continue;
                }
                if (word.Length > 0)
                {
                    wordCount++;
                    if (word == searchWord)
                    {
                        result += replacementWord;
                        replacedCount++;
                    }
                    else
                    {
                        result += word;
                    }
                    word = "";
                }
                if (i < text.Length)
                {
                    result += text[i];
                }
            }
            File.WriteAllText(path, result);

            Console.WriteLine("Words in file: " + wordCount);
            Console.WriteLine("Replaced \"" + searchWord + "\" with \"" + replacementWord + "\": " + replacedCount + " times");
        }
        catch (Exception error)
        {
            Console.WriteLine(error.Message);
        }
    }
}
