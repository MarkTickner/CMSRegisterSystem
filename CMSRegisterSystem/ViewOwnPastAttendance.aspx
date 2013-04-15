<%@ Page Title="View Own Past Attendance" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewOwnPastAttendance.aspx.cs" Inherits="CMSRegisterSystem.ViewOwnPastAttendance" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>View Own Past Attendance</h2>
    <table style="width: 100%">
        <tr>
            <td colspan="2">
                <p>
                    <asp:Label ID="lblInfo" runat="server"></asp:Label>
                </p>
            </td>
        </tr>
        <tr>
            <td>This is your own attendance. Click &#39;Notify Of Absence&#39; above to send the reason to your tutor.</td>
            <td align="right">
                <asp:DropDownList ID="drpFilter" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpFilter_SelectedIndexChanged">
                    <asp:ListItem Value="filter" Selected="True">Filter results by:</asp:ListItem>
                    <asp:ListItem Value="present">Present</asp:ListItem>
                    <asp:ListItem Value="authorised">Absence authorised</asp:ListItem>
                    <asp:ListItem Value="rejected">Absence rejected</asp:ListItem>
                    <asp:ListItem Value="notification">Waiting for authorisation</asp:ListItem>
                    <asp:ListItem Value="notify">To be notified</asp:ListItem>
                </asp:DropDownList>
                &nbsp;<asp:Button ID="btnAllResults" runat="server" OnClick="btnViewAll_Click" Text="All" />
            </td>
        </tr>
        <tr>
            <td colspan="2"></td>
        </tr>
        <tr>
            <td colspan="2">
                <p>
                    <asp:Table ID="tblOwnAttendance" runat="server" GridLines="Both"
                        Width="920px" CellPadding="2" CellSpacing="1"
                        BorderColor="#465767" Font-Bold="False">
                        <asp:TableRow ID="TableRow1" runat="server">
                            <asp:TableCell ID="TableCell1" runat="server" BackColor="#4B6C9E" ForeColor="#FFFFFF" Font-Bold="True" Width="40%">Course Name and Code</asp:TableCell>
                            <asp:TableCell ID="TableCell2" runat="server" BackColor="#4B6C9E" ForeColor="#FFFFFF" Font-Bold="True" Width="15%">Week Commencing</asp:TableCell>
                            <asp:TableCell ID="TableCell3" runat="server" BackColor="#4B6C9E" ForeColor="#FFFFFF" Font-Bold="True" Width="15%">Session Type</asp:TableCell>
                            <asp:TableCell ID="TableCell4" runat="server" BackColor="#4B6C9E" ForeColor="#FFFFFF" Font-Bold="True" Width="15%">Date</asp:TableCell>
                            <asp:TableCell ID="TableCell5" runat="server" BackColor="#4B6C9E" ForeColor="#FFFFFF" Font-Bold="True" Width="15%"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </p>
            </td>
        </tr>
    </table>
</asp:Content>