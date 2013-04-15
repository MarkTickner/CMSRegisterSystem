<%@ Page Title="Course Attendance Statistics" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="CourseAttendance.aspx.cs" Inherits="CMSRegisterSystem.CourseAttendance" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Course Attendance Statistics</h2>
    <table style="width: 100%">
        <tr>
            <td>
                <asp:Label ID="lblInfo" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
        <tr>
            <td>To view attendance statistics for an individual course, type its course code below
                and press &#39;Search&#39;.
            </td>
        </tr>
        <tr>
            <td>Enter course code:
                <asp:TextBox ID="txtCourseCode" runat="server"></asp:TextBox>&nbsp;<asp:Button ID="btnSearch"
                    runat="server" Text="Search" OnClick="btnSearch_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <div id="attendanceDetails" runat="server" visible="false">
                    <br />
                    Course name:
                    <asp:Label ID="lblCourseName" runat="server" Font-Bold="True"></asp:Label><br />
                    <br />
                    Number of days present:
                    <asp:Label ID="lblDaysPresent" runat="server" Font-Bold="True"></asp:Label><br />
                    Percentage of days present:
                    <asp:Label ID="lblDaysPresentPercentage" runat="server" Font-Bold="True"></asp:Label>
                    <br />
                    <br />
                    Number of days absent:
                    <asp:Label ID="lblDaysAbsent" runat="server" Font-Bold="True"></asp:Label><br />
                    Percentage of days absent:
                    <asp:Label ID="lblDaysAbsentPercentage" runat="server" Font-Bold="True"></asp:Label>
                    <br />
                    <br />
                    Number of absences authorised:
                    <asp:Label ID="lblAuthorisedAbsences" runat="server" Font-Bold="True"></asp:Label><br />
                    Percentage of absences authorised:
                    <asp:Label ID="lblAuthorisedAbsencesPercentage" runat="server" Font-Bold="True"></asp:Label>
                    <br />
                    <br />
                    Number of absences unauthorised:
                    <asp:Label ID="lblUnauthorisedAbsences" runat="server" Font-Bold="True"></asp:Label><br />
                    Percentage of absences unauthorised:
                    <asp:Label ID="lblUnauthorisedAbsencesPercentage" runat="server" Font-Bold="True"></asp:Label>
                </div>
                <asp:Label ID="lblError" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>