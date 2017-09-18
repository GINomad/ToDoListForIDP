using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using ToDoList.Models;
using ToDoList.ViewModels;
using ToDoList.Repositories;
using Microsoft.AspNet.Identity;

namespace ToDoList.Controllers
{
    public class TaskController : Controller
    {
        // GET: Task
        [HttpGet]
        public ActionResult Tasks()
        {
            return View("Home/Index");
            
        }       
    }
}