﻿#nullable disable
using System;
using System.Collections.Generic;

namespace WM_Attendance_System.Models
{
    public partial class User
    {
        public User()
        {
            //Attendances = new HashSet<Attendance>();
            HolidayCalendarEvents = new HashSet<HolidayCalendarEvent>();
            LeaveCalendarEvents = new HashSet<LeaveCalendarEvent>();
            ApproveLeaveDetails = new HashSet<LeaveDetail>();
            SenderLeaveDetails = new HashSet<LeaveDetail>();
            Leaves = new HashSet<Leave>();
            NotificationReceivers = new HashSet<Notification>();
            NotificationSenders = new HashSet<Notification>();
            Reports = new HashSet<Report>();
            Settings = new HashSet<Setting>();
            Sms = new HashSet<Sms>();
            CalendarEvents = new HashSet<HolidayCalendarEvent>();
            SmsNavigation = new HashSet<Sms>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string Nic { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string ProfilePic { get; set; }
        public int? Type { get; set; }
        public float? NoOfAnnualLeaves { get; set; }


        //public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<HolidayCalendarEvent> HolidayCalendarEvents { get; set; }
        public virtual ICollection<LeaveCalendarEvent> LeaveCalendarEvents { get; set; }
        public virtual ICollection<LeaveDetail> ApproveLeaveDetails { get; set; }
        public virtual ICollection<LeaveDetail> SenderLeaveDetails { get; set; }
        public virtual ICollection<Leave> Leaves { get; set; }
        public virtual ICollection<Notification> NotificationReceivers { get; set; }
        public virtual ICollection<Notification> NotificationSenders { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<Setting> Settings { get; set; }
        public virtual ICollection<Sms> Sms { get; set; }
        public virtual ICollection<HolidayCalendarEvent> CalendarEvents { get; set; }
        public virtual ICollection<Sms> SmsNavigation { get; set; }
    }
}