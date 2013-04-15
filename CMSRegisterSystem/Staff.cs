using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSRegisterSystem
{
    public class Staff
    {
        private int staffID;
        public int StaffID
        {
            get { return staffID; }
        }

        private string staffUserID;
        public string StaffUserID
        {
            get { return staffUserID; }
        }

        private string staffTitle;
        public string StaffTitle
        {
            get { return staffTitle; }
        }

        private string staffFirstName;
        public string StaffFirstName
        {
            get { return staffFirstName; }
        }

        private string staffSurname;
        public string StaffSurname
        {
            get { return staffSurname; }
        }

        private StaffType staffType;
        public StaffType StaffType
        {
            get { return staffType; }
        }

        public Staff(int staffID, string staffUserID, string staffTitle, string staffFirstName, string staffSurname, StaffType staffType)
        {
            this.staffID = staffID;
            this.staffUserID = staffUserID;
            this.staffTitle = staffTitle;
            this.staffFirstName = staffFirstName;
            this.staffSurname = staffSurname;
            this.staffType = staffType;
        }

        public Staff(string staffFirstName, string staffSurname)
        {
            this.staffFirstName = staffFirstName;
            this.staffSurname = staffSurname;
        }

        public Staff(int staffID, string staffFirstName, string staffSurname)
        {
            this.staffID = staffID;
            this.staffFirstName = staffFirstName;
            this.staffSurname = staffSurname;
        }
    }
}