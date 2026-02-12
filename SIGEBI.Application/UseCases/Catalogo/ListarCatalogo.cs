using SIGEBI.Application.Abstractions;
using SIGEBI.Contracts.Resources;

namespace SIGEBI.Application.UseCases.Catalogo;

public class ListarCatalogo
{
    private readonly IRecursoRepository _repo;

    public ListarCatalogo(IRecursoRepository repo) => _repo = repo;

    public async Task<List<ResourceDto>> ExecuteAsync(CancellationToken ct)
    {
        var recursos = await _repo.ListarAsync(ct);

        return recursos.Select(r => new ResourceDto
        {
            Id = r.Id,
            Titulo = r.Titulo,
            Autor = r.Autor,
            Isbn = r.Isbn,
            CopiasDisponibles = 0 // luego lo conectamos con Ejemplares
        }).ToList();
    }
}
