﻿using BulbaCourses.Video.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulbaCourses.Video.Data.Interfaces
{
    public interface ICommentRepository : IDisposable
    {
        CommentDb GetById(string commentId);
        IEnumerable<CommentDb> GetAll();
        void Add(CommentDb comment);
        void Update(CommentDb comment);
        void Remove(CommentDb comment);

        Task<CommentDb> GetByIdAsync(string commentId);
        Task<IEnumerable<CommentDb>> GetAllAsync();
        Task<int> AddAsync(CommentDb commentDb);
        Task<int> UpdateAsync(CommentDb commentDb);
        Task<int> RemoveAsync(CommentDb comment);
    }
}