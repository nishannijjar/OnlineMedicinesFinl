using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineMedicines.Data;
using OnlineMedicines.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMedicines.Controllers
{
    namespace OnlineMedicines.Controllers
    {
        public class MedicineController : Controller
        {
            private readonly OnlineMedicinesContext _context;

            public MedicineController(OnlineMedicinesContext context)
            {
                _context = context;
            }

            // GET: Medicine
            public async Task<IActionResult> Index()
            {

                // if the admin is Login then only this page can be viewed
                if (HttpContext.Session.GetString("Id") != null || HttpContext.Session.GetString("Name") != null)
                {
                    var value = HttpContext.Session.GetString("Id");
                    var name = HttpContext.Session.GetString("Name");
                    var login = _context.Login.Where(x => x.Id == Convert.ToInt32(value)).FirstOrDefault();

                    if (name == "Admin@admin.com")
                    {
                        return View(await _context.Medicine.ToListAsync());
                    }
                    else
                    {
                        if (_context.Medicine.ToList().Count == 0)
                        {
                            return RedirectToAction(nameof(MedicineNotFound));
                        }
                        else
                            return RedirectToAction("Index", "Medicine");
                    }
                }
                else
                {
                    return RedirectToAction("UserLogin", "Logins");
                }
                }

            // GET: Medicine/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var Medicine = await _context.Medicine
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (Medicine == null)
                {
                    return NotFound();
                }

                return View(Medicine);
            }

            // GET: Medicine/Create
            public IActionResult MedicineNotFound()
            {
                return View();
            }
             public IActionResult Create()
            {
                return View();
            }

            // POST: Medicine/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("Id,MeidicineName,Strips,Company,Rate")] Medicine Medicine)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(Medicine);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(Medicine);
            }

            // GET: Medicine/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var Medicine = await _context.Medicine.FindAsync(id);
                if (Medicine == null)
                {
                    return NotFound();
                }
                return View(Medicine);
            }

            // POST: Medicine/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,MeidicineName,Strips,Company,Rate")] Medicine Medicine)
            {
                if (id != Medicine.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(Medicine);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!MedicineExists(Medicine.Id))
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
                return View(Medicine);
            }

            // GET: Medicine/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var Medicine = await _context.Medicine
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (Medicine == null)
                {
                    return NotFound();
                }

                return View(Medicine);
            }

            // POST: Medicine/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var Medicine = await _context.Medicine.FindAsync(id);
                _context.Medicine.Remove(Medicine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool MedicineExists(int id)
            {
                return _context.Medicine.Any(e => e.Id == id);
            }
        }
    }
}
