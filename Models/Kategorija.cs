namespace KnjigoMenjava.Models;

public class Kategorija
{
    public int Id { get; set; }
    public string Ime { get; set; }

    public ICollection<Knjiga>? Knjige { get; set; }
}
