using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMSRegisterSystem
{
    public partial class AuthoriseNotifiedAbsence : System.Web.UI.Page
    {
        Controller controller = new Controller();

        int attendanceIDFromURL; // the attendanceID will be saved from the website URL here
        string notificationsTypeFromURL; // the type of notifications to show will be saved from the website URL here
        int absenceID; // the absenceID will be saved here

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

            // Stop students from accessing this page
            if (controller.LoggedInStaff == null)
            {
                // Redirect to home page
                Response.Redirect("~/Default.aspx");
            }

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
                lblNotificationList.Text = "The following students have notified absences. Click to view reasons." + notificationsTypeFromURL;

                // List all absent student attendances
                List<Attendance> absentAttendances = controller.ListAttendancesAbsentAndNotifiedOnly();

                // Check if the 'absentAttendances' list is empty
                if (absentAttendances.Count() == 0)
                {
                    // List is empty
                    // Show error message
                    TableRow rowError = new TableRow();
                    TableCell cellError = new TableCell();
                    cellError.Text = "There are no notified absences to show.";
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
                        return a.Session.SessionDate.CompareTo(b.Session.SessionDate);
                    });

                    // Show each 'Attendance' object from the 'absentAttendances' list in a dynamically created table
                    foreach (var absentAttendance in absentAttendances)
                    {
                        // Display the absences in a dynamically created table
                        TableRow row = new TableRow();

                        // Student name
                        TableCell cell1 = new TableCell();
                        cell1.Text = absentAttendance.Student.StudentFirstName + " " + absentAttendance.Student.StudentSurname;
                        row.Cells.Add(cell1);

                        // Course name and code
                        TableCell cell2 = new TableCell();
                        cell2.Text = absentAttendance.Session.CourseCode.CourseName + " - " + absentAttendance.Session.CourseCode.CourseCode;
                        row.Cells.Add(cell2);

                        // Session type
                        TableCell cell3 = new TableCell();
                        cell3.Text = absentAttendance.Session.SessionType;
                        row.Cells.Add(cell3);

                        // Session date
                        TableCell cell4 = new TableCell();
                        cell4.Text = Convert.ToDateTime(absentAttendance.Session.SessionDate).ToShortDateString();
                        row.Cells.Add(cell4);

                        // Notification and authorisation
                        TableCell cell5 = new TableCell();

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
                                    cell5.BackColor = System.Drawing.Color.FromArgb(0x78, 0xAB, 0x46);
                                }
                                else
                                {
                                    // Notified, not authorised
                                    btnAbsentNotifyAbsence.Text = "Rejected";
                                    cell5.BackColor = System.Drawing.Color.FromArgb(0xCC, 0x00, 0x00);
                                }
                            }
                        }

                        if (btnAbsentNotifyAbsence.Text.Equals(""))
                        {
                            // Notified, awaiting authorisation
                            btnAbsentNotifyAbsence.Text = "Waiting for authorisation";
                            cell5.BackColor = System.Drawing.Color.FromArgb(0xFF, 0x99, 0x33);
                        }

                        cell5.Controls.Add(btnAbsentNotifyAbsence);
                        row.Cells.Add(cell5);

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
                notificationAcceptReject.Visible = false;
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
                    lblNotificationSendView.Text = "Notification sent on " + absence.NotifiedDate + ".";

                    // Populate lables and text boxes with details from the selected absence
                    lblStudentDetails.Text = absence.Attendance.Student.StudentFirstName + " " + absence.Attendance.Student.StudentSurname;
                    lblCourseDetails.Text = absence.Attendance.Session.CourseCode.CourseName + " - " + absence.Attendance.Session.CourseCode.CourseCode;
                    lblSessionDetails.Text = absence.Attendance.Session.SessionType + ", " + Convert.ToDateTime(absence.Attendance.Session.SessionDate).ToShortDateString() + " " + absence.Attendance.Session.Time;
                    txtAbsenceReason.Text = absence.Reason;

                    // Save absenceID to int
                    absenceID = absence.AbsenceID;

                    // Check if notified absence has already been authorised or rejected
                    List<NotifiedAbsenceAuthorised> notifiedAbsenceAuthoriseds = controller.ListNotifiedAbsenceAuthorisedsByAbsenceID(absence.AbsenceID);
                    foreach (var notifiedAbsenceAuthorised in notifiedAbsenceAuthoriseds)
                    {
                        if (notifiedAbsenceAuthorised.Authorised == true)
                        {
                            // Notified absence has been authorised
                            // Append message
                            lblNotificationSendView.Text += " Authorised on " + notifiedAbsenceAuthorised.AuthorisedRejectedDate + ".";

                            // Hide buttons
                            btnAuthoriseNotification.Visible = false;
                            btnRejectNotification.Visible = false;
                        }
                        else if (notifiedAbsenceAuthorised.Authorised == false)
                        {
                            // Notified absence has been rejected
                            // Append message
                            lblNotificationSendView.Text += " Rejected on " + notifiedAbsenceAuthorised.AuthorisedRejectedDate + ".";

                            // Hide buttons
                            btnAuthoriseNotification.Visible = false;
                            btnRejectNotification.Visible = false;
                        }
                    }

                    // Append message
                    lblNotificationSendView.Text += " Reason for absence sent is shown below:";
                }

                // Disable and hide controls
                txtAbsenceReason.Enabled = false;
            }
            else
            {
                // Absence has not already been notified
                // Redirect page to 'Authorise Notified Absence'
                Response.Redirect("AuthoriseNotifiedAbsence.aspx");
            }
        }

        // Event handler for the 'Authorise' button
        protected void btnAuthoriseNotification_Click(object sender, EventArgs e)
        {
            // Save the absence notification authorisation to the database using the 'SaveNotificationAuthorisationRejection' method
            if (controller.SaveNotificationAuthorisationRejection(absenceID, true) == true)
            {
                // Saved successfully
                // Redirect page to 'Authorise Notified Absence'
                Response.Redirect("AuthoriseNotifiedAbsence.aspx");
            }
        }

        // Event handler for the 'Reject' button
        protected void btnRejectNotification_Click(object sender, EventArgs e)
        {
            // Save the absence notification rejection to the database using the 'SaveNotificationAuthorisationRejection' method
            if (controller.SaveNotificationAuthorisationRejection(absenceID, false) == true)
            {
                // Saved successfully
                // Redirect page to 'Authorise Notified Absence'
                Response.Redirect("AuthoriseNotifiedAbsence.aspx");
            }
        }

        // Event handler for the 'Back' button
        protected void btnBack_Click(object sender, EventArgs e)
        {
            // Redirect page to 'Authorise Notified Absence'
            Response.Redirect("AuthoriseNotifiedAbsence.aspx");
        }

        // Event handler for the 'btnAbsentNotifyAbsence' button which redirects the user to the 'Authorise Notified Absence' page with the 'attendanceID' as a query string
        protected void btnAbsentNotifyAbsence_Click(object sender, EventArgs e)
        {
            // Reload the discussion board page with the question ID of the question as a query string
            Response.Redirect("AuthoriseNotifiedAbsence.aspx?attendanceID=" + int.Parse(((Button)sender).CommandArgument));
        }

        // Event handler for the 'drpFilter' drop down list
        protected void drpFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpFilter.SelectedValue == "all")
            {
                // Reload the page
                Response.Redirect("AuthoriseNotifiedAbsence.aspx");
            }
            else
            {
                // Reload the page with filter details as a string query
                Response.Redirect("AuthoriseNotifiedAbsence.aspx?&notifications=" + drpFilter.SelectedValue);
            }
        }
    }
}