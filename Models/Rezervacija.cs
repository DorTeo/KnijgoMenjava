namespace KnjigoMenjava.Models;
using System.ComponentModel.DataAnnotations;

public class Rezervacija
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Datum rezervacije")]
    public DateTime DatumRezervacije { get; set; }
    [Display(Name = "Datum vrnitve")]
    public DateTime? DatumVrnitve { get; set; }

    [Required]
    public int KnjigaId { get; set; }
    public Knjiga Knjiga { get; set; }


    [Required]
    public string UporabnikId { get; set; }
    public Uporabnik Uporabnik { get; set; }
}
