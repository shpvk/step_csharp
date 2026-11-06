static class StringExtensions
{
    public static bool HasValidBrackets(this string text)
    {
        if (text == null)
        {
            throw new Exception("Text cannot be empty");
        }
        char[] openBrackets = new char[text.Length];
        int openCount = 0;
        for (int i = 0; i < text.Length; i++)
        {
            char symbol = text[i];
            if (symbol == '(' || symbol == '{' || symbol == '[')
            {
                openBrackets[openCount] = symbol;
                openCount++;
            }
            else if (symbol == ')' || symbol == '}' || symbol == ']')
            {
                if (openCount == 0)
                {
                    return false;
                }
                char lastOpen = openBrackets[openCount - 1];
                if ((symbol == ')' && lastOpen != '(') || (symbol == '}' && lastOpen != '{') || (symbol == ']' && lastOpen != '['))
                {
                    return false;
                }
                openCount--;
            }
        }
        return openCount == 0;
    }
}

class Program
{
    static void Main()
    {
        string[] tests = { "{}[]", "(())", "[{}]", "[}", "[[{]}]", "(", ")(", "a(b[c]{d})e" };
        foreach (string test in tests)
        {
            if (test.HasValidBrackets())
            {
                Console.WriteLine(test + " - valid");
            }
            else
            {
                Console.WriteLine(test + " - invalid");
            }
        }
    }
}
