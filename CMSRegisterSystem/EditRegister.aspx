<%@ Page Title="Edit Student Attendance" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditRegister.aspx.cs" Inherits="CMSRegisterSystem.EditRegister" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Edit Student Attendance</h2>

    <table style="width: 100%">
        <tr>
            <td>
                <asp:Label ID="lblInfo" runat="server" Text="Label"></asp:Label>
                <br />
                Select the teaching session and required date and view the register:<br />
                ID Type&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Code&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Room&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Day&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Time&nbsp; Duration<br />
                <asp:ListBox ID="studentList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="teachSessionList_SelectedIndexChanged"></asp:ListBox>
                <br />
                <br />
                <asp:DropDownList ID="dateDD" runat="server">
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="viewBtn" runat="server" OnClick="viewBtn_Click" Text="View Register" />
            </td>
        </tr>
        <tr>
            <td>Edit the register as required and submit it:<br />
                <asp:CheckBoxList ID="studentListBox" runat="server" AutoPostBack="True">
                </asp:CheckBoxList>
                <br />
                <br />
                <asp:Button ID="saveBtn" runat="server" OnClick="Button1_Click" Text="Submit" />
                <br />
                <asp:Label ID="outLbl" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>