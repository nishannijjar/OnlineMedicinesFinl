using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public class FeedbackController : Controller
        {
            private readonly OnlineMedicinesContext _context;

            public FeedbackController(OnlineMedicinesContext context)
            {
                _context = context;
            }

            // GET: Feedback
            public async Task<IActionResult> Index()
            {
                return View(await _context.Feedback.ToListAsync());
            }

            // GET: Feedback/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var Feedback = await _context.Feedback
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (Feedback == null)
                {
                    return NotFound();
                }

                return View(Feedback);
            }

            // GET: Feedback/Create
            public IActionResult Create()
            {
                return View();
            }

            // POST: Feedback/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("Id,Name,Email,Contact,Message")] Feedback Feedback)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(Feedback);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(Feedback);
            }

            // GET: Feedback/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var Feedback = await _context.Feedback.FindAsync(id);
                if (Feedback == null)
                {
                    return NotFound();
                }
                return View(Feedback);
            }

            // POST: Feedback/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Contact,Message")] Feedback Feedback)
            {
                if (id != Feedback.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(Feedback);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!FeedbackExists(Feedback.Id))
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
                return View(Feedback);
            }

            // GET: Feedback/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var Feedback = await _context.Feedback
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (Feedback == null)
                {
                    return NotFound();
                }

                return View(Feedback);
            }

            // POST: Feedback/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var Feedback = await _context.Feedback.FindAsync(id);
                _context.Feedback.Remove(Feedback);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool FeedbackExists(int id)
            {
                return _context.Feedback.Any(e => e.Id == id);
            }
        }
    }
}
