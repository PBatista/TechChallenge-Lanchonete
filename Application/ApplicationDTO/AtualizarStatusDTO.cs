using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ApplicationDTO
{
    public class AtualizarStatusDTO
    {
        public AtualizarStatusDTO(string status)
        {
            Status = status;
        }
        public string Status { get; set; }
    }
}
