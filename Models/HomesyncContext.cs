using System;
using System.Collections.Generic;
using InfinitySystems.Models;
using Microsoft.EntityFrameworkCore;

namespace InfinitySystems.Models
{
    public partial class HomesyncContext : DbContext
    {
        public HomesyncContext()
        {
        }

        public HomesyncContext(DbContextOptions<HomesyncContext> options)
            : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<AssignedTo> AssignedTos { get; set; }

        public DbSet<Calendar> Calendars { get; set; }

        public DbSet<Communication> Communications { get; set; }

        public DbSet<Device> Devices { get; set; }

        public DbSet<Finance> Finances { get; set; }

        public DbSet<Guest> Guests { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<RoomSchedule> RoomSchedules { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<Users> User { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("Server=localhost;Database=Homesync;User Id=sa;Password=NorkAgency@1901;TrustServerCertificate=true");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AssignedTo>()
                .HasKey(a => new { a.Admin_Id, a.Task_Id, a.User_Id });
            modelBuilder.Entity<Calendar>()
                .HasKey(c => new { c.Event_Id, c.User_Assigned_To });
            modelBuilder.Entity<RoomSchedule>()
                .HasKey(r => new { r.Creator_Id, r.Start_Time });
        }
    }
}
