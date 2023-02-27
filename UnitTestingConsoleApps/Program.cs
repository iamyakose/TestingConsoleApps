using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnitTestingConsoleApps.Tests")]
internal class Program
{
  public static void Main(string[] args)
  {
    var userName = AskUserForTheirName();
    GreetUser(userName);

  }
  private static void GreetUser(string userName)
  {
    Console.WriteLine($"Hello {userName}");
  }
  private static string AskUserForTheirName()
  {
    string userName = string.Empty;
    while (string.IsNullOrWhiteSpace(userName))
    {
      Console.WriteLine("What is your name?");
      userName = Console.ReadLine();
    }

    return userName;
  }
}