﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PaperwoffNet.Infrastructure;
using PaperwoffNet.Infrastructure.Data;

namespace PaperwoffNet.Web.Controllers
{
    public class DisponibilidadController : Controller
    {
        private readonly PaperwoffNetDbContext _context;

        public DisponibilidadController(PaperwoffNetDbContext context)
        {
            _context = context;
        }

        // GET: Disponibilidad
        public async Task<IActionResult> Index()
        {
            var paperwoffNetDbContext = _context.Disponibilidad.Include(d => d.TutoresIdTutoresNavigation);
            return View(await paperwoffNetDbContext.ToListAsync());
        }

        // GET: Disponibilidad/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disponibilidad = await _context.Disponibilidad
                .Include(d => d.TutoresIdTutoresNavigation)
                .FirstOrDefaultAsync(m => m.IdDisponibilidad == id);
            if (disponibilidad == null)
            {
                return NotFound();
            }

            return View(disponibilidad);
        }

        // GET: Disponibilidad/Create
        public IActionResult Create()
        {
            ViewData["TutoresIdTutores"] = new SelectList(_context.Tutores, "IdTutores", "IdTutores");
            return View();
        }

        // POST: Disponibilidad/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDisponibilidad,Fecha,HoraInicio,HoraFin,TutoresIdTutores")] Disponibilidad disponibilidad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disponibilidad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TutoresIdTutores"] = new SelectList(_context.Tutores, "IdTutores", "IdTutores", disponibilidad.TutoresIdTutores);
            return View(disponibilidad);
        }

        // GET: Disponibilidad/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disponibilidad = await _context.Disponibilidad.FindAsync(id);
            if (disponibilidad == null)
            {
                return NotFound();
            }
            ViewData["TutoresIdTutores"] = new SelectList(_context.Tutores, "IdTutores", "IdTutores", disponibilidad.TutoresIdTutores);
            return View(disponibilidad);
        }

        // POST: Disponibilidad/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdDisponibilidad,Fecha,HoraInicio,HoraFin,TutoresIdTutores")] Disponibilidad disponibilidad)
        {
            if (id != disponibilidad.IdDisponibilidad)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disponibilidad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisponibilidadExists(disponibilidad.IdDisponibilidad))
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
            ViewData["TutoresIdTutores"] = new SelectList(_context.Tutores, "IdTutores", "IdTutores", disponibilidad.TutoresIdTutores);
            return View(disponibilidad);
        }

        // GET: Disponibilidad/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disponibilidad = await _context.Disponibilidad
                .Include(d => d.TutoresIdTutoresNavigation)
                .FirstOrDefaultAsync(m => m.IdDisponibilidad == id);
            if (disponibilidad == null)
            {
                return NotFound();
            }

            return View(disponibilidad);
        }

        // POST: Disponibilidad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var disponibilidad = await _context.Disponibilidad.FindAsync(id);
            _context.Disponibilidad.Remove(disponibilidad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisponibilidadExists(long id)
        {
            return _context.Disponibilidad.Any(e => e.IdDisponibilidad == id);
        }
    }
}
