using System;
using System.Net;

namespace AT.Services
{
    public class Utility
    {
        public static List<string> UserInputString(int numberOfInput, List<string> args)
        {
            Console.WriteLine("provide the following information");
            if (numberOfInput != args.Count)
            {
                throw new Exception($"The number {numberOfInput} does not match the list count {args.Count}.");
            }

            List<string> inputList = new List<string>();

            for (int i = 0; i < numberOfInput; i++)
            {
                Console.WriteLine(args[i]);
                string? input = Console.ReadLine();
                inputList.Add(input ?? string.Empty);
            }

            return inputList;
        }


        public static List<int> UserInputInt(int numberOfInput, string[] args)
        {
            if (numberOfInput != args.Length)
            {
                throw new Exception($"The number {numberOfInput} does not match the number of prompts {args.Length}.");
            }

            List<int> inputList = new List<int>();

            for (int i = 0; i < numberOfInput; i++)
            {
                Console.WriteLine(args[i]);

                int input;
                while (!int.TryParse(Console.ReadLine(), out input))
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer:");
                }

                inputList.Add(input);
            }

            return inputList;
        }

    }
}