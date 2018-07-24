using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MonitoringSystem.Models;

namespace MonitoringSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Temperature>().ToTable("Temperature");
            builder.Entity<Humidity>().ToTable("Humidity");
            builder.Entity<Fan>().ToTable("Fan");
            builder.Entity<Rack>().ToTable("Rack");
            builder.Entity<Room>().ToTable("Room");
            builder.Entity<Sensor>().ToTable("Sensor");
            builder.Entity<Status>().ToTable("Status");
            builder.Entity<Log>().ToTable("Log");
        }

        public DbSet<Temperature> Temperatures { get; set; }
        public DbSet<Humidity> Humidities { get; set; }
        public DbSet<Fan> Fans { get; set; }
        public DbSet<Rack> Racks { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
