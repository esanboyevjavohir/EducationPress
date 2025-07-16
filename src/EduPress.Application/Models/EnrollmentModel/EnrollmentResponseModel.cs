using EduPress.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduPress.Application.Models.EnrollmentModel
{
    public class EnrollmentResponseModel : BaseResponseModel
    {
        public Guid UserId { get; set; }
        public Guid CoursesId { get; set; }
        public EnrollmentStatus EnrollmentStatus { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
