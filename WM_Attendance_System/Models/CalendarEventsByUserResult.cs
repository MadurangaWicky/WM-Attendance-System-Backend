using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WM_Attendance_System.Models
{
    public partial class CalendarEventsByUserResult
    {
        public string eventName { get; set; }
        public DateTime? date { get; set; }
        public float duration { get; set; }
        public string comment { get; set; }
        public string type { get; set; }
    }
}
