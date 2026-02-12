using SIGEBI.Application.Abstractions;
using SIGEBI.Contracts.Resources;
using SIGEBI.Domain.Entities;

namespace SIGEBI.Application.UseCases.Recursos;

public class CrearRecurso
{
    private readonly IRecursoRepository _repo;

    public CrearRecurso(IRecursoRepository repo)
    {
        _repo = repo;
    }

    public async Task<Recurso> Ejecutar(CreateResourceRequest request, CancellationToken ct)
    {
        var recurso = new Recurso(request.Titulo, request.Autor, request.Isbn);

        await _repo.AgregarAsync(recurso, ct);
        await _repo.GuardarCambiosAsync(ct);

        return recurso;
    }
}
