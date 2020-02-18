using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Dto;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    /**
     * Tutaj wykorzystujemy automatycznie mapowanie controller na url. Ponieważ klasa nazywa się PostController i dziedziczy po Controller
     * to metody z tej klasy będą mapowane na url /Posts
     */
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext _context)
        {
            this._context = _context;
        }

        /*
         * GET /Posts
         * Nie musimy używać [HttpGet], ani [Route]. Ponieważ jest to controller to ta metoda zostanie automatycznie zmapowana na url GET /Posts
         */
        public IActionResult Index()
        {
            return View();
        }

        
        /**
         * Używamy atrybutu [HttpPost] ponieważ do tworzenia zasobów powinniśmy używać metody HTTP POST (nie musimy, ale powinniśmy).
         * Bez tego atrybutu wszystkie metody HTTP będą mogły być wykonane na tej metodzie klasy, więc nie jest potrzebna do działa (technicznie).
         */
        // [HttpPost] // Dzieki tej adnotacji metoda CommentPost klasy PostsController odpowie tylko na zapytania HTTP POST
        public async Task<IActionResult> CommentPost(CommentPostRequest commentPostRequest)
        {

            // Zrob zmienna typu Comment, wpisz do niej content z obiektu commentPostRequest
            // Znajdz w bazie obiek post o id commentPostRequest.postId i pzypisz do wlasnie utworzonego komentarza
            // Zapisz zmiany w bazie. 

            Comment comment = new Comment();
            comment.PostId = commentPostRequest.PostId;
            comment.post = _context.Posts.Find(commentPostRequest.PostId);
            comment.Content = commentPostRequest.Content;

            _context.Add(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new {
                id = commentPostRequest.PostId
            });
        }

        // GET /Posts/Details/{id}
        public async Task<IActionResult> Details(long id)
        {
            var post = _context.Posts.Include("CommentKey").Where(post => post.Id == id).First();
            ViewBag.post = post;
            return View();
        }

    }
}