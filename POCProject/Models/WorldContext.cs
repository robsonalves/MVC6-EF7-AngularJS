using System;
using Microsoft.Data.Entity;
using POCProject.Models;

namespace POCProject.Model
{
    public class WorldContext : DbContext
    {
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Stop> Stops { get; set; }
    }
}
