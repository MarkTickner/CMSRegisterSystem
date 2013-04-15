using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSRegisterSystem
{
    public class Controller
    {
        // SHARED ---------------------------------------------------------------------------------------------------
        private Student loggedInStudent;
        public Student LoggedInStudent
        {
            get { return loggedInStudent; }
            set { loggedInStudent = value; }
        }

        private Staff loggedInStaff;
        public Staff LoggedInStaff
        {
            get { return loggedInStaff; }
            set { loggedInStaff = value; }
        }

        // Method that checks user log in details and returns 'logInState'
        public void LogInUser(string username, string password)
        {
            DatabaseController.LogInUser(username, password);
        }

        // Method which checks what user is logged in and saves it to a session
        public void CheckLogin()
        {
            // Get logged in student from session, if it exists
            if (HttpContext.Current.Session["loggedInStudent"] == null)
            {
                // Session doesn't exist - student isn't logged in
                loggedInStudent = null;
            }
            else
            {
                // Session exists - user is logged in as a student
                // Get student from session
                loggedInStudent = (Student)HttpContext.Current.Session["loggedInStudent"];
            }

            // Get logged in staff from session, if it exists
            if (HttpContext.Current.Session["loggedInStaff"] == null)
            {
                // Session doesn't exist - staff isn't logged in
                loggedInStaff = null;
            }
            else
            {
                // Session exists - user is logged in as a staff
                // Get staff from session
                loggedInStaff = (Staff)HttpContext.Current.Session["loggedInStaff"];
            }
        }
        // SHARED ---------------------------------------------------------------------------------------------------

        // MARK -----------------------------------------------------------------------------------------------------
        // Method that returns a list of 'Attendance' objects (absent and notified only)
        public List<Attendance> ListAttendancesAbsentAndNotifiedOnly()
        {
            return DatabaseController.ListAttendancesAbsentAndNotifiedOnly();
        }

        // Method that returns a list of 'NotifiedAbsenceAuthorised' objects
        public List<NotifiedAbsenceAuthorised> ListNotifiedAbsenceAuthoriseds()
        {
            return DatabaseController.ListNotifiedAbsenceAuthoriseds();
        }

        // Method which checks to see if an absence has already been notified, using the 'AttendanceID'
        public bool CheckIfAbsenceIsAlreadyNotified(int attendanceID)
        {
            return DatabaseController.CheckIfAbsenceIsAlreadyNotified(attendanceID);
        }

        // Method that returns a list of 'NotifiedAbsence' objects by 'AttendanceID'
        public List<NotifiedAbsence> ListNotifiedAbsencesByAttendanceID(int attendanceID)
        {
            return DatabaseController.ListNotifiedAbsencesByAttendanceID(attendanceID);
        }

        // Method that returns a list of 'NotifiedAbsenceAuthorised' objects by 'AbsenceID'
        public List<NotifiedAbsenceAuthorised> ListNotifiedAbsenceAuthorisedsByAbsenceID(int absenceID)
        {
            return DatabaseController.ListNotifiedAbsenceAuthorisedsByAbsenceID(absenceID);
        }

        // Method that saves an absence notification authorisation or rejection to the database
        public bool SaveNotificationAuthorisationRejection(int absenceID, bool authorised)
        {
            return DatabaseController.SaveNotificationAuthorisationRejection(absenceID, authorised);
        }

        // Method that returns a list of 'Attendance' objects (absent only) by 'StudentID'
        public List<Attendance> ListAttendancesAbsentOnlyByStudentID(int studentID)
        {
            return DatabaseController.ListAttendancesAbsentOnlyByStudentID(studentID);
        }

        // Method that returns a list of 'Attendance' objects by 'AttendanceID'
        public List<Attendance> ListAttendancesByAttendanceID(int attendanceID)
        {
            return DatabaseController.ListAttendancesByAttendanceID(attendanceID);
        }

        // Method that saves an absence notification to the database
        public bool SaveNotification(int attendanceID, string reason)
        {
            return DatabaseController.SaveNotification(attendanceID, reason);
        }

        // Method that returns the week commencing date of the date specified as a parameter
        public DateTime GetFirstDayOfWeek(DateTime dayInWeek)
        {
            return FirstDayOfWeekUtility.GetFirstDayOfWeek(dayInWeek);
        }

        // Method that returns a list of 'Attendance' objects by 'StudentID'
        public List<Attendance> ListAttendancesByStudentID(int studentID)
        {
            return DatabaseController.ListAttendancesByStudentID(studentID);
        }

        // Method that returns a list of 'Attendance' objects
        public List<Attendance> ListAttendances()
        {
            return DatabaseController.ListAttendances();
        }






        // MARK -----------------------------------------------------------------------------------------------------

        // HOLLY ----------------------------------------------------------------------------------------------------
        public List<TeachingSession> ListTeachSession(Staff loggedInStaff)
        {
            List<TeachingSession> TeachingSess = DatabaseController.ListTeachingSession(loggedInStaff.StaffID);
            return TeachingSess;
        }

        public List<Student> Register(int sessionID)
        {
            List<Student> register = DatabaseController.getRegister(sessionID);
            return register;
        }

        public void saveRegister(List<string> register)
        {
            DatabaseController.saveRegister(register);
        }

        public List<string> dates(int sessionID)
        {
            List<string> dates = DatabaseController.getDates(sessionID);
            return dates;
        }

        public List<string> editRegister(string sessionID, string sessDate)
        {
            List<string> editReg = DatabaseController.getRegisterEdit(sessionID, sessDate);
            return editReg;
        }

        public void updateRegister(List<string> studentIDs, string sessionDate)
        {
            DatabaseController.updateRegister(studentIDs, sessionDate);
        }
        // HOLLY ----------------------------------------------------------------------------------------------------

        // EMILY AND MARK -------------------------------------------------------------------------------------------
        // Method that returns a list of 'StudentAbsent' objects
        public int ListAbsentStudentsByID(int studentID)
        {
            return DatabaseController.ListAbsentStudentsByID(studentID);
        }

        // Method that returns a list of 'StudentPresent' objects
        public int ListPresentStudentsByID(int studentID)
        {
            return DatabaseController.ListPresentStudentsByID(studentID);
        }

        // Method to return Student Object
        public Student getStudent(int StudentID)
        {
            return DatabaseController.getStudent(StudentID);
        }
        // EMILY AND MARK -------------------------------------------------------------------------------------------
    }
}