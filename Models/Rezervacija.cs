namespace KnjigoMenjava.Models;

public class Rezervacija
{
    public int Id { get; set; }

    public DateTime DatumRezervacije { get; set; }
    public DateTime? DatumVrnitve { get; set; }


    public int KnjigaId { get; set; }
    public Knjiga Knjiga { get; set; }


    public string UporabnikId { get; set; }
    public Uporabnik Uporabnik { get; set; }
}
