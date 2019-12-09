﻿using BulbaCourse.Video.Data.DatabaseContex;
using BulbaCourse.Video.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Video.Data.Models;

namespace BulbaCourse.Video.Data.Repositories
{    
    public class RoleRepository : IRoleRepository
    {
        private readonly VideoDbContext videoDbContext;

        public RoleRepository(VideoDbContext videoDbContext)
        {
            this.videoDbContext = videoDbContext;
        }
        public IEnumerable<RoleDb> GetAll()
        {
            var roleList = videoDbContext.Roles.ToList().AsReadOnly();
            return roleList;
        }

        public RoleDb GetById(string rolelId)
        {
            var role = videoDbContext.Roles.FirstOrDefault(b => b.RoleId.Equals(rolelId));
            return role;
        }
        public void Add(RoleDb role)
        {
            videoDbContext.Roles.Add(role);
            videoDbContext.SaveChanges();
        }

        public void Remove(RoleDb role)
        {
            videoDbContext.Roles.Remove(role);
            videoDbContext.SaveChanges();
        }

        public void RemoveById(string roleId)
        {
            var deletedRole = videoDbContext.Roles.FirstOrDefault(b => b.RoleId.Equals(roleId));
            Remove(deletedRole);
        }

        public void Update(RoleDb role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }
            videoDbContext.Entry(role).State = EntityState.Modified;
            videoDbContext.SaveChanges();
        }
    }
}