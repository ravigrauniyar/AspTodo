namespace Todo.Models
{
    public class LoginItemTodo
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool IsLoggedIn { get; set; }
    }
}
