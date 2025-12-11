using System.ComponentModel.DataAnnotations;
namespace KnjigoMenjava.Models;

public class Knjiga
{
    public int Id { get; set; }

    [Required]
    public string Naslov { get; set; }
    [Required]
    public string Avtor { get; set; }
    public string? Opis { get; set; }

    [Display(Name = "Datum dodajanja")]
    [Required]
    public DateTime DatumDodajanja { get; set; }


    [Display(Name = "Kategorija")]
    public int KategorijaId { get; set; }
    public Kategorija? Kategorija { get; set; }

    [Display(Name = "Lastnik")]
    public string LastnikId { get; set; }
    public Uporabnik? Lastnik { get; set; }
}
