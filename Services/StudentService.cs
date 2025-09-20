using AT.Models;

namespace AT.Services
{
    public static class StudentService
    {
        public static void ManageStudents(Classroom myClass)
        {
            while (true)
            {
                Console.WriteLine("\n--- Student Management ---");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. View All Students");
                Console.WriteLine("3. Search by Surname");
                Console.WriteLine("4. Go Back");

                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddStudent(myClass);
                        break;

                    case "2":
                        var students = myClass.GetStudentList();
                        students.ForEach(Console.WriteLine);
                        break;

                    case "3":
                        Console.WriteLine("Enter surname:");
                        string? surname = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(surname))
                        {
                            Console.WriteLine("❌ Surname cannot be empty.");
                            break;
                        }
                        var matches = myClass.GetStudentBySurname(surname);
                        if (matches.Count == 0)
                            Console.WriteLine("No match found.");
                        else
                            matches.ForEach(s => Console.WriteLine(s.ShowStudent()));
                        break;

                    case "4":
                        return;

                    default:
                        Console.WriteLine("❌ Invalid choice.");
                        break;
                }
            }
        }

        private static void AddStudent(Classroom myClass)
        {
            var prompts = new List<string>
            {
                "Enter ID:",
                "Enter Surname:",
                "Enter Other Name(s):",
                "Is the student a member? (true/false):",
                "Enter Date of Birth (yyyy-MM-dd):",
                "Enter Ward:"
            };

            var responses = Utility.UserInputString(prompts.Count, prompts);

            try
            {
                int id = int.Parse(responses[0]);
                string surname = responses[1];
                string name = responses[2];
                bool isMember = bool.Parse(responses[3]);
                DateTime dob = DateTime.Parse(responses[4]);
                string ward = responses[5];

                Student student = new Student(id, surname, name, isMember, dob, ward);
                myClass.AddStudent(student);

                Console.WriteLine("✅ Student added.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ {ex.Message}");
            }
        }
    }
}
