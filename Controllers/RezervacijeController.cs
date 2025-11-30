using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KnjigoMenjava.Data;
using KnjigoMenjava.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace KnijgoMenjava.Controllers
{
    public class RezervacijeController : Controller
    {
        private readonly AppDbContext _context;

        public RezervacijeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Rezervacije
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentSort"] = sortOrder;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            IQueryable<Rezervacija> AppDBcontext = _context.Rezervacije
                .Include(r => r.Knjiga)
                    .ThenInclude(k => k.Lastnik)
                .Include(r => r.Uporabnik)
                .Where(r => r.UporabnikId == userId || r.Knjiga.LastnikId == userId);

            if (!String.IsNullOrEmpty(searchString))
            {
                AppDBcontext = AppDBcontext.Where(r => r.Knjiga.Naslov.Contains(searchString) || r.Uporabnik.UserName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    AppDBcontext = AppDBcontext.OrderByDescending(r => r.Knjiga.Naslov);
                    break;
                case "Date":
                    AppDBcontext = AppDBcontext.OrderBy(r => r.DatumRezervacije);
                    break;
                case "date_desc":
                    AppDBcontext = AppDBcontext.OrderByDescending(r => r.DatumRezervacije);
                    break;
                default:
                    AppDBcontext = AppDBcontext.OrderBy(r => r.Knjiga.Naslov);
                    break;
            }

            int pageSize = 4;    
            return View(await PaginatedList<Rezervacija>.CreateAsync(AppDBcontext.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Rezervacije/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezervacija = await _context.Rezervacije
                .Include(r => r.Knjiga)
                .Include(r => r.Uporabnik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rezervacija == null)
            {
                return NotFound();
            }

            return View(rezervacija);
        }

        // GET: Rezervacije/Create
        public IActionResult Create()
        {
            ViewData["KnjigaId"] = new SelectList(_context.Knjige, "Id", "Id");
            ViewData["UporabnikId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Rezervacije/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DatumRezervacije,DatumVrnitve,KnjigaId,UporabnikId")] Rezervacija rezervacija)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rezervacija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KnjigaId"] = new SelectList(_context.Knjige, "Id", "Id", rezervacija.KnjigaId);
            ViewData["UporabnikId"] = new SelectList(_context.Users, "Id", "Id", rezervacija.UporabnikId);
            return View(rezervacija);
        }

        // GET: Rezervacije/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezervacija = await _context.Rezervacije.FindAsync(id);
            if (rezervacija == null)
            {
                return NotFound();
            }
            ViewData["KnjigaId"] = new SelectList(_context.Knjige, "Id", "Id", rezervacija.KnjigaId);
            ViewData["UporabnikId"] = new SelectList(_context.Users, "Id", "Id", rezervacija.UporabnikId);
            return View(rezervacija);
        }

        // POST: Rezervacije/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DatumRezervacije,DatumVrnitve,KnjigaId,UporabnikId")] Rezervacija rezervacija)
        {
            if (id != rezervacija.Id)
            {
                return NotFound();
            }

            
            ModelState.Remove("Knjiga");
            ModelState.Remove("Uporabnik");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rezervacija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RezervacijaExists(rezervacija.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["KnjigaId"] = new SelectList(_context.Knjige, "Id", "Id", rezervacija.KnjigaId);
            ViewData["UporabnikId"] = new SelectList(_context.Users, "Id", "Id", rezervacija.UporabnikId);
            return View(rezervacija);
        }

        // GET: Rezervacije/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezervacija = await _context.Rezervacije
                .Include(r => r.Knjiga)
                .Include(r => r.Uporabnik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rezervacija == null)
            {
                return NotFound();
            }

            if (rezervacija.UporabnikId != User.FindFirstValue(ClaimTypes.NameIdentifier) && rezervacija.Knjiga.LastnikId != User.FindFirstValue(ClaimTypes.NameIdentifier) && !User.IsInRole("Administrator"))
            {
                return Forbid();
            }

            return View(rezervacija);
        }

        // POST: Rezervacije/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rezervacija = await _context.Rezervacije.FindAsync(id);
            if (rezervacija != null)
            {
                _context.Rezervacije.Remove(rezervacija);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RezervacijaExists(int id)
        {
            return _context.Rezervacije.Any(e => e.Id == id);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PotrdiVrnjeno(int id)
        {
            var rezervacija = await _context.Rezervacije
                .Include(r => r.Knjiga)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (rezervacija == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!string.IsNullOrEmpty(userId) && rezervacija.Knjiga.LastnikId != userId && !User.IsInRole("Administrator")) return Forbid();

            if (rezervacija.DatumVrnitve == null)
            {
                rezervacija.DatumVrnitve = DateTime.Now;
                _context.Update(rezervacija);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
