string sentence = Console.ReadLine()!;
string vowels = "aeiouAEIOU";
int count = 0;

foreach (char letter in sentence)
{
    foreach (char vowel in vowels)
    {
        if (letter == vowel)
        {
            count++;
        }
    }
}

Console.WriteLine(count);
