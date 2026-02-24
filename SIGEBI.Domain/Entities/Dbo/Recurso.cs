using System.Collections.Generic;


namespace SIGEBI.Domain.Entities;

public class Recurso
{
    public Guid Id { get; private set; }
    public string Titulo { get; private set; } = string.Empty;
    public string Autor { get; private set; } = string.Empty;
    public string Isbn { get; private set; } = string.Empty;

    private Recurso() { } // Para EF Core

    public Recurso(string titulo, string autor, string isbn)
    {
        Id = Guid.NewGuid();
        Titulo = titulo;
        Autor = autor;
        Isbn = isbn;
    }

    public void Actualizar(string titulo, string autor, string isbn)
    {
        Titulo = titulo;
        Autor = autor;
        Isbn = isbn;
    }

    public ICollection<Ejemplar> Ejemplares { get; set; } = new List<Ejemplar>();

}
