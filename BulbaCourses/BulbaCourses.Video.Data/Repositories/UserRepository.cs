﻿using BulbaCourses.Video.Data.DatabaseContext;
using BulbaCourses.Video.Data.Interfaces;
using BulbaCourses.Video.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulbaCourses.Video.Data.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {

        public UserRepository(VideoDbContext videoDbContext) : base(videoDbContext)
        {
        }

        public void Add(UserDb user)
        {
            _videoDbContext.Users.Add(user);
            _videoDbContext.SaveChanges();
        }

        public async Task<int> AddAsync(UserDb user)
        {
            _videoDbContext.Users.Add(user);
            var result = await _videoDbContext.SaveChangesAsync().ConfigureAwait(false);
            return result;
        }

        public IEnumerable<UserDb> GetAll()
        {
            var userList = _videoDbContext.Users.ToList().AsReadOnly();
            return userList;
        }

        public async Task<IEnumerable<UserDb>> GetAllAsync()
        {
            var userList = await _videoDbContext.Users.ToListAsync().ConfigureAwait(false);
            return userList.AsReadOnly();
        }

        public UserDb GetById(string id)
        {
            var user = _videoDbContext.Users.FirstOrDefault(b => b.UserId.Equals(id));
            return user;
        }

        public async Task<UserDb> GetByIdAsync(string userId)
        {
            var user = await _videoDbContext.Users.SingleOrDefaultAsync(b => b.UserId.Equals(userId)).ConfigureAwait(false);
            return user;
        }

        public void Remove(UserDb user)
        {
            _videoDbContext.Users.Remove(user);
            _videoDbContext.SaveChanges();
        }

        public async Task<int> RemoveAsync(UserDb user)
        {
            _videoDbContext.Users.Remove(user);
            return await _videoDbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public void Update(UserDb user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            _videoDbContext.Entry(user).State = EntityState.Modified;
            _videoDbContext.SaveChanges();
        }

        public async Task<int> UpdateAsync(UserDb user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            _videoDbContext.Entry(user).State = EntityState.Modified;
            return await _videoDbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<bool> IsLoginExistAsync(string login)
        {
            return await _videoDbContext.Users.AnyAsync(c => c.Login.Equals(login)).ConfigureAwait(false);
        }

        public async Task<bool> IsEmailExistAsync(string email)
        {
            return await _videoDbContext.Users.AnyAsync(c => c.Email.Equals(email)).ConfigureAwait(false);
        }
    }
}