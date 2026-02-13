using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGEBI.Application.Abstractions;
using SIGEBI.Contracts.Prestamos;
using SIGEBI.Domain.Entities;

namespace SIGEBI.Application.UseCases.Prestamos;

public class CrearPrestamo
{
    private readonly IPrestamoRepository _prestamos;

    public CrearPrestamo(IPrestamoRepository prestamos)
    {
        _prestamos = prestamos;
    }

    public async Task<Prestamo> Ejecutar(CreatePrestamoRequest req, CancellationToken ct)
    {
        var prestamo = new Prestamo(
            req.UsuarioId,
            req.RecursoId,
            DateTime.UtcNow,
            req.DiasPrestamo
        );

        await _prestamos.AgregarAsync(prestamo, ct);
        await _prestamos.GuardarCambiosAsync(ct);

        return prestamo;
    }
}
