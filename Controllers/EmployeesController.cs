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
    public class EmployeesController : Controller
    {
        private readonly BulkDbContext _context;

        public EmployeesController(BulkDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employee.ToListAsync());
        }


        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Emp_ID,FirstName,LastName,BirthDate,Email_ID")] Employee employee)
        {
            // Check if the email already exists
            if (_context.Employee.Any(e => e.Email_ID == employee.Email_ID))
            {
                ModelState.AddModelError("Email_ID", "Email already exists");
                return View(employee);
            }

            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
                [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Emp_ID,FirstName,LastName,BirthDate,Email_ID")] Employee employee)
        {
            if (id != employee.Emp_ID)
            {
                return NotFound();
            }

            // Check if the new email ID is unique
            if (!IsEmailUnique(employee.Email_ID, employee.Emp_ID))
            {
                ModelState.AddModelError("Email_ID", "The provided email address is already in use.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Emp_ID))
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
            return View(employee);
        }

        // Custom method to check if the email ID is unique
        private bool IsEmailUnique(string email, int empId)
        {
            return !_context.Employee.Any(e => e.Email_ID == email && e.Emp_ID != empId);
        }


        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.Emp_ID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            if (employee != null)
            {
                _context.Employee.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Emp_ID == id);
        }

        //// GET: Employees/AddMenu/5
        //public async Task<IActionResult> AddMenu(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    // Retrieve the employee from the database
        //    var employee = await _context.Employee
        //        .Include(e => e.EmpMenuMapping) 
        //        .ThenInclude(em => em.Menus)   
        //        .FirstOrDefaultAsync(e => e.Emp_ID == id);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }
        //    // Retrieve the list of menus from the database
        //    var menus = await _context.Menus.ToListAsync();
        //    // Pass the employee and menu list to the view
        //    ViewBag.EmployeeId = id;
        //    ViewBag.Menus = menus;
        //    return View();
        //}


        // GET: Employees/AddMenu/5
        public async Task<IActionResult> AddMenu(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Retrieve the employee from the database with associated menus
            var employee = await _context.Employee
                .Include(e => e.EmpMenuMapping)
                .ThenInclude(em => em.Menus)
                .FirstOrDefaultAsync(e => e.Emp_ID == id);

            if (employee == null)
            {
                return NotFound();
            }

            // Retrieve the list of menus from the database
            var allMenus = await _context.Menus.ToListAsync();

            // Get the menu IDs that are already associated with the employee
            var associatedMenuIds = employee.EmpMenuMapping.Select(em => em.MenuID).ToList();

            // Filter and give those are not associate
            var menus = allMenus.Where(m => !associatedMenuIds.Contains(m.MenuID)).ToList();

            // Pass the employee and menu list to the view
            ViewBag.EmployeeId = id;
            ViewBag.Menus = menus;

            return View();
        }


        // POST: Employees/AddMenu/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMenu(int id, int selectedMenuId)
        {
            var employee = await _context.Employee
                .Include(e => e.EmpMenuMapping) // Include the EmpMenuMapping relationship
                .FirstOrDefaultAsync(e => e.Emp_ID == id);
            if (employee == null)
            {
                return NotFound();
            }
            if (selectedMenuId != 0)
            {
                // Check if the menu is not already associated with the employee
                if (!employee.EmpMenuMapping.Any(em => em.MenuID == selectedMenuId))
                {
                    // Add the menu to the employee's EmpMenuMapping
                    employee.EmpMenuMapping.Add(new EmpMenuMapping { MenuID = selectedMenuId });
                    await _context.SaveChangesAsync();
                }
            }
            // Redirect to the "Index" action with the employee's ID
            return RedirectToAction("Index", new { id = id });
        }
        // GET: Employees/Details/5
        public async Task<IActionResult> MenuDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // Retrieve the employee from the database with associated EmpMenuMapping and Menus
            var employee = await _context.Employee
                .Include(e => e.EmpMenuMapping)
                .ThenInclude(em => em.Menus)
                .FirstOrDefaultAsync(e => e.Emp_ID == id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
    }
}
