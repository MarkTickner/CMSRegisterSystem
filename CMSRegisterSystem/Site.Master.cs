using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMSRegisterSystem
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        Controller controller = new Controller();

        protected void Page_Load(object sender, EventArgs e)
        {
            // User navigation menu -------------------------------------------------------------------------------------
            controller.CheckLogin();

            // Get logged in user
            if (controller.LoggedInStudent == null && controller.LoggedInStaff == null)
            {
                // If not logged in
                // Logged out navigation menu
                loggedOutNavigationDiv.Visible = true;
            }
            else if (controller.LoggedInStudent != null)
            {
                // Logged in as a student
                // Student navigation menu
                studentNavigationDiv.Visible = true;
            }
            else if (controller.LoggedInStaff != null)
            {
                // Logged in as staff
                // Check staff type
                if (controller.LoggedInStaff.StaffType.StaffTypeID == 1)
                {
                    // Logged in as a course coordinator
                    // Course coordinator navigation menu
                    staffCourseCoordinatorNavigationDiv.Visible = true;
                }
                else if (controller.LoggedInStaff.StaffType.StaffTypeID == 2)
                {
                    // Logged in as a tutor
                    // Tutor navigation menu
                    staffTutorNavigationDiv.Visible = true;
                }
                else if (controller.LoggedInStaff.StaffType.StaffTypeID == 3)
                {
                    // Logged in as a manager
                    // Manager naviagtion menu
                    staffManagerNavigationDiv.Visible = true;
                }
                else if (controller.LoggedInStaff.StaffType.StaffTypeID == 4)
                {
                    // Logged in as admin staff
                    // Admin staff navigation menu
                    staffAdminNavigationDiv.Visible = true;
                }
            }
            // User navigation menu -------------------------------------------------------------------------------------
        }
    }
}