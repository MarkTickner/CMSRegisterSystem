using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSRegisterSystem
{
    public class SessionStudentAssociation
    {
        private int studentID;
        public int StudentID
        {
            get { return studentID; }
        }

        private string courseN;
        public string CourseN
        {
            get { return courseN; }
        }

        private string courseC;
        public string CourseC
        {
            get { return courseC; }
        }

        private string sessionN;
        public string SessionN
        {
            get { return sessionN; }
        }

        private string sessionT;
        public string SessionT
        {
            get { return sessionT; }
        }

        public SessionStudentAssociation(int studentID, string courseN, string courseC, string sessionN, string sessionT)
        {
            this.studentID = studentID;
            this.courseN = courseN;
            this.courseC = courseC;
            this.sessionN = sessionN;
            this.sessionT = sessionT;
        }
    }
}