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
                TempData["Added"] = "Todo successfully created";
                ViewBag.added = TempData["Added"];
                return RedirectToAction("Index");
            }
            return View("AddTodo");
        }

        [HttpGet("{todoid}/Edit")]
        public IActionResult Edit(int todoid)
        {
            ViewBag.single = _todoFactory.FindById(todoid);
            return View();
        }

        [HttpPost("{todoid}/Edit/EditTodo")]
        public IActionResult EditTodo(int todoid)
        {
            if(ModelState.IsValid)
            {
                _todoFactory.EditTodo(todoid);
                return RedirectToAction("SingleTodo");
            }
            return View("Edit");
        }
        [HttpGet("{todoid}/Delete")]
        public IActionResult Delete(int todoid)
        { 
            _todoFactory.DeleteTodo(todoid);
            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
