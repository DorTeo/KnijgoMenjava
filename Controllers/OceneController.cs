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
    public class OceneController : Controller
    {
        private readonly AppDbContext _context;

        public OceneController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Ocene
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Ocene.Include(o => o.Knjiga).Include(o => o.Uporabnik);
            return View(await appDbContext.ToListAsync());
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
        public IActionResult Create()
        {
            ViewData["KnjigaId"] = new SelectList(_context.Knjige, "Id", "Id");
            ViewData["UporabnikId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Ocene/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Zvezdice,Komentar,KnjigaId,UporabnikId")] Ocena ocena)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ocena);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KnjigaId"] = new SelectList(_context.Knjige, "Id", "Id", ocena.KnjigaId);
            ViewData["UporabnikId"] = new SelectList(_context.Users, "Id", "Id", ocena.UporabnikId);
            return View(ocena);
        }

        // GET: Ocene/Edit/5
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
            ViewData["UporabnikId"] = new SelectList(_context.Users, "Id", "Id", ocena.UporabnikId);
            return View(ocena);
        }

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
