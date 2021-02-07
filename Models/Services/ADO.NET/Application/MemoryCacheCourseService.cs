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
            return memoryCache.GetOrCreateAsync($"Course{id}", cacheEntry =>{
                cacheEntry.SetSize(1);
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(300));
                return courseservice.GetCourseAsync(id);
            });
        }

        public Task<List<CourseViewModel>> GetCoursesAsync(string search, int page, string orderby, bool ascending)
        {
            return memoryCache.GetOrCreateAsync($"Courses-{search}-{page}-{orderby}-{ascending}", cacheEntry =>{
                cacheEntry.SetSize(3);
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(300));
                return courseservice.GetCoursesAsync(search, page, orderby, ascending);
            });
        }
    }
}