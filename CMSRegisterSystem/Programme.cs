using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSRegisterSystem
{
    public class Programme
    {
        private string programmeID;
        public string ProgrammeID
        {
            get { return programmeID; }
        }

        private string programmeName;
        public string ProgrammeName
        {
            get { return programmeName; }
        }

        public Programme(string programmeID, string programmeName)
        {
            this.programmeID = programmeID;
            this.programmeName = programmeName;
        }
    }
}