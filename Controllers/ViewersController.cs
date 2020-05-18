using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Information_Wiki_Final.Models;
using Microsoft.AspNetCore.Authorization;

namespace Information_Wiki_Final.Controllers
{
    [Authorize(Roles = "viewer")]
    public class ViewersController : Controller
    {
        private readonly Information_Wiki_DataContext _context;

        public ViewersController(Information_Wiki_DataContext context)
        {
            _context = context;
        }

        // GET: Viewers
        public async Task<IActionResult> Index()
        {

            var viewers = (from viewer in _context.Viewer
                           where viewer.Email.Equals(User.Identity.Name)
                           select viewer);
            return View(await viewers.ToListAsync());
        }

        // GET: Viewers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewer = await _context.Viewer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (viewer == null)
            {
                return NotFound();
            }

            return View(viewer);
        }

       

       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewer = await _context.Viewer.FindAsync(id);
            if (viewer == null)
            {
                return NotFound();
            }
            return View(viewer);
        }

        // POST: Viewers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email")] Viewer viewer)
        {
            if (id != viewer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ViewerExists(viewer.Id))
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
            return View(viewer);
        }

        // GET: Viewers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewer = await _context.Viewer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (viewer == null)
            {
                return NotFound();
            }

            return View(viewer);
        }

        // POST: Viewers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var viewer = await _context.Viewer.FindAsync(id);
            _context.Viewer.Remove(viewer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ViewerExists(int id)
        {
            return _context.Viewer.Any(e => e.Id == id);
        }
    }
}
