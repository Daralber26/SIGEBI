using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Domain.Entities.Dbo
{
    public class Reserva
    {
        public Guid Id { get; private set; }

        public Guid UsuarioId { get; private set; }

        public Guid RecursoId { get; private set; }

        public DateTime FechaCreacionUtc { get; private set; }

        public EstadoReserva Estado { get; private set; }

        private Reserva() { } // Constructor vacío para EF

        public Reserva(Guid usuarioId, Guid recursoId, DateTime utcNow)
        {
            if (usuarioId == Guid.Empty)
                throw new ArgumentException("UsuarioId inválido.");

            if (recursoId == Guid.Empty)
                throw new ArgumentException("RecursoId inválido.");

            Id = Guid.NewGuid();
            UsuarioId = usuarioId;
            RecursoId = recursoId;
            FechaCreacionUtc = utcNow;
            Estado = EstadoReserva.Activa;
        }

        public void Cancelar()
        {
            if (Estado != EstadoReserva.Activa)
                throw new InvalidOperationException("Solo reservas activas pueden cancelarse.");

            Estado = EstadoReserva.Cancelada;
        }

        public void Atender()
        {
            if (Estado != EstadoReserva.Activa)
                throw new InvalidOperationException("Solo reservas activas pueden atenderse.");

            Estado = EstadoReserva.Atendida;
        }

        public void Expirar()
        {
            if (Estado != EstadoReserva.Activa)
                throw new InvalidOperationException("Solo reservas activas pueden expirar.");

            Estado = EstadoReserva.Expirada;
        }
    }
}
