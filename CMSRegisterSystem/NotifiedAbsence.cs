using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSRegisterSystem
{
    public class NotifiedAbsence
    {
        private int absenceID;
        public int AbsenceID
        {
            get { return absenceID; }
        }

        private Attendance attendance;
        public Attendance Attendance
        {
            get { return attendance; }
        }

        private string reason;
        public string Reason
        {
            get { return reason; }
        }

        private string notifiedDate;
        public string NotifiedDate
        {
            get { return notifiedDate; }
        }

        public NotifiedAbsence(int absenceID, Attendance attendance, string reason, string notifiedDate)
        {
            this.absenceID = absenceID;
            this.attendance = attendance;
            this.reason = reason;
            this.notifiedDate = notifiedDate;
        }
    }
}