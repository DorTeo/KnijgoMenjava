namespace KnjigoMenjava.Models;
using System.ComponentModel.DataAnnotations;

public class Rezervacija
{
    public int Id { get; set; }

    
    [Display(Name = "Datum rezervacije")]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = true)]
    [Required]
    public DateTime DatumRezervacije { get; set; }

    
    [Display(Name = "Datum vrnitve")]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = true)]
    public DateTime? DatumVrnitve { get; set; }

    [Required]
    public int KnjigaId { get; set; }
    public Knjiga Knjiga { get; set; }


    [Required]
    public string UporabnikId { get; set; }
    public Uporabnik Uporabnik { get; set; }
}
