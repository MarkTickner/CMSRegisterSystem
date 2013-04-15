using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSRegisterSystem
{
    public class StaffType
    {
        private int staffTypeID;
        public int StaffTypeID
        {
            get { return staffTypeID; }
        }

        private string staffTypeName;
        public string StaffTypeName
        {
            get { return staffTypeName; }
        }

        public StaffType(int staffTypeID, string staffType)
        {
            this.staffTypeID = staffTypeID;
            this.staffTypeName = staffType;
        }
    }
}