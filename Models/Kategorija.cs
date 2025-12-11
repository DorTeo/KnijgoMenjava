
using System.ComponentModel.DataAnnotations;

namespace KnjigoMenjava.Models;

public class Kategorija
{
    public int Id { get; set; }
    [Required]
    [Display(Name = "Ime kategorije")]
    public string Ime { get; set; }

    public ICollection<Knjiga>? Knjige { get; set; }
}
