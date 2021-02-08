using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Corso10157.Models.Services.ADO.NET.Infrastructure;
using Corso10157.Models.ViewModel;
using Microsoft.Extensions.Caching.Memory;

namespace Corso10157.Models.Services.ADO.NET.Application
{
    public class MemoryCacheCourseService : ICachedCourseService
    {
        private readonly ICourseServiceAsync courseservice;
        private readonly IMemoryCache memoryCache;

        public MemoryCacheCourseService(ICourseServiceAsync courseservice, IMemoryCache memoryCache)
        {
            this.courseservice = courseservice;
            this.memoryCache = memoryCache;
        }

        public Task<CourseDetailViewModel> GetCourseAsync(int id)
        {
            return memoryCache.GetOrCreateAsync($"Course{id}", cacheEntry =>
            {
                cacheEntry.SetSize(1);
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(300));
                return courseservice.GetCourseAsync(id);
            });
        }

        public Task<ListViewModel<CourseViewModel>> GetCoursesAsync(string search, int page, string orderby, bool ascending, int limit, int offset)
        {
            bool canCache = page <= 5 && string.IsNullOrEmpty(search);
            if (canCache)
            {
                return memoryCache.GetOrCreateAsync($"Courses-{page}-{orderby}-{ascending}", cacheEntry =>
                {
                    cacheEntry.SetSize(3);
                    cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(300));
                    return courseservice.GetCoursesAsync(search, page, orderby, ascending, limit, offset);
                });
            }
            return courseservice.GetCoursesAsync(search, page, orderby, ascending, limit, offset);
        }

        public Task<List<CourseViewModel>> GetMostRecentCoursesAsync()
        {
            return memoryCache.GetOrCreateAsync("MostRecentCourses", cacheEntry =>
                {
                    cacheEntry.SetSize(3);
                    cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(600));
                    return courseservice.GetMostRecentCoursesAsync();
                });
        }

        public Task<List<CourseViewModel>> GetBestRatingCoursesAsync()
        {
            return memoryCache.GetOrCreateAsync("BestRatingCourses", cacheEntry =>
                {
                    cacheEntry.SetSize(3);
                    cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(600));
                    return courseservice.GetBestRatingCoursesAsync();
                });
        }
    }
}