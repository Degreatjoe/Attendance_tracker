using AT.Models;

namespace AT.Services
{
    public static class AttendanceService
    {
        public static void TakeAttendance(Classroom myClass)
        {
            var prompts = new List<string>
            {
                "Enter student ID:",
                "Enter status (Present/Absent):",
                "Enter date (yyyy-MM-dd):"
            };

            var res = Utility.UserInputString(prompts.Count, prompts);

            if (!int.TryParse(res[0], out int studentId))
            {
                Console.WriteLine("❌ Invalid student ID.");
                return;
            }

            string status = res[1];
            if (!DateTime.TryParse(res[2], out DateTime date))
            {
                Console.WriteLine("❌ Invalid date format. Defaulting to today.");
                myClass.MarkAttendance(studentId, status);
            }
            else
            {
                myClass.MarkAttendance(studentId, date, status);
            }

            Console.WriteLine("✅ Attendance recorded.");
        }

        public static void ManageAttendance(Classroom myClass)
        {
            while (true)
            {
                Console.WriteLine("\n--- Attendance Management ---");
                Console.WriteLine("1. View Attendance by Date");
                Console.WriteLine("2. View Attendance for a Student");
                Console.WriteLine("3. Go Back");

                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("Enter date (yyyy-MM-dd):");
                        string? dateInput = Console.ReadLine();
                        if (DateTime.TryParse(dateInput, out DateTime date))
                        {
                            var list = myClass.GetAttendanceByDate(date);
                            if (list.Count == 0)
                                Console.WriteLine("No records found.");
                            else
                                list.ForEach(Console.WriteLine);
                        }
                        else
                        {
                            Console.WriteLine("❌ Invalid date.");
                        }
                        break;

                    case "2":
                        Console.WriteLine("Enter Student ID:");
                        string? idInput = Console.ReadLine();
                        if (int.TryParse(idInput, out int id))
                        {
                            var records = myClass.GetAttendanceForStudent(id);
                            if (records.Count == 0)
                                Console.WriteLine("No records found.");
                            else
                                records.ForEach(Console.WriteLine);
                        }
                        else
                        {
                            Console.WriteLine("❌ Invalid ID.");
                        }
                        break;

                    case "3":
                        return;

                    default:
                        Console.WriteLine("❌ Invalid choice.");
                        break;
                }
            }
        }
    }
}
