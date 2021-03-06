using BulbaCourses.Video.Data.DatabaseContext;
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
    /// <summary>
    /// Provides a mechanism for working author repository.
    /// </summary>
    public class AuthorRepository : BaseRepository, IAuthorRepository
    {
        public AuthorRepository(VideoDbContext videoDbContext) : base(videoDbContext)
        {
        }

        /// <summary>
        /// Create a new author in repository.
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        public async Task<AuthorDb> AddAsync(AuthorDb author)
        {
            _videoDbContext.Authors.Add(author);
            _videoDbContext.SaveChangesAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            return await Task.FromResult(author);
        }

        /// <summary>
        /// Gets all authors in repository.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AuthorDb>> GetAllAsync()
        {
            var authorList = await _videoDbContext.Authors.ToListAsync().ConfigureAwait(false);
            return authorList.AsReadOnly();
        }

        /// <summary>
        /// Shows author details by id in repository.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AuthorDb> GetByIdAsync(string id)
        {
            var author = await _videoDbContext.Authors.SingleOrDefaultAsync(b => b.AuthorId.Equals(id)).ConfigureAwait(false);
            return author;
        }

        /// <summary>
        /// Shows all author courses by author id in repository.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CourseDb>> GetCoursesAsync(string id)
        {
            var courses = await _videoDbContext.Authors.Where(c => c.AuthorId.Equals(id))?.SelectMany(c => c.AuthorCourses).ToListAsync();
            return courses.AsReadOnly(); ;
        }

        /// <summary>
        /// Remove author in repository.
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        public async Task RemoveAsync(AuthorDb author)
        {
            if (author == null)
            {
                throw new ArgumentNullException("author");
            }
            _videoDbContext.Authors.Remove(author);
            await _videoDbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Remove author by id in repository.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task RemoveAsyncById(string id)
        {
            var author = _videoDbContext.Authors.SingleOrDefault(b => b.AuthorId.Equals(id));
            if (author == null)
            {
                throw new ArgumentNullException("author");
            }
            _videoDbContext.Authors.Remove(author);
            await _videoDbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Update author.
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        public async Task<AuthorDb> UpdateAsync(AuthorDb author)
        {
            if (author == null)
            {
                throw new ArgumentNullException("author");
            }
            _videoDbContext.Entry(author).State = EntityState.Modified;
            _videoDbContext.SaveChangesAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            return await Task.FromResult(author);
        }
    }
}
