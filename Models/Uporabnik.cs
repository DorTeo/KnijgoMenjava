using Microsoft.AspNetCore.Identity;

namespace KnjigoMenjava.Models;

public class Uporabnik : IdentityUser
{
    public string? Ime { get; set; }
    public string? Priimek { get; set; }
    public string? Mesto { get; set; }
}
