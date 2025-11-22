namespace KnjigoMenjava.Models;

public class Ocena
{
    public int Id { get; set; }

    public int Zvezdice { get; set; }   
    public string? Komentar { get; set; }


    public int KnjigaId { get; set; }
    public Knjiga Knjiga { get; set; }


    public string UporabnikId { get; set; }
    public Uporabnik Uporabnik { get; set; }
}
