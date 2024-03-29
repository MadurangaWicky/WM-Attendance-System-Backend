﻿#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WM_Attendance_System.Models;

namespace WM_Attendance_System.Data
{
    public partial class Hybrid_Attendance_SystemContext : DbContext
    {
        public Hybrid_Attendance_SystemContext()
        {
        }

        public Hybrid_Attendance_SystemContext(DbContextOptions<Hybrid_Attendance_SystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<HolidayCalendarEvent> HolidayCalendarEvents { get; set; }
        public virtual DbSet<HolidayCalendarEventView> HolidayCalendarEventViews { get; set; }
        public virtual DbSet<Leave> Leaves { get; set; }
        public virtual DbSet<LeaveCalendarEvent> LeaveCalendarEvents { get; set; }
        public virtual DbSet<LeaveCalendarEventView> LeaveCalendarEventsViews { get; set; }
        public virtual DbSet<LeaveDetail> LeaveDetails { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<PendingRequestView> PendingRequests { get; set; }
        public virtual DbSet<PendingShortLeaveRequestsView> PendingShortLeaveRequests { get; set; }
        public virtual DbSet<PendingUser> PendingUsers { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<ShortLeaveRequest> ShortLeaves { get; set; }
        public virtual DbSet<Sms> Smss { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<VideoConference> VideoConferences { get; set; }
        public virtual DbSet<BlackListedEmail> BlackListedEmails { get; set; }
        public virtual DbSet<VideoConferenceHasUser> VideoConferenceHasUsers { get; set; } 
        public virtual DbSet<EmergencyLeaveDetail> EmergencyLeaveDetails { get; set; }
        public virtual DbSet<PendingEmergencyLeave> PendingEmergencyLeaves { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.ToTable("Attendance");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.InTime)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("in_time");

                entity.Property(e => e.OutTime)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("out_time");

                entity.Property(e => e.Type)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("type");

                entity.Property(e => e.UId).HasColumnName("u_id");

                //entity.HasOne(d => d.UIdNavigation)
                  //  .WithMany(p => p.Attendances)
                    //.HasForeignKey(d => d.UId)
                    //.HasConstraintName("FK_Attendance");
            });

            modelBuilder.Entity<HolidayCalendarEvent>(entity =>
            {
                entity.ToTable("HolidayCalendarEvent");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comment)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("comment");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.EditorId).HasColumnName("editor_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Time).HasColumnName("time");

                entity.HasOne(d => d.Editor)
                    .WithMany(p => p.HolidayCalendarEvents)
                    .HasForeignKey(d => d.EditorId)
                    .HasConstraintName("FK_CalendarEvent");

                entity.HasMany(d => d.UIds)
                    .WithMany(p => p.CalendarEvents)
                    .UsingEntity<Dictionary<string, object>>(
                        "HolidayCalendarEventHasUser",
                        l => l.HasOne<User>().WithMany().HasForeignKey("UId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK2_HolidayCalendarEventHasUser"),
                        r => r.HasOne<HolidayCalendarEvent>().WithMany().HasForeignKey("CalendarEventId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK1_HolidayCalendarEventHasUser"),
                        j =>
                        {
                            j.HasKey("CalendarEventId", "UId");

                            j.ToTable("HolidayCalendarEventHasUser");

                            j.IndexerProperty<int>("CalendarEventId").HasColumnName("calendar_event_id");

                            j.IndexerProperty<int>("UId").HasColumnName("u_id");
                        });
            });

            modelBuilder.Entity<HolidayCalendarEventView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("HolidayCalendarEvents");

                entity.Property(e => e.Comment)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("comment");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Time).HasColumnName("time");

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<Leave>(entity =>
            {
                entity.ToTable("Leave");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AdminId).HasColumnName("admin_id");

                entity.Property(e => e.Type)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("type");

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.Leaves)
                    .HasForeignKey(d => d.AdminId)
                    .HasConstraintName("FK_Leave");
            });

            modelBuilder.Entity<LeaveCalendarEvent>(entity =>
            {
                entity.ToTable("LeaveCalendarEvent");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comment)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("comment");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.LeaveId).HasColumnName("leave_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Time).HasColumnName("time");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Leave)
                    .WithMany(p => p.LeaveCalendarEvents)
                    .HasForeignKey(d => d.LeaveId)
                    .HasConstraintName("FK1_LeaveCalendarEvent");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LeaveCalendarEvents)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK2_LeaveCalendarEvent");
            });

            modelBuilder.Entity<LeaveCalendarEventView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("LeaveCalendarEvents");

                entity.Property(e => e.Comment)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("comment");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Time).HasColumnName("time");

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<LeaveDetail>(entity =>
            {
                entity.ToTable("LeaveDetails");

                entity.HasKey(e => e.LeaveId);

                entity.Property(e => e.LeaveId).HasColumnName("leave_id");

                entity.Property(e => e.ApprovalId).HasColumnName("approval_id");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.LeaveTypeId).HasColumnName("leave_type_id");

                entity.Property(e => e.Type)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("type");

                entity.Property(e => e.Duration)
                    .HasColumnName("duration");

                entity.Property(e => e.SpecialNote)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("special_note");

                entity.Property(e => e.SenderId)
                    .HasColumnName("sender_id");

                entity.HasOne(d => d.Approval)
                    .WithMany(p => p.ApproveLeaveDetails)
                    .HasForeignKey(d => d.ApprovalId)
                    .HasConstraintName("FK1_LeaveDetails");

                entity.HasOne(d => d.LeaveType)
                    .WithMany(p => p.LeaveDetails)
                    .HasForeignKey(d => d.LeaveTypeId)
                    .HasConstraintName("FK2_LeaveDetails");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.SenderLeaveDetails)
                    .HasForeignKey(d => d.SenderId)
                    .HasConstraintName("FK3_LeaveDetails");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notification");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Message)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("message");

                entity.Property(e => e.ReceiverId).HasColumnName("receiver_id");

                entity.Property(e => e.SenderId).HasColumnName("sender_id");

                entity.Property(e => e.Time).HasColumnName("time");

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.NotificationReceivers)
                    .HasForeignKey(d => d.ReceiverId)
                    .HasConstraintName("FK2_Notification");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.NotificationSenders)
                    .HasForeignKey(d => d.SenderId)
                    .HasConstraintName("FK1_Notification");
            });

            modelBuilder.Entity<PendingRequestView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("PendingRequests");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Name)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Nic)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("nic");

                entity.Property(e => e.Type)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("type");

                entity.Property(e => e.DurationType)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("duration_type");

                entity.Property(e => e.SpecialNote)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("special_note");

                entity.Property(e => e.Duration)
                    .HasColumnName("duration");

                entity.Property(e => e.ProfilePic)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("profile_pic");

                entity.Property(e => e.Id)
                    .HasColumnName("request_id");
            });

            modelBuilder.Entity<PendingShortLeaveRequestsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("PendingShortLeaveRequests");

                entity.Property(e => e.Name)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Nic)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("nic");

                entity.Property(e => e.ProfilePic)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("profile_pic");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.StartTime)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("start_time");

                entity.Property(e => e.EndTime)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("end_time");

                entity.Property(e => e.SpecialNote)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("special_notes");

                entity.Property(e => e.Id)
                    .HasColumnName("id");
            });

            modelBuilder.Entity<PendingUser>(entity =>
            {
                entity.HasKey(e => e.PendingUserId)
                    .HasName("PK_pending_user");

                entity.ToTable("pendingUserTable");

                entity.Property(e => e.PendingUserId).HasColumnName("pending_user_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.Email)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Nic)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("nic");

                entity.Property(e => e.NoOfAnnualLeaves).HasColumnName("no_of_annual_leaves");

                entity.Property(e => e.Password)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.ProfilePic)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("profile_pic");

                entity.Property(e => e.Telephone)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("telephone");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.Status)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.Confirm)
                    .HasColumnName("confirm");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("Report");

                entity.Property(e => e.ReportId).HasColumnName("report_id");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("end_date");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("start_date");

                entity.Property(e => e.Type)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("type");

                entity.Property(e => e.RequesterId)
                    .HasColumnName("requester_id");

                entity.Property(e => e.UId).HasColumnName("u_id");

                entity.HasOne(d => d.UIdNavigation)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.UId)
                    .HasConstraintName("FK_Report");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.ToTable("Request");

                entity.Property(e => e.RequestId).HasColumnName("request_id");

                entity.Property(e => e.ApprovalId).HasColumnName("approval_id");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.LeaveTypeId).HasColumnName("leave_type_id");

                entity.Property(e => e.SenderId).HasColumnName("sender_id");

                entity.Property(e => e.Status)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.Type)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("type");

                entity.Property(e => e.Duration).HasColumnName("duration");

                entity.Property(e => e.SpecialNote)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("special_note");

            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.ToTable("Settings");

                entity.HasKey(e => e.SettingsId);

                entity.Property(e => e.SettingsId).HasColumnName("settings_id");

                entity.Property(e => e.AdminId).HasColumnName("admin_id");

                entity.Property(e => e.NoOfAnnualLeaves).HasColumnName("no_of_annual_leaves");

                entity.Property(e => e.NoOfWorkingDaysPerWeek).HasColumnName("no_of_working_days_per_week");

                entity.Property(e => e.NoOfWorkingHoursPerDay).HasColumnName("no_of_working_hours_per_day");

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.Settings)
                    .HasForeignKey(d => d.AdminId)
                    .HasConstraintName("FK_Settings");
            });

            modelBuilder.Entity<ShortLeaveRequest>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("shortLeaveDetails");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.StartTime)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("start_time");

                entity.Property(e => e.EndTime)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("end_time");

                entity.Property(e => e.SpecialNote)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("special_notes");

                entity.Property(e => e.Status)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.SenderId)
                    .HasColumnName("requester_id");

                entity.Property(e => e.ApprovalId)
                    .HasColumnName("approval_id");
            });

            modelBuilder.Entity<Sms>(entity =>
            {
                entity.ToTable("SMS");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AdminId).HasColumnName("admin_id");

                entity.Property(e => e.Messsage)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("messsage");

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.Sms)
                    .HasForeignKey(d => d.AdminId)
                    .HasConstraintName("FK_SMS");

                entity.HasMany(d => d.Users)
                    .WithMany(p => p.SmsNavigation)
                    .UsingEntity<Dictionary<string, object>>(
                        "SmshasUser",
                        l => l.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK2_SMSHasUser"),
                        r => r.HasOne<Sms>().WithMany().HasForeignKey("SmsId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK1_SMSHasUser"),
                        j =>
                        {
                            j.HasKey("SmsId", "UserId");

                            j.ToTable("SMSHasUser");

                            j.IndexerProperty<int>("SmsId").HasColumnName("sms_id");

                            j.IndexerProperty<int>("UserId").HasColumnName("user_id");
                        });
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_user");

                entity.ToTable("userTable");

                entity.Property(e => e.UserId).HasColumnType("int").HasColumnName("user_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.Email)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Nic)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("nic");

                entity.Property(e => e.NoOfAnnualLeaves).HasColumnName("no_of_annual_leaves");

                entity.Property(e => e.Password)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.ProfilePic)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("profile_pic");

                entity.Property(e => e.Telephone)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("telephone");

                entity.Property(e => e.Type).HasColumnName("type");

            });

            modelBuilder.Entity<VideoConference>(entity =>
            {
                entity.HasKey(e => e.ConferenceId);

                entity.ToTable("VideoConference");

                entity.Property(e => e.ConferenceId)
		            .HasMaxLength(20)
                    .IsUnicode(false)
		            .HasColumnName("conference_id");

                entity.Property(e => e.Date)
                      .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("date");

                entity.Property(e => e.HostId)
                    .HasColumnName("host_id");

                entity.Property(e => e.SchedulerId).HasColumnName("scheduler_id");

                entity.Property(e => e.Time)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("time");
                
            });

            modelBuilder.Entity<BlackListedEmail>(entity => 
            {
                entity.HasKey(e => e.Email).HasName("PK_blackListedEmails");

                entity.ToTable("blackListedEmails");

                entity.Property(e => e.Email)
                     .HasMaxLength(70)
                     .IsUnicode(false)
                     .HasColumnName("email");
                
            });

            modelBuilder.Entity<VideoConferenceHasUser>(entity =>
            {
                entity.ToTable("VideoConferenceHasUser");

                entity.HasKey(e => new { e.ConferenceId, e.UserId }).HasName("PK_VCHU");

                entity.Property(e => e.ConferenceId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("c_id");

                entity.Property(e => e.UserId)
                    .HasColumnName("u_id");

            });

            modelBuilder.Entity<EmergencyLeaveDetail>(entity =>
            {
                entity.ToTable("emergencyLeaveDetails");

                entity.HasKey(e => e.Id).HasName("PK_emergencyLeaveDetails");

                entity.Property(e => e.Reason)
                    .IsUnicode(false)
                    .HasMaxLength(300)
                    .HasColumnName("reason");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.RequesterId)
                    .HasColumnName("requester_id");

                entity.Property(e => e.Status)
                    .IsUnicode(false)
                    .HasMaxLength(15)
                    .HasColumnName("status");

                entity.Property(e => e.ApprovalId)
                    .HasColumnName("approval_id");

            });

            modelBuilder.Entity<PendingEmergencyLeave>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("pendingEmergencyLeaves");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name");

                entity.Property(e => e.Nic)
                    .IsUnicode(false)
                    .HasMaxLength(15)
                    .HasColumnName("nic");

                entity.Property(e => e.ProfilePic)
                    .IsUnicode(false)
                    .HasMaxLength(150)
                    .HasColumnName("profile_pic");

                entity.Property(e => e.Reason)
                    .IsUnicode(false)
                    .HasMaxLength(300)
                    .HasColumnName("reason");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id");

            });

            OnModelCreatingGeneratedFunctions(modelBuilder);
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}