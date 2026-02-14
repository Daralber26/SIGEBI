using System;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Domain.Entities
{
    public class Ejemplar
    {
        public Guid Id { get; private set; }

        public Guid RecursoId { get; private set; }
        public Recurso Recurso { get; private set; } = null!;

        public string Codigo { get; private set; } = null!;

        public EjemplarEstado Estado { get; private set; }

        public bool Activo { get; private set; }

        public DateTime FechaAlta { get; private set; }

        // Constructor para EF
        private Ejemplar() { }

        // Constructor real de dominio
        public Ejemplar(Guid recursoId, string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                throw new ArgumentException("El código del ejemplar es obligatorio.");

            Id = Guid.NewGuid();
            RecursoId = recursoId;
            Codigo = codigo.Trim();
            Estado = EjemplarEstado.Disponible;
            Activo = true;
            FechaAlta = DateTime.UtcNow;
        }

        public void MarcarPrestado()
        {
            if (!Activo)
                throw new InvalidOperationException("El ejemplar está inactivo.");

            if (Estado != EjemplarEstado.Disponible)
                throw new InvalidOperationException("El ejemplar no está disponible.");

            Estado = EjemplarEstado.Prestado;
        }

        public void MarcarDisponible()
        {
            if (Estado != EjemplarEstado.Prestado)
                throw new InvalidOperationException("El ejemplar no está prestado.");

            Estado = EjemplarEstado.Disponible;
        }

        public void MarcarDanado()
        {
            Estado = EjemplarEstado.Danado;
        }

        public void MarcarPerdido()
        {
            Estado = EjemplarEstado.Perdido;
        }

        public void DarDeBaja()
        {
            Activo = false;
            Estado = EjemplarEstado.Baja;
        }
    }
}
