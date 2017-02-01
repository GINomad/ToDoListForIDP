using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ToDoList.Models;
using ToDoList.ViewModels;

namespace ToDoList.App_Start
{
    public class MappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<MyTask, TaskViewModel>().ReverseMap();
            CreateMap<Comment, CommentViewModel>().ReverseMap();           
        }
    }
}