namespace csharp;

class Program
{
 static void Main(string[] args)
 {
  /* Користувач вводить з клавіатури чотири цифри. Не
   обхідно створити число, яке містить ці цифри. Напри
   клад, якщо з клавіатури введено 1, 5, 7, 8 тоді потрібно 
  сформувати число 1578. */
  Console.WriteLine("Введіть чотири цифри:");
  int digit1 = int.Parse(Console.ReadLine()!);
  int digit2 = int.Parse(Console.ReadLine()!);
  int digit3 = int.Parse(Console.ReadLine()!);
  int digit4 = int.Parse(Console.ReadLine()!);
  int number = digit1 * 1000 + digit2 * 100 + digit3 * 10 + digit4;
  Console.WriteLine(number);

  /* Користувач вводить з клавіатури два числа. Перше 
     число — це значення, друге число — відсоток, який не
     обхідно підрахувати. Наприклад, ми ввели з клавіатури 
     90 і 10. Потрібно вивести на екран 10 відсотків від 90. 
     Результат: 9 */
  Console.WriteLine("Введіть число і відсоток:");
  double value = double.Parse(Console.ReadLine()!);
  double percent = double.Parse(Console.ReadLine()!);
  double result = value * percent / 100;
  Console.WriteLine(result);
 }
}