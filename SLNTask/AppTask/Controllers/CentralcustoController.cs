using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppTask.Models;

namespace AppTask.Controllers
{
    public class CentralcustoController : Controller
    {
        private readonly DbTasksContext _context;

        public CentralcustoController(DbTasksContext context)
        {
            _context = context;
        }

        // GET: Centralcusto
        public async Task<IActionResult> Index()
        {
            return View(await _context.Centralcustos.ToListAsync());
        }

        // GET: Centralcusto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centralcusto = await _context.Centralcustos
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (centralcusto == null)
            {
                return NotFound();
            }

            return View(centralcusto);
        }

        // GET: Centralcusto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Centralcusto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,NomeCusto,ValorAnualMeta")] Centralcusto centralcusto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(centralcusto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(centralcusto);
        }

        // GET: Centralcusto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centralcusto = await _context.Centralcustos.FindAsync(id);
            if (centralcusto == null)
            {
                return NotFound();
            }
            return View(centralcusto);
        }

        // POST: Centralcusto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,NomeCusto,ValorAnualMeta")] Centralcusto centralcusto)
        {
            if (id != centralcusto.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(centralcusto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CentralcustoExists(centralcusto.Codigo))
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
            return View(centralcusto);
        }

        // GET: Centralcusto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centralcusto = await _context.Centralcustos
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (centralcusto == null)
            {
                return NotFound();
            }

            return View(centralcusto);
        }

        // POST: Centralcusto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var centralcusto = await _context.Centralcustos.FindAsync(id);
            if (centralcusto != null)
            {
                _context.Centralcustos.Remove(centralcusto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CentralcustoExists(int id)
        {
            return _context.Centralcustos.Any(e => e.Codigo == id);
        }
    }
}
