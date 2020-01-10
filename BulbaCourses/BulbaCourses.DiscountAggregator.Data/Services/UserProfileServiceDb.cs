﻿using BulbaCourses.DiscountAggregator.Data.Context;
using BulbaCourses.DiscountAggregator.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulbaCourses.DiscountAggregator.Data.Services
{
    public class UserProfileServiceDb : IUserProfileServiceDb
    {
        private readonly CourseContext context;

        public UserProfileServiceDb(CourseContext context)
        {
            this.context = context;
        }

        public void Add(UserProfileDb profile)
        {
            context.Profiles.Add(profile);
            context.SaveChanges();
        }

        public IEnumerable<UserProfileDb> GetAll()
        {
            var profiles = context.Profiles.ToList().AsReadOnly();
            return profiles;
        }

        public async Task<IEnumerable<UserProfileDb>> GetAllAsync()
        {
            var userList = await context.Profiles.ToListAsync().ConfigureAwait(false);
            return userList.AsReadOnly();
        }

        public UserProfileDb GetById(string id)
        {
            var profile = context.Profiles.FirstOrDefault(c => c.Id.Equals(id));
            return profile;
        }

        public void Delete(UserProfileDb profile)
        {
            context.Profiles.Remove(profile);
            context.SaveChanges();
        }

        public void Update(UserProfileDb profile)
        {
            if (profile != null)
            {
                context.Entry(profile).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await context.Profiles.AnyAsync(b => b.Id == id).ConfigureAwait(false);
        }









        //private readonly CourseContext courseContext;

        //public UserAccountServiceDB(CourseContext courseService)
        //{
        //    courseContext = courseService;
        //}

        //public IEnumerable<UserAccountDb> GetAll()
        //{
        //    var usersList = courseContext.Users/*.Include(x => x.UserProfile)*/.ToList().AsReadOnly();
        //    return usersList;
        //}

        //public void Add(UserAccountDb userAccount)
        //{
        //    userAccount.Password = HashingPassword.HashPassword(userAccount.Password);
        //    //courseContext
        //    courseContext.Users.Add(userAccount);
        //    courseContext.SaveChanges();
        //}

        //public void Update(UserAccountDb userAccount)
        //{
        //    if (userAccount != null)
        //    {
        //        //if(HashingPassword.VerifyHashedPassword(userAccount.Password,"123"))
        //        userAccount.Password = HashingPassword.HashPassword(userAccount.Password);
        //        courseContext.Entry(userAccount.UserProfile).State = EntityState.Modified;
        //        courseContext.Entry(userAccount).State = EntityState.Modified;
        //        courseContext.SaveChanges();
        //    }
        //}

        //public void DeleteById(string userId)
        //{
        //    if (!string.IsNullOrEmpty(userId))
        //    {
        //        courseContext.Profiles.Remove(courseContext.Profiles
        //            .Where(x => x.Id == courseContext.Users.Where(i => i.Id == userId).FirstOrDefault().UserProfile.Id)
        //            .FirstOrDefault());
        //        courseContext.Users.Remove(courseContext.Users.Where(x => x.Id == userId).FirstOrDefault());
        //        courseContext.SaveChanges();
        //        //по идее должно и так удалять, но так не чистит связанную таблицу
        //        //courseContext.Entry(courseContext.Users.Where(x => x.Id == userId).FirstOrDefault()).State = EntityState.Deleted;
        //        //courseContext.SaveChanges();
        //    }
        //}




    }
}
