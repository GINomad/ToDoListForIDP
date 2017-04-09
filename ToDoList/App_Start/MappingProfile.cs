using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ToDoList.Models;
using ToDoList.ViewModels;
using Microsoft.AspNet.Identity;

namespace ToDoList.App_Start
{
    public class MappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<MyTask, TaskViewModel>()
                .ForMember(t => t.TaskId,x => x.MapFrom(m => m.MyTaskId))
                //.ForMember(u => u.ApplicationUserId, i => i.MapFrom(task => task.ApplicationUserId))
                .ReverseMap().AfterMap((dest, source) => {
                    dest.Users.ToList().AddRange(source.Users.Select(x => new UserViewModel { Id = x.Id, UserName = x.UserName}));

                });
            CreateMap<Comment, CommentViewModel>().ReverseMap();
                       
        }
    }
}