using SIGEBI.Application.Abstractions;
using SIGEBI.Domain.Entities;

namespace SIGEBI.Infrastructure.Repositories;

public class RecursoRepositoryInMemory : IRecursoRepository
{
    private static readonly List<Recurso> _recursos = new();

    public Task<List<Recurso>> ListarAsync(CancellationToken ct)
    {
        return Task.FromResult(_recursos.ToList());
    }

    public Task<Recurso?> ObtenerPorIdAsync(Guid id, CancellationToken ct)
    {
        var recurso = _recursos.FirstOrDefault(r => r.Id == id);
        return Task.FromResult(recurso);
    }

    public Task AgregarAsync(Recurso recurso, CancellationToken ct)
    {
        _recursos.Add(recurso);
        return Task.CompletedTask;
    }

    public Task EliminarAsync(Recurso recurso, CancellationToken ct)
    {
        _recursos.Remove(recurso);
        return Task.CompletedTask;
    }

    public Task GuardarCambiosAsync(CancellationToken ct)
    {
        // InMemory no guarda en BD
        return Task.CompletedTask;
    }
}
