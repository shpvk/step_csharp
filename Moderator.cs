class Program
{
    static string[] ReadWords(string text)
    {
        string[] words = new string[0];
        string word = "";
        for (int i = 0; i <= text.Length; i++)
        {
            if (i < text.Length && char.IsLetterOrDigit(text[i]))
            {
                word += text[i];
                continue;
            }
            if (word.Length > 0)
            {
                string[] newWords = new string[words.Length + 1];
                for (int j = 0; j < words.Length; j++)
                {
                    newWords[j] = words[j];
                }
                newWords[words.Length] = word;
                words = newWords;
                word = "";
            }
        }
        return words;
    }

    static bool IsModerated(string word, string[] moderatedWords)
    {
        for (int i = 0; i < moderatedWords.Length; i++)
        {
            if (moderatedWords[i] == word)
            {
                return true;
            }
        }
        return false;
    }

    static void Main()
    {
        try
        {
            Console.Write("Enter text file path: ");
            string textPath = Console.ReadLine();
            Console.Write("Enter moderation words file path: ");
            string wordsPath = Console.ReadLine();
            if (textPath == null || wordsPath == null || !File.Exists(textPath) || !File.Exists(wordsPath))
            {
                throw new Exception("File not found");
            }

            string[] moderatedWords = ReadWords(File.ReadAllText(wordsPath));
            string text = File.ReadAllText(textPath);
            string result = "";
            string word = "";
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
                    if (IsModerated(word, moderatedWords))
                    {
                        for (int j = 0; j < word.Length; j++)
                        {
                            result += "*";
                        }
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
            File.WriteAllText(textPath, result);

            Console.WriteLine("Moderation words: " + moderatedWords.Length);
            Console.WriteLine("Words replaced: " + replacedCount);
            Console.WriteLine("Result:");
            Console.WriteLine(result);
        }
        catch (Exception error)
        {
            Console.WriteLine(error.Message);
        }
    }
}
