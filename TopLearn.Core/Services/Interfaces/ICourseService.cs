using System;
using System.Collections.Generic;
using System.Text;


using TopLearn.DataLayer.Entities.Course;


namespace TopLearn.Core.Services.Interfaces
{
    public interface ICourseService
    {
        List<CourseGroup> GetAllGroup();
    } 
   
}
