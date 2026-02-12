using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGEBI.Contracts.Resources;

public class ResourceDto
{
    public Guid Id { get; set; }

    public string Titulo { get; set; } = string.Empty;

    public string Autor { get; set; } = string.Empty;

    public string Isbn { get; set; } = string.Empty;

    public int CopiasDisponibles { get; set; }
}
