using AutoMapper;
using BulbaCourses.Video.Data.Interfaces;
using BulbaCourses.Video.Data.Models;
using BulbaCourses.Video.Logic.InterfaceServices;
using BulbaCourses.Video.Logic.Models;
using BulbaCourses.Video.Logic.Models.ResultModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulbaCourses.Video.Logic.Services
{
    /// <summary>
    /// Provides a mechanism for working with Courses.
    /// </summary>
    public class CourseService : ICourseService
    {
        private readonly IMapper _mapper;
        private readonly ICourseRepository _courseRepository;

        /// <summary>
        /// Creates a new course service.
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="courseRepository"></param>
        public CourseService(IMapper mapper, ICourseRepository courseRepository)
        {
            _mapper = mapper;
            _courseRepository = courseRepository;
        }

        /// <summary>
        /// Gets all courses.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CourseInfo> GetAll()
        {
            var courses = _courseRepository.GetAll();
            var result = _mapper.Map<IEnumerable<CourseDb>, IEnumerable<CourseInfo>>(courses);
            return result;
        }

        /// <summary>
        /// Remove course.
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public void Delete(CourseInfo course)
        {
            var courseDb = _mapper.Map<CourseInfo, CourseDb>(course);
            _courseRepository.Remove(courseDb);
        }

        /// <summary>
        /// Remove course by id.
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public void DeleteById(string courseId)
        {
            var course = _courseRepository.GetById(courseId);
            _courseRepository.Remove(course);
        }

        /// <summary>
        /// Updates course.
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public void Update(CourseInfo course)
        {
            var courseDb = _mapper.Map<CourseInfo, CourseDb>(course);
            _courseRepository.Update(courseDb);
        }

        /// <summary>
        /// Create a new course.
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public void AddCourse(CourseInfo course)
        {
            var courseDb = _mapper.Map<CourseInfo, CourseDb>(course);
            _courseRepository.Add(courseDb);
        }

        /// <summary>
        /// Add description to course.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public void AddDiscription(string courseId, string description)
        {
            var course = _courseRepository.GetById(courseId);
            course.Description = description;
            _courseRepository.Update(course);

        }

        /// <summary>
        /// Add video to course.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="video"></param>
        /// <returns></returns>
        public void AddVideoToCourse(string courseId, VideoMaterialInfo video)
        {
            var videoDb = _mapper.Map<VideoMaterialInfo, VideoMaterialDb>(video);
            var courseVideos = _courseRepository.GetById(courseId).Videos;
            courseVideos.Add(videoDb);
        }

        /// <summary>
        /// Add tag to course.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public void AddTagToCourse(string courseId, TagInfo tag)
        {
            var tagDb = _mapper.Map<TagInfo, TagDb>(tag);
            var courseTags = _courseRepository.GetById(courseId).Tags;
            courseTags.Add(tagDb);
        }

        /// <summary>
        /// Show course details by id.
        /// </summary>
        /// /// <param name="courseId"></param>
        /// <returns></returns>
        public CourseInfo GetCourseById(string courseId)
        {
            var course = _courseRepository.GetById(courseId);
            var courseInfo = _mapper.Map<CourseDb, CourseInfo>(course);
            return courseInfo;
        }

        /// <summary>
        /// Show course details by course name.
        /// </summary>
        /// /// <param name="courseName"></param>
        /// <returns></returns>
        public CourseInfo GetCourseByName(string courseName)
        {
            var course = _courseRepository.GetAll().FirstOrDefault(c => c.Name.Equals(courseName));
            var courseInfo = _mapper.Map<CourseDb, CourseInfo>(course);
            return courseInfo;
        }

        /// <summary>
        /// Show course level by course id.
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public int GetCourseLevel(string courseId)
        {
            var result = _courseRepository.GetById(courseId).Level;
            return result;
        }

        /// <summary>
        /// Show all tags from course by course id.
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public IEnumerable<TagInfo> GetTags(string courseId)
        {
            var courseTags = _courseRepository.GetById(courseId).Tags.ToList().AsReadOnly();
            var courseTagsInfo = _mapper.Map<IEnumerable<TagDb>, IEnumerable<TagInfo>>(courseTags);
            return courseTagsInfo;
        }

        /// <summary>
        /// Show video from course by course id and video order.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="videoOrder"></param>
        /// <returns></returns>
        public VideoMaterialInfo GetVideoByOrder(string courseId, int videoOrder)
        {
            var course = _courseRepository.GetById(courseId);
            var videoDb = course.Videos.FirstOrDefault(c => c.Order == videoOrder);
            var result = _mapper.Map<VideoMaterialDb, VideoMaterialInfo>(videoDb);
            return result;
        }

        /// <summary>
        /// Update course level by course id and new level.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public void UpdateCourseLevel(string courseId, int level)
        {
            var course = _courseRepository.GetById(courseId);
            course.Level = level;
            _courseRepository.Update(course);
        }

        /// <summary>
        /// Show course videos by course id.
        /// </summary>
        /// /// <param name="courseId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VideoMaterialInfo>> GetCourseVideosAsync(string courseId)
        {
            var videos = await _courseRepository.GetCoursesAsync(courseId);
            var result = _mapper.Map<IEnumerable<VideoMaterialDb>, IEnumerable<VideoMaterialInfo>>(videos);
            return result;
        }

        /// <summary>
        /// Gets all courses.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CourseInfo>> GetAllAsync()
        {
            var courses = await _courseRepository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<CourseDb>, IEnumerable<CourseInfo>>(courses);
            return result;
        }

        /// <summary>
        /// Show course details by id.
        /// </summary>
        /// /// <param name="courseId"></param>
        /// <returns></returns>
        public async Task<CourseInfo> GetCourseByIdAsync(string courseId)
        {
            var course = await _courseRepository.GetByIdAsync(courseId);
            var courseInfo = _mapper.Map<CourseDb, CourseInfo>(course);
            return courseInfo;
        }

        /// <summary>
        /// Create a new course.
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public async Task<Result<CourseInfo>> AddCourseAsync(CourseInfo course)
        {
            var courseDb = _mapper.Map<CourseInfo, CourseDb>(course);
            courseDb.Date = DateTime.Now;
            try
            {
                await _courseRepository.AddAsync(courseDb);
                return Result<CourseInfo>.Ok(_mapper.Map<CourseInfo>(courseDb));
            }
            catch (DbUpdateConcurrencyException e)
            {
                return (Result<CourseInfo>)Result.Fail($"Cannot save course. {e.Message}");
            }
            catch (DbUpdateException e)
            {
                return (Result<CourseInfo>)Result.Fail($"Cannot save course. Duplicate field. {e.Message}");
            }
            catch (DbEntityValidationException e)
            {
                return (Result<CourseInfo>)Result.Fail($"Invalid course. {e.Message}");
            }
        }

        /// <summary>
        /// Update course.
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public async Task<Result<CourseInfo>> UpdateAsync(CourseInfo course)
        {
            var courseDb = _courseRepository.GetByIdAsync(course.CourseId).GetAwaiter().GetResult();
            if (courseDb == null)
            {
                return (Result<CourseInfo>)Result.Fail($"Invalid course. Course doesn't exist");
            }
            courseDb.Name = course.Name;
            courseDb.Level = course.Level;
            courseDb.Raiting = course.Raiting;
            courseDb.Description = course.Description;
            courseDb.Price = course.Price;
            try
            {
                await _courseRepository.UpdateAsync(courseDb);
                return Result<CourseInfo>.Ok(_mapper.Map<CourseInfo>(courseDb));
            }
            catch (DbUpdateConcurrencyException e)
            {
                return (Result<CourseInfo>)Result.Fail($"Cannot update course. {e.Message}");
            }
            catch (DbUpdateException e)
            {
                return (Result<CourseInfo>)Result.Fail($"Cannot update course. Duplicate field. {e.Message}");
            }
            catch (DbEntityValidationException e)
            {
                return (Result<CourseInfo>)Result.Fail($"Invalid course. {e.Message}");
            }
        }

        /// <summary>
        /// Remove course by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Result> DeleteByIdAsync(string id)
        {
            _courseRepository.RemoveAsyncById(id);
            return Task.FromResult(Result.Ok());
        }

        /// <summary>
        /// Check if course exists with this Name.
        /// </summary>
        /// <param name="courseName"></param>
        /// <returns></returns>
        public async Task<bool> ExistNameAsync(string courseName)
        {
            return await _courseRepository.IsNameExistAsync(courseName);
        }

        /// <summary>
        /// Show course details by course name.
        /// </summary>
        /// /// <param name="courseName"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CourseInfo>> GetCoursesByNameAsync(string courseName)
        {
            var allCourses = await _courseRepository.GetAllAsync();
            var courses = allCourses.Where(c => c.Name.Contains(courseName));
            var coursesInfo = _mapper.Map<IEnumerable<CourseDb>, IEnumerable<CourseInfo>>(courses);
            return coursesInfo;
        }

        /// <summary>
        /// Add rating to course.
        /// </summary>
        /// <param name="course"></param>
        /// <param name="Assessment"></param>
        /// <returns></returns>
        public Task<Result<CourseInfo>> RateCourse(CourseInfo course, int assessment)
        {
            int newRateCount = course.RateCount++;
            double newRaiting = ((course.Raiting * Convert.ToDouble(course.RateCount)) + assessment) / newRateCount;

            course.Raiting = newRaiting;
            course.RateCount = newRateCount;
            return UpdateAsync(course);

        }

        /// <summary>
        /// Add video to course.
        /// </summary>
        /// <param name="course"></param>
        /// <param name="video"></param>
        /// <returns></returns>
        public Task<Result> AddVideoAsync(CourseInfo course, VideoMaterialInfo video)
        {
            var videoDb = _mapper.Map<VideoMaterialInfo, VideoMaterialDb>(video);
            var courseVideos = _mapper.Map<CourseInfo, CourseDb>(course);
            try
            {
                courseVideos.Videos.Add(videoDb);
                return Task.FromResult(Result.Ok());
            }
            catch (DbUpdateConcurrencyException e)
            {
                return Task.FromResult(Result.Fail($"Cannot add video to course. {e.Message}"));
            }
            catch (DbUpdateException e)
            {
                return Task.FromResult(Result.Fail($"Cannot add video to course. Duplicate field. {e.Message}"));
            }
            catch (DbEntityValidationException e)
            {
                return Task.FromResult(Result.Fail($"Invalid tag. {e.Message}"));
            }
        }

        /// <summary>
        /// Add tag to course.
        /// </summary>
        /// <param name="course"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public Task<Result> AddTagAsync(CourseInfo course, TagInfo tag)
        {
            var tagDb = _mapper.Map<TagInfo, TagDb>(tag);
            var courseDb = _mapper.Map<CourseInfo, CourseDb>(course);
            try
            {
                courseDb.Tags.Add(tagDb);
                return Task.FromResult(Result.Ok());
            }
            catch (DbUpdateConcurrencyException e)
            {
                return Task.FromResult(Result.Fail($"Cannot add tag to course. {e.Message}"));
            }
            catch (DbUpdateException e)
            {
                return Task.FromResult(Result.Fail($"Cannot add tag to course. Duplicate field. {e.Message}"));
            }
            catch (DbEntityValidationException e)
            {
                return Task.FromResult(Result.Fail($"Invalid tag. {e.Message}"));
            }
        }

        /// <summary>
        /// Update course level by course and new level.
        /// </summary>
        /// <param name="course"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public Task<Result<CourseInfo>> ChangeLevel(CourseInfo course, int level)
        {
            course.Level = level;
            return UpdateAsync(course);
        }
    }
}
