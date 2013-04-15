<%@ Page Title="Student Attendance Statistics" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="StudentAttendance.aspx.cs" Inherits="CMSRegisterSystem.StudentAttendance" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Student Attendance Statistics</h2>
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
            <td>To view attendance statistics for an individual student, type their student ID number
                below and press &#39;Search&#39;.
            </td>
        </tr>
        <tr>
            <td>Enter student ID:
                <asp:TextBox ID="txtStudentID" runat="server"></asp:TextBox>&nbsp;<asp:Button ID="btnSearch"
                    runat="server" Text="Search" OnClick="btnSearch_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <div id="attendanceDetails" runat="server" visible="false">
                    <br />
                    Student name:
                    <asp:Label ID="lblStudentName" runat="server" Font-Bold="True"></asp:Label>
                    <br />
                    <br />
                    Number of days present:
                    <asp:Label ID="lblDaysPresent" runat="server" Font-Bold="True"></asp:Label>
                    <br />
                    <br />
                    Number of days absent:
                    <asp:Label ID="lblDaysAbsent" runat="server" Font-Bold="True"></asp:Label>
                    <br />
                    <br />
                    Number of authorised absences:
                    <asp:Label ID="lblAuthorisedAbsences" runat="server" Font-Bold="True"></asp:Label>
                    <br />
                    <br />
                    Number of unauthorised absences:
                    <asp:Label ID="lblUnauthorisedAbsences" runat="server" Font-Bold="True"></asp:Label>
                </div>
                <asp:Label ID="lblError" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>