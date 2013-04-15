<%@ Page Title="Delete a Student from a Course" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeleteStudentCourse.aspx.cs" Inherits="CMSRegisterSystem.DeleteStudentCourse" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Delete a Student from a Course</h2>
    <table style="width: 100%">
        <tr>
            <td class="style1">
                <asp:Label ID="lblInfo" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
        <tr>
            <td>Enter Student ID:&nbsp;
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                <br />
                Enter Course ID:&nbsp;&nbsp;
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="Button6" runat="server" OnClick="Button6_Click" Text="Delete" />
                <br />
                <br />
                <asp:Button ID="Button7" runat="server" OnClick="Button7_Click" Text="Back" />
                <br />
                <asp:Label ID="Label1" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <p>
    </p>
</asp:Content>