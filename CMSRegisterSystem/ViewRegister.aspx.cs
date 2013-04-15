using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMSRegisterSystem
{
    public partial class ViewRegister : System.Web.UI.Page
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
                }
                // User login -----------------------------------------------------------------------------------------------

                if (HttpContext.Current.Session["sessionID"] != null)
                {
                    int sessionID = int.Parse(HttpContext.Current.Session["sessionID"].ToString());
                    List<Student> register = controller.Register(sessionID);

                    if (register == null)
                    {
                        registerListBox.Items.Add("No teaching sessions found.");
                    }
                    else
                    {
                        foreach (Student stu in register)
                        {
                            registerListBox.Items.Add(stu.StudentID + " " + stu.StudentFirstName + " " + stu.StudentSurname + " " + stu.StudentRegistrationStatus);
                        }
                        HttpContext.Current.Session["register"] = register;
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            List<Student> register = (List<Student>)HttpContext.Current.Session["register"];
            int count = 0;
            List<string> studentIDs = new List<string>();

            foreach (Student stu in register)
            {
                if (register != null)
                {
                    if (registerListBox.Items[count].Selected == true)
                    {
                        // http://www.dotnetperls.com/split
                        string str = registerListBox.Items[count].ToString();
                        string[] delimiters = new string[] { " " };
                        string[] split = str.Split(delimiters, StringSplitOptions.None);
                        string studentID = split[0];
                        studentIDs.Add(studentID + " Yes");
                    }
                    else
                    {
                        string str = registerListBox.Items[count].ToString();
                        string[] delimiters = new string[] { " " };
                        string[] split = str.Split(delimiters, StringSplitOptions.None);
                        string studentID = split[0];
                        studentIDs.Add(studentID + " No");
                    }
                }
                else
                {
                }
                count++;
            }

            controller.saveRegister(studentIDs);
            outputLbl.Text = "The register has been saved successfully.";
        }

        protected void printBtn_Click(object sender, EventArgs e)
        {
            // Neudesic, LLC (2009)
            ClientScript.RegisterClientScriptBlock(this.GetType(), "key", "window.print()", true);
        }
    }
}