using Microsoft.EntityFrameworkCore;
using Todo.Models.Domain;

namespace Todo.Data
{
    public class ItemContext : DbContext
    {
        public ItemContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<ItemTodo> ItemTodos { get; set; }
    }
}
