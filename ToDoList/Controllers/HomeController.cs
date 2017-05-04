using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using AutoMapper;
using ToDoList.Models;
using ToDoList.ViewModels;
using PagedList;
using ToDoList.Repositories;

namespace ToDoList.Controllers
{
    //All models need to refactor
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Index1");
        }

        public ActionResult Tasks()
        {
            return View("Index");
        }

        public ActionResult Statistic()
        {
            return View();
        }
       



    }
}