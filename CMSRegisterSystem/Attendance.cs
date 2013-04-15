using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSRegisterSystem
{
    public class Attendance
    {
        private int attendanceID;
        public int AttendanceID
        {
            get { return attendanceID; }
        }

        private TeachingSession session;
        public TeachingSession Session
        {
            get { return session; }
        }

        private Student student;
        public Student Student
        {
            get { return student; }
        }

        private bool present;
        public bool Present
        {
            get { return present; }
        }

        public Attendance(int attendanceID, TeachingSession session, Student student, bool present)
        {
            this.attendanceID = attendanceID;
            this.session = session;
            this.student = student;
            this.present = present;
        }
    }
}