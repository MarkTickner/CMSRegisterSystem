using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSRegisterSystem
{
    public class Course
    {
        private string courseCode;
        public string CourseCode
        {
            get { return courseCode; }
        }

        private string courseName;
        public string CourseName
        {
            get { return courseName; }
        }

        private int courseLength;
        public int CourseLength
        {
            get { return courseLength; }
        }

        private Staff courseCoordinator;
        public Staff CourseCoordinator
        {
            get { return courseCoordinator; }
        }

        public Course(string courseCode, string courseName, int courseLength, Staff courseCoordinator)
        {
            this.courseCode = courseCode;
            this.courseName = courseName;
            this.courseLength = courseLength;
            this.courseCoordinator = courseCoordinator;
        }

        public Course(string courseName, string courseCode)
        {
            this.courseName = courseName;
            this.courseCode = courseCode;
        }
    }
}