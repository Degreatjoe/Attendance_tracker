using System;
using AT.Models;
using AT.Services;
public class Program
{
    public static void Main(string[] args)
    {
        var myClass = new Classroom("Seminary", "Great Joseph", "Sokponba Stake centre");
        Console.WriteLine("Welcome!!");

        while (true)
        {
            int choice = MenuService.GetMainMenuChoice();
            MenuService.ExecuteMainMenuChoice(choice, myClass);
        }
    }
}
