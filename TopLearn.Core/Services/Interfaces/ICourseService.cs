using System;
using System.Collections.Generic;
using System.Text;


using Microsoft.AspNetCore.Mvc.Rendering;


using TopLearn.DataLayer.Entities.Course;


namespace TopLearn.Core.Services.Interfaces
{
    public interface ICourseService
    {
        List<CourseGroup> GetAllGroup();
        List<SelectListItem> GetGroupForManageCourse();
        List<SelectListItem> GetSubGroupForManageCourse(int groupId);
        List<SelectListItem> GetTeachers();
        List<SelectListItem> GetLevels();
        List<SelectListItem> GetStatues();
    } 
   
}
