using System.ComponentModel.DataAnnotations;
namespace KnjigoMenjava.Models;

public class Knjiga
{
    public int Id { get; set; }

    public string Naslov { get; set; }
    public string Avtor { get; set; }
    public string? Opis { get; set; }

    public DateTime DatumDodajanja { get; set; }


    [Display(Name = "Kategorija")]
    public int KategorijaId { get; set; }
    public Kategorija? Kategorija { get; set; }

    [Display(Name = "Lastnik")]
    public string LastnikId { get; set; }
    public Uporabnik? Lastnik { get; set; }
}
