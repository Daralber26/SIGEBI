namespace SIGEBI.Domain.Entities;

public class Usuario
{
    public Guid Id { get; private set; }

    public string Nombre { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;

    public string PasswordHash { get; private set; } = string.Empty;

    public bool Activo { get; private set; } = true;

    public DateTime FechaRegistro { get; private set; }

    private Usuario() { } // EF

    public Usuario(Guid id, string nombre, string email, string passwordHash)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id inválido.");
        if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentException("Nombre es obligatorio.");
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email es obligatorio.");
        if (string.IsNullOrWhiteSpace(passwordHash)) throw new ArgumentException("PasswordHash es obligatorio.");

        Id = id;
        Nombre = nombre.Trim();
        Email = email.Trim();
        PasswordHash = passwordHash;
        Activo = true;
        FechaRegistro = DateTime.UtcNow;
    }

    public void CambiarNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("Nombre es obligatorio.");

        Nombre = nombre.Trim();
    }

    public void CambiarEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email es obligatorio.");

        Email = email.Trim();
    }

    public void CambiarPasswordHash(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("PasswordHash es obligatorio.");

        PasswordHash = passwordHash;
    }

    public void Desactivar() => Activo = false;

    public void Activar() => Activo = true;
}