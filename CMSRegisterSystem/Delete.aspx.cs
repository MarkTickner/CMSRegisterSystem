using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMSRegisterSystem
{
    public partial class Delete : System.Web.UI.Page
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

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("DeleteStudent.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("DeleteStaff.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("DeleteCourse.aspx");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("DeleteStudentCourse.aspx");
        }
    }
}