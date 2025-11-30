using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KnjigoMenjava.Data;
using KnjigoMenjava.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace KnijgoMenjava.Controllers
{
    
    public class KnjigeController : Controller
    {
        private readonly AppDbContext _context;

        public KnjigeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Knjige
        public async Task<IActionResult> Index(string sortOrder, string searchString, int? pageNumber)
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
                searchString = searchString;
            }

            ViewData["CurrentFilter"] = searchString;

            var appDbContext = _context.Knjige
                .Include(k => k.Kategorija)
                .Include(k => k.Lastnik)
                .Where(k => !_context.Rezervacije.Any(r => r.KnjigaId == k.Id && r.DatumVrnitve == null));

            if (!String.IsNullOrEmpty(searchString))
            {
                appDbContext = appDbContext.Where(k => k.Naslov.Contains(searchString) || k.Avtor.Contains(searchString) || k.Lastnik.UserName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    appDbContext = appDbContext.OrderByDescending(k => k.Naslov);
                    break;
                case "Date":
                    appDbContext = appDbContext.OrderBy(k => k.DatumDodajanja);
                    break;
                case "date_desc":
                    appDbContext = appDbContext.OrderByDescending(k => k.DatumDodajanja);
                    break;
                default:
                    appDbContext = appDbContext.OrderBy(k => k.Naslov);
                    break;
            }

            int pageSize = 4;    
            return View(await PaginatedList<Knjiga>.CreateAsync(appDbContext.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Knjige/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var knjiga = await _context.Knjige
                .Include(k => k.Kategorija)
                .Include(k => k.Lastnik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (knjiga == null)
            {
                return NotFound();
            }

            return View(knjiga);
        }
        [Authorize]

        // GET: Knjige/Create
        public IActionResult Create()
        {
            ViewData["KategorijaId"] = new SelectList(_context.Kategorije.OrderBy(k => k.Ime), "Id", "Ime");
            return View();
        }


        [Authorize]
        // POST: Knjige/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naslov,Avtor,Opis,KategorijaId")] Knjiga knjiga)
        {
            
            knjiga.LastnikId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            
           
            knjiga.DatumDodajanja = DateTime.Now;

           
            ModelState.Remove("LastnikId");
            ModelState.Remove("DatumDodajanja");

            if (ModelState.IsValid)
            {
                _context.Add(knjiga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategorijaId"] = new SelectList(_context.Kategorije.OrderBy(k => k.Ime), "Id", "Ime", knjiga.KategorijaId);
            return View(knjiga);
        }

        [Authorize]
        // GET: Knjige/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (id == null)
            {
                return NotFound();
            }

            var knjiga = await _context.Knjige.FindAsync(id);
            if (knjiga == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(currentUserId) && knjiga.LastnikId != currentUserId && !User.IsInRole("Administrator")) return Forbid();
            ViewData["KategorijaId"] = new SelectList(_context.Kategorije.OrderBy(k => k.Ime), "Id", "Ime", knjiga.KategorijaId);
            ViewData["LastnikId"] = new SelectList(_context.Users, "Id", "UserName");
            return View(knjiga);
        }

        [Authorize]
        // POST: Knjige/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naslov,Avtor,Opis,DatumDodajanja,KategorijaId,LastnikId")] Knjiga knjiga)
        {
            if (id != knjiga.Id)
            {
                return NotFound();
            }

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(knjiga);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KnjigaExists(knjiga.Id))
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

            if (!string.IsNullOrEmpty(currentUserId) && knjiga.LastnikId != currentUserId && !User.IsInRole("Administrator")) return Forbid();
            ViewData["KategorijaId"] = new SelectList(_context.Kategorije.OrderBy(k => k.Ime), "Id", "Ime", knjiga.KategorijaId);
            ViewData["LastnikId"] = new SelectList(_context.Users, "Id", "UserName");
            return View(knjiga);
        }

        [Authorize]
        // GET: Knjige/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var knjiga = await _context.Knjige
                .Include(k => k.Kategorija)
                .Include(k => k.Lastnik)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (knjiga != null)
            {
                if (!string.IsNullOrEmpty(currentUserId) && knjiga.LastnikId != currentUserId && !User.IsInRole("Administrator")) return Forbid();    
            }

            if (knjiga == null)
            {
                return NotFound();
            }

            return View(knjiga);
        }

        [Authorize]
        // POST: Knjige/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var knjiga = await _context.Knjige.FindAsync(id);
            if (knjiga != null)
            {
                _context.Knjige.Remove(knjiga);
            }

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (knjiga != null && !string.IsNullOrEmpty(currentUserId) && knjiga.LastnikId != currentUserId && !User.IsInRole("Administrator")) return Forbid();

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KnjigaExists(int id)
        {
            return _context.Knjige.Any(e => e.Id == id);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Rezerviraj(int id)
        {
            var knjiga = await _context.Knjige.FindAsync(id);
            if (knjiga == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (string.IsNullOrEmpty(userId)) return Forbid();

            var rezervacija = new Rezervacija
            {
                KnjigaId = id,
                UporabnikId = userId!,
                DatumRezervacije = DateTime.Now
            };
            _context.Add(rezervacija);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}




