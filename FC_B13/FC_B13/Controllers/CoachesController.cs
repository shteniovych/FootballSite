﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FC_B13.Models.DB;

namespace FC_B13.Controllers
{
    public class CoachesController : Controller
    {
        private readonly FootballTeamContext _context;

        public CoachesController(FootballTeamContext context)
        {
            _context = context;
        }

        // GET: Coaches
        public async Task<IActionResult> Index()
        {
            var footballTeamContext = _context.Coach.Include(c => c.Contract).Include(c=>c.TeamCoach).ThenInclude(t=>t.Team);
            return View(await footballTeamContext.ToListAsync());
        }

        // GET: Coaches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coach = await _context.Coach
                .Include(c => c.Contract)
                .SingleOrDefaultAsync(m => m.CoachId == id);
            if (coach == null)
            {
                return NotFound();
            }

            return View(coach);
        }

        // GET: Coaches/Create
        public IActionResult Create()
        {
            ViewData["ContractId"] = new SelectList(_context.Contract, "ContractId", "ContractId");
            return View();
        }

        // POST: Coaches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CoachId,Name,DateOfBirhday,Address,PhoneNumber,PositionInTeam,Teams,ContractId")] Coach coach)
        {
            if (ModelState.IsValid)
            {
                var teams = coach.Teams.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach(var team in teams)
                {
                    var teamDb = _context.Team.FirstOrDefault(s => s.NameTeam == team);
                    if(teamDb != null)
                    {
                        _context.Add(new TeamCoach { Team = teamDb, Coach = coach });
                    }
                }
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContractId"] = new SelectList(_context.Contract, "ContractId", "ContractId", coach.ContractId);
            return View(coach);
        }

        // GET: Coaches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coach = await _context.Coach.SingleOrDefaultAsync(m => m.CoachId == id);
            
            //======================Дістаємо список команд, які тренує поточний тренер=============================
            string listOfTeams = "";
            foreach(var c in _context.Coach.Include(c => c.Contract).Include(c => c.TeamCoach).ThenInclude(t => t.Team))
            {
                foreach(var coachTeam in c.TeamCoach.Select(t=>t.Team).Select(p=>p.NameTeam))
                {
                    if(c.CoachId == coach.CoachId)
                    {
                        listOfTeams += coachTeam+",";
                    }
                }
            }
            //=======================================================================================================


            if (coach == null)
            {
                return NotFound();
            }
            ViewData["ContractId"] = new SelectList(_context.Contract, "ContractId", "ContractId", coach.ContractId);
            ViewData["Teams"] = listOfTeams;
            return View(coach);
        }

        // POST: Coaches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CoachId,Name,DateOfBirhday,Address,PhoneNumber,PositionInTeam,TeamCoach,Teams,ContractId")] Coach coach)
        {
            if (id != coach.CoachId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var teams = coach.Teams.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    var teamsToRemove = _context.TeamCoach.Where(t => t.CoachId==coach.CoachId).ToList();

                    foreach(var tToRemove in teamsToRemove)
                    {
                        _context.TeamCoach.Remove(tToRemove);
                    }

                    _context.SaveChanges();


                    foreach (var t in teams)
                    {
                        var teamDb = _context.Team.FirstOrDefault(s => s.NameTeam == t);
                        if (teamDb != null)
                        {
                            _context.Add(new TeamCoach { Team = teamDb, Coach = coach });
                        }
                    }

                    _context.Update(coach);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoachExists(coach.CoachId))
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
            ViewData["ContractId"] = new SelectList(_context.Contract, "ContractId", "ContractId", coach.ContractId);
            return View(coach);
        }

        // GET: Coaches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coach = await _context.Coach
                .Include(c => c.Contract)
                .SingleOrDefaultAsync(m => m.CoachId == id);
            if (coach == null)
            {
                return NotFound();
            }

            return View(coach);
        }

        // POST: Coaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coach = await _context.Coach.SingleOrDefaultAsync(m => m.CoachId == id);
            _context.Coach.Remove(coach);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoachExists(int id)
        {
            return _context.Coach.Any(e => e.CoachId == id);
        }
    }
}
