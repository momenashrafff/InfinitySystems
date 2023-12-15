using System;
using System.Collections.Generic;
using InfinitySystems.Models;
using Microsoft.EntityFrameworkCore;

namespace InfinitySystems.Models
{
    public partial class HomesyncContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public HomesyncContext()
        {
        }

        public HomesyncContext(DbContextOptions<HomesyncContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
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
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
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
