using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGEBI.Domain.Entities.Dbo;

namespace SIGEBI.Application.Abstractions;

public interface IReservaRepository
{
    Task<bool> ExisteReservaActivaAsync(Guid usuarioId, Guid recursoId, CancellationToken ct);
    Task AgregarAsync(Reserva reserva, CancellationToken ct);
    Task GuardarCambiosAsync(CancellationToken ct);
}