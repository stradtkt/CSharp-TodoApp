using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Factory;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly TodoFactory _todoFactory;
        public HomeController(TodoFactory tFactory)
        {
            _todoFactory = tFactory;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.todos = _todoFactory.FindAllTodos();
            return View();
        }

        [HttpGet("AddTodo")]
        public IActionResult AddTodo()
        {
            return View();
        }

        [HttpPost("CreateTodo")]
        public IActionResult CreateTodo(Todo todo)
        {
            if(ModelState.IsValid)
            {
                _todoFactory.CreateTodo(todo);
                return RedirectToAction("Index");
            }
            return View("AddTodo");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
