using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Blog.Data;
using Blog.Models;
using System.Security.Claims;

namespace Blog
{
    public class AdminPostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminPostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdminPosts
        public async Task<IActionResult> Index()
        {
            var posts = _context.Posts.ToList();
            posts.ForEach(post =>
            {
                // Uzupełniamy pole CreatedBy użytkownikiem z bazy danych
                post.CreatedBy = _context.Users.Find(post.CreatedById);
            });

            return View(posts);
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(long? id) /* Znak zapytanie ponieważ id może być nullem */
        {
            if (id == null)
            {
                // Nie mamy id więc zwracamy kod HTTP 404 - nie znaleziono
                return NotFound();
            }

            // Szukamy postu po jego id
            var post = await _context.Posts.FirstOrDefaultAsync(m => m.Id == id);
            
            // Jak nie znajdziemy postu to zwracamy błąd z kodem HTTP 404 - nie znaleziono
            if (post == null)
            {
                return NotFound();
            }

            // Mamy post więc zwracamy go do widoku - będziemy mogli użyć go w pliku .cshtml
            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            // Wchodząc na /AdminPosts/Create obsługujemy GET w celu początkowo zbudowania formyularza. W metodzie Create(HttpPost) będziemy obsługiwać dane z formularza.
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] // Wczytuje z ciała metody Http tylko pola podane do atrubutu Bind(...)
        public async Task<IActionResult> Create([Bind("Id,Title,Content,Description,ImageUrl")] Post post)
        {
            // Pozwalamy na dodanie posta tylko gdy użytkownik jest zaloogowany i formularz jest poprawny
            if (ModelState.IsValid && HttpContext.User.Identity.IsAuthenticated == true)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                post.CreatedById = userId;
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Jeżeli formularz nie jest poprawny/użytkownik nie jest zalgoowany ponownie wyświetlamy formularz (bez zapisu)
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                // Jak nie mamy id to zwracamy kod http 404 - nie znaleziono
                return NotFound();
            }

            // Szukamy postu po id
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            // Jak mamy post to przekazujemy go do widoku w celu zbudowania odpowiedzi HTML
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Title,Content,Description,ImageUrl")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            // Próbujemy uaktualnić post tylko gdy dane z UI są poprawne
            if (ModelState.IsValid)
            {
                try
                {
                    // Próbujemy zapisać zmieniopny post
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) // Jak nie uda się zmienić (bo np. ktoś inny już go zmienił wmiędzy czasie to ten wyjątek wystąpi)
                {
                    //
                    if (!PostExists(post.Id))
                    {
                        // Zwracamy kode HTTP 404 - nie znalezionio jeżeli post został w między czasie usunięty
                        return NotFound();
                    }
                    else
                    {
                        // Propagujemy wyjątek
                        throw;
                    }
                }

                // Przekierowujemy użyutkownika na /AdminPosts - stronę główną administracyjną postów (z tego kontrollera - metoda Index)
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        // Wyświetlamy stronę do usuwania postów
        public async Task<IActionResult> Delete(long? id)
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

        // POST: Posts/Delete/5
        // Usuwamy post po jego id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(long id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
