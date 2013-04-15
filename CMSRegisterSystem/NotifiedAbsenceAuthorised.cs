using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSRegisterSystem
{
    public class NotifiedAbsenceAuthorised
    {
        private int authorisedAbsenceID;
        public int AuthorisedAbsenceID
        {
            get { return authorisedAbsenceID; }
        }

        private NotifiedAbsence notifiedAbsence;
        public NotifiedAbsence NotifiedAbsence
        {
            get { return notifiedAbsence; }
        }

        private bool authorised;
        public bool Authorised
        {
            get { return authorised; }
        }

        private string authorisedRejectedDate;
        public string AuthorisedRejectedDate
        {
            get { return authorisedRejectedDate; }
        }

        public NotifiedAbsenceAuthorised(int authorisedAbsenceID, NotifiedAbsence notifiedAbsence, bool authorised, string authorisedRejectedDate)
        {
            this.authorisedAbsenceID = authorisedAbsenceID;
            this.notifiedAbsence = notifiedAbsence;
            this.authorised = authorised;
            this.authorisedRejectedDate = authorisedRejectedDate;
        }
    }
}