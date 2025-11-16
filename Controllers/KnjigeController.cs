using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KnjigoMenjava.Data;
using KnjigoMenjava.Models;

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
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Knjige.Include(k => k.Kategorija).Include(k => k.Lastnik);
            return View(await appDbContext.ToListAsync());
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

        // GET: Knjige/Create
        public IActionResult Create()
        {
            ViewData["KategorijaId"] = new SelectList(_context.Kategorije, "Id", "Id");
            ViewData["LastnikId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Knjige/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naslov,Avtor,Opis,DatumDodajanja,KategorijaId,LastnikId")] Knjiga knjiga)
        {
            if (ModelState.IsValid)
            {
                _context.Add(knjiga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategorijaId"] = new SelectList(_context.Kategorije, "Id", "Id", knjiga.KategorijaId);
            ViewData["LastnikId"] = new SelectList(_context.Users, "Id", "Id", knjiga.LastnikId);
            return View(knjiga);
        }

        // GET: Knjige/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var knjiga = await _context.Knjige.FindAsync(id);
            if (knjiga == null)
            {
                return NotFound();
            }
            ViewData["KategorijaId"] = new SelectList(_context.Kategorije, "Id", "Id", knjiga.KategorijaId);
            ViewData["LastnikId"] = new SelectList(_context.Users, "Id", "Id", knjiga.LastnikId);
            return View(knjiga);
        }

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
            ViewData["KategorijaId"] = new SelectList(_context.Kategorije, "Id", "Id", knjiga.KategorijaId);
            ViewData["LastnikId"] = new SelectList(_context.Users, "Id", "Id", knjiga.LastnikId);
            return View(knjiga);
        }

        // GET: Knjige/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KnjigaExists(int id)
        {
            return _context.Knjige.Any(e => e.Id == id);
        }
    }
}
