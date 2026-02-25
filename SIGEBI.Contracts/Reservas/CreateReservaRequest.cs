using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGEBI.Contracts.Reservas;

public sealed record CreateReservaRequest(Guid UsuarioId, Guid RecursoId);
