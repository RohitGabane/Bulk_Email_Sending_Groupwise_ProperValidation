using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bulk_Email_Sending_Groupwise.Models;

namespace Bulk_Email_Sending_Groupwise.Controllers
{
    public class EmpMenuMappingsController : Controller
    {
        private readonly BulkDbContext _context;

        public EmpMenuMappingsController(BulkDbContext context)
        {
            _context = context;
        }

        // GET: EmpMenuMappings
        public async Task<IActionResult> Index()
        {
            var bulkDbContext = _context.EmpMenuMapping.Include(e => e.Employee).Include(e => e.Menus);
            return View(await bulkDbContext.ToListAsync());
        }

        // GET: EmpMenuMappings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empMenuMapping = await _context.EmpMenuMapping
                .Include(e => e.Employee)
                .Include(e => e.Menus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empMenuMapping == null)
            {
                return NotFound();
            }

            return View(empMenuMapping);
        }

        // GET: EmpMenuMappings/Create
        public IActionResult Create()
        {
            ViewData["EmpId"] = new SelectList(_context.Employee, "Emp_ID", "Email_ID");
            ViewData["MenuID"] = new SelectList(_context.Menus, "MenuID", "MenuID");
            return View();
        }

        // POST: EmpMenuMappings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmpId,MenuID")] EmpMenuMapping empMenuMapping)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empMenuMapping);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpId"] = new SelectList(_context.Employee, "Emp_ID", "Email_ID", empMenuMapping.EmpId);
            ViewData["MenuID"] = new SelectList(_context.Menus, "MenuID", "MenuID", empMenuMapping.MenuID);
            return View(empMenuMapping);
        }

        // GET: EmpMenuMappings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empMenuMapping = await _context.EmpMenuMapping.FindAsync(id);
            if (empMenuMapping == null)
            {
                return NotFound();
            }
            ViewData["EmpId"] = new SelectList(_context.Employee, "Emp_ID", "Email_ID", empMenuMapping.EmpId);
            ViewData["MenuID"] = new SelectList(_context.Menus, "MenuID", "MenuID", empMenuMapping.MenuID);
            return View(empMenuMapping);
        }

        // POST: EmpMenuMappings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmpId,MenuID")] EmpMenuMapping empMenuMapping)
        {
            if (id != empMenuMapping.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empMenuMapping);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpMenuMappingExists(empMenuMapping.Id))
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
            ViewData["EmpId"] = new SelectList(_context.Employee, "Emp_ID", "Email_ID", empMenuMapping.EmpId);
            ViewData["MenuID"] = new SelectList(_context.Menus, "MenuID", "MenuID", empMenuMapping.MenuID);
            return View(empMenuMapping);
        }

        // GET: EmpMenuMappings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empMenuMapping = await _context.EmpMenuMapping
                .Include(e => e.Employee)
                .Include(e => e.Menus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empMenuMapping == null)
            {
                return NotFound();
            }

            return View(empMenuMapping);
        }

        // POST: EmpMenuMappings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empMenuMapping = await _context.EmpMenuMapping.FindAsync(id);
            if (empMenuMapping != null)
            {
                _context.EmpMenuMapping.Remove(empMenuMapping);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpMenuMappingExists(int id)
        {
            return _context.EmpMenuMapping.Any(e => e.Id == id);
        }
    }
}
