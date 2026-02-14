using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGEBI.Contracts.Prestamos;

public class CreatePrestamoRequest
{
    public Guid UsuarioId { get; set; }
    public Guid EjemplarId { get; set; }
    public int DiasPrestamo { get; set; } = 7;
}
