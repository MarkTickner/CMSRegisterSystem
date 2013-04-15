using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMSRegisterSystem
{
    public partial class AssociateStaff : System.Web.UI.Page
    {
        Controller controller = new Controller();

        List<Course> courseName;
        List<TeachingSession> occurrenceName;
        List<Staff> staffName;
        List<Room> roomNr;

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

            if (controller.LoggedInStaff.StaffType.StaffTypeID != 1)
            {
                Response.Redirect("~/Default.aspx");
            }

            if (!Page.IsPostBack)
            {
                courseName = DatabaseController.ListCoursenames();

                DropDownList1.DataSource = courseName;
                DropDownList1.DataTextField = "CourseName";
                DropDownList1.DataValueField = "CourseCode";
                DropDownList1.DataBind();


                occurrenceName = DatabaseController.OccurrenceList();
                foreach (var str in occurrenceName)
                {
                    DropDownList3.Items.Add(new ListItem(str.SessionType));
                }

                staffName = DatabaseController.StaffList();
                foreach (var str1 in staffName)
                {
                    DropDownList4.Items.Add(new ListItem(str1.StaffFirstName + " " + str1.StaffSurname, str1.StaffID.ToString()));
                }

                roomNr = DatabaseController.RoomList();
                foreach (var str2 in roomNr)
                {
                    DropDownList5.Items.Add(new ListItem(str2.RoomName));
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (DatabaseController.AssociateStaff(DropDownList1.SelectedItem.ToString(), DropDownList1.SelectedValue.ToString(), DropDownList3.SelectedItem.ToString(), int.Parse(DropDownList4.SelectedValue), DropDownList4.SelectedItem.ToString(), DropDownList5.SelectedItem.ToString()))
            {
                Label1.Text = "The assignment was successful!";
            }
            else
            {
                Label1.Text = "Error! Check again all the details you want to store!";
            }
        }
    }
}