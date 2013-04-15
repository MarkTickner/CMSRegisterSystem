using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMSRegisterSystem
{
    public partial class StudentAttendance : System.Web.UI.Page
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
            int studentID;
            int authorisedAbsences = 0;

            // Check for valid number
            if (int.TryParse(txtStudentID.Text, out studentID))
            {
                // List all absent attendances of entered student ID
                List<Attendance> absentAttendances = controller.ListAttendancesAbsentOnlyByStudentID(studentID);

                // List the authorised notified absences
                List<NotifiedAbsenceAuthorised> notifiedAbsenceAuthoriseds = controller.ListNotifiedAbsenceAuthoriseds();

                // Loop over each 'Attendance' object from the 'absentAttendances' list
                foreach (var absentAttendance in absentAttendances)
                {
                    // Loop over each 'NotifiedAbsenceAuthorised' object from the 'notifiedAbsenceAuthoriseds' list
                    foreach (var notifiedAbsenceAuthorised in notifiedAbsenceAuthoriseds)
                    {
                        // Check notified absences list against current attendance
                        if (notifiedAbsenceAuthorised.NotifiedAbsence.Attendance.AttendanceID == absentAttendance.AttendanceID)
                        {
                            // Check if the notified absence has been authorised or not authorised
                            if (notifiedAbsenceAuthorised.Authorised == true)
                            {
                                // Notified, authorised
                                authorisedAbsences++;
                            }
                        }
                    }
                }

                try
                {
                    // Populate labels
                    lblStudentName.Text = controller.getStudent(studentID).StudentFirstName + " " + controller.getStudent(studentID).StudentSurname;
                    lblDaysPresent.Text = controller.ListPresentStudentsByID(studentID).ToString();
                    lblDaysAbsent.Text = controller.ListAbsentStudentsByID(studentID).ToString();
                    lblAuthorisedAbsences.Text = authorisedAbsences.ToString();
                    lblUnauthorisedAbsences.Text = (int.Parse(lblDaysAbsent.Text) - int.Parse(lblAuthorisedAbsences.Text)).ToString();

                    // Show div
                    attendanceDetails.Visible = true;
                    lblError.Visible = false;
                }
                catch
                {
                    // Show error message
                    lblError.Text = "Error seaching for student, student not found.";

                    // Hide div
                    attendanceDetails.Visible = false;
                    lblError.Visible = true;
                }
            }
            else
            {
                // Show error message
                lblError.Text = "Error seaching for student, enter only numbers into the search box.";

                // Hide div
                attendanceDetails.Visible = false;
                lblError.Visible = true;
            }
        }
    }
}