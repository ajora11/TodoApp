using Microsoft.EntityFrameworkCore;
using TodoApp.API.Models;

namespace TodoApp.API.Data
{
    public class DataContext : DbContext
    { 
        public DataContext() { }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Todo> Todos { get; set; } 
        public DbSet<Folder> Folders { get; set; } 
        public DbSet<User> Users { get; set; }
    }

}