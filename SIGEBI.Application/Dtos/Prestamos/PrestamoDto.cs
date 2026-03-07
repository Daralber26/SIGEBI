using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGEBI.Application.Dtos.Prestamos;

public sealed class PrestamoDto
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public int EjemplarId { get; set; }

    public DateTime FechaPrestamo { get; set; }
}