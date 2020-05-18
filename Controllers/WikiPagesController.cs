using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Information_Wiki_Final.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Information_Wiki_Final.Controllers
{
    public class WikiPagesController : Controller
    {
        private readonly Information_Wiki_DataContext _context;

        public WikiPagesController(Information_Wiki_DataContext context)
        {
            _context = context;
        }

        // GET: WikiPages

        [Authorize(Roles = "author, viewer")]
        public async Task<IActionResult> Index()
        {
            var information_Wiki_DataContext = _context.WikiPage.Include(w => w.Author);

            return View(await information_Wiki_DataContext.ToListAsync());
        }

        // GET: WikiPages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wikiPage = await _context.WikiPage
                .Include(w => w.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wikiPage == null)
            {
                return NotFound();
            }

            return View(wikiPage);
        }

        // GET: WikiPages/Create

        [Authorize(Roles = "author")]
        public IActionResult Create()
        {
           
            return View();
        }

        // POST: WikiPages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "author")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Content,LastModifed")] WikiPage wikiPage)
        {


            var author = (from auth in _context.Author
                          where auth.Email.Equals(User.Identity.Name)
                          select auth).FirstOrDefault();
            if (ModelState.IsValid)
            {
                wikiPage.AuthorId = author.Id;
                _context.Add(wikiPage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
            return View(wikiPage);
        }

        // GET: WikiPages/Edit/5
        [Authorize(Roles = "author")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wikiPage = await _context.WikiPage.FindAsync(id);
            if (wikiPage == null)
            {
                return NotFound();
            }
          
            return View(wikiPage);
        }

        // POST: WikiPages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "author")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,LastModifed,AuthorId")] WikiPage wikiPage)
        {
            if (id != wikiPage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wikiPage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WikiPageExists(wikiPage.Id))
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
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "Id", wikiPage.AuthorId);
            return View(wikiPage);
        }

        // GET: WikiPages/Delete/5

        [Authorize(Roles = "author")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wikiPage = await _context.WikiPage
                .Include(w => w.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wikiPage == null)
            {
                return NotFound();
            }

            return View(wikiPage);
        }

        // POST: WikiPages/Delete/5
        [Authorize(Roles = "author")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wikiPage = await _context.WikiPage.FindAsync(id);
            _context.WikiPage.Remove(wikiPage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WikiPageExists(int id)
        {
            return _context.WikiPage.Any(e => e.Id == id);
        }
    }
}
