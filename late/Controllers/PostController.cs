using System;
using System.Linq;
using System.Threading.Tasks;
using late.Data;
using late.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace late.Controllers
{
    [Authorize]
    public class PostController : BaseController
    {
        private readonly LateContext _context;

        public PostController(LateContext context)
        {
            _context = context;
        }

        // GET: Post
        public async Task<IActionResult> Index()
        {
            var posts = await _context.Posts.Include(p => p.Catalog).ToListAsync();
            return View(posts);
        }

        // GET: Post/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Post/Create
        public async Task<IActionResult> Create()
        {
            var catalogs = await _context.Catalog.AsNoTracking().Select(c => new SelectListItem(c.Title, c.Id.ToString())).ToArrayAsync();

            return View(new PostCreateViewModel()
            {
                CatalogSelectItems = catalogs
            });
        }

        // POST: Post/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Post post = new Post();
                post.Id = Guid.NewGuid();
                post.Catalog = await _context.Catalog.FindAsync(viewModel.Catalog);
                post.Time = DateTime.Now;
                post.Title = viewModel.Title;
                post.Content = viewModel.Content;

                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Post/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.Include(p => p.Catalog).FirstOrDefaultAsync(p => p.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            var catalogs = await _context.Catalog.AsNoTracking().Select(c => new SelectListItem(c.Title, c.Id.ToString())).ToArrayAsync();

            return View(new PostEditViewModel
            {
                Id = post.Id,
                Catalog = post.Catalog.Id,
                Title = post.Title,
                Content = post.Content,
                CatalogSelectItems = catalogs
            });
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PostEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Post post = await _context.Posts.FindAsync(viewModel.Id);
                if (post == null)
                {
                    return NotFound();
                }

                post.Title = viewModel.Title;
                post.Content = viewModel.Content;
                post.Catalog = await _context.Catalog.FindAsync(viewModel.Catalog);

                _context.Update(post);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Post/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(Guid id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
