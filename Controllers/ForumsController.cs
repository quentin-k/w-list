using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using w_list.Data;
using w_list.Models;

namespace w_list.Controllers
{
    public class ForumsController : Controller
    {
        private readonly WListContext _context;

        public ForumsController(WListContext context)
        {
            _context = context;
        }

        // GET: Forums
        public async Task<IActionResult> Index()
        {
            return View(await _context.ForumModel.ToListAsync());
        }

        // GET: Forums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var forumModel = await _context.ForumModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (forumModel == null)
            {
                return NotFound();
            }

            return View(forumModel);
        }

        // GET: Forums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Forums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Locked,IsPublic,AllowedWord1,AllowedWord2,AllowedWord3,AllowedWord4,AllowedWord5,AllowedWord6,AllowedWord7,AllowedWord8,AllowedWord9,AllowedWord10")] ForumModel forumModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(forumModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(forumModel);
        }

        // GET: Forums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var forumModel = await _context.ForumModel.FindAsync(id);
            if (forumModel == null)
            {
                return NotFound();
            }
            return View(forumModel);
        }

        // POST: Forums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Locked,IsPublic,AllowedWord1,AllowedWord2,AllowedWord3,AllowedWord4,AllowedWord5,AllowedWord6,AllowedWord7,AllowedWord8,AllowedWord9,AllowedWord10")] ForumModel forumModel)
        {
            if (id != forumModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(forumModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ForumModelExists(forumModel.Id))
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
            return View(forumModel);
        }

        // GET: Forums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var forumModel = await _context.ForumModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (forumModel == null)
            {
                return NotFound();
            }

            return View(forumModel);
        }

        // POST: Forums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var forumModel = await _context.ForumModel.FindAsync(id);
            _context.ForumModel.Remove(forumModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ForumModelExists(int id)
        {
            return _context.ForumModel.Any(e => e.Id == id);
        }
    }
}
