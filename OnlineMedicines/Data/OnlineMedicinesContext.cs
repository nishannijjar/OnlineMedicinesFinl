using Microsoft.EntityFrameworkCore;
using OnlineMedicines.Models;

namespace OnlineMedicines.Data
{
    public class OnlineMedicinesContext : DbContext
    {
        public OnlineMedicinesContext (DbContextOptions<OnlineMedicinesContext> options)
            : base(options)
        {
        }

        public DbSet<Medicine> Medicine { get; set; }

        public DbSet<OrderMedicine> OrderMedicine { get; set; }

        public DbSet<Feedback> Feedback { get; set; }

        public DbSet<Login> Login { get; set; }
    }
}
