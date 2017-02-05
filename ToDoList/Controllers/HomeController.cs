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
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;           
        }
        [HttpGet]
        public ActionResult GetGroups()
        {
            var groups = _unitOfWork.Groups.Groups.ToList();
            return PartialView("GroupTabsPartial", groups);
        }
        public ActionResult Index(string groupid)
        {
            return View();
        }
       



    }
}