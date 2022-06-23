using System;
using TODOLIST.Domain.Enum;

namespace TODOLIST.Domain.ViewModels{
    
     public class ToDoViewModel
    {
        public int? ToDoId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public StatusTodoEnum Status { get; set; }
        public DateTime? DataCriada { get; set; }
        public DateTime DataTarefa  { get; set; }
        public int UserId { get; set; }
    }
}