﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using PropertyRentals.Models;

namespace PropertyRentals.Controllers
{
    [Authorize(Roles = "Owner")]
    public class ManagersController : Controller
    {
        private readonly PropertyRentalDbContext _context;

        public ManagersController(PropertyRentalDbContext context)
        {
            _context = context;
        }

        // GET: Managers
        public async Task<IActionResult> Index()
        {
            var propertyRentalDbContext = _context.Managers.Include(m => m.User);



            var managersData = await (from m in _context.Managers
                                          join u in _context.Users on m.UserId equals u.UserId 
                                          select new
                                          {
                                              Managers = m,
                                              UserDetails = u
                                          }).ToListAsync();

            ViewBag.ManagersData = managersData;
            return View(await propertyRentalDbContext.ToListAsync());
            //return View();
        }

        // GET: Managers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _context.Managers
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.ManagerId == id);
            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
        }

        // GET: Managers/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: Managers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ManagerId,UserId,FirstName,LastName,Email,Phone")] Manager manager)
        {
           var user = (from u in _context.Users
                      where u.UserId == manager.UserId
                      select u).FirstOrDefault();

            user.UserType = "Manager";
            _context.Users.Update(user);

            manager.FirstName = "-";
            manager.LastName = "-";
            manager.Email = "-";
            manager.Phone = "-";

            _context.Add(manager);

            var tenant = (from t in _context.Tenants
                          where t.UserId == user.UserId
                          select t).FirstOrDefault();


            _context.Tenants.Remove(tenant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
           
        }

        // GET: Managers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _context.Managers.FindAsync(id);
            if (manager == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", manager.UserId);
            return View(manager);
        }

        // POST: Managers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ManagerId,UserId,FirstName,LastName,Email,Phone")] Manager manager)
        {
            if (id != manager.ManagerId)
            {
                return NotFound();
            }


                try
                {
                    _context.Update(manager);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManagerExists(manager.ManagerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            //ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", manager.UserId);
            //return View(manager);
        }

        // GET: Managers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _context.Managers
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.ManagerId == id);
            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
        }

        // POST: Managers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var manager = await _context.Managers.FindAsync(id);

            
            if (manager != null)
            {
                var user = (from u in _context.Users
                            where u.UserId == manager.UserId
                            select u).FirstOrDefault();
                user.UserType = "Tenant";
                _context.Managers.Remove(manager);
                _context.Update(user);

                Tenant tenant = new Tenant
                {
                    UserId = user.UserId,
                    FirstName = "-",
                    LastName = "-",
                    Email = "-",
                    Phone = "-",
                };

                _context.Add(tenant);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManagerExists(int id)
        {
            return _context.Managers.Any(e => e.ManagerId == id);
        }
    }
}
