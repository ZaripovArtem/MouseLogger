using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseLogger.Model
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Email> Emails { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=MouseLogger.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Role admin = new() { Id = 1, Name = "Admin" };
            Role user = new() { Id = 2, Name = "User" };
            User user1 = new() { Id = 1, Login = "user", Password = "user", RoleId = 2 };
            User user2 = new() { Id = 2, Login = "admin", Password = "admin", RoleId = 1 };
            Email email = new() { Id = 1, EmailAddress = "Enter_Email_Address_Here" }; 
            // Заменить EmailAddress на существующий для отправки на него сообщений
            modelBuilder.Entity<Role>().HasData(new Role[] {admin, user});
            modelBuilder.Entity<User>().HasData(new User[] {user1, user2});
            modelBuilder.Entity<Email>().HasData(new Email[] {email});
            base.OnModelCreating(modelBuilder);
        }
    }
}