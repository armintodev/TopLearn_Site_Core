using System;
using System.Collections.Generic;
using System.Text;

namespace TopLearn.Core.DTOs
{
    public class InformationUserViewModel
    {
        public string UserName { get; set; }
        public string  Email { get; set; }
        public DateTime RegisterDate { get; set; }
        public double wallet { get; set; }
    }
}
