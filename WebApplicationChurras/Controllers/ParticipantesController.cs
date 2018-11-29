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
    public class ParticipantesController : Controller
    {
        private readonly dbContext _context;

        public ParticipantesController(dbContext context)
        {
            _context = context;
        }

        // GET: Participantes
        public IActionResult Index()
        {
            return View(_context.Participantes.ToList());
        }

        // GET: Participantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participante = await _context.Participantes
                .SingleOrDefaultAsync(m => m.ParticipanteID == id);
            if (participante == null)
            {
                return NotFound();
            }

            return View(participante);
        }

        // GET: Participantes/Create
        public IActionResult Create(int? churrascoID)
        {
            if (churrascoID == null)
                return NotFound();

            Participante p = new Participante();
            p.ComBebida = true;
            Churrasco churras = _context.Churrascos.FirstOrDefault(c => c.ChurrascoID == churrascoID);
            ViewBag.ValorCom = churras.ValorSugeridoComBebida;
            ViewBag.ValorSem = churras.ValorSugeridoSemBebida;
            p.ValorContribuicao = churras.ValorSugeridoComBebida;
            p.ChurrascoID = (int)churrascoID;

            return View(p);
        }

        // POST: Participantes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Participante participante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(participante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), "Churrascos", new { id = participante.ChurrascoID });
            }
            return View(participante);
        }

        // GET: Participantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var participante = await _context.Participantes.SingleOrDefaultAsync(m => m.ParticipanteID == id);
            if (participante == null)
                return NotFound();

            Churrasco churras = _context.Churrascos.FirstOrDefault(c => c.ChurrascoID == participante.ChurrascoID);
            ViewBag.ValorCom = churras.ValorSugeridoComBebida;
            ViewBag.ValorSem = churras.ValorSugeridoSemBebida;

            return View(participante);
        }

        // POST: Participantes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Participante participante)
        {
            if (id != participante.ParticipanteID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(participante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParticipanteExists(participante.ParticipanteID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), "Churrascos", new { id = participante.ChurrascoID });
            }
            return View(participante);
        }

        // GET: Participantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participante = await _context.Participantes
                .SingleOrDefaultAsync(m => m.ParticipanteID == id);
            if (participante == null)
            {
                return NotFound();
            }

            return View(participante);
        }

        // POST: Participantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var participante = await _context.Participantes.SingleOrDefaultAsync(m => m.ParticipanteID == id);
            _context.Participantes.Remove(participante);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), "Churrascos", new { id = participante.ChurrascoID });
        }

        private bool ParticipanteExists(int id)
        {
            return _context.Participantes.Any(e => e.ParticipanteID == id);
        }
    }
}
