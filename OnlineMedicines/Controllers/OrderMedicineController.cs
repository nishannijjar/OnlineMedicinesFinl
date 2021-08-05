using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnlineMedicines.Data;
using OnlineMedicines.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMedicines.Controllers
{
    namespace OnlineMedicines.Controllers
    {
        public class OrderMedicineController : Controller
        {
            private readonly OnlineMedicinesContext _context;

            public OrderMedicineController(OnlineMedicinesContext context)
            {
                _context = context;
            }

            // GET: OrderMedicine
            public async Task<IActionResult> Index()
            {
                var value = HttpContext.Session.GetString("Id");

                if (HttpContext.Session.GetString("Id") != null)                
                    return View(await _context.OrderMedicine.Where(x => x.UserId == Convert.ToInt32(value)).ToListAsync());
                else
                    return RedirectToAction("UserLogin", "Logins");
            }

            // GET: OrderMedicine/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var OrderMedicine = await _context.OrderMedicine
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (OrderMedicine == null)
                {
                    return NotFound();
                }

                return View(OrderMedicine);
            }

            // GET: OrderMedicine/Create
            public IActionResult Create()
            {
                return View();
            }

            // POST: OrderMedicine/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("Id,Name,Contact,MeidicineName,Strips,Address,UserId")] OrderMedicine OrderMedicine)
            {
                if (ModelState.IsValid)
                {
                    OrderMedicine.UserId = Convert.ToInt32(HttpContext.Session.GetString("Id"));
                    _context.Add(OrderMedicine);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(OrderMedicine);
            }

            // GET: OrderMedicine/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var OrderMedicine = await _context.OrderMedicine.FindAsync(id);
                if (OrderMedicine == null)
                {
                    return NotFound();
                }
                return View(OrderMedicine);
            }

            // POST: OrderMedicine/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Contact,MeidicineName,Strips,Address")] OrderMedicine OrderMedicine)
            {
                if (id != OrderMedicine.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(OrderMedicine);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!OrderMedicineExists(OrderMedicine.Id))
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
                return View(OrderMedicine);
            }

            // GET: OrderMedicine/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var OrderMedicine = await _context.OrderMedicine
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (OrderMedicine == null)
                {
                    return NotFound();
                }

                return View(OrderMedicine);
            }

            // POST: OrderMedicine/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var OrderMedicine = await _context.OrderMedicine.FindAsync(id);
                _context.OrderMedicine.Remove(OrderMedicine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool OrderMedicineExists(int id)
            {
                return _context.OrderMedicine.Any(e => e.Id == id);
            }
        }
    }
}
