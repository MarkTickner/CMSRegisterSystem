using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSRegisterSystem
{
    public class SessionStaffAssociation
    {
        private string courseN;
        public string CourseN
        {
            get { return courseN; }
        }

        private string courseC;
        public string CourseC
        {
            get { return CourseC; }
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

        private string staffFN;
        public string StaffFN
        {
            get { return staffFN; }
        }

        private string staffS;
        public string StaffS
        {
            get { return staffS; }
        }

        private string roomN;
        public string RoomN
        {
            get { return roomN; }
        }

        public SessionStaffAssociation(string courseN, string courseC, string sessionN, string sessionT, string staffFN, string staffS, string roomN)
        {
            this.courseN = courseN;
            this.courseC = courseC;
            this.sessionN = sessionN;
            this.sessionT = sessionT;
            this.staffFN = staffFN;
            this.staffS = staffS;
            this.roomN = roomN;
        }
    }
}