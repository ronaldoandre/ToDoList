using System.Collections.Generic;

namespace TODOLIST.Domain.ViewModels{
    
     public class UserViewModel
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public IEnumerable<ToDoViewModel> ToDos { get; set; }
    }
}