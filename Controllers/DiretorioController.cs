using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using saturnApp.Data;
using saturnUpload;

namespace saturnApp.Controllers
{
    public class DiretorioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiretorioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Diretorio/5
        public async Task<IActionResult> Index(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diretorios = _context.Diretorio
                .Where(d => d.PaiId == id).ToListAsync();


            if (diretorios == null)
            {
                //return NotFound();
            }

            ViewData["currentDir"] = await _context.Diretorio
                .Include(d => d.Pai)
                .SingleOrDefaultAsync(m => m.Id == id);

            ViewData["arquivos"] = await _context.Arquivo
                .Where(d => d.PaiId == id).ToListAsync();

            ViewData["diretorios"] = await _context.Diretorio
                .Where(d => d.PaiId == id).ToListAsync();

            return View(await diretorios);
        }

        // GET: Diretorio/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diretorio = await _context.Diretorio
                .Include(d => d.Pai)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (diretorio == null)
            {
                return NotFound();
            }

            return View(diretorio);
        }

        // GET: Diretorio/Create
        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["PaiId"] = id;
            return View();
        }

        // POST: Diretorio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,PaiId")] Diretorio diretorio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diretorio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = diretorio.PaiId });
            }
            //ViewData["PaiId"] = new SelectList(_context.Set<No>(), "Id", "Discriminator", diretorio.PaiId);
            return View(diretorio);
        }

        // GET: Diretorio/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diretorio = await _context.Diretorio.SingleOrDefaultAsync(m => m.Id == id);
            if (diretorio == null)
            {
                return NotFound();
            }
            ViewData["PaiId"] = new SelectList(_context.Set<No>(), "Id", "Discriminator", diretorio.PaiId);
            return View(diretorio);
        }

        // POST: Diretorio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nome,PaiId")] Diretorio diretorio)
        {
            if (id != diretorio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diretorio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiretorioExists(diretorio.Id))
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
            ViewData["PaiId"] = new SelectList(_context.Set<No>(), "Id", "Discriminator", diretorio.PaiId);
            return View(diretorio);
        }

        // GET: Diretorio/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diretorio = await _context.Diretorio
                .Include(d => d.Pai)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (diretorio == null)
            {
                return NotFound();
            }

            return View(diretorio);
        }

        // POST: Diretorio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var diretorio = await _context.Diretorio.SingleOrDefaultAsync(m => m.Id == id);
            _context.Diretorio.Remove(diretorio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiretorioExists(long id)
        {
            return _context.Diretorio.Any(e => e.Id == id);
        }
    }
}
