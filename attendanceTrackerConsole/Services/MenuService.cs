using AT.Models;

namespace AT.Services
{
    public static class MenuService
    {
        public static int GetMainMenuChoice()
        {
            List<string> menuList = new List<string>
            {
                "Take Attendance.",
                "Manage Student.",
                "Advanced Attendance.",
                "Send Attendance Report",
                "Quit"
            };

            Console.WriteLine("\n--- Main Menu ---");
            for (int i = 0; i < menuList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {menuList[i]}");
            }

            string? input = Console.ReadLine();
            if (int.TryParse(input, out int choice) && choice >= 1 && choice <= menuList.Count)
                return choice;

            Console.WriteLine("❌ Invalid input. Try again.");
            return GetMainMenuChoice();
        }

        public static void ExecuteMainMenuChoice(int choice, Classroom myClass)
        {
            switch (choice)
            {
                case 1:
                    AttendanceService.TakeAttendance(myClass);
                    break;

                case 2:
                    StudentService.ManageStudents(myClass);
                    break;

                case 3:
                    AttendanceService.ManageAttendance(myClass);
                    break;

                case 4:
                    var report = myClass.GenerateClassReport();
                    Console.WriteLine("\n--- Class Report ---");
                    foreach (var line in report)
                        Console.WriteLine(line);
                    break;

                case 5:
                    Console.WriteLine("Goodbye!");
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
