using System;
using SIGEBI.Application.Abstractions;
using SIGEBI.Domain.Entities;

namespace SIGEBI.Application.UseCases.Ejemplares;

public class CrearEjemplar
{
    private readonly IRecursoRepository _recursos;
    private readonly IEjemplarRepository _ejemplares;

    public CrearEjemplar(IRecursoRepository recursos, IEjemplarRepository ejemplares)
    {
        _recursos = recursos;
        _ejemplares = ejemplares;
    }

    public async Task<Guid> EjecutarAsync(Guid recursoId, string codigo, CancellationToken ct)
    {
        // 1) Normalizar y validar código
        if (string.IsNullOrWhiteSpace(codigo))
            throw new InvalidOperationException("El código del ejemplar es obligatorio.");

        codigo = codigo.Trim();

        if (codigo.Length < 3)
            throw new InvalidOperationException("Código inválido.");

        if (codigo.Equals("string", StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Código inválido.");

        // 2) Validar que el recurso exista
        var recurso = await _recursos.ObtenerPorIdAsync(recursoId, ct);
        if (recurso is null)
            throw new InvalidOperationException("El recurso no existe.");

        // 3) Validar código único
        var existeCodigo = await _ejemplares.ExisteCodigoAsync(codigo, ct);
        if (existeCodigo)
            throw new InvalidOperationException("Ya existe un ejemplar con ese código.");

        // 4) Crear entidad
        var ejemplar = new Ejemplar(recursoId, codigo);

        // 5) Guardar
        await _ejemplares.AgregarAsync(ejemplar, ct);
        await _ejemplares.GuardarCambiosAsync(ct);

        return ejemplar.Id;
    }
}
