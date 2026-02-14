using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGEBI.Contracts.Ejemplares;

public class CreateEjemplarRequest
{
    public Guid RecursoId { get; set; }
    public string Codigo { get; set; } = string.Empty;
}