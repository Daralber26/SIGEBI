namespace SIGEBI.Domain.Entities;

public class Auditoria
{
    public int Id { get; private set; }

    public DateTime FechaUtc { get; private set; } = DateTime.UtcNow;

    public string Metodo { get; private set; } = default!;
    public string Ruta { get; private set; } = default!;
    public int StatusCode { get; private set; }

    public string TraceId { get; private set; } = default!;
    public string? Ip { get; private set; }

    public string? Usuario { get; private set; } // por ahora string (luego UsuarioId)
    public string? Detalle { get; private set; } // opcional: resumen (ej: "CrearPrestamo", "DevolverPrestamo")

    private Auditoria() { } // EF

    public Auditoria(string metodo, string ruta, int statusCode, string traceId, string? ip, string? usuario, string? detalle)
    {
        Metodo = metodo;
        Ruta = ruta;
        StatusCode = statusCode;
        TraceId = traceId;
        Ip = ip;
        Usuario = usuario;
        Detalle = detalle;
    }
}