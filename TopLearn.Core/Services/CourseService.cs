using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Course;


namespace TopLearn.Core.Services
{
    public class CourseService : ICourseService
    {
        private TopLearnContext _context;

        public CourseService(TopLearnContext context)
        {
            _context = context;
        }

        public List<CourseGroup> GetAllGroup()
        {
            return _context.CourseGroups.ToList();
        }
    }
}
