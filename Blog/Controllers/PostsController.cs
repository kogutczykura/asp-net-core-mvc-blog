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
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext _context)
        {
            this._context = _context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
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

        public async Task<IActionResult> Details(long id)
        {
            var post = _context.Posts.Include("CommentKey").Where(post => post.Id == id).First();
            ViewBag.post = post;
            return View();
        }

    }
}