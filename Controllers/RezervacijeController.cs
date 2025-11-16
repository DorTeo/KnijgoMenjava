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
    public class RezervacijeController : Controller
    {
        private readonly AppDbContext _context;

        public RezervacijeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Rezervacije
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Rezervacije.Include(r => r.Knjiga).Include(r => r.Uporabnik);
            return View(await appDbContext.ToListAsync());
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
    }
}
