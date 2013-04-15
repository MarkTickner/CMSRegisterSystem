using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMSRegisterSystem
{
    public partial class MaintainLists : System.Web.UI.Page
    {
        Controller controller = new Controller();

        List<Course> courseName;
        List<TeachingSession> occuranceName;
        int s;

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

                DropDownList2.DataSource = courseName;
                DropDownList2.DataTextField = "CourseName";
                DropDownList2.DataValueField = "CourseCode";
                DropDownList2.DataBind();


                occuranceName = DatabaseController.OccurrenceList();
                foreach (var str in occuranceName)
                {
                    DropDownList4.Items.Add(new ListItem(str.SessionType));
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            s = int.Parse(TextBox1.Text);
            Label1.Text = s + " " + DropDownList2.SelectedValue + " " + DropDownList4.SelectedItem.ToString();

            if (DatabaseController.MaintainList(s, DropDownList2.SelectedItem.ToString(), DropDownList2.SelectedValue.ToString(), DropDownList4.SelectedItem.ToString()))
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