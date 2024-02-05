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
    public class MenusController : Controller
    {
        private readonly BulkDbContext _context;

        public MenusController(BulkDbContext context)
        {
            _context = context;
        }

        // GET: Menus
        public async Task<IActionResult> Index()
        {
            return View(await _context.Menus.ToListAsync());
        }

        // GET: Menus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menus = await _context.Menus
                .FirstOrDefaultAsync(m => m.MenuID == id);
            if (menus == null)
            {
                return NotFound();
            }

            return View(menus);
        }

        // GET: Menus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Menus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MenuID,MenuName,ParentID")] Menus menus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menus);
        }

        // GET: Menus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menus = await _context.Menus.FindAsync(id);
            if (menus == null)
            {
                return NotFound();
            }
            return View(menus);
        }

        // POST: Menus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MenuID,MenuName,ParentID")] Menus menus)
        {
            if (id != menus.MenuID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenusExists(menus.MenuID))
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
            return View(menus);
        }

        // GET: Menus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menus = await _context.Menus
                .FirstOrDefaultAsync(m => m.MenuID == id);
            if (menus == null)
            {
                return NotFound();
            }

            return View(menus);
        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menus = await _context.Menus.FindAsync(id);
            if (menus != null)
            {
                _context.Menus.Remove(menus);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenusExists(int id)
        {
            return _context.Menus.Any(e => e.MenuID == id);
        }
    }
}
