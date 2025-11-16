namespace KnjigoMenjava.Models;

public class Ocena
{
    public int Id { get; set; }

    public int Zvezdice { get; set; }   // 1–5
    public string? Komentar { get; set; }

    // povezava knjige
    public int KnjigaId { get; set; }
    public Knjiga Knjiga { get; set; }

    // povezava uporabnika
    public string UporabnikId { get; set; }
    public Uporabnik Uporabnik { get; set; }
}
