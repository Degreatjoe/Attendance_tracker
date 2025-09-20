using System;
using System.Collections.Generic;
using System.Dynamic;

namespace AT.Models
{
    public class AttendanceRecord
    {
        public DateTime _date { get; set; }
        public int _studentId { get; set; }
        public string _status { get; set; }

        public AttendanceRecord(int id, string stat)
        {
            _studentId = id;
            _status = stat;
        }

        public AttendanceRecord(DateTime date, int id, string stat)
        {
            _date = date;
            _studentId = id;
            _status = stat;
        }

        public Dictionary<string, string> GetAttendance()
        {
            Dictionary<string, string> register = new Dictionary<string, string>()
            {
                { "date", _date.ToString("dd-MM-yyyy") },
                { "studentId", _studentId.ToString() },
                { "status", _status}
            };
            return register;
        }
    }
}