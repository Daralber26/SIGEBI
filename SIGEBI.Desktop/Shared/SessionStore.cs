namespace SIGEBI.Desktop.Shared;

public sealed class SessionStore
{
    public Guid? UsuarioId { get; private set; }
    public string? Nombre { get; private set; }
    public string? Email { get; private set; }

    public bool IsAuthenticated => UsuarioId.HasValue;

    public void Start(Guid usuarioId, string nombre, string email)
    {
        UsuarioId = usuarioId;
        Nombre = nombre;
        Email = email;
    }

    public void Clear()
    {
        UsuarioId = null;
        Nombre = null;
        Email = null;
    }
}