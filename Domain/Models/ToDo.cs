using System;

namespace TODOLIST.Domain.Models{
    
     public class ToDo
    {
        public int ToDoId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}