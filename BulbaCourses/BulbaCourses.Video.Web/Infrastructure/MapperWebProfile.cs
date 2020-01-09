﻿using AutoMapper;
using BulbaCourses.Video.Logic.Models;
using BulbaCourses.Video.Web.Models;
using BulbaCourses.Video.Web.Models.CourseViews;
using BulbaCourses.Video.Web.Models.UserViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BulbaCourses.Video.Web.Infrastructure
{
    public class MapperWebProfile : Profile
    {
        public MapperWebProfile()
        {
            CreateMap<UserProfileView, UserInfo>().ReverseMap();
            CreateMap<CourseView, CourseInfo>().ReverseMap();
            CreateMap<CommentView, CommentInfo>().ReverseMap();
        }
    }
}