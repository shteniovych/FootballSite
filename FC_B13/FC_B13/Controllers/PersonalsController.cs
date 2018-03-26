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
    public class PersonalsController : Controller
    {
        private readonly FootballTeamContext _context;

        public PersonalsController(FootballTeamContext context)
        {
            _context = context;
        }

        // GET: Personals
        public async Task<IActionResult> Index()
        {
            var footballTeamContext = _context.Personal.Include(p => p.Contract).Include(p=>p.PersonalProfession).ThenInclude(p=>p.Profession);
            return View(await footballTeamContext.ToListAsync());
        }

        // GET: Personals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personal = await _context.Personal
                .Include(p => p.Contract)
                .SingleOrDefaultAsync(m => m.PersonalId == id);
            if (personal == null)
            {
                return NotFound();
            }

            return View(personal);
        }

        // GET: Personals/Create
        public IActionResult Create()
        {
            ViewData["ContractId"] = new SelectList(_context.Contract, "ContractId", "ContractId");
            return View();
        }

        // POST: Personals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonalId,Name,DataOfBirthday,PhoneNumber,Address,ProfessionId,InForceContract,ContractId,Professions")] Personal personal)
        {
            if (ModelState.IsValid)
            {
                var professions = personal.Professions.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var profession in professions)
                {
                    var professionDb = _context.Profession.FirstOrDefault(s => s.ProfessionName == profession);
                    if (professionDb != null)
                    {
                        _context.Add(new PersonalProfession { Profession = professionDb, Personal = personal });
                    }
                }
                _context.Add(personal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContractId"] = new SelectList(_context.Contract, "ContractId", "ContractId", personal.ContractId);
            return View(personal);
        }

        // GET: Personals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personal = await _context.Personal.SingleOrDefaultAsync(m => m.PersonalId == id);

            //======================Дістаємо список команд, які тренує поточний тренер=============================
            string listOfProfessions = "";
            foreach (var c in _context.Personal.Include(c => c.Contract).Include(c => c.PersonalProfession).ThenInclude(t => t.Profession))
            {
                foreach (var pp in c.PersonalProfession.Select(t => t.Profession).Select(p => p.ProfessionName))
                {
                    if (c.PersonalId == personal.PersonalId)
                    {
                        listOfProfessions += pp + ",";
                    }
                }
            }
            //=======================================================================================================

            if (personal == null)
            {
                return NotFound();
            }
            ViewData["ContractId"] = new SelectList(_context.Contract, "ContractId", "ContractId", personal.ContractId);
            ViewData["Professions"] = listOfProfessions;
            return View(personal);
        }

        // POST: Personals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonalId,Name,DataOfBirthday,PhoneNumber,Address,ProfessionId,ContractId,Professions")] Personal personal)
        {
            if (id != personal.PersonalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var professions = personal.Professions.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    var teamsToRemove = _context.PersonalProfession.Where(t => t.PersonalId == personal.PersonalId).ToList();

                    foreach (var tToRemove in teamsToRemove)
                    {
                        _context.PersonalProfession.Remove(tToRemove);
                    }

                    _context.SaveChanges();


                    foreach (var t in professions)
                    {
                        var professionDb = _context.Profession.FirstOrDefault(s => s.ProfessionName == t);
                        if (professionDb != null)
                        {
                            _context.Add(new PersonalProfession { Profession = professionDb, Personal = personal });
                        }
                    }

                    _context.Update(personal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonalExists(personal.PersonalId))
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
            ViewData["ContractId"] = new SelectList(_context.Contract, "ContractId", "ContractId", personal.ContractId);
            return View(personal);
        }

        // GET: Personals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personal = await _context.Personal
                .Include(p => p.Contract)
                .SingleOrDefaultAsync(m => m.PersonalId == id);
            if (personal == null)
            {
                return NotFound();
            }

            return View(personal);
        }

        // POST: Personals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personal = await _context.Personal.SingleOrDefaultAsync(m => m.PersonalId == id);
            _context.Personal.Remove(personal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonalExists(int id)
        {
            return _context.Personal.Any(e => e.PersonalId == id);
        }
    }
}
