using SIGEBI.Application.Abstractions;
using SIGEBI.Contracts.Resources;

namespace SIGEBI.Application.UseCases.Recursos;

public class ActualizarRecurso
{
    private readonly IRecursoRepository _repo;

    public ActualizarRecurso(IRecursoRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Ejecutar(Guid id, UpdateResourceRequest request, CancellationToken ct)
    {
        var recurso = await _repo.ObtenerPorIdAsync(id, ct);
        if (recurso is null) return false;

        recurso.Actualizar(request.Titulo, request.Autor, request.Isbn);
        await _repo.GuardarCambiosAsync(ct);

        return true;
    }
}
