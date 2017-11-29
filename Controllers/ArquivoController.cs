using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using saturnApp.Data;
using saturnUpload;

namespace saturnApp.Controllers
{
    public class ArquivoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArquivoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Arquivo
        public async Task<IActionResult> Index()
        {
            var saturnContext = _context.Arquivo.Include(a => a.Pai);
            return View(await saturnContext.ToListAsync());
        }

        // GET: Arquivo/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arquivo = await _context.Arquivo
                .Include(a => a.Pai)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (arquivo == null)
            {
                return NotFound();
            }

            return View(arquivo);
        }

        // GET: Arquivo/Create
        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["PaiId"] = id;
            return View();
        }

        // POST: Arquivo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> CreateAjax(int id) // Arquivo arquivo, 
        {
            var arquivos = Request.Form.Files;
            long tamanhoTotal = 0;

            if (arquivos.Count == 0)
            {
                return Content("Arquivo Inválido");
            }
            else
            {
                foreach (var arquivo in arquivos)
                {

                    if (arquivo == null || arquivo.Length == 0)
                    {
                        return Content("Arquivo Inválido");
                    }
                    else
                    {

                        var now = DateTime.Now.Ticks.ToString();

                        var path = Path.Combine(Directory.GetCurrentDirectory(),
                            "wwwroot/files/", now);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await arquivo.CopyToAsync(stream);
                        }

                        tamanhoTotal += arquivo.Length;

                        var arquivoDb = new Arquivo();
                        arquivoDb.criacao = DateTime.Now;
                        arquivoDb.Endereco = now;
                        arquivoDb.Tipo = arquivo.ContentType;
                        arquivoDb.Nome = arquivo.FileName;
                        arquivoDb.Tamanho = arquivo.Length;
                        arquivoDb.PaiId = id;

                        _context.Add(arquivoDb);
                        await _context.SaveChangesAsync();
                    }
                }

                string message = $"{arquivos.Count} arquivo(s) { tamanhoTotal } bytes enviados!";
                return Json(message);
            }
        }

        public async Task<IActionResult> Download(int? id)
        {
            
            if (id == null) {
                return Content("Arquivo Invalido");
            }

            Arquivo arquivo = await _context.Arquivo.
                SingleOrDefaultAsync(a => a.Id == id);


            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot/files/", arquivo.Endereco);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, arquivo.Tipo, Path.GetFileName(path));
        }

        // GET: Arquivo/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arquivo = await _context.Arquivo.SingleOrDefaultAsync(m => m.Id == id);
            if (arquivo == null)
            {
                return NotFound();
            }
            ViewData["PaiId"] = new SelectList(_context.Set<No>(), "Id", "Discriminator", arquivo.PaiId);
            return View(arquivo);
        }

        // POST: Arquivo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Endereco,Hash,Tamanho,Tipo,Extensao,Id,Nome,PaiId")] Arquivo arquivo)
        {
            if (id != arquivo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(arquivo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArquivoExists(arquivo.Id))
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
            ViewData["PaiId"] = new SelectList(_context.Set<No>(), "Id", "Discriminator", arquivo.PaiId);
            return View(arquivo);
        }

        // GET: Arquivo/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arquivo = await _context.Arquivo
                .Include(a => a.Pai)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (arquivo == null)
            {
                return NotFound();
            }

            return View(arquivo);
        }

        // POST: Arquivo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var arquivo = await _context.Arquivo.SingleOrDefaultAsync(m => m.Id == id);
            _context.Arquivo.Remove(arquivo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArquivoExists(long id)
        {
            return _context.Arquivo.Any(e => e.Id == id);
        }
    }
}
