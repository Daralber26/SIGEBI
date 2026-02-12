using SIGEBI.Application.Abstractions;

namespace SIGEBI.Application.UseCases.Recursos;

public class EliminarRecurso
{
    private readonly IRecursoRepository _repo;

    public EliminarRecurso(IRecursoRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Ejecutar(Guid id, CancellationToken ct)
    {
        var recurso = await _repo.ObtenerPorIdAsync(id, ct);
        if (recurso is null) return false;

        await _repo.EliminarAsync(recurso, ct);
        await _repo.GuardarCambiosAsync(ct);

        return true;
    }
}
