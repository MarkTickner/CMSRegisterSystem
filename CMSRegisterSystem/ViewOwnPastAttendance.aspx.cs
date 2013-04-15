using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMSRegisterSystem
{
    public partial class ViewOwnPastAttendance : System.Web.UI.Page
    {
        Controller controller = new Controller();

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

            // Gets the type of notification from the URL if available and saves it as a string
            notificationsTypeFromURL = Request.QueryString["notifications"];

            // Disable the 'Filter results by:" item in the drop down menu
            ListItem i = drpFilter.Items.FindByValue("filter");
            i.Attributes.Add("disabled", "true");

            // List all own attendance of logged in student in a table
            List<Attendance> attendances = controller.ListAttendancesByStudentID(controller.LoggedInStudent.StudentID);

            // List the authorised notified absences
            List<NotifiedAbsenceAuthorised> notifiedAbsenceAuthoriseds = controller.ListNotifiedAbsenceAuthoriseds();

            string cell1Str = "";
            string cell2Str = "";

            // Show each 'Attendance' object from the 'attendances' list in a dynamically created table
            foreach (var attendance in attendances)
            {
                TableRow row = new TableRow();

                // Course name and code
                TableCell cell1 = new TableCell();

                // Check if a filter is being used
                if (notificationsTypeFromURL == "" || notificationsTypeFromURL == null)
                {
                    // Filter not being used
                    // Check if the text to go in cell is already in the previous cell to avoid duplicates from showing
                    if (cell1Str.Equals(attendance.Session.CourseCode.CourseName + " - " + attendance.Session.CourseCode.CourseCode))
                    {
                        cell1.Text = "";
                    }
                    else
                    {
                        cell1Str = attendance.Session.CourseCode.CourseName + " - " + attendance.Session.CourseCode.CourseCode;
                        cell1.Text = attendance.Session.CourseCode.CourseName + " - " + attendance.Session.CourseCode.CourseCode;
                    }
                }
                else
                {
                    // Filter being used
                    // Show text in every cell
                    cell1.Text = attendance.Session.CourseCode.CourseName + " - " + attendance.Session.CourseCode.CourseCode;
                }

                row.Cells.Add(cell1);

                // Week commencing
                TableCell cell2 = new TableCell();

                // Check if a filter is being used
                if (notificationsTypeFromURL == "" || notificationsTypeFromURL == null)
                {
                    // Filter not being used
                    // Check if the text to go in cell is already in the previous cell to avoid duplicates from showing
                    if (cell2Str.Equals(controller.GetFirstDayOfWeek(Convert.ToDateTime(attendance.Session.SessionDate)).ToShortDateString()))
                    {
                        cell2.Text = "";
                    }
                    else
                    {
                        cell2Str = controller.GetFirstDayOfWeek(Convert.ToDateTime(attendance.Session.SessionDate)).ToShortDateString();
                        cell2.Text = controller.GetFirstDayOfWeek(Convert.ToDateTime(attendance.Session.SessionDate)).ToShortDateString();
                    }
                }
                else
                {
                    // Filter being used
                    // Show text in every cell
                    cell2.Text = controller.GetFirstDayOfWeek(Convert.ToDateTime(attendance.Session.SessionDate)).ToShortDateString();
                }

                row.Cells.Add(cell2);

                // Session type
                TableCell cell3 = new TableCell();
                cell3.Text = attendance.Session.SessionType;
                row.Cells.Add(cell3);

                // Session date
                TableCell cell4 = new TableCell();
                cell4.Text = Convert.ToDateTime(attendance.Session.SessionDate).ToShortDateString();
                row.Cells.Add(cell4);

                // Present
                TableCell cell5 = new TableCell();
                // This button uses the 'btnAbsentNotifyAbsence' event handler
                // It is formatted using CSS to look like hyperlinked text
                Button btnAbsentNotifyAbsence = new Button();
                btnAbsentNotifyAbsence.Text = "";
                btnAbsentNotifyAbsence.CommandArgument = attendance.AttendanceID.ToString();
                btnAbsentNotifyAbsence.Click += new EventHandler(btnAbsentNotifyAbsence_Click);
                btnAbsentNotifyAbsence.CssClass += "hidden_btn";

                if (attendance.Present == true)
                {
                    cell5.Text = "Present";
                    cell5.ForeColor = System.Drawing.Color.FromArgb(0xFF, 0xFF, 0xFF);
                    cell5.BackColor = System.Drawing.Color.FromArgb(0x78, 0xAB, 0x46);

                    // For the purpose of the filter
                    btnAbsentNotifyAbsence.Text = "Present";
                }
                else
                {
                    cell5.Controls.Add(btnAbsentNotifyAbsence);

                    foreach (var notifiedAbsenceAuthorised in notifiedAbsenceAuthoriseds)
                    {
                        // Check notified absences list against current attendance
                        if (notifiedAbsenceAuthorised.NotifiedAbsence.Attendance.AttendanceID == attendance.AttendanceID)
                        {
                            // Check if the notified absence has been authorised or not authorised
                            if (notifiedAbsenceAuthorised.Authorised == true)
                            {
                                // Notified, authorised
                                btnAbsentNotifyAbsence.Text = "Absence authorised";
                                cell5.BackColor = System.Drawing.Color.FromArgb(0x78, 0xAB, 0x46);
                            }
                            else
                            {
                                // Notified, not authorised
                                btnAbsentNotifyAbsence.Text = "Absence rejected";
                                cell5.BackColor = System.Drawing.Color.FromArgb(0xCC, 0x00, 0x00);
                            }
                        }

                        // Check if the absence has already been notified
                        else if (controller.CheckIfAbsenceIsAlreadyNotified(attendance.AttendanceID) == true && btnAbsentNotifyAbsence.Text.Equals(""))
                        {
                            // Notified, awaiting authorisation
                            btnAbsentNotifyAbsence.Text = "Notification sent";
                            cell5.BackColor = System.Drawing.Color.FromArgb(0xFF, 0x99, 0x33);
                        }
                        else if (controller.CheckIfAbsenceIsAlreadyNotified(attendance.AttendanceID) == false && btnAbsentNotifyAbsence.Text.Equals(""))
                        {
                            // Not notified
                            btnAbsentNotifyAbsence.Text = "Notify of absence";
                            cell5.BackColor = System.Drawing.Color.FromArgb(0xFF, 0x99, 0x33);
                        }
                    }
                }
                row.Cells.Add(cell5);

                // Check if a filter is being used
                if (notificationsTypeFromURL == null)
                {
                    // No filter
                    // Add to table
                    tblOwnAttendance.Rows.Add(row);
                }
                else if (btnAbsentNotifyAbsence.Text.ToUpperInvariant().Contains(notificationsTypeFromURL.ToUpperInvariant()))
                {
                    // Filter match
                    // Add to table
                    tblOwnAttendance.Rows.Add(row);
                }
                else
                {
                    // Filter no match
                    // Move to next object in list
                    continue;
                }
            }
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
                Response.Redirect("ViewOwnPastAttendance.aspx");
            }
            else
            {
                // Reload the page with filter details as a string query
                Response.Redirect("ViewOwnPastAttendance.aspx?&notifications=" + drpFilter.SelectedValue);
            }
        }

        // Event handler for the 'All' button
        protected void btnViewAll_Click(object sender, EventArgs e)
        {
            // Reload the page
            Response.Redirect("ViewOwnPastAttendance.aspx");
        }
    }
}