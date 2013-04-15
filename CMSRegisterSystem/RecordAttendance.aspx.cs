using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMSRegisterSystem
{
    public partial class RecordAttendance : System.Web.UI.Page
    {
        Controller controller = new Controller();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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

                    List<TeachingSession> TeachingSession = controller.ListTeachSession(controller.LoggedInStaff);

                    if (TeachingSession == null)
                    {
                        teachSessListBox.Items.Add("No teaching sessions found.");
                    }
                    else
                    {
                        foreach (TeachingSession ts in TeachingSession)
                        {
                            string code = ts.CourseCode.CourseCode;
                            string id = ts.RoomID.RoomID;
                            teachSessListBox.Items.Add(ts.SessionID + "     " + ts.SessionType + "    " + code + "    " + id + "    " + ts.Day + "    " + ts.Time + "    " + ts.Duration);
                        }
                    }
                }
                // User login -----------------------------------------------------------------------------------------------
            }
        }

        protected void teachSessListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = teachSessListBox.SelectedItem.ToString();
            string[] delimiters = new string[] { " " };
            string[] split = str.Split(delimiters, StringSplitOptions.None);
            string sessionID = split[0];
            HttpContext.Current.Session["sessionID"] = sessionID;
        }

        protected void viewBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewAttendance.aspx");
        }
    }
}