int[] first = { 1, 2, 3, 4, 4, 5 };
int[] second = { 4, 5, 6, 4, 1 };
int[] common = new int[first.Length];
int count = 0;

foreach (int number in first)
{
    bool inSecond = false;
    bool inCommon = false;

    foreach (int other in second)
    {
        if (other == number)
        {
            inSecond = true;
        }
    }

    for (int i = 0; i < count; i++)
    {
        if (common[i] == number)
        {
            inCommon = true;
        }
    }

    if (inSecond && !inCommon)
    {
        common[count] = number;
        count++;
    }
}

for (int i = 0; i < count; i++)
{
    Console.Write(common[i] + " ");
}
