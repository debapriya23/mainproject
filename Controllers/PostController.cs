using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuthRoleBased.Data;
using KOODLE.Models;
using KOODLE.Data.FileManager;

namespace KOODLE.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IFileManager _fileManager;

        public PostController(ApplicationDbContext context,
            IFileManager FileManage)
        {
            _context = context;
            _fileManager = FileManage;
        }

        // GET: Post
        public async Task<IActionResult> Index()
        {
            return View(await _context.Post.ToListAsync());
        }

        // GET: Post/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .FirstOrDefaultAsync(m => m.Post_Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Post/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Post/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Post_Id,Post_Title,Post_Txt,Image,Post_Date,Post_Like")] Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Post/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return View(new PostViewModel());
            }
            else
            {
                var Post = await _context.Post.FindAsync(id);

                return View(new PostViewModel
                {
                    Post_Id = Post.Post_Id,
                    Post_Title = Post.Post_Title,
                    Post_Like = Post.Post_Like,
                    Post_Txt = Post.Post_Txt,
                    Post_Date = Post.Post_Date

                });
            }
        }
    

        // POST: Post/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Post_Id,Post_Title,Post_Txt,Image,Post_Date,Post_Like")] PostViewModel vm)
        {
            var post = new Post
            {
                Post_Id = vm.Post_Id,
                Post_Title = vm.Post_Title,
                Post_Like = vm.Post_Like,
                Post_Txt = vm.Post_Txt,
                Post_Date = vm.Post_Date,
                Image =await _fileManager.SaveImage(vm.Image)

            };

            if (id != post.Post_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Post_Id))
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
            return View(post);
        }

        // GET: Post/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .FirstOrDefaultAsync(m => m.Post_Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Post.FindAsync(id);
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.Post_Id == id);
        }
    }
}
