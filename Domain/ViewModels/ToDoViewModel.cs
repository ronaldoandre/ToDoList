using System;

namespace TODOLIST.Domain.ViewModels{
    
     public class ToDoViewModel
    {
        public int ToDoId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int UserId { get; set; }
    }
}