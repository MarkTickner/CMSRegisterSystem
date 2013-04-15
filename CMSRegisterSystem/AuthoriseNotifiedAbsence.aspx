<%@ Page Title="Authorise Notified Absence" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AuthoriseNotifiedAbsence.aspx.cs" Inherits="CMSRegisterSystem.AuthoriseNotifiedAbsence" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Authorise Notified Absence</h2>
    <table style="width: 100%">
        <tr>
            <td colspan="2">
                <p>
                    <asp:Label ID="lblInfo" runat="server"></asp:Label>
                </p>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div id="notificationAcceptReject" runat="server">
                    <p>
                        <asp:Label ID="lblStudentDetails" runat="server" Style="font-weight: 700"></asp:Label><br />
                        <asp:Label ID="lblCourseDetails" runat="server"></asp:Label><br />
                        <asp:Label ID="lblSessionDetails" runat="server"></asp:Label>
                    </p>
                    <p>
                        <asp:Label ID="lblNotificationSendView" runat="server"></asp:Label><br />
                        <asp:TextBox ID="txtAbsenceReason" runat="server" Height="60px" TextMode="MultiLine" Width="915px"></asp:TextBox>
                    </p>
                    <p>
                        <asp:Button ID="btnAuthoriseNotification" runat="server" OnClick="btnAuthoriseNotification_Click" Text="Authorise" Width="100px" />&nbsp;<asp:Button ID="btnRejectNotification" runat="server" OnClick="btnRejectNotification_Click" Text="Reject" Width="100px" />&nbsp;<asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" Width="100px" />
                    </p>
                </div>
                <div id="notificationList" runat="server">
                    <table cellpadding="0" cellspacing="0" style="width: 100%;">
                        <tr>
                            <td>
                                <asp:Label ID="lblNotificationList" runat="server"></asp:Label></td>
                            <td align="right">
                                <asp:DropDownList ID="drpFilter" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpFilter_SelectedIndexChanged">
                                    <asp:ListItem Value="filter" Selected="True">Filter results by:</asp:ListItem>
                                    <asp:ListItem Value="authorised">Authorised</asp:ListItem>
                                    <asp:ListItem Value="rejected">Rejected</asp:ListItem>
                                    <asp:ListItem Value="waiting">Waiting for authorisation</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;<asp:Button ID="btnAllResults" runat="server" OnClick="btnBack_Click" Text="All" />
                            </td>
                        </tr>
                    </table>
                    <p>
                        <asp:Table ID="tblAbsences" runat="server" GridLines="Both"
                            Width="920px" CellPadding="2" CellSpacing="1"
                            BorderColor="#465767" Font-Bold="False">
                            <asp:TableRow ID="TableRow1" runat="server">
                                <asp:TableCell ID="TableCell1" runat="server" BackColor="#4B6C9E" ForeColor="#FFFFFF" Font-Bold="True" Width="20%">Student Name</asp:TableCell>
                                <asp:TableCell ID="TableCell2" runat="server" BackColor="#4B6C9E" ForeColor="#FFFFFF" Font-Bold="True" Width="40%">Course Name and Code</asp:TableCell>
                                <asp:TableCell ID="TableCell3" runat="server" BackColor="#4B6C9E" ForeColor="#FFFFFF" Font-Bold="True" Width="10%">Session Type</asp:TableCell>
                                <asp:TableCell ID="TableCell4" runat="server" BackColor="#4B6C9E" ForeColor="#FFFFFF" Font-Bold="True" Width="10%">Date</asp:TableCell>
                                <asp:TableCell ID="TableCell5" runat="server" BackColor="#4B6C9E" ForeColor="#FFFFFF" Font-Bold="True" Width="20%"></asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </p>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>