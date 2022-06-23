using System;
using TODOLIST.Domain.Enum;

namespace TODOLIST.Domain.Models{
    
     public class ToDo
    {
        public int ToDoId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public StatusTodoEnum Status { get; set; }
        public DateTime? DataCriada { get; set; }
        public DateTime DataTarefa  { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public bool isValid
        {
            get
            {
                return !(UserId < 1) && !string.IsNullOrEmpty(Titulo);
            }
        }
    }
}