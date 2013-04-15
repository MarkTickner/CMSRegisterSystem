using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSRegisterSystem
{
    public class Student
    {
        private int studentID;
        public int StudentID
        {
            get { return studentID; }
        }

        private string studentUserID;
        public string StudentUserID
        {
            get { return studentUserID; }
        }

        private string studentFirstName;
        public string StudentFirstName
        {
            get { return studentFirstName; }
        }

        private string studentSurname;
        public string StudentSurname
        {
            get { return studentSurname; }
        }

        private string studentRegistrationStatus;
        public string StudentRegistrationStatus
        {
            get { return studentRegistrationStatus; }
        }

        private string studentHolds;
        public string StudentHolds
        {
            get { return studentHolds; }
        }

        private string studentPhoto;
        public string StudentPhoto
        {
            get { return studentPhoto; }
        }

        private int studentYearOfStudy;
        public int StudentYearOfStudy
        {
            get { return studentYearOfStudy; }
        }

        private string programmeID;
        public string ProgrammeID
        {
            get { return programmeID; }
        }

        public Student(int studentID, string studentUserID, string studentFirstName, string studentSurname, string studentRegistrationStatus, string studentHolds, string studentPhoto, int studentYearOfStudy, string programmeID)
        {
            this.studentID = studentID;
            this.studentUserID = studentUserID;
            this.studentFirstName = studentFirstName;
            this.studentSurname = studentSurname;
            this.studentRegistrationStatus = studentRegistrationStatus;
            this.studentHolds = studentHolds;
            this.studentPhoto = studentPhoto;
            this.studentYearOfStudy = studentYearOfStudy;
            this.programmeID = programmeID;
        }
    }
}