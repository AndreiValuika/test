﻿using BulbaCourses.Video.Data.Interfaces;
using BulbaCourses.Video.Data.Repositories;
using BulbaCourses.Video.Logic.InterfaceServices;
using BulbaCourses.Video.Logic.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulbaCourses.Video.Logic.Infrastructure
{
    public class LogicLoadModule : NinjectModule
    {
        public override void Load()
        {
            //bind repositories
            Bind<IUserRepository>().To<UserRepository>();
            Bind<ICommentRepository>().To<CommentRepository>();
            Bind<ICourseRepository>().To<CourseRepository>();
            Bind<IRoleRepository>().To<RoleRepository>();
            Bind<ITegRepository>().To<TegRepository>();
            Bind<ITransactionRepository>().To<TransactionRepository>();
            Bind<IVideoRepository>().To<VideoRepository>();

            //bind services
            Bind<IUserService>().To<UserService>();
            Bind<ICourseService>().To<CourseService>();
            Bind<ICommentService>().To<CommentService>();

        }
    }
}