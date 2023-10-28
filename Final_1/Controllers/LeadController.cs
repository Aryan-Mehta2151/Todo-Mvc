using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Final_1.Data;
using Final_1.Models;
using Microsoft.AspNetCore.Authorization;

namespace Final_1.Controllers
{
    [Authorize]
    public class LeadController : Controller
    {
        private readonly ApplicationDBContext _context;

        public LeadController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Lead
        public async Task<IActionResult> Index()
        {
            var mainLeadsWithEmail = await _context.MainLead
            .Where(mainLead => mainLead.Email == HttpContext.Session.GetString("email"))
            .ToListAsync();

            if (mainLeadsWithEmail != null && mainLeadsWithEmail.Any())
            {
                return View(mainLeadsWithEmail);
            }
            else
            {
                return Problem(HttpContext.Session.GetString("email"));
            }
     
        }

        // GET: Lead/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MainLead == null)
            {
                return NotFound();
            }

            var leadUserEntity = await _context.MainLead
                .FirstOrDefaultAsync(m => m.id == id);
            if (leadUserEntity == null)
            {
                return NotFound();
            }

            return View(leadUserEntity);
        }

        // GET: Lead/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lead/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,FirstName,LastName,Email,Source")] LeadUserEntity leadUserEntity)
        {
            if (ModelState.IsValid)
            {
                leadUserEntity.Email = HttpContext.Session.GetString("email");
                _context.Add(leadUserEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); 
            }
            return View(leadUserEntity);
        }

        // GET: Lead/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MainLead == null)
            {
                return NotFound();
            }

            var leadUserEntity = await _context.MainLead.FindAsync(id);
            if (leadUserEntity == null)
            {
                return NotFound();
            }
            return View(leadUserEntity);
        }

        // POST: Lead/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,FirstName,LastName,Email,Source")] LeadUserEntity leadUserEntity)
        {
            if (id != leadUserEntity.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leadUserEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeadUserEntityExists(leadUserEntity.id))
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
            return View(leadUserEntity);
        }

        // GET: Lead/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MainLead == null)
            {
                return NotFound();
            }

            var leadUserEntity = await _context.MainLead
                .FirstOrDefaultAsync(m => m.id == id);
            if (leadUserEntity == null)
            {
                return NotFound();
            }

            return View(leadUserEntity);
        }

        // POST: Lead/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MainLead == null)
            {
                return Problem("Entity set 'ApplicationDBContext.MainLead'  is null.");
            }
            var leadUserEntity = await _context.MainLead.FindAsync(id);
            if (leadUserEntity != null)
            {
                _context.MainLead.Remove(leadUserEntity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeadUserEntityExists(int id)
        {
          return (_context.MainLead?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
