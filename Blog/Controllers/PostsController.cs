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
        // Pozwala na dostęp do bazy danych
        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext _context)
        {
            this._context = _context;
        }

        /*
         * GET /Posts
         * Nie musimy używać [HttpGet], ani [Route]. Ponieważ jest to controller to ta metoda zostanie automatycznie zmapowana na url GET /Posts.
         * Typ IActionResult jest interfejsem, w ramach któego możemy zwrócić np. View (widok), 
         * albo NotFound(kod HTTP odpowiedzi 404 bez widoku) albo np. NotFound("abc") - kod odpowiedzi 404 z odpowiedzią tekstową "abc".
         * Więcej info (https://docs.microsoft.com/pl-pl/aspnet/core/web-api/action-return-types?view=aspnetcore-3.1)
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

            // Tworzymy nowy obiekt komentarza i wpisujemy do niego wartości z obiekty klasy CommentPostRequest
            Comment comment = new Comment();
            comment.PostId = commentPostRequest.PostId;

            // Znajdujemy w bazie danych Post, który komentujemy i przypisujemy do komentarza ten post.
            comment.post = _context.Posts.Find(commentPostRequest.PostId);
            comment.Content = commentPostRequest.Content;

            // Deklarujemy, że chcemy zapisać komentarz w bazie danych
            _context.Add(comment);
            
            // Zapisujmey wszystkie zmiany w bazie danych w ramach tej metody
            await _context.SaveChangesAsync();

            // Przekierowujemy tę metodę do akcji /Details/{postId}
            return RedirectToAction("Details", new {
                id = commentPostRequest.PostId
            });
        }

        // GET /Posts/Details/{id}
        public async Task<IActionResult> Details(long id)
        {
            // Pobieramy post jednocześnie mówiąc, że chcemy pobrać również jego komentarze.
            var post = _context.Posts.Include("CommentKey").Where(post => post.Id == id).First();

            // Wpisujemy komentarz do ViewBag - dynamiczna struktura dostępna w widoku HTML.
            ViewBag.post = post;
            return View();
        }

    }
}