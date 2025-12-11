using System.ComponentModel.DataAnnotations;

namespace KnjigoMenjava.Models;

public class Ocena
{
    public int Id { get; set; }

    [Required]
    [Range(1, 5)]
    public int Zvezdice { get; set; } 
    public string? Komentar { get; set; }

    [Required]
    public int KnjigaId { get; set; }
    public Knjiga Knjiga { get; set; }

    [Required]
    public string UporabnikId { get; set; }
    public Uporabnik Uporabnik { get; set; }
}
