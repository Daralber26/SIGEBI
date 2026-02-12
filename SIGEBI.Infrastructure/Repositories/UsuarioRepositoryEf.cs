using Microsoft.EntityFrameworkCore;
using SIGEBI.Application.Abstractions;
using SIGEBI.Domain.Entities;
using SIGEBI.Infrastructure.Persistence;

namespace SIGEBI.Infrastructure.Repositories;

public class UsuarioRepositoryEf : IUsuarioRepository
{
    private readonly AppDbContext _db;

    public UsuarioRepositoryEf(AppDbContext db)
    {
        _db = db;
    }

    public Task<Usuario?> GetByEmailAsync(string email, CancellationToken ct)
    {
        return _db.Usuarios.FirstOrDefaultAsync(u => u.Email == email, ct);
    }
}
