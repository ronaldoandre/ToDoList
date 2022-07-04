using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.ToDo
{
    public class ToDoCreateDto
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Data { get; set; }
        public string Hora { get; set; }
    }

}
