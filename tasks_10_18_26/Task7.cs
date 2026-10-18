string sentence = Console.ReadLine()!;
string result = "";
string word = "";

foreach (char letter in sentence)
{
    if (letter == ' ')
    {
        result += word + ' ';
        word = "";
    }
    else
    {
        word = letter + word;
    }
}

result += word;
Console.WriteLine(result);
