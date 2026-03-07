using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGEBI.Application.Dtos.Recursos;

public sealed class RecursoDto
{
    public int Id { get; set; }

    public string Titulo { get; set; } = string.Empty;

    public string? Isbn { get; set; }

    public string? Clasificacion { get; set; }
}