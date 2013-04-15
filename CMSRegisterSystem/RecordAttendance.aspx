<%@ Page Title="Record Student Attendance" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecordAttendance.aspx.cs" Inherits="CMSRegisterSystem.RecordAttendance" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Record Student Attendance</h2>
    <p>
        <asp:Label ID="lblInfo" runat="server" Text="Label"></asp:Label>
    </p>
    <table style="width: 100%">
        <tr>
            <td>Choose teaching session to register:<br />
                ID Type&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Code&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Room&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Day&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Time&nbsp; Duration</td>
        </tr>
        <tr>
            <td>
                <asp:ListBox ID="teachSessListBox" runat="server" OnSelectedIndexChanged="teachSessListBox_SelectedIndexChanged" AutoPostBack="True"></asp:ListBox>
                <br />
                <br />
                <asp:Button ID="viewBtn" runat="server" OnClick="viewBtn_Click" PostBackUrl="~/ViewRegister.aspx" Text="View Register" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>