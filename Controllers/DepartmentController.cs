using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bulk_Email_Sending_Groupwise.Models;
using System.Net.Mail;
using System.Net;

namespace Bulk_Email_Sending_Groupwise.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly BulkDbContext _context;

        private const string SmtpServer = "smtp.gmail.com";
        private const int SmtpPort = 587;
        private const string SmtpUsername = "rohitgabane1234@gmail.com";
        private const string SmtpPassword = "fzir fvrv rjnf xtzq";
        public DepartmentController(BulkDbContext context)
        {
            _context = context;
        }

        // GET: Department
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Department.ToListAsync());
            var departments = await _context.Department
       .Include(d => d.EmpDeptMapping) 
       .ToListAsync();

            return View(departments);
        }

        // GET: Department/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Department
                .FirstOrDefaultAsync(m => m.Dept_Id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Department/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Department/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Dept_Id,Dept_Name")] Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Department/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Department.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Department/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Dept_Id,Dept_Name")] Department department)
        {
            if (id != department.Dept_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Dept_Id))
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
            return View(department);
        }

        // GET: Department/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Department
                .FirstOrDefaultAsync(m => m.Dept_Id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Department.FindAsync(id);
            if (department != null)
            {
                _context.Department.Remove(department);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id)
        {
            return _context.Department.Any(e => e.Dept_Id == id);
        }





        public async Task<IActionResult> SendBirthdayEmails(int id)
        {
            var department = await _context.Department
                .Include(d => d.EmpDeptMapping)
                .ThenInclude(de => de.Employee)
                .FirstOrDefaultAsync(m => m.Dept_Id == id);

            if (department == null)
            {
                return NotFound();
            }

            foreach (var detailsEmp in department.EmpDeptMapping)
            {
                var employee = detailsEmp.Employee;

                //if (IsBirthdayToday(employee.BirthDate))
                //{
                //    await SendEmailToEmployee(employee.Email_ID, $"Happy Birthday, {employee.FirstName}!",
                //        $"Dear {employee.FirstName},\nWishing you a fantastic birthday! 🎉🎂");
                //}
                if (IsBirthdayToday(employee.BirthDate))
                {
                    string subject = $"Good Evening, {employee.FirstName}! Greetings from {department.Dept_Name} Department";
                    //string body = $"Dear {employee.FirstName},\nWishing you a fantastic birthday! 🎉🎂";
                    string body =  $@"
            <!DOCTYPE html>
            <html lang='en'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <style>
                    body {{
                        font-family: 'Arial', sans-serif;
                        background-color: #ffd700; /* Gold */
                        padding: 20px;
                        margin: 0;
                    }}
                    .container {{
                        max-width: 600px;
                        margin: 0 auto;
                        background-color: #ffffff; /* White */
                        padding: 20px;
                        border-radius: 10px;
                        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                    }}
                    h1 {{
                        color: #ff4500; /* OrangeRed */
                    }}
                    p {{
                        color: #333;
                    }}
                    .emoji {{
                        font-size: 1.5em;
                        margin-right: 5px;
                    }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <h1> Good Evening, {employee.FirstName}! </h1>
                    <p>Dear {employee.FirstName},</p>
                    <p>I hope this message finds you well. Unfortunately, I bring news that our cricket game scheduled for tomorrow has to be canceled due to unforeseen circumstances. Regrettably, some of our key players are unavailable, and we believe it's in the best interest of the team to reschedule the match.

We understand this may cause inconvenience, and we sincerely apologize for any disruptions to your plans. We value your commitment and enthusiasm for the game, and we assure you that we are working to address the issues and organize another match soon.

Thank you for your understanding, and we look forward to your continued support</p>
                    //<p>So sorry for inconvinent </p>
                    <p>Best regards,<br>Your {department.Dept_Name} Department</p>
                </div>
            </body>
            </html>
        ";
                    await SendEmailToEmployee(employee.Email_ID, subject, body);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private bool IsBirthdayToday(DateOnly birthDate)
        {
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            return birthDate.Month == currentDate.Month && birthDate.Day == currentDate.Day;
        }

        private async Task SendEmailToEmployee(string toEmail, string subject, string body)
        {
            using (SmtpClient smtpClient = new SmtpClient(SmtpServer, SmtpPort))
            {
                smtpClient.Credentials = new NetworkCredential(SmtpUsername, SmtpPassword);
                smtpClient.EnableSsl = true;

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(SmtpUsername),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(toEmail);

                await smtpClient.SendMailAsync(mailMessage);
            }
            Console.WriteLine($"Birthday mail to {toEmail}");

        }
    }
}
