using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSRegisterSystem
{
    public class TeachingSession
    {
        private int sessionID;
        public int SessionID
        {
            get { return sessionID; }
        }

        private string sessionType;
        public string SessionType
        {
            get { return sessionType; }
        }

        private Course courseCode;
        public Course CourseCode
        {
            get { return courseCode; }
        }

        private Room roomID;
        public Room RoomID
        {
            get { return roomID; }
        }

        private string sessionDate;
        public string SessionDate
        {
            get { return sessionDate; }
        }

        private string day;
        public string Day
        {
            get { return day; }
        }

        private string time;
        public string Time
        {
            get { return time; }
        }

        private int duration;
        public int Duration
        {
            get { return duration; }
        }

        private Staff leadStaffID;
        public Staff LeadStaffID
        {
            get { return leadStaffID; }
        }

        public TeachingSession(int sessionID, string sessionType, Course courseCode, Room roomID, string sessionDate, string day, string time, int duration, Staff leadStaffID)
        {
            this.sessionID = sessionID;
            this.sessionType = sessionType;
            this.courseCode = courseCode;
            this.roomID = roomID;
            this.sessionDate = sessionDate;
            this.day = day;
            this.time = time;
            this.duration = duration;
            this.leadStaffID = leadStaffID;
        }

        public TeachingSession(int sessionID, string sessionType, Course courseCode, Room roomID, string day, string time, int duration, Staff leadStaffID)
        {
            this.sessionID = sessionID;
            this.sessionType = sessionType;
            this.courseCode = courseCode;
            this.roomID = roomID;
            this.day = day;
            this.time = time;
            this.duration = duration;
            this.leadStaffID = leadStaffID;
        }

        public TeachingSession(string sessionType)
        {
            this.sessionType = sessionType;
        }
    }
}