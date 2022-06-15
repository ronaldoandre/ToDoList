namespace TODOLIST.Domain.Models{
    
     public class User
    {
        public int UserId { get;}
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
    }
}