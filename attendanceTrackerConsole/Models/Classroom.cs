using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using Microsoft.VisualBasic;

namespace AT.Models
{
    public class Classroom
    {
        private List<Student> _students = new List<Student>();
        private List<AttendanceRecord> _attendances = new List<AttendanceRecord>();
        private string _className;
        private string _teacherName;
        private string _location;

        public Classroom(string className, string teacherName, string location)
        {
            _className = className;
            _teacherName = teacherName;
            _location = location;
        }

        public void AddStudent(Student student)
        {
            if (_students.Any(s => s._id == student._id))
            {
                throw new ArgumentException($"Student with ID {student._id} already exists.");
            }
            _students.Add(student);
        }

        public Student GetStudentById(int id)
        {
            var student = _students.Find(s => s._id == id);
            if (student == null)
            {
                throw new KeyNotFoundException($"Student with ID {id} not found.");
            }
            return student;
        }

        public List<Student> GetStudentBySurname(string surname)
        {
            if (string.IsNullOrWhiteSpace(surname))
                throw new ArgumentException("Surname cannot be null or empty.", nameof(surname));

            return _students.FindAll(s => s._surname == surname);
        }

        public void MarkAttendance(int id, string stat)
        {
            var attendance = new AttendanceRecord(id, stat);
            _attendances.Add(attendance);
        }

        public void MarkAttendance(int id, DateTime date, string stat)
        {
            var attendance = new AttendanceRecord(date, id, stat);
            _attendances.Add(attendance);
        }

        public List<string> GetStudentList()
        {
            return _students.ConvertAll(student => student.ShowStudent());
        }

        public List<string> GetAttendanceByDate(DateTime date)
        {
            var records = _attendances.FindAll(a => a._date.Date == date.Date);
            return records.ConvertAll(a => $"ID: {a._studentId}, Status: {a._status}");
        }

        public List<string> GetAttendanceForStudent(int studentId)
        {
            var records = _attendances.FindAll(a => a._studentId == studentId);
            return records.ConvertAll(a => $"Date: {a._date:d}, Status: {a._status}");
        }

        public double CalculateAttendancePercentage(int studentId)
        {
            var studentRecords = _attendances.FindAll(a => a._studentId == studentId);

            if (studentRecords.Count == 0)
            {
                throw new InvalidOperationException($"No attendance records found for student ID {studentId}.");
            }

            int presentCount = studentRecords.Count(r =>
                string.Equals(r._status, "Present", StringComparison.OrdinalIgnoreCase));

            double percentage = (double)presentCount / studentRecords.Count * 100;
            return Math.Round(percentage, 2);
        }

        public List<string> GenerateClassReport()
        {
            List<string> report = new List<string>();

            foreach (var student in _students)
            {
                var studentRecords = _attendances.FindAll(a => a._studentId == student._id);
                int totalDays = studentRecords.Count;
                int presentDays = studentRecords.Count(r =>
                    string.Equals(r._status, "Present", StringComparison.OrdinalIgnoreCase));

                double percentage = totalDays > 0 ? (double)presentDays / totalDays * 100 : 0.0;

                string line = $"ID: {student._id} | Name: {student.ShowStudent()} | " +
                            $"Attendance: {Math.Round(percentage, 2)}% ({presentDays}/{totalDays} days)";

                report.Add(line);
                report = report.OrderByDescending(r => percentage).ToList();
            }

            return report;
        }

    }

}