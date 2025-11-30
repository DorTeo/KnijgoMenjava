using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KnjigoMenjava.Data;
using KnjigoMenjava.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace KnijgoMenjava.Controllers
{
    public class OceneController : Controller
    {
        private readonly AppDbContext _context;

        public OceneController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Ocene
        public async Task<IActionResult> Index(string sortOrder, int? knjigaId, int? pageNumber)
        {
            ViewData["ZvezdiceSortParm"] = String.IsNullOrEmpty(sortOrder) ? "ZvezdiceSortParm_desc" : "";
            ViewData["CurrentSort"] = sortOrder;

            IQueryable<Ocena> appDbContext = _context.Ocene.Include(o => o.Knjiga).Include(o => o.Uporabnik);
            
            if (knjigaId != null)
            {
                appDbContext = appDbContext.Where(o => o.KnjigaId == knjigaId);
            }

            switch (sortOrder)
            {
                case "ZvezdiceSortParm_desc":
                    appDbContext = appDbContext.OrderByDescending(o => o.Zvezdice);
                    break;
                default:
                    appDbContext = appDbContext.OrderBy(o => o.Zvezdice);
                    break;
            }
            
            int pageSize = 4;
            return View(await PaginatedList<Ocena>.CreateAsync(appDbContext.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Ocene/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ocena = await _context.Ocene
                .Include(o => o.Knjiga)
                .Include(o => o.Uporabnik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ocena == null)
            {
                return NotFound();
            }

            return View(ocena);
        }

        // GET: Ocene/Create
        [Authorize]
        public async Task<IActionResult> Create(int? knjigaId)
        {
            return View(new Ocena { KnjigaId = knjigaId.Value });
        }

        // POST: Ocene/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Zvezdice,Komentar,KnjigaId")] Ocena ocena)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ocena.UporabnikId = userId;


            ModelState.Remove("Uporabnik");
            ModelState.Remove("UporabnikId");
            ModelState.Remove("Knjiga");

            if (ModelState.IsValid)
            {
                _context.Add(ocena);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { knjigaId = ocena.KnjigaId });
            }
            
            return View(ocena);
        }

        // GET: Ocene/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ocena = await _context.Ocene.FindAsync(id);
            if (ocena == null)
            {
                return NotFound();
            }
            ViewData["KnjigaId"] = new SelectList(_context.Knjige, "Id", "Id", ocena.KnjigaId);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ocena.UporabnikId != userId && !User.IsInRole("Administrator"))
            {
                return Forbid();
            }
            return View(ocena);
        }

        [Authorize]
        // POST: Ocene/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Zvezdice,Komentar,KnjigaId,UporabnikId")] Ocena ocena)
        {
            if (id != ocena.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ocena);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OcenaExists(ocena.Id))
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
            ViewData["KnjigaId"] = new SelectList(_context.Knjige, "Id", "Id", ocena.KnjigaId);
            ViewData["UporabnikId"] = new SelectList(_context.Users, "Id", "Id", ocena.UporabnikId);
            return View(ocena);
        }

        // GET: Ocene/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ocena = await _context.Ocene
                .Include(o => o.Knjiga)
                .Include(o => o.Uporabnik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ocena == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ocena.UporabnikId != userId && !User.IsInRole("Administrator"))
            {
                return Forbid();
            }

            return View(ocena);
        }

        // POST: Ocene/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ocena = await _context.Ocene.FindAsync(id);
            if (ocena != null)
            {
                _context.Ocene.Remove(ocena);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OcenaExists(int id)
        {
            return _context.Ocene.Any(e => e.Id == id);
        }
    }
}
