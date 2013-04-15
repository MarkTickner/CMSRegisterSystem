using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMSRegisterSystem
{
    public partial class NotifyOfAbsence : System.Web.UI.Page
    {
        Controller controller = new Controller();

        int attendanceIDFromURL; // the attendanceID will be saved from the website URL here
        string notificationsTypeFromURL; // the type of notifications to show will be saved from the website URL here

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
                // Logged in as staff
                // Show message to user
                lblInfo.Text = controller.LoggedInStaff.StaffFirstName + " " + controller.LoggedInStaff.StaffSurname + ", you are logged in as a " + controller.LoggedInStaff.StaffType.StaffTypeName + ".";
            }
            // User login -----------------------------------------------------------------------------------------------

            try
            {
                // Gets the 'attendance' from the URL if available, and passes it to the 'ShowNotifyAbsence' method
                attendanceIDFromURL = int.Parse(Request.QueryString["attendanceID"]);

                // Run the 'ShowNotifyAbsence' method with a parameter from the URL
                ShowNotifyAbsence(attendanceIDFromURL);

                // Hide 'notificationList' div
                notificationList.Visible = false;
            }
            catch
            {
                // Gets the type of notification from the URL if available and saves it as a string
                notificationsTypeFromURL = Request.QueryString["notifications"];

                // Disable the 'Filter results by:" item in the drop down menu
                ListItem i = drpFilter.Items.FindByValue("filter");
                i.Attributes.Add("disabled", "true");

                // Show message
                lblNotificationList.Text = "This is a list of your absences. Check the status of your notifications here and click them to give a reason.";

                // List all own absent attendances
                List<Attendance> absentAttendances = controller.ListAttendancesAbsentOnlyByStudentID(controller.LoggedInStudent.StudentID);

                // Check if the 'absentAttendances' list is empty
                if (absentAttendances.Count() == 0)
                {
                    // List is empty
                    // Show error message
                    TableRow rowError = new TableRow();
                    TableCell cellError = new TableCell();
                    cellError.Text = "There are no absences to show.";
                    cellError.ColumnSpan = 4;
                    rowError.Cells.Add(cellError);
                    tblAbsences.Rows.Add(rowError);
                }
                else
                {
                    // List is not empty
                    // List the authorised notified absences
                    List<NotifiedAbsenceAuthorised> notifiedAbsenceAuthoriseds = controller.ListNotifiedAbsenceAuthoriseds();

                    // Sort the absent student attendances list into descending order of 'SessionDate'
                    absentAttendances.Sort(delegate(Attendance a, Attendance b)
                    {
                        return b.Session.SessionDate.CompareTo(a.Session.SessionDate);
                    });

                    // Show each 'Attendance' object from the 'absentAttendances' list in a dynamically created table
                    foreach (var absentAttendance in absentAttendances)
                    {
                        // Display the absences in a dynamically created table
                        TableRow row = new TableRow();

                        // Course name and code
                        TableCell cell1 = new TableCell();
                        cell1.Text = absentAttendance.Session.CourseCode.CourseName + " - " + absentAttendance.Session.CourseCode.CourseCode;
                        row.Cells.Add(cell1);

                        // Session type
                        TableCell cell2 = new TableCell();
                        cell2.Text = absentAttendance.Session.SessionType;
                        row.Cells.Add(cell2);

                        // Session date
                        TableCell cell3 = new TableCell();
                        cell3.Text = Convert.ToDateTime(absentAttendance.Session.SessionDate).ToShortDateString();
                        row.Cells.Add(cell3);

                        // Notification and authorisation
                        TableCell cell4 = new TableCell();

                        // This button uses the 'btnAbsentNotifyAbsence' event handler
                        // It is formatted using CSS to look like hyperlinked text
                        Button btnAbsentNotifyAbsence = new Button();
                        btnAbsentNotifyAbsence.Text = "";
                        btnAbsentNotifyAbsence.CommandArgument = absentAttendance.AttendanceID.ToString();
                        btnAbsentNotifyAbsence.Click += new EventHandler(btnAbsentNotifyAbsence_Click);
                        btnAbsentNotifyAbsence.CssClass += "hidden_btn";

                        foreach (var notifiedAbsenceAuthorised in notifiedAbsenceAuthoriseds)
                        {
                            // Check notified absences list against current attendance
                            if (notifiedAbsenceAuthorised.NotifiedAbsence.Attendance.AttendanceID == absentAttendance.AttendanceID)
                            {
                                // Check if the notified absence has been authorised or not authorised
                                if (notifiedAbsenceAuthorised.Authorised == true)
                                {
                                    // Notified, authorised
                                    btnAbsentNotifyAbsence.Text = "Authorised";
                                    cell4.BackColor = System.Drawing.Color.FromArgb(0x78, 0xAB, 0x46);
                                }
                                else
                                {
                                    // Notified, not authorised
                                    btnAbsentNotifyAbsence.Text = "Rejected";
                                    cell4.BackColor = System.Drawing.Color.FromArgb(0xCC, 0x00, 0x00);
                                }
                            }

                            // Check if the absence has already been notified
                            else if (controller.CheckIfAbsenceIsAlreadyNotified(absentAttendance.AttendanceID) == true && btnAbsentNotifyAbsence.Text.Equals(""))
                            {
                                // Notified, awaiting authorisation
                                btnAbsentNotifyAbsence.Text = "Waiting for authorisation";
                                cell4.BackColor = System.Drawing.Color.FromArgb(0xFF, 0x99, 0x33);
                            }
                            else if (controller.CheckIfAbsenceIsAlreadyNotified(absentAttendance.AttendanceID) == false && btnAbsentNotifyAbsence.Text.Equals(""))
                            {
                                // Not notified
                                btnAbsentNotifyAbsence.Text = "Notification not sent";
                                cell4.BackColor = System.Drawing.Color.FromArgb(0xFF, 0x99, 0x33);
                            }
                        }

                        cell4.Controls.Add(btnAbsentNotifyAbsence);
                        row.Cells.Add(cell4);

                        // Check if a filter is being used
                        if (notificationsTypeFromURL == null)
                        {
                            // No filter
                            // Add to table
                            tblAbsences.Rows.Add(row);
                        }
                        else if (btnAbsentNotifyAbsence.Text.ToUpperInvariant().Contains(notificationsTypeFromURL.ToUpperInvariant()))
                        {
                            // Filter match
                            // Add to table
                            tblAbsences.Rows.Add(row);
                        }
                        else
                        {
                            // Filter no match
                            // Move to next object in list
                            continue;
                        }
                    }
                }

                // Hide 'notificationSendView' div
                notificationSendView.Visible = false;
            }
        }

        // Method that displays the details of the selected attendance
        public void ShowNotifyAbsence(int attendanceID)
        {
            // Check if the selected absence has already been notified
            if (controller.CheckIfAbsenceIsAlreadyNotified(attendanceID))
            {
                // Absence has already been notified
                // Create a list of 'NotifiedAbsence' objects using the 'ListNotifiedAbsences' method
                List<NotifiedAbsence> absences = controller.ListNotifiedAbsencesByAttendanceID(attendanceIDFromURL);

                foreach (var absence in absences)
                {
                    // Show message
                    lblNotificationSendView.Text = "Notification already sent on " + absence.NotifiedDate + ". Reason for absence sent is shown below:";

                    // Populate lables and text boxes with details from the selected absence
                    lblCourseDetails.Text = absence.Attendance.Session.CourseCode.CourseName + " - " + absence.Attendance.Session.CourseCode.CourseCode;
                    lblSessionDetails.Text = absence.Attendance.Session.SessionType + ", " + Convert.ToDateTime(absence.Attendance.Session.SessionDate).ToShortDateString() + " " + absence.Attendance.Session.Time;
                    txtAbsenceReason.Text = absence.Reason;
                }

                // Disable and hide controls
                txtAbsenceReason.Enabled = false;
                btnSendNotification.Visible = false;
                btnCancelNotification.Visible = false;
                RequiredFieldValidator.Visible = false;
            }
            else
            {
                // Absence has not already been notified
                // Hide controls
                lblNotificationSendView.Text = "Reason for absence:";
                btnBack.Visible = false;

                // Create a list of 'Attendance' objects using the 'ListAttendancesByAttendanceID' method
                List<Attendance> attendances = controller.ListAttendancesByAttendanceID(attendanceIDFromURL);
                foreach (var attendance in attendances)
                {
                    // Check if attendance is not present
                    if (attendance.Present == false)
                    {
                        // Not present
                        // Populate lables and text boxes with details from the selected absence
                        lblCourseDetails.Text = attendance.Session.CourseCode.CourseName + " - " + attendance.Session.CourseCode.CourseCode;
                        lblSessionDetails.Text = attendance.Session.SessionType + ", " + Convert.ToDateTime(attendance.Session.SessionDate).ToShortDateString() + " " + attendance.Session.Time;
                    }
                    else
                    {
                        // Present
                        // Redirect page to 'Notify Of Absence'
                        Response.Redirect("NotifyOfAbsence.aspx");
                    }
                }
            }
        }

        // Event handler for the 'Send' button
        protected void btnSendNotification_Click(object sender, EventArgs e)
        {
            // Save the absence notification to the database using the 'SaveNotification' method
            if (controller.SaveNotification(attendanceIDFromURL, txtAbsenceReason.Text) == true)
            {
                // Saved successfully
                // Redirect page to 'Notify Of Absence'
                Response.Redirect("NotifyOfAbsence.aspx");
            }
        }

        // Event handler for the 'Cancel' button
        protected void btnCancelNotification_Click(object sender, EventArgs e)
        {
            // Redirect page to 'Notify Of Absence'
            Response.Redirect("NotifyOfAbsence.aspx");
        }

        // Event handler for the 'btnAbsentNotifyAbsence' button which redirects the user to the 'Notify Of Absence' page with the 'attendanceID' as a query string
        protected void btnAbsentNotifyAbsence_Click(object sender, EventArgs e)
        {
            // Reload the discussion board page with the question ID of the question as a query string
            Response.Redirect("NotifyOfAbsence.aspx?attendanceID=" + int.Parse(((Button)sender).CommandArgument));
        }

        // Event handler for the 'drpFilter' drop down list
        protected void drpFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpFilter.SelectedValue == "all")
            {
                // Reload the page
                Response.Redirect("NotifyOfAbsence.aspx");
            }
            else
            {
                // Reload the page with filter details as a string query
                Response.Redirect("NotifyOfAbsence.aspx?&notifications=" + drpFilter.SelectedValue);
            }
        }
    }
}