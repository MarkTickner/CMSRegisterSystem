using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMSRegisterSystem
{
    public partial class EditRegister : System.Web.UI.Page
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

                    List<TeachingSession> sessions = controller.ListTeachSession(controller.LoggedInStaff);
                    foreach (TeachingSession t in sessions)
                    {
                        string code = t.CourseCode.CourseCode;
                        string id = t.RoomID.RoomID;
                        studentList.Items.Add(t.SessionID + " " + t.SessionType + " " + code + " " + id + " " + t.Day + " " + t.Time + " " + t.Duration);
                    }
                }
                // User login -----------------------------------------------------------------------------------------------

                dateDD.Visible = false;
            }
        }

        protected void teachSessionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            dateDD.Visible = true;
            dateDD.Items.Clear();
            string str;
            str = studentList.SelectedItem.ToString();
            string[] delimiters = new string[] { " " };
            string[] split = str.Split(delimiters, StringSplitOptions.None);
            string sessionID = split[0];
            HttpContext.Current.Session["sessionID2"] = sessionID;
            List<string> dates = controller.dates(int.Parse(sessionID));
            foreach (string s in dates)
            {
                dateDD.Items.Add(s);
            }
        }

        protected void viewBtn_Click(object sender, EventArgs e)
        {
            string str = studentList.SelectedItem.ToString();
            string[] delimiters = new string[] { " " };
            string[] split = str.Split(delimiters, StringSplitOptions.None);
            string studentID = split[0];
            List<string> register = controller.editRegister(studentID, dateDD.SelectedItem.ToString());
            HttpContext.Current.Session["editRegister"] = register;
            int count = 0;
            foreach (string s in register)
            {
                string[] delimiters2 = new string[] { " " };
                string[] split2 = s.Split(delimiters2, StringSplitOptions.None);
                string present = split2[0];
                string student = split2[1] + " " + split2[2] + " " + split2[3] + " " + split2[4];

                if (split2[0] == "True")
                {
                    studentListBox.Items.Add(student);
                    studentListBox.Items[count].Selected = true;
                }
                else
                {
                    studentListBox.Items.Add(student);
                    studentListBox.Items[count].Selected = false;
                }
                count++;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            List<string> register = (List<string>)HttpContext.Current.Session["editRegister"];
            if (register != null)
            {
                int count = 0;
                List<string> studentIDs = new List<string>();
                int max = studentListBox.Items.Count;
                while (count < max)
                {
                    if (studentListBox.Items[count].Selected == true)
                    {
                        string str = studentListBox.Items[count].ToString();
                        string[] delimiters = new string[] { " " };
                        string[] split = str.Split(delimiters, StringSplitOptions.None);
                        string studentID = split[0];
                        studentIDs.Add(studentID + " Yes");
                    }
                    else
                    {
                        string str = studentListBox.Items[count].ToString();
                        string[] delimiters = new string[] { " " };
                        string[] split = str.Split(delimiters, StringSplitOptions.None);
                        string studentID = split[0];
                        studentIDs.Add(studentID + " No");
                    }
                    count++;
                }

                controller.updateRegister(studentIDs, dateDD.SelectedItem.ToString());
                outLbl.Text = "The register has been saved successfully.";
            }
            else
            {
                outLbl.Text = "Select and view a register before submitting.";
            }
        }
    }
}