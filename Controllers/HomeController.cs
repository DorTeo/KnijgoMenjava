using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using KnijgoMenjava.Models;

using KnjigoMenjava.Data;
using Microsoft.EntityFrameworkCore;

namespace KnijgoMenjava.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var zadnjeKnjige = await _context.Knjige
            .Include(k => k.Kategorija)
            .OrderByDescending(k => k.DatumDodajanja)
            .Take(3)
            .ToListAsync();
        return View(zadnjeKnjige);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
