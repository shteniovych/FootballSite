using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FC_B13.Models.DB;

namespace FC_B13.Controllers
{
    public class InventarsController : Controller
    {
        private readonly FootballTeamContext _context;

        public InventarsController(FootballTeamContext context)
        {
            _context = context;
        }

        // GET: Inventars
        public async Task<IActionResult> Index()
        {
            var footballTeamContext = _context.Inventar.Include(i => i.Team);
            return View(await footballTeamContext.ToListAsync());
        }

        // GET: Inventars
        [HttpGet("Index/{teamId}")]
        public async Task<IActionResult> Index(int teamId)
        {
            var footballTeamContext = _context.Inventar.Include(i => i.Team).Where(i => i.TeamId == teamId);
            return View(await footballTeamContext.ToListAsync());
        }


        // GET: Inventars/Create
        public IActionResult Create()
        {
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "NameTeam");
            return View();
        }

        // POST: Inventars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InventarId,InventarName,Count,TeamId")] Inventar inventar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "NameTeam", inventar.TeamId);
            return View(inventar);
        }

        // GET: Inventars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventar = await _context.Inventar.SingleOrDefaultAsync(m => m.InventarId == id);
            if (inventar == null)
            {
                return NotFound();
            }
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "NameTeam", inventar.TeamId);
            return View(inventar);
        }

        // POST: Inventars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InventarId,InventarName,Count,TeamId")] Inventar inventar)
        {
            if (id != inventar.InventarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventarExists(inventar.InventarId))
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
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "NameTeam", inventar.TeamId);
            return View(inventar);
        }

        // GET: Inventars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventar = await _context.Inventar
                .Include(i => i.Team)
                .SingleOrDefaultAsync(m => m.InventarId == id);
            if (inventar == null)
            {
                return NotFound();
            }

            return View(inventar);
        }

        // POST: Inventars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventar = await _context.Inventar.SingleOrDefaultAsync(m => m.InventarId == id);
            _context.Inventar.Remove(inventar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventarExists(int id)
        {
            return _context.Inventar.Any(e => e.InventarId == id);
        }
    }
}
