using SIGEBI.Application.Abstractions;
using SIGEBI.Application.Dtos.Recursos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.UseCases.Recursos;
using SIGEBI.Contracts.Resources;
using System.Linq;

namespace SIGEBI.Application.Services;

public sealed class RecursoService : IRecursoService
{
    private readonly IRecursoRepository _repo;
    private readonly CrearRecurso _crearRecurso;
    private readonly ActualizarRecurso _actualizarRecurso;
    private readonly EliminarRecurso _eliminarRecurso;

    public RecursoService(
        IRecursoRepository repo,
        CrearRecurso crearRecurso,
        ActualizarRecurso actualizarRecurso,
        EliminarRecurso eliminarRecurso)
    {
        _repo = repo;
        _crearRecurso = crearRecurso;
        _actualizarRecurso = actualizarRecurso;
        _eliminarRecurso = eliminarRecurso;
    }

    public async Task<IReadOnlyList<RecursoDto>> GetAllActiveAsync(CancellationToken ct = default)
    {
        var recursos = await _repo.ListarAsync(ct);

        return recursos.Select(r => new RecursoDto
        {
            Id = r.Id,
            Titulo = r.Titulo,
            Autor = r.Autor,
            Isbn = r.Isbn
        }).ToList();
    }

    public async Task<RecursoDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var recurso = await _repo.ObtenerPorIdAsync(id, ct);

        if (recurso is null)
            return null;

        return new RecursoDto
        {
            Id = recurso.Id,
            Titulo = recurso.Titulo,
            Autor = recurso.Autor,
            Isbn = recurso.Isbn
        };
    }

    public async Task AddAsync(SaveRecursoDto dto, CancellationToken ct = default)
    {
        var request = new CreateResourceRequest
        {
            Titulo = dto.Titulo,
            Autor = dto.Autor,
            Isbn = dto.Isbn
        };

        await _crearRecurso.Ejecutar(request, ct);
    }

    public async Task UpdateAsync(UpdateRecursoDto dto, CancellationToken ct = default)
    {
        var request = new UpdateResourceRequest
        {
            Titulo = dto.Titulo,
            Autor = dto.Autor,
            Isbn = dto.Isbn
        };

        await _actualizarRecurso.Ejecutar(dto.Id, request, ct);
    }

    public async Task SoftDeleteAsync(RemoveRecursoDto dto, CancellationToken ct = default)
    {
        await _eliminarRecurso.Ejecutar(dto.Id, ct);
    }
}