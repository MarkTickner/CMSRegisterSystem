using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMSRegisterSystem
{
    public partial class CourseAttendance : System.Web.UI.Page
    {
        Controller controller = new Controller();

        protected void Page_Load(object sender, EventArgs e)
        {
            // User login -----------------------------------------------------------------------------------------------
            controller.CheckLogin();

            // Get logged in user
            if (controller.LoggedInStudent == null && controller.LoggedInStaff == null)
            {
                // If not logged in
                lblInfo.Text = "You are not logged in.";
            }
            else if (controller.LoggedInStudent != null)
            {
                // Logged in as a student
                // Show message to user
                lblInfo.Text = controller.LoggedInStudent.StudentFirstName + " " + controller.LoggedInStudent.StudentSurname + ", you are logged in as a student.";
            }
            else if (controller.LoggedInStaff != null)
            {
                // Logged in as a STAFF
                // Show message to user
                lblInfo.Text = controller.LoggedInStaff.StaffFirstName + " " + controller.LoggedInStaff.StaffSurname + ", you are logged in as a " + controller.LoggedInStaff.StaffType.StaffTypeName + ".";
            }
            // User login -----------------------------------------------------------------------------------------------
        }

        // Event handler for 'Search' button
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // Variables
            string courseName = null;
            int courseAttendancesCount = 0;
            double presents = 0;
            double absences = 0;
            double authorisedAbsences = 0;

            // List all attendances
            List<Attendance> attendances = controller.ListAttendances();

            // List all authorised notified absences
            List<NotifiedAbsenceAuthorised> notifiedAbsenceAuthoriseds = controller.ListNotifiedAbsenceAuthoriseds();

            // Loop over each 'Attendance' object from the 'attendances' list
            foreach (var attendance in attendances)
            {
                // Check if current attendance course matches searched course
                if (attendance.Session.CourseCode.CourseCode.Equals(txtCourseCode.Text.ToUpperInvariant()))
                {
                    // Course matches
                    // Check if attendance is present
                    if (attendance.Present == true)
                    {
                        // Present
                        presents++;
                    }
                    else if (attendance.Present == false)
                    {
                        // Absent
                        absences++;
                    }

                    // Increment 'courseAttendancesCount' int
                    courseAttendancesCount++;

                    // Save course name to string
                    courseName = attendance.Session.CourseCode.CourseName;
                }
                else
                {
                    // Course does not match
                    // Move to next object in list
                    continue;
                }

                // Loop over each 'NotifiedAbsenceAuthorised' object from the 'notifiedAbsenceAuthoriseds' list
                foreach (var notifiedAbsenceAuthorised in notifiedAbsenceAuthoriseds)
                {
                    // Check notified absences list against current attendance has authorised
                    if (notifiedAbsenceAuthorised.Authorised == true && notifiedAbsenceAuthorised.NotifiedAbsence.Attendance.AttendanceID == attendance.AttendanceID)
                    {
                        // Notified, authorised
                        authorisedAbsences++;
                    }
                }
            }

            try
            {
                // Check if any matching courses were found in the 'attendances' list
                if ((presents + absences) != 0)
                {
                    // Matching courses were found
                    // Populate labels, round percentages to two decimal places
                    lblCourseName.Text = courseName;
                    lblDaysPresent.Text = presents.ToString();
                    lblDaysPresentPercentage.Text = Math.Round(((presents / courseAttendancesCount) * 100), 2) + "%";
                    lblDaysAbsent.Text = absences.ToString();
                    lblDaysAbsentPercentage.Text = Math.Round(((absences / courseAttendancesCount) * 100), 2) + "%";
                    lblAuthorisedAbsences.Text = authorisedAbsences.ToString();
                    lblAuthorisedAbsencesPercentage.Text = Math.Round(((authorisedAbsences / absences) * 100), 2) + "%";
                    lblUnauthorisedAbsences.Text = (absences - authorisedAbsences).ToString();
                    lblUnauthorisedAbsencesPercentage.Text = Math.Round((((absences - authorisedAbsences) / absences) * 100), 2) + "%";

                    // Show div
                    attendanceDetails.Visible = true;
                    lblError.Visible = false;
                }
                else
                {
                    // Matching course were not found
                    // Throw exception
                    throw new Exception("Error seaching for course, course not found.");
                }
            }
            catch (Exception ex)
            {
                // Catch deliberately thrown exception
                // Show error message
                lblError.Text = ex.Message;

                // Hide div
                attendanceDetails.Visible = false;
                lblError.Visible = true;
            }
        }
    }
}