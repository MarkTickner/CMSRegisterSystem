using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMSRegisterSystem
{
    public partial class _Default : System.Web.UI.Page
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
                btnLogOut.Enabled = false;
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
            if (controller.LoggedInStudent != null || controller.LoggedInStaff != null)
            {
                // Logged in
                txtUsername.Enabled = false;
                txtPassword.Enabled = false;
                btnLogIn.Enabled = false;
            }
            // User login -----------------------------------------------------------------------------------------------
        }

        // Event handler for the 'Log In' button
        protected void btnLogIn_Click(object sender, EventArgs e)
        {
            // Call the log in method
            controller.LogInUser(txtUsername.Text, txtPassword.Text);

            controller.LoggedInStudent = (Student)Session["loggedInStudent"];
            controller.LoggedInStaff = (Staff)Session["loggedInStaff"];

            if (Session["loggedInStudent"] != null)
            {
                // Successful log in as a student
                // Show message to user
                lblInfo.Text = "Success, " + controller.LoggedInStudent.StudentFirstName + ", you are now logged in as a student.";

                // Enable log out fields
                btnLogOut.Enabled = true;

                // Disable log in fields
                txtUsername.Enabled = false;
                txtPassword.Enabled = false;
                btnLogIn.Enabled = false;

                // Clear text from log in fields
                txtUsername.Text = "";
                txtPassword.Text = "";

                // Reload the page (so that the navigation can be swapped by the site master)
                Response.Redirect("Default.aspx");
            }
            else if (Session["loggedInStaff"] != null)
            {
                // Successful log in as staff
                // Show message to user
                lblInfo.Text = "Success, " + controller.LoggedInStaff.StaffFirstName + ", you are now logged in as a " + controller.LoggedInStaff.StaffType.StaffTypeName + ".";

                // Enable log out fields
                btnLogOut.Enabled = true;

                // Disable log in fields
                txtUsername.Enabled = false;
                txtPassword.Enabled = false;
                btnLogIn.Enabled = false;

                // Clear text from log in fields
                txtUsername.Text = "";
                txtPassword.Text = "";

                // Reload the page (so that the navigation can be swapped by the site master)
                Response.Redirect("Default.aspx");
            }
            else
            {
                // Unsuccessful log in
                // Show message to user
                lblInfo.Text = "Error logging in, check username and password.";
            }
        }

        // Event handler for the 'Log Out' button
        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            // Clear variables
            controller.LoggedInStudent = null;
            controller.LoggedInStaff = null;

            // Store in session
            Session["loggedInStudent"] = controller.LoggedInStudent;
            Session["loggedInStaff"] = controller.LoggedInStaff;

            // Show message to user
            lblInfo.Text = "You are now logged out.";

            // Enable log in fields
            txtUsername.Enabled = true;
            txtPassword.Enabled = true;
            btnLogIn.Enabled = true;

            // Disable log out fields
            btnLogOut.Enabled = false;

            // Reload the page (so that the navigation can be swapped by the site master)
            Response.Redirect("Default.aspx");
        }
    }
}