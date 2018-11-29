using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationChurras.Models;

namespace WebApplicationChurras.Controllers
{
    public class ChurrascosController : Controller
    {
        private  dbContext _context;

        public ChurrascosController(dbContext context)
        {
            _context = context;
        }

        // GET: Churrascos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Churrascos.Include(c => c.Participantes).ToListAsync());
        }

        // GET: Churrascos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var churrasco = await _context.Churrascos.Include( c => c.Participantes)
                .SingleOrDefaultAsync(m => m.ChurrascoID == id);
            if (churrasco == null)
            {
                return NotFound();
            }

            return View(churrasco);
        }

        // GET: Churrascos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Churrascos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Churrasco churrasco)
        {
            if (ModelState.IsValid)
            {
                _context.Add(churrasco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = churrasco.ChurrascoID });
            }
            return View(churrasco);
        }

        // GET: Churrascos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var churrasco = await _context.Churrascos.SingleOrDefaultAsync(m => m.ChurrascoID == id);
            if (churrasco == null)
            {
                return NotFound();
            }
            return View(churrasco);
        }

        // POST: Churrascos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Churrasco churrasco)
        {
            if (id != churrasco.ChurrascoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(churrasco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChurrascoExists(churrasco.ChurrascoID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = id });
            }
            return View(churrasco);
        }

        // GET: Churrascos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var churrasco = await _context.Churrascos
                .SingleOrDefaultAsync(m => m.ChurrascoID == id);
            if (churrasco == null)
            {
                return NotFound();
            }

            return View(churrasco);
        }

        // POST: Churrascos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var churrasco = await _context.Churrascos.Include(c => c.Participantes).SingleOrDefaultAsync(m => m.ChurrascoID == id);
            _context.Participantes.RemoveRange(churrasco.Participantes);
            _context.Churrascos.Remove(churrasco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChurrascoExists(int id)
        {
            return _context.Churrascos.Any(e => e.ChurrascoID == id);
        }
    }
}
