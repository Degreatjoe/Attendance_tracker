using System;
using System.Collections.Generic;

namespace AT.Models
{
    public class Student
    {
        public int _id { get; set; }
        public string _surname { get; set; }
        private string _names;
        private bool _isMember;
        private string _ward;
        private DateTime _dob;
        public int Age => CalculateAge(_dob);


        public Student(int id, string surname, string name, bool member, DateTime dob, string ward)
        {
            // a constructor
            _id = id;
            _surname = surname;
            _names = name;
            _isMember = member;
            _dob = dob;
            _ward = ward;
        }

        public static Student Create(int id, string surname, string name, bool member, DateTime dob, string ward)
        {
            // creates a new student from the student class
            return new Student(id, surname, name, member, dob, ward);
        }

        public static Student FromDictionary(Dictionary<string, string> studentData)
        {
            // creates a student instance from a dictionary
            int id = int.Parse(studentData["id"]);
            string surname = studentData["surname"];
            string name = studentData["name"];
            bool isMember = bool.Parse(studentData["isMember"]);
            DateTime dob = DateTime.Parse(studentData["dob"]);
            string ward = studentData.ContainsKey("ward") ? studentData["ward"] : "";

            return new Student(id, surname, name, isMember, dob, ward);
        }

        public string GetFullName()
        {
            return $"{_surname}, {_names}";
        }

        public string ShowStudent()
        {
            return $"{_id} {GetFullName()}";
        }

        public Dictionary<string, string> GetStudent()
        {
            // returns a dictionary object of a user
            Dictionary<string, string> studentDict = new Dictionary<string, string>()
            {
                { "id", _id.ToString() },
                { "surname", _surname },
                { "name", _names },
                { "isMember", _isMember.ToString() },
                { "dob", _dob.ToString("dd-MM-yyyy") }, // format date as needed
                { "age", Age.ToString() },
                { "ward", _ward }
            };

            return studentDict;
        }

        private int CalculateAge(DateTime dob)
        {
            // automatically calculates the age of the student
            DateTime today = DateTime.Today;
            int age = today.Year - dob.Year;
            if (dob.Date > today.AddYears(-age)) age--;
            return age;
        }
    }

}
