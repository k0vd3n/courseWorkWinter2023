using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication01.Data;
using WebApplication01.Models;

namespace WebApplication01.Views.Delivery
{
    [Authorize(Roles = "Vendor, PurchaseManager, Customer, OrderManager, Admin")]
    public class DeliveryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeliveryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Delivery
        public async Task<IActionResult> Index()
        {
            return View(await _context.Delivery.ToListAsync());
        }

        // GET: Delivery/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliver = await _context.Delivery
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deliver == null)
            {
                return NotFound();
            }

            return View(deliver);
        }

        // GET: Delivery/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Delivery/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DeliveryType")] Deliver deliver)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deliver);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deliver);
        }

        // GET: Delivery/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliver = await _context.Delivery.FindAsync(id);
            if (deliver == null)
            {
                return NotFound();
            }
            return View(deliver);
        }

        // POST: Delivery/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DeliveryType")] Deliver deliver)
        {
            if (id != deliver.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deliver);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliverExists(deliver.Id))
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
            return View(deliver);
        }

        // GET: Delivery/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliver = await _context.Delivery
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deliver == null)
            {
                return NotFound();
            }

            return View(deliver);
        }

        // POST: Delivery/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deliver = await _context.Delivery.FindAsync(id);
            if (deliver != null)
            {
                _context.Delivery.Remove(deliver);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliverExists(int id)
        {
            return _context.Delivery.Any(e => e.Id == id);
        }
    }
}
